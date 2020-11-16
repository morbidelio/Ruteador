using Newtonsoft.Json;
using Ruteador.App_Code.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class App_Mapa_Global : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_buscarFecha.Text = DateTime.Now.ToShortDateString();
            CargaDrops();
            ObtenerRutas();
        }
    }
    #region Buttons
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        ObtenerRutas();
    }
    protected void btn_vehiculoGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            PreRutaBC p = new PreRutaBC();
            p.ID = Convert.ToInt32(hf_idRuta.Value);
            p.TRAILER.TRAI_ID = Convert.ToInt32(ddl_vehiculoTrailer.SelectedValue);
            p.TRACTO.TRAC_ID = Convert.ToInt32(ddl_vehiculoTracto.SelectedValue);
            p.CONDUCTOR.COND_ID = Convert.ToInt32(ddl_vehiculoConductor.SelectedValue);
            p.GuardarDetalle();
            utils.ShowMessage2(this, "guardar", "success_modificar");
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
            utils.CerrarModal(this, "modalVehiculo");
            ObtenerRutas();
        }
    }
    protected void btn_colorGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            PreRutaBC p = new PreRutaBC();
            p.ID = Convert.ToInt32(hf_idRuta.Value);
            p.RUTA_COLOR = txt_editColor.Text;
            p.GuardarDetalle();
            utils.ShowMessage2(this, "guardar", "success_modificar");
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
            utils.CerrarModal(this, "modalColor");
            ObtenerRutas();
        }
    }
    protected void btn_confEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            int peru_id = Convert.ToInt32(hf_idRuta.Value);
            if (new PreRutaBC().Eliminar(peru_id))
            {
                utils.ShowMessage2(this, "eliminar", "success");
                utils.CerrarModal(this, "modalConf");
            }
            else
                utils.ShowMessage2(this, "eliminar", "error");
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
            ObtenerRutas();
        }
    }
    protected void btn_guardar_Click(object sender, EventArgs e)
    {

        try
        {
            //List<PreRutaBC> pre_rutas = JsonConvert.DeserializeObject<List<PreRutaBC>>(hf_jsonRutas.Value);
            PreRutaBC pre_ruta = JsonConvert.DeserializeObject<PreRutaBC>(hf_jsonRuta.Value);
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
            if (pre_ruta.GuardarPuntos())
            {
                utils.ShowMessage2(this, "guardar", "success_modificar");
            }
            var debug = "";
            //    DataTable dt2 = JsonConvert.DeserializeObject<DataTable>(respuesta_direcction.Value);
            //    DataTable dt = JsonConvert.DeserializeObject<DataTable>(hf_puntosruta.Value);
            //    StringBuilder sb = new StringBuilder();
            //    StringBuilder sb2 = new StringBuilder();
            //    p.TRAILER.TRAI_ID = Convert.ToInt32(ddl_vehiculoTrailer.SelectedValue);
            //    p.TRACTO.TRAC_ID = Convert.ToInt32(ddl_vehiculoTracto.SelectedValue);
            //    p.CONDUCTOR.COND_ID = Convert.ToInt32(ddl_vehiculoConductor.SelectedValue);
            //    p.RETORNO = hf_retorno.Value;
            //    if (hf_idRuta.Value == "")
            //    {
            //        p.GuardarPuntos(sb.ToString(), sb2.ToString(), ddl_buscarHorario.SelectedItem.Text);
            //        utils.ShowMessage2(this, "guardar", "success_nuevo");
            //    }
            //    else
            //    {
            //        p.ID = Convert.ToInt32(hf_idRuta.Value);
            //        p.GuardarPuntos(sb.ToString(), sb2.ToString(), ddl_buscarHorario.SelectedItem.Text);
            //    }
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
        }
    }
    #endregion
    #region DropDownList
    protected void ddl_vehiculoTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt;
        int trti_id = Convert.ToInt32(ddl_vehiculoTipo.SelectedValue);
        dt = new TrailerBC().ObtenerTodo(trti_id: trti_id);
        utils.CargaDropNormal(ddl_vehiculoTrailer, "TRAI_ID", "TRAI_PLACA", dt);
        ddl_vehiculoTrailer.Items.Insert(0, new RadComboBoxItem("Sin Vehículo", "0"));
        ddl_vehiculoTrailer.SelectedIndex = 0;
    }
    #endregion
    #region UtilsWeb
    private void ObtenerRutas()
    {
        DateTime fecha = Convert.ToDateTime(txt_buscarFecha.Text);
        int hora_id = Convert.ToInt32(ddl_buscarHorario.SelectedValue);
        List<OrigenBC> arrOrigen = new OrigenBC().ObtenerArray();
        hf_jsonOrigenes.Value = JsonConvert.SerializeObject(arrOrigen);
        List<PreRutaBC> arrRuta = new PreRutaBC().ObtenerArray(desde: fecha, hasta: fecha, hora_id: hora_id, puntos_ruta: true);
        hf_jsonRutas.Value = JsonConvert.SerializeObject(arrRuta);
        List<PedidoBC> arrPedido = new PedidoBC().ObtenerArray(desde: fecha, hasta: fecha, hora_id: hora_id, solo_sin_ruta: true);
        hf_jsonPedidos.Value = JsonConvert.SerializeObject(arrPedido);
        //up_hidden.Update();
        //up_contenedor.Update();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mapilla", "mapa();", true);

    }
    private void CargaDrops()
    {
        DataTable dt;
        dt = new HorarioBC().ObtenerTodo();
        utils.CargaDropNormal(ddl_buscarHorario, "HORA_ID", "HORA_COD", dt);

        dt = new TractoBC().ObtenerTodo();
        utils.CargaDropNormal(ddl_vehiculoTracto, "TRAC_ID", "TRAC_PLACA", dt);
        ddl_vehiculoTracto.Items.Insert(0, new RadComboBoxItem("Sin Tracto", "0"));
        ddl_vehiculoTracto.SelectedIndex = 0;

        dt = new TrailerTipoBC().ObtenerTodo();
        utils.CargaDropTodos(ddl_vehiculoTipo, "TRTI_ID", "TRTI_DESC", dt);
        ddl_vehiculoTipo.SelectedIndex = 0;
        ddl_vehiculoTipo_SelectedIndexChanged(null, null);

        dt = new ConductorBC().ObtenerTodo(cond_activo: true, cond_bloqueado: false);
        utils.CargaDropNormal(ddl_vehiculoConductor, "COND_ID", "COND_RUT", dt);
        ddl_vehiculoConductor.Items.Insert(0, new RadComboBoxItem("Sin Conductor", "0"));
        ddl_vehiculoConductor.SelectedIndex = 0;
    }
    #endregion
}