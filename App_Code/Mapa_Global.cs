using Newtonsoft.Json;
using Ruteador.App_Code.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Descripción breve de Response
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
[System.Web.Script.Services.ScriptService]
public class Mapa_Global : System.Web.Services.WebService
{

    public Mapa_Global()
    {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hola a todos";
    }
    [System.Web.Services.WebMethod(true)]
    public void GuardarResponse(string response, long id_ruta)
    {
        Context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
        Context.Response.AppendHeader("Content-type", "application/json");
        try
        {
            new RespuestaBC().Guardar(response, id_ruta);
        }
        catch (Exception ex)
        {
            Context.Response.StatusDescription = ex.Message;
        }
    }
    [System.Web.Services.WebMethod(true)]
    public void GuardarRuta(string jsonRuta)
    {
        Context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
        Context.Response.AppendHeader("Content-type", "application/json");
        try
        {
            PreRutaBC pre_ruta = JsonConvert.DeserializeObject<PreRutaBC>(jsonRuta);
            pre_ruta.Guardar();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID_DESTINO", typeof(int));
            dt.Columns.Add("SECUENCIA", typeof(int));
            dt.Columns.Add("FH_LLEGADA", typeof(DateTime));
            dt.Columns.Add("FH_SALIDA", typeof(DateTime));
            dt.Columns.Add("TIEMPO", typeof(int));

            DateTime fechaRelativa = pre_ruta.FECHA_DESPACHOEXP;
            string[] temp = pre_ruta.PEDIDOS[0].HORA_SALIDA.HORA_COD.Split(":".ToCharArray());

            fechaRelativa = fechaRelativa.Date.AddHours(Convert.ToInt32(temp[0])).AddMinutes(Convert.ToInt32(temp[1]));

            foreach (PedidoBC p in pre_ruta.PEDIDOS)
            {
                p.RUTA_PEDIDO.tiempo = Convert.ToInt32(p.RUTA_PEDIDO.FH_LLEGADA.Subtract(fechaRelativa).TotalMinutes);
                fechaRelativa = p.RUTA_PEDIDO.FH_SALIDA;
                DataRow dr = dt.NewRow();
                dr["ID_DESTINO"] = p.PERU_ID;
                dr["SECUENCIA"] = p.RUTA_PEDIDO.SECUENCIA;
                dr["FH_LLEGADA"] = p.RUTA_PEDIDO.FH_LLEGADA;
                dr["FH_SALIDA"] = p.RUTA_PEDIDO.FH_SALIDA;
                dr["TIEMPO"] = p.RUTA_PEDIDO.tiempo;
                dt.Rows.Add(dr);
            }
            pre_ruta.GuardarPuntos();
            var debug = "";
        }
        catch (Exception ex)
        {
            Context.Response.StatusDescription = ex.Message;
        }
    }
    [System.Web.Services.WebMethod(true)]
    public string NuevaRuta(int hora_id, string fecha_despacho)
    {
        try
        {
            PreRutaBC pre_ruta = new PreRutaBC();
            pre_ruta.HORARIO.HORA_ID = hora_id;
            pre_ruta.FECHA_DESPACHOEXP = Convert.ToDateTime(fecha_despacho);
            List<OrigenBC> arrOrigen = new OrigenBC().ObtenerArray();
            pre_ruta.ORIGEN = arrOrigen[0];

            pre_ruta.FH_CREACION = DateTime.Now;

            var random = new Random();
            var color = String.Format("#{0:X6}", random.Next(0x1000000));
            pre_ruta.RUTA_COLOR = color;

            pre_ruta.Guardar();
            pre_ruta = pre_ruta.ObtenerXId();
            //hf_jsonRuta.Value = JsonConvert.SerializeObject(pre_ruta.ObtenerXId(pre_ruta.ID));
            var debug = "";
            return JsonConvert.SerializeObject(pre_ruta);
        }
        catch (Exception ex)
        {
            Context.Response.StatusDescription = ex.Message;
            return null;
        }
    }
    [System.Web.Services.WebMethod(true)]
    public void EliminarRuta(int ruta_id)
    {
        try
        {
            new PreRutaBC().Eliminar(ruta_id);
        }
        catch (Exception ex)
        {
            Context.Response.StatusDescription = ex.Message;
        }
    }

}
