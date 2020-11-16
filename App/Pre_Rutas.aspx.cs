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


        //  string callBackClientID = Page.ClientScript.GetCallbackEventReference(this, "arg", "rutaCallback", "context", "rutaCallbackError", true);

        //  string clientIDfunction = "function GetRuta(arg,context) { " + callBackClientID + "; }";

        //   Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetRuta", clientIDfunction, true);


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

            DataTable dt = new PedidoBC().ObtenerTodo(solo_sin_ruta: true);
            hf_todos.Value = JsonConvert.SerializeObject(dt);


            DataTable dt2 = new OrigenBC().ObtenerTodo();
            hf_origenes.Value = JsonConvert.SerializeObject(dt2);

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
            lbl_puntoSalida.Text = "Tiempo de viaje: ";
        }
        if (e.CommandName == "DETALLE")
        {
            Limpiar();
            hf_idRuta.Value = e.CommandArgument.ToString();
            PreRutaBC p = new PreRutaBC().ObtenerXId(Convert.ToInt32(hf_idRuta.Value));
            ddl_vehiculoTracto.SelectedValue = p.TRACTO.TRAC_ID.ToString();
            ddl_vehiculoTrailer.SelectedValue = p.TRAILER.TRAI_ID.ToString();
            ddl_vehiculoConductor.SelectedValue = p.CONDUCTOR.COND_ID.ToString();
            dv_detalle.Visible = true;
            utils.AbrirModal(this.Page, "modalVehiculo");
        }
        if (e.CommandName == "ELIMINAR")
        {
            hf_idRuta.Value = e.CommandArgument.ToString();
            lbl_confTitulo.Text = "Eliminar Propuesta";
            lbl_confMensaje.Text = "Se eliminará la propuesta de ruta seleccionada ¿Desea continuar?";
            utils.AbrirModal(this, "modalConf");
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
        hf_idPunto.Value = "";
        hf_puntosruta.Value = "";
        hf_origen.Value = "";
        hf_circular.Value = "";
        ddl_puntoNombre.SelectedIndex = 0;
        ddl_vehiculoTracto.SelectedIndex = 0;
        ddl_vehiculoTipo.SelectedIndex = 0;
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

        gv_listar.DataSource = dw;
        gv_listar.DataBind();
    }
    private void ObtenerPuntosRuta(bool forzarBD)
    {
        PreRutaBC p = new PreRutaBC().ObtenerXId(Convert.ToInt32(hf_idRuta.Value));
        hf_circular.Value = p.RETORNO;
        DataTable dt;
        dt = p.ObtenerPuntos(p.ID);
        hf_puntosruta.Value = JsonConvert.SerializeObject(dt);
        
        dt = new PedidoBC().ObtenerTodo(desde: p.FH_CREACION, hasta: p.FH_CREACION, solo_sin_ruta: true, id_ruta: p.ID);
        hf_todos.Value = JsonConvert.SerializeObject(dt);
        utils.CargaDrop(ddl_puntoNombre, "PERU_ID", "PERU_NUMERODROP", dt);

        ddl_puntosCambiarPreruta.Visible = true;
        ddl_puntosCambiarPreruta.SelectedValue = p.ID.ToString();
        hf_origen.Value = JsonConvert.SerializeObject(p.ORIGEN);
        ddl_vehiculoTracto.SelectedValue = p.TRACTO.TRAC_ID.ToString();
        ddl_vehiculoTipo.SelectedValue = p.TRAILER.TRAILER_TIPO.TRTI_ID.ToString();
        ddl_vehiculoTipo_SelectedIndexChanged(null, null);
        ddl_vehiculoTrailer.SelectedValue = p.TRAILER.TRAI_ID.ToString();
        ddl_vehiculoConductor.SelectedValue = p.CONDUCTOR.COND_ID.ToString();
        lbl_puntoTracto.Text = (string.IsNullOrEmpty(p.TRACTO.TRAC_PLACA)) ? "Sin tracto" : "Tracto: " + p.TRACTO.TRAC_PLACA;
        lbl_puntoTrailer.Text = (string.IsNullOrEmpty(p.TRAILER.TRAI_PLACA)) ? "Sin trailer" : "Trailer: " + p.TRAILER.TRAI_PLACA;
        lbl_puntoConductor.Text = (string.IsNullOrEmpty(p.CONDUCTOR.COND_NOMBRE)) ? "Sin conductor" : "Conductor: " + p.CONDUCTOR.COND_NOMBRE;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "map_cargar", string.Format("abrirModal('modalPuntos'); setColor('{0}'); mapa(false);", p.RUTA_COLOR), true);
    }
    private void CargaDrops()
    {
        DataTable dt;
        dt = new RegionBC().ObtenerTodo();
        utils.CargaDropTodos(ddl_buscarRegion, "REGI_ID", "REGI_NOMBRE", dt);
        ddl_buscarRegion_SelectedIndexChanged(null, null);

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

        DataTable dt = new PreRutaBC().ObtenerPuntos();
        hf_puntosruta.Value = JsonConvert.SerializeObject(dt);

        DateTime fh = Convert.ToDateTime(txt_buscarFecha.Text);
        dt = new PedidoBC().ObtenerTodo(desde: fh, hasta: fh, solo_sin_ruta: true);
        hf_todos.Value = JsonConvert.SerializeObject(dt);
        utils.CargaDrop(ddl_puntoNombre, "PERU_ID", "PERU_NUMERODROP", dt);

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
            PreRutaBC p = new PreRutaBC();
            DataTable dt2 = JsonConvert.DeserializeObject<DataTable>(respuesta_direcction.Value);
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(hf_puntosruta.Value);
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            p.TRAILER.TRAI_ID = Convert.ToInt32(ddl_vehiculoTrailer.SelectedValue);
            p.TRACTO.TRAC_ID = Convert.ToInt32(ddl_vehiculoTracto.SelectedValue);
            p.CONDUCTOR.COND_ID = Convert.ToInt32(ddl_vehiculoConductor.SelectedValue);
            p.RETORNO = hf_circular.Value;
            foreach (DataRow dr in dt.Rows)
            {
                if (sb.Length != 0)
                    sb.Append(",");
                sb.Append(dr["PERU_ID"]);
            }
            foreach (DataRow dr2 in dt2.Rows)
            {
                if (sb2.Length != 0)
                    sb2.Append(",");
                sb2.Append(dr2["value"]);
            }
            if (hf_idRuta.Value == "")
            {
                p.GuardarPuntos(sb.ToString(), sb2.ToString(), ddl_buscarHorario.SelectedItem.Text);
                utils.ShowMessage2(this, "guardar", "success_nuevo");
            }
            else
            {
                p.ID = Convert.ToInt32(hf_idRuta.Value);
                p.GuardarPuntos(sb.ToString(), sb2.ToString(), ddl_buscarHorario.SelectedItem.Text);
                utils.ShowMessage2(this, "guardar", "success_modificar");
            }
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
            ObtenerPuntosRuta(true);
            ObtenerRutas(true, true);
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
            ObtenerRutas(true, false);
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