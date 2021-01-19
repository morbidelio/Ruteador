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

        if (Page.IsCallback)
            return;

        if (!IsPostBack)
        {
            txt_buscarFecha.Text = DateTime.Now.ToShortDateString();
            CargaDrops();
            //ObtenerRutas();
        }
    }
    #region Buttons
    //protected void btn_rutaGuardar_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        PreRutaBC pre_ruta = JsonConvert.DeserializeObject<PreRutaBC>(hf_jsonRuta.Value);
    //        pre_ruta.Guardar();
    //        DataTable dt = new DataTable();
    //        dt.Columns.Add("ID_DESTINO", typeof(int));
    //        dt.Columns.Add("SECUENCIA", typeof(int));
    //        dt.Columns.Add("FH_LLEGADA", typeof(DateTime));
    //        dt.Columns.Add("FH_SALIDA", typeof(DateTime));
    //        dt.Columns.Add("TIEMPO", typeof(int));

    //        DateTime fechaRelativa = pre_ruta.FECHA_DESPACHOEXP;
    //        string[] temp = pre_ruta.PEDIDOS[0].HORA_SALIDA.HORA_COD.Split(":".ToCharArray());

    //        fechaRelativa = fechaRelativa.Date.AddHours(Convert.ToInt32(temp[0])).AddMinutes(Convert.ToInt32(temp[1]));

    //        foreach (PedidoBC p in pre_ruta.PEDIDOS)
    //        {
    //            p.RUTA_PEDIDO.tiempo = Convert.ToInt32(p.RUTA_PEDIDO.FH_LLEGADA.Subtract(fechaRelativa).TotalMinutes);
    //            fechaRelativa = p.RUTA_PEDIDO.FH_SALIDA;
    //            DataRow dr = dt.NewRow();
    //            dr["ID_DESTINO"] = p.PERU_ID;
    //            dr["SECUENCIA"] = p.RUTA_PEDIDO.SECUENCIA;
    //            dr["FH_LLEGADA"] = p.RUTA_PEDIDO.FH_LLEGADA;
    //            dr["FH_SALIDA"] = p.RUTA_PEDIDO.FH_SALIDA;
    //            dr["TIEMPO"] = p.RUTA_PEDIDO.tiempo;
    //            dt.Rows.Add(dr);
    //        }
    //        pre_ruta.GuardarPuntos();
    //        var debug = "";
    //    }
    //    catch (Exception ex)
    //    {
    //        utils.ShowMessage(this, ex.Message, "error", false);
    //    }
    //    finally
    //    {
    //    }
    //}
    protected void btn_refrescar_Click(object sender, EventArgs e)
    {
        ObtenerRutas();
    }
    protected void btn_confEliminarRuta_Click(object sender, EventArgs e)
    {
        try
        {
            PreRutaBC p = new PreRutaBC();
            p.ID = Convert.ToInt64(hf_idRuta.Value);
            p.Eliminar();
            utils.ShowMessage2(this, "eliminar", "success");
            utils.CerrarModal(this, "modalConf");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "elimina2", "eliminarRuta2();", true);
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
    }
    protected void btn_rutaNuevo_Click(object sender, EventArgs e)
    {
        try
        {
            PreRutaBC pre_ruta = new PreRutaBC();
            pre_ruta.HORARIO.HORA_ID = Convert.ToInt32(ddl_buscarHorario.SelectedValue);
            pre_ruta.FECHA_DESPACHOEXP = Convert.ToDateTime(txt_buscarFecha.Text);
            List<OrigenBC> arrOrigen = new OrigenBC().ObtenerArray();
            pre_ruta.ORIGEN = arrOrigen[0];


            
            pre_ruta.FH_CREACION = DateTime.Now;

            var random = new Random();
            var color = String.Format("#{0:X6}", random.Next(0x1000000));
            pre_ruta.RUTA_COLOR = color;

            pre_ruta.Guardar();
            hf_idRuta.Value = pre_ruta.ID.ToString();
            //hf_jsonRuta.Value = JsonConvert.SerializeObject(pre_ruta.ObtenerXId(pre_ruta.ID));
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "nuevaruta", string.Format("nuevaRuta('{0}');", JsonConvert.SerializeObject(pre_ruta.ObtenerXId(pre_ruta.ID))), true);
            var debug = "";
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
    }
    protected void btn_detalleAbrir_Click(object sender, EventArgs e)
    {
        DateTime fecha = Convert.ToDateTime(txt_buscarFecha.Text);
        int hora_id = Convert.ToInt32(ddl_buscarHorario.SelectedValue);
        DataTable dt;
        ddl_editTracto.Items.Clear();
        ddl_editConductor.Items.Clear();
        ddl_editTracto.Items.Add(new RadComboBoxItem("Sin Tracto", "0"));
        ddl_editConductor.Items.Add(new RadComboBoxItem("Sin Conductor", "0"));
        dt = new TractoBC().ObtenerTodo(fecha: fecha, hora_id: hora_id);
        foreach (DataRow dr in dt.Rows)
        {
            RadComboBoxItem cb = new RadComboBoxItem(dr["TRAC_PLACA"].ToString(), dr["TRAC_ID"].ToString());
            if (dr["ID_RUTA"] != DBNull.Value)
            {
                long id_ruta = Convert.ToInt32(dr["ID_RUTA"]);
                if (id_ruta.ToString() != hf_idRuta.Value)
                {
                    cb.Enabled = false;
                }
            }
            ddl_editTracto.Items.Add(cb);
        }
        dt = new ConductorBC().ObtenerTodo(fecha: fecha, hora_id: hora_id, cond_activo: true, cond_bloqueado: false);
        foreach (DataRow dr in dt.Rows)
        {
            RadComboBoxItem cb = new RadComboBoxItem((dr["COND_RUT"].ToString() + "/" + dr["COND_NOMBRE"].ToString()), dr["COND_ID"].ToString());
            if (dr["ID_RUTA"] != DBNull.Value)
            {
                long id_ruta = Convert.ToInt32(dr["ID_RUTA"]);
                if (id_ruta.ToString() != hf_idRuta.Value)
                {
                    cb.Enabled = false;
                }
            }
            ddl_editConductor.Items.Add(cb);
        }
        ddl_editTracto.SelectedIndex = 0;
        ddl_editConductor.SelectedIndex = 0;
        PreRutaBC p = new PreRutaBC().ObtenerXId(Convert.ToInt64(hf_idRuta.Value));
        ddl_editTipo.SelectedValue = p.TRAILER.TRAILER_TIPO.TRTI_ID.ToString();
        ddl_vehiculoTipo_SelectedIndexChanged(null, null);
        ddl_editConductor.SelectedValue = p.CONDUCTOR.COND_ID.ToString();
        ddl_editTracto.SelectedValue = p.TRACTO.TRAC_ID.ToString();
        ddl_editTrailer.SelectedValue = p.TRAILER.TRAI_ID.ToString();
    }
    #endregion
    #region DropDownList
    protected void ddl_vehiculoTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        int trti_id = Convert.ToInt32(ddl_editTipo.SelectedValue);
        DateTime fecha = Convert.ToDateTime(txt_buscarFecha.Text);
        DataTable dt = new TrailerBC().ObtenerTodo(fecha: fecha
                                        , hora_id: Convert.ToInt32(ddl_buscarHorario.SelectedValue)
                                        , trti_id: trti_id);
        ddl_editTrailer.Items.Clear();
        ddl_editTrailer.Items.Add(new RadComboBoxItem("Sin Vehículo", "0"));
        foreach (DataRow dr in dt.Rows)
        {
            RadComboBoxItem cb = new RadComboBoxItem(dr["TRAI_PLACA"].ToString(), dr["TRAI_ID"].ToString());
            if (dr["ID_RUTA"] != DBNull.Value)
            {
                int id_ruta = Convert.ToInt32(dr["ID_RUTA"]);
                if (id_ruta.ToString() != hf_idRuta.Value)
                {
                    cb.Enabled = false;
                }
            }
            ddl_editTrailer.Items.Add(cb);
        }
        ddl_editTrailer.SelectedIndex = 0;
    }
    protected void ddl_buscarHorario_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObtenerRutas();
    }
    #endregion
    #region UtilsWeb
    private void ObtenerRutas()
    {
        DateTime fecha = Convert.ToDateTime(txt_buscarFecha.Text);
        int hora_id = Convert.ToInt32(ddl_buscarHorario.SelectedValue);
        List<OrigenBC> arrOrigen = new OrigenBC().ObtenerArray();
        List<PreRutaBC> arrRuta = new PreRutaBC().ObtenerArray(desde: fecha, hasta: fecha, hora_id: hora_id, puntos_ruta: true);
        List<PedidoBC> arrPedido = new PedidoBC().ObtenerArray(desde: fecha, hasta: fecha, hora_id: hora_id, solo_sin_ruta: true);
        ScriptManager.RegisterStartupScript(this.Page
            , this.GetType()
            , "mapilla"
            , string.Format("jsonPedidos = {0};" +
                "jsonOrigenes = {1};" +
                "jsonRutas = {2};" +
                "mapa();"
                , JsonConvert.SerializeObject(arrPedido)
                , JsonConvert.SerializeObject(arrOrigen)
                , JsonConvert.SerializeObject(arrRuta))
            , true);

    }
    private void CargaDrops()
    {
        DataTable dt;
        dt = new HorarioBC().ObtenerTodo();
        utils.CargaDropNormal(ddl_buscarHorario, "HORA_ID", "HORA_COD", dt);

        dt = new TrailerTipoBC().ObtenerTodo();
        utils.CargaDropTodos(ddl_editTipo, "TRTI_ID", "TRTI_DESC", dt);
        ddl_editTipo.SelectedIndex = 0;
    }
    #endregion
    #region TextBox
    protected void txt_buscarFecha_TextChanged(object sender, EventArgs e)
    {
        ObtenerRutas();
    }
    #endregion
}