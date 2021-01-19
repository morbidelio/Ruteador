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
using System.IO;
using Microsoft.Reporting.WebForms;
using Telerik.Web.UI;

public partial class App_Pre_Rutas : System.Web.UI.Page // , ICallbackEventHandler
{
    UtilsWeb utils = new UtilsWeb();
    UsuarioBC user;
    string callbackReturnValue;

    bool mostrarCosa = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["retorno_ruta"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsCallback)
            return;

        if (Session["Usuario"] == null)
        {
            Response.Redirect("~/Inicio.aspx");
        }

        user = (UsuarioBC)Session["Usuario"];


        if (IsPostBack == false)
        {
            CargaDrops();
            txt_buscarFecha.Text = DateTime.Now.ToShortDateString();
            ObtenerRutas(true);

            List<OrigenBC> dt2 = new OrigenBC().ObtenerArray();
            hf_jsonOrigenes.Value = JsonConvert.SerializeObject(dt2);

        }
    }
    #region GridView
    protected void gv_listar_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.TableSection = TableRowSection.TableFooter;
        }
    }
    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerRutas(false);
    }
    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "COLOR")
        {
            hf_idRuta.Value = e.CommandArgument.ToString();
            PreRutaBC p = new PreRutaBC().ObtenerXId(Convert.ToInt32(hf_idRuta.Value));
            txt_editColor.Text = p.RUTA_COLOR;
            utils.AbrirModal(this.Page, "modalColor");
        }
        if (e.CommandName == "PUNTOS")
        {
            Limpiar();
            dv_detalle.Visible = false;
            //utils.AbrirModal(this.Page, "modalPuntos");
            gv_listar.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int id_preruta = Convert.ToInt32(gv_listar.SelectedDataKey.Values[0]);
            int id_origen = Convert.ToInt32(gv_listar.SelectedDataKey.Values[1]);
            hf_idRuta.Value = id_preruta.ToString();
            ObtenerPuntosRuta(true);
            //lbl_puntoSalida.Text = "Tiempo de viaje: ";
        }
        if (e.CommandName == "DETALLE")
        {
            Limpiar();
            hf_idRuta.Value = e.CommandArgument.ToString();
            PreRutaBC p = new PreRutaBC().ObtenerXId(Convert.ToInt32(hf_idRuta.Value));
            DateTime fecha = Convert.ToDateTime(txt_buscarFecha.Text);
            int hora_id = Convert.ToInt32(ddl_buscarHorario.SelectedValue);
            DataTable dt;
            ddl_vehiculoTracto.Items.Clear();
            ddl_vehiculoConductor.Items.Clear();
            txt_editNombre.Text = "";
            ddl_vehiculoTracto.Items.Add(new RadComboBoxItem("Sin Tracto", "0"));
            ddl_vehiculoConductor.Items.Add(new RadComboBoxItem("Sin Conductor", "0"));
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
                ddl_vehiculoTracto.Items.Add(cb);
            }
            dt = new ConductorBC().ObtenerTodo(fecha: fecha, hora_id: hora_id, cond_activo: true, cond_bloqueado: false);
            foreach (DataRow dr in dt.Rows)
            {
                RadComboBoxItem cb = new RadComboBoxItem((dr["COND_RUT"].ToString() + " - " + dr["COND_NOMBRE"].ToString()), dr["COND_ID"].ToString());
                if (dr["ID_RUTA"] != DBNull.Value)
                {
                    long id_ruta = Convert.ToInt32(dr["ID_RUTA"]);
                    if (id_ruta.ToString() != hf_idRuta.Value)
                    {
                        cb.Enabled = false;
                    }
                }
                ddl_vehiculoConductor.Items.Add(cb);
            }
            ddl_vehiculoTipo.SelectedValue = p.TRAILER.TRAILER_TIPO.TRTI_ID.ToString();
            ddl_vehiculoTipo_SelectedIndexChanged(null, null);
            ddl_vehiculoConductor.SelectedValue = p.CONDUCTOR.COND_ID.ToString();
            ddl_vehiculoTracto.SelectedValue = p.TRACTO.TRAC_ID.ToString();
            txt_editNombre.Text = p.NUMERO.ToString();
            ddl_vehiculoTrailer.SelectedValue = p.TRAILER.TRAI_ID.ToString();
            dv_detalle.Visible = true;
            utils.AbrirModal(this.Page, "modalVehiculo");
        }
        if (e.CommandName == "ELIMINAR")
        {
            hf_idRuta.Value = e.CommandArgument.ToString();
            lbl_confTitulo.Text = "Eliminar Propuesta";
            lbl_confMensaje.Text = "Se eliminará la propuesta de ruta seleccionada ¿Desea continuar?";
            utils.AbrirModal(this, "modalConf");
            btn_confEliminar.Visible = true;
            btn_confEliminarTodos.Visible = false;
        }

    }
    protected void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string color = DataBinder.Eval(e.Row.DataItem, "RUTA_COLOR").ToString();
            LinkButton btn_color = (LinkButton)e.Row.FindControl("btn_color");
            btn_color.Style.Add("background-color", color);
        }
    }
    #endregion
    #region utilsPagina
    private static void PrepareControlForExport(Control control)
    {
        for (int i = 0; i < control.Controls.Count; i++)
        {
            Control current = control.Controls[i];
            if (current is LinkButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
            }
            else if (current is ImageButton)
            {
                control.Controls.Remove(current);
            }
            else if (current is HyperLink)
            {
                control.Controls.Remove(current);
            }
            else if (current is DropDownList)
            {
                control.Controls.Remove(current);
            }
            else if (current is CheckBox)
            {
                control.Controls.Remove(current);
            }
            else if (current is HiddenField)
            {
                control.Controls.Remove(current);
            }
            if (current.HasControls())
            {
                PrepareControlForExport(current);
            }
        }
    }
    private void Limpiar()
    {
        hf_idRuta.Value = "";
        hf_idPedido.Value = "";
        hf_jsonRuta.Value = "";
        hf_origen.Value = "";
        hf_circular.Value = "";
        ddl_puntoNombre.SelectedIndex = 0;
        ddl_vehiculoTracto.SelectedIndex = 0;
        ddl_vehiculoTipo.SelectedIndex = 0;
        txt_editNombre.Text = "";
        ddl_vehiculoTipo_SelectedIndexChanged(null, null);
        ddl_vehiculoConductor.SelectedIndex = 0;
    }
    private void ObtenerRutas(bool forzarBD, bool refrescar_combo = true)
    {
        if (ViewState["listar"] == null || forzarBD)
        {
            DateTime desde = (string.IsNullOrEmpty(txt_buscarFecha.Text)) ? DateTime.MinValue : Convert.ToDateTime(txt_buscarFecha.Text);
            DateTime hasta = (string.IsNullOrEmpty(txt_buscarFecha.Text)) ? DateTime.MinValue : Convert.ToDateTime(txt_buscarFecha.Text);
            int regi_id = Convert.ToInt32(ddl_buscarRegion.SelectedValue);
            int ciud_id = Convert.ToInt32(ddl_buscarCiudad.SelectedValue);
            int comu_id = Convert.ToInt32(ddl_buscarComuna.SelectedValue);
            int hora_id = Convert.ToInt32(ddl_buscarHorario.SelectedValue);
            string envio = txt_buscaenvio.Text;
            ViewState["listar"] = new PreRutaBC().ObtenerTodo(desde: desde
                                        , hasta: hasta
                                        , regi_id: regi_id
                                        , ciud_id: ciud_id
                                        , comu_id: comu_id
                                        , usua_id: user.USUA_ID
                                        , peru_numero: txt_buscarNro.Text
                                        , hora_id: hora_id
                                        , envio: envio);
        }
        DataView dw = new DataView((DataTable)ViewState["listar"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)ViewState["sortExpresion"];
        }

        if (refrescar_combo == true)
        {
            utils.CargaDropNormal(ddl_puntosCambiarPreruta, "ID", "NUMERO", dw.ToTable());
        }
        DateTime fh = Convert.ToDateTime(txt_buscarFecha.Text);
        List<PedidoBC> cosa = new PedidoBC().ObtenerArray(desde: fh, hasta: fh, solo_sin_ruta: true);
        hf_jsonPedidos.Value = JsonConvert.SerializeObject(cosa);

        gv_listar.DataSource = dw;
        gv_listar.DataBind();
    }
    private void ObtenerPuntosRuta(bool forzarBD)
    {
        DateTime fh = Convert.ToDateTime(txt_buscarFecha.Text);
        int hora_id = Convert.ToInt32(ddl_buscarHorario.SelectedValue);
        PreRutaBC p = new PreRutaBC().ObtenerXId(Convert.ToInt32(hf_idRuta.Value), true);
        hf_jsonRuta.Value = JsonConvert.SerializeObject(p);
        hf_circular.Value = p.RETORNO;
        DataTable dt;
        dt = new PedidoBC().ObtenerTodo(desde: fh, hasta: fh, solo_sin_ruta: true, id_ruta: p.ID);
        utils.CargaDrop(ddl_puntoNombre, "PERU_ID", "PERU_NUMERODROP", dt);

        ddl_puntosCambiarPreruta.Visible = true;
        ddl_puntosCambiarPreruta.SelectedValue = p.ID.ToString();
        hf_origen.Value = JsonConvert.SerializeObject(p.ORIGEN);
        ddl_vehiculoTipo.SelectedValue = p.TRAILER.TRAILER_TIPO.TRTI_ID.ToString();
        ddl_vehiculoTipo_SelectedIndexChanged(null, null);
        ddl_vehiculoTrailer.SelectedValue = p.TRAILER.TRAI_ID.ToString();
        lbl_puntoTracto.Text = (string.IsNullOrEmpty(p.TRACTO.TRAC_PLACA)) ? "Sin tracto" : "Tracto: " + p.TRACTO.TRAC_PLACA;
        lbl_puntoTrailer.Text = (string.IsNullOrEmpty(p.TRAILER.TRAI_PLACA)) ? "Sin trailer" : "Trailer: " + p.TRAILER.TRAI_PLACA;
        lbl_puntoConductor.Text = (string.IsNullOrEmpty(p.CONDUCTOR.COND_NOMBRE)) ? "Sin conductor" : "Conductor: " + p.CONDUCTOR.COND_NOMBRE;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "map_cargar", "abrirModal('modalPuntos'); mapa(false);", true);

        ddl_vehiculoTracto.Items.Clear();
        ddl_vehiculoTracto.Items.Add(new RadComboBoxItem("Sin Tracto", "0"));
        dt = new TractoBC().ObtenerTodo(fecha: fh, hora_id: hora_id);
        foreach (DataRow dr in dt.Rows)
        {
            RadComboBoxItem cb = new RadComboBoxItem(dr["TRAC_PLACA"].ToString(), dr["TRAC_ID"].ToString());
            if (dr["ID_RUTA"] != DBNull.Value)
            {
                long id_ruta = Convert.ToInt32(dr["ID_RUTA"]);
                cb.Enabled = (id_ruta.ToString() == hf_idRuta.Value);
            }
            ddl_vehiculoTracto.Items.Add(cb);
        }
        ddl_vehiculoTracto.SelectedValue = p.TRACTO.TRAC_ID.ToString();
        txt_editNombre.Text = p.NUMERO;
        ddl_vehiculoConductor.Items.Clear();
        ddl_vehiculoConductor.Items.Add(new RadComboBoxItem("Sin Conductor", "0"));
        dt = new ConductorBC().ObtenerTodo(fecha: fh, hora_id: hora_id, cond_activo: true, cond_bloqueado: false);
        foreach (DataRow dr in dt.Rows)
        {
            RadComboBoxItem cb = new RadComboBoxItem((dr["COND_RUT"].ToString() + " - " + dr["COND_NOMBRE"].ToString()), dr["COND_ID"].ToString());
            if (dr["ID_RUTA"] != DBNull.Value)
            {
                long id_ruta = Convert.ToInt32(dr["ID_RUTA"]);
                cb.Enabled = (id_ruta.ToString() == hf_idRuta.Value);
            }
            ddl_vehiculoConductor.Items.Add(cb);
        }
        ddl_vehiculoConductor.SelectedValue = p.CONDUCTOR.COND_ID.ToString();
    }
    private void CargaDrops()
    {
        DataTable dt;
        dt = new RegionBC().ObtenerTodo();
        utils.CargaDropTodos(ddl_buscarRegion, "REGI_ID", "REGI_NOMBRE", dt);
        ddl_buscarRegion_SelectedIndexChanged(null, null);

        dt = new HorarioBC().ObtenerTodo();
        utils.CargaDropNormal(ddl_buscarHorario, "HORA_ID", "HORA_COD", dt);

        dt = new TrailerTipoBC().ObtenerTodo();
        utils.CargaDropTodos(ddl_vehiculoTipo, "TRTI_ID", "TRTI_DESC", dt);
        ddl_vehiculoTipo.SelectedIndex = 0;
    }
    #endregion
    #region Buttons
    protected void btn_confEnviar_Click(object sender, EventArgs e)
    {
        PreRutaBC gd = new PreRutaBC();
        try
        {
            DataTable excel = gd.CrearEnvio(hseleccionado.Value.ToString(), user.USUA_ID, chk_archivar.Checked);
            ViewState["lista"] = excel;
            utils.CerrarModal(this.Page, "modalenviar");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "exp", "exportar();", true);
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
            ObtenerRutas(true);
        }
    }


    protected void btn_eliminar_todos_click(object sender, EventArgs e)
    {
        PreRutaBC gd = new PreRutaBC();
        String mimeType = "";

        try
        {
            // DataTable excel = gd.CrearEnvio(hseleccionado.Value.ToString(), user.USUA_ID, chk_archivar.Checked);
            // ViewState["lista"] = excel;

            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "exp", "exportar();", true);


            if (hseleccionado.Value == "")
            {
                utils.ShowMessage(this, "Seleccione al menos un viaje", "error", false);
                return;
            }

            hf_idRuta.Value = "";

            lbl_confTitulo.Text = "Eliminar Multiple Propuesta";
            lbl_confMensaje.Text = "Se eliminará la propuesta de ruta seleccionada ¿Desea continuar?";
            btn_confEliminar.Visible = false;
            btn_confEliminarTodos.Visible = true;
            utils.AbrirModal(this, "modalConf");

                      
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
 
        }

    }

    protected void btn_pre_pdf_click(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "exp", "exportarpdf();", true);

    }
    protected void btn_pdf_click(object sender, EventArgs e)
    {
        PreRutaBC gd = new PreRutaBC();
        String mimeType = "";

        try
        {
            // DataTable excel = gd.CrearEnvio(hseleccionado.Value.ToString(), user.USUA_ID, chk_archivar.Checked);
            // ViewState["lista"] = excel;

            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "exp", "exportar();", true);


            if (hseleccionado.Value == "")
            {
                utils.ShowMessage(this, "Seleccione al menos un viaje", "error", false);
                return;
            }


            this.pnlReport.Visible = true;
            ReportBC report = new ReportBC();
            //      VIAJEBC v = new VIAJEBC().ObtenerXID(Convert.ToInt32(this.tbidviajed.Text));
            List<int> ids = hseleccionado.Value.ToString().Split(',').Select(int.Parse).ToList();

            string zip = GenerateFileNamezipPDF("hr_", ".zip");
            int contador = 0;


            if (ids.Count == 1)
            {
                try
                {
                    // DataTable excel = gd.CrearEnvio(hseleccionado.Value.ToString(), user.USUA_ID, chk_archivar.Checked);
                    // ViewState["lista"] = excel;

                    //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "exp", "exportar();", true);

                    this.pnlReport.Visible = true;
                    DataTable datos = report.obrenerReporteDespachoViaje(ids[0].ToString());
                    ReportDataSource dataSource = new ReportDataSource("Datos", datos);

                    this.ReportViewer1.LocalReport.DataSources.Clear();
                    this.ReportViewer1.LocalReport.DataSources.Add(dataSource);



                    Warning[] warnings;
                    string[] streamids;

                    string encoding;
                    string extension;
                    //Word
                    byte[] bytes = this.ReportViewer1.LocalReport.Render(
                        "PDF", null, out mimeType, out encoding,
                        out extension,
                        out streamids, out warnings);
                    //byte[] renderedBytes = this.ReportViewer1.LocalReport.Render("PDF");
                    this.Response.Clear();

                    this.Response.ContentType = mimeType;

                    this.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}{1}{2}", datos.Rows[0]["numero"].ToString(), '.', extension));

                    this.Response.BinaryWrite(bytes);

                    this.Response.End();



                }
                catch (Exception ex)
                {
                    utils.ShowMessage(this, ex.Message, "error", false);
                }
                finally
                {
                    ObtenerRutas(true);
                }


            }
            else
            {
                while (contador < ids.Count)
                {

                    DataTable datos = report.obrenerReporteDespachoViaje(ids[contador].ToString());
                    ReportDataSource dataSource = new ReportDataSource("Datos", datos);

                    this.ReportViewer1.LocalReport.DataSources.Clear();
                    this.ReportViewer1.LocalReport.DataSources.Add(dataSource);
                    Warning[] warnings;
                    string[] streamids;
                    string encoding;
                    string extension;
                    //Word
                    byte[] bytes = this.ReportViewer1.LocalReport.Render(
                        "PDF", null, out mimeType, out encoding,
                        out extension,
                        out streamids, out warnings);
                    //byte[] renderedBytes = this.ReportViewer1.LocalReport.Render("PDF");

                    Stream stream = new MemoryStream(bytes);


                    UtilsWeb.AddStreamToZip(zip, stream, GenerateFileNamezipPDF("hr_" + datos.Rows[0]["numero"].ToString(), ".pdf"));
                    contador = contador + 1;

                }




                //this.Response.Clear();
                //this.Response.ContentType = mimeType;
                //this.Response.AddHeader("content-disposition", string.Format("attachment; filename=Hoja_Ruta_{0}{1}{2}", 'a', '.', extension));
                //this.Response.BinaryWrite(bytes);
                //this.Response.End();


                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + "descarga_multiple.zip");
                Response.BinaryWrite(File.ReadAllBytes(zip));
                //   File.Delete(Server.MapPath("./cargadefotos/Output.zip"));
                File.Delete(zip);

                Response.End();
            }


        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
            ObtenerRutas(true);
        }
    }
    protected void btn_exportarExcel_Click(object sender, EventArgs e)
    {
        DataView view = new DataView();
        view.Table = (DataTable)ViewState["lista"];
        GridView gv = new GridView();

        gv.DataSource = view;
        gv.DataBind();

        string fileName = "rutas_generadas.xls";
        PrepareControlForExport(gv);
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("Content-type", "application / xls");

        HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
        HttpContext.Current.Response.Charset = "";
        HttpContext.Current.Response.Cache.SetCacheability(System.Web.HttpCacheability.Public);
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        try
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    System.Web.UI.WebControls.Table table = new System.Web.UI.WebControls.Table();
                    table.GridLines = gv.GridLines;

                    if (gv.HeaderRow != null)
                    {
                        PrepareControlForExport(gv.HeaderRow);
                        table.Rows.Add(gv.HeaderRow);
                    }

                    foreach (GridViewRow row in gv.Rows)
                    {
                        PrepareControlForExport(row);
                        table.Rows.Add(row);
                    }

                    if (gv.FooterRow != null)
                    {
                        PrepareControlForExport(gv.FooterRow);
                        table.Rows.Add(gv.FooterRow);
                    }

                    gv.GridLines = GridLines.Both;
                    table.RenderControl(htw);
                    HttpContext.Current.Response.Write(sw.ToString());
                    HttpContext.Current.Response.End();
                }
            }
        }
        catch (HttpException ex)
        {
            throw ex;
        }

    }
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        ObtenerRutas(true);
    }

    protected void btn_confEliminartodos_Click(object sender, EventArgs e)
    {
        try
        {
          //  int peru_id = Convert.ToInt32(hf_idRuta.Value);
            if (new PreRutaBC().EliminarMultiple(hseleccionado.Value))
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
            ObtenerRutas(true);
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
            ObtenerRutas(true);
        }
    }
    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        Limpiar();
        dv_detalle.Visible = false;
        utils.AbrirModal(this, "modalPuntos");
        ddl_puntosCambiarPreruta.Visible = false;
        OrigenBC o = new OrigenBC().ObtenerXId(1);
        hf_origen.Value = JsonConvert.SerializeObject(o);

      //  DataTable dt = new PreRutaBC().ObtenerPuntos();
      //  hf_jsonRuta.Value = JsonConvert.SerializeObject(dt);

        DateTime fh = Convert.ToDateTime(txt_buscarFecha.Text);
        //        dt = new PedidoBC().ObtenerTodo(desde: fh, hasta: fh, solo_sin_ruta: true);
        DataTable dt = new PedidoBC().ObtenerTodo(desde: fh, hasta: fh, hora_id: int.Parse(ddl_buscarHorario.SelectedValue), regi_id: 0, ciud_id: 0, comu_id : null, usua_id : 0, peru_numero : null, solo_sin_ruta : true, id_ruta : 0);
        hf_jsonPedidos.Value = JsonConvert.SerializeObject(dt);
        utils.CargaDrop(ddl_puntoNombre, "PERU_ID", "PERU_NUMERODROP", dt);

        PreRutaBC p = new PreRutaBC();
        p.FECHA_DESPACHOEXP = DateTime.Parse( txt_buscarFecha.Text);
        p.FH_CREACION = DateTime.Now;

        p.HORARIO.HORA_COD = ddl_buscarHorario.SelectedItem.Text;
        p.HORARIO.HORA_ID = int.Parse(ddl_buscarHorario.SelectedItem.Value);
        var random = new Random();
        var color = String.Format("#{0:X6}", random.Next(0x1000000));
        p.RUTA_COLOR = color;
        hf_jsonRuta.Value = JsonConvert.SerializeObject(p);

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "map_cargar", "mapa(true);", true);
    }
    protected void btn_enviar_Click(object sender, EventArgs e)
    {
        Label2.Text = "Marcar para archivar";
        lbl_titulo_enviar.Text = "Archivar rutas y pedidos seleccionados?";
        utils.AbrirModal(this, "modalenviar");
    }
    protected void btn_puntosGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            bool nuevo=false;
            if (hf_idRuta.Value == "") nuevo = true;

            PreRutaBC pre_ruta = JsonConvert.DeserializeObject<PreRutaBC>(hf_jsonRuta.Value);

            pre_ruta.NUMERO = txt_editNombre.Text;
            pre_ruta.Guardar();
            hf_idRuta.Value = pre_ruta.ID.ToString();
            
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
           if (nuevo) utils.ShowMessage2(this, "guardar", "success_nuevo");
           else utils.ShowMessage2(this, "guardar", "success_modificar");

           ObtenerRutas(true, false);
           ListItem yo = ddl_puntosCambiarPreruta.Items.FindByValue(pre_ruta.ID.ToString());
            if (yo!=null)
            {
                
                yo.Text = pre_ruta.NUMERO;

            }
            else
            {

            
                ddl_puntosCambiarPreruta.Items.Add(new ListItem(pre_ruta.NUMERO, pre_ruta.ID.ToString()));
                ddl_puntosCambiarPreruta.SelectedValue = pre_ruta.ID.ToString();
            }
            

        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
            ObtenerPuntosRuta(true);
            
        }
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
            p.NUMERO = txt_editNombre.Text;
            p.Guardar();
            utils.ShowMessage2(this, "guardar", "success_modificar");
            ddl_puntosCambiarPreruta.Items.FindByValue(p.ID.ToString()).Text = p.NUMERO;
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
            utils.CerrarModal(this, "modalVehiculo");
            ObtenerRutas(true, false);
        }
    }
    protected void btn_colorGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            PreRutaBC p = new PreRutaBC();
            p.ID = Convert.ToInt32(hf_idRuta.Value);
            p.RUTA_COLOR = txt_editColor.Text;
            p.Guardar();
            utils.ShowMessage2(this, "guardar", "success_modificar");
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
            utils.CerrarModal(this, "modalColor");
            ObtenerRutas(true, false);
        }
    }
    #endregion
    #region DropDownList
    protected void ddl_vehiculoTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        int trti_id = Convert.ToInt32(ddl_vehiculoTipo.SelectedValue);
        DateTime fecha = Convert.ToDateTime(txt_buscarFecha.Text);
        DataTable dt = new TrailerBC().ObtenerTodo(fecha: fecha
                                        , hora_id: Convert.ToInt32(ddl_buscarHorario.SelectedValue)
                                        , trti_id: trti_id);
        ddl_vehiculoTrailer.Items.Clear();
        ddl_vehiculoTrailer.Items.Add(new RadComboBoxItem("Sin Vehículo", "0"));
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
            ddl_vehiculoTrailer.Items.Add(cb);
        }
        ddl_vehiculoTrailer.SelectedIndex = 0;
    }
    protected void ddl_puntosCambiarPreruta_SelectedIndexChanged(object sender, EventArgs e)
    {
        Limpiar();
        int id_ruta = Convert.ToInt32(ddl_puntosCambiarPreruta.SelectedValue);
        hf_idRuta.Value = id_ruta.ToString();
        PreRutaBC p = new PreRutaBC().ObtenerXId(id_ruta);
        hf_origen.Value = JsonConvert.SerializeObject(p.ORIGEN);
        ObtenerPuntosRuta(true);
    }
    protected void ddl_buscarRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        int regi_id = Convert.ToInt32(ddl_buscarRegion.SelectedValue);
        DataTable dt = new CiudadBC().ObtenerTodo(regi_id);
        utils.CargaDropTodos(ddl_buscarCiudad, "CIUD_ID", "CIUD_NOMBRE", dt);
        ddl_buscarCiudad_SelectedIndexChanged(null, null);
    }
    protected void ddl_buscarCiudad_SelectedIndexChanged(object sender, EventArgs e)
    {
        int regi_id = Convert.ToInt32(ddl_buscarRegion.SelectedValue);
        int ciud_id = Convert.ToInt32(ddl_buscarCiudad.SelectedValue);
        DataTable dt = new ComunaBC().ObtenerTodo(regi_id, ciud_id);
        utils.CargaDropTodos(ddl_buscarComuna, "COMU_ID", "COMU_NOMBRE", dt);
    }
    #endregion

    private string GenerateFileNamePDF(string prefijo = "", string extension = ".txt")
    {
        //   string pageName = prefijo; // +Path.GetFileNameWithoutExtension(this.Page.AppRelativeVirtualPath);

        string file = string.Format("{0}", extension);

        //       file = Path.Combine(Server.MapPath("~/ViewStateFiles") + "/" + file);  
        file = string.Format("{0}\\{1}", this.utils.pathviewstate(), file);

        return file;
    }

    private string GenerateFileNamezipPDF(string prefijo = "", string extension = ".txt")
    {
        //    string pageName = prefijo + Path.GetFileNameWithoutExtension(this.Page.AppRelativeVirtualPath);

        string file = string.Format("{0}{1}", prefijo, extension);

        //       file = Path.Combine(Server.MapPath("~/ViewStateFiles") + "/" + file);  
        file = string.Format("{0}\\{1}", this.utils.pathviewstate(), file);

        return file;
    }


}