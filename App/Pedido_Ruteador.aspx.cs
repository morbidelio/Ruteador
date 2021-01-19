// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruteador.App_Code.Models;
using Telerik.Web.UI;

public partial class App2_envio_pedido : System.Web.UI.Page
{
    private UtilsWeb utils = new UtilsWeb();
    UsuarioBC user;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsCallback)
            return;

        if (Session["Usuario"] == null)
        {
            Response.Redirect("~/Inicio.aspx");
        }
        user = (UsuarioBC)Session["Usuario"];

        if (!this.IsPostBack)
        {
            CargaDrops();
            txt_buscarHasta.Text = DateTime.Now.ToShortDateString();
            txt_buscarDesde.Text = DateTime.Now.AddDays(-1).ToShortDateString();
            ObtenerPedidos(true);
        }
    }
    #region GridView
    protected void gv_listar_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            e.Row.TableSection = TableRowSection.TableFooter;
        }
    }
    protected void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            bool enviado = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "PERU_ENVIADO_RUTEADOR"));

            int propuesta_ruta = Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "propuesta_ruta"));
            if (enviado || propuesta_ruta > 0)
            {
                ((LinkButton)e.Row.FindControl("btn_modificar")).Enabled = false;
                ((LinkButton)e.Row.FindControl("btn_eliminar")).Enabled = false;
                ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Row.FindControl("check")).Visible = false;
            }

            //if (propuesta_ruta > 0)
            //{
            //    ((LinkButton)e.Row.FindControl("btn_modificar")).Enabled = false;
            //    ((LinkButton)e.Row.FindControl("btn_eliminar")).Enabled = false;
            //    ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Row.FindControl("check")).Visible = false;
            //}
        }
    }
    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EDITAR")
        {
            Limpiar();
            int peru_id = Convert.ToInt32(e.CommandArgument);
            PedidoBC p = new PedidoBC().ObtenerXId(peru_id);
            LlenarDatos(p);
            up_modalEdit.Update();
            //utils.AbrirModal(this, "modalEdit");

        }
        if (e.CommandName == "ELIMINAR")
        {
            hf_idPeru.Value = e.CommandArgument.ToString();
            lbl_confTitulo.Text = "Eliminar Pedido";
            lbl_confMensaje.Text = "Se eliminará el pedido seleccionado ¿Desea continuar?";
            utils.AbrirModal(this, "modalConf");
        }
    }
    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerPedidos(false);
    }
    protected void gv_detalle_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            e.Row.TableSection = TableRowSection.TableFooter;
        }
    }
    protected void gv_detalle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EDITAR")
        {
            LimpiarDetalle();
            int index = Convert.ToInt32(e.CommandArgument);
            gv_detalle.SelectedIndex = index;
            DataTable dt = (DataTable)ViewState["detalle"];
            DataRow dr = dt.Rows[index];
            txt_detalleCod.Text = Convert.ToString(dr["CODIGO_PRODUCTO"]);
            txt_detalleDesc.Text = Convert.ToString(dr["PEDE_DESC_PRODUCTO"]);
            txt_detalleCodCl.Text = Convert.ToString(dr["CODIGO_CLIENTE"]);
            txt_detalleDireccionCl.Text = Convert.ToString(dr["DIRECCION_CLIENTE"]);
            txt_detalleNomCl.Text = Convert.ToString(dr["NOMBRE_CLIENTE"]);
            ddl_detalleRegionCl.SelectedValue = Convert.ToString(dr["REGI_ID_CLIENTE"]);
            ddl_detalleRegionCl_SelectedIndexChanged(null, null);
            ddl_detalleCiudadCl.SelectedValue = Convert.ToString(dr["CIUD_ID_CLIENTE"]);
            ddl_detalleCiudadCl_SelectedIndexChanged(null, null);
            ddl_detalleComunaCl.SelectedValue = Convert.ToString(dr["ID_COMUNA_CLIENTE"]);
            txt_detalleNro.Text = Convert.ToString(dr["NUMERO_GUIA"]);
            txt_detallePeso.Text = Convert.ToString(dr["PESO_PEDIDO"]);
            txt_detalleCant.Text = Convert.ToString(dr["PEDE_CANTIDAD"]);
            //utils.AbrirModal(this, "modalDetalle");
        }
        if (e.CommandName == "ELIMINAR")
        {
            gv_detalle.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            if (!string.IsNullOrEmpty(hf_idPeru.Value))
            {
                try
                {
                    long pede_id = Convert.ToInt64(gv_detalle.SelectedDataKey.Value);
                    new PedidoDetalleBC().Eliminar(pede_id);
                }
                catch (Exception ex)
                {
                    utils.ShowMessage(this, ex.Message, "error", false);
                }
                finally
                {
                    ObtenerDetalle(true);
                }
            }
            else
            {
                DataTable dt = (DataTable)ViewState["detalle"];
                dt.Rows.RemoveAt(gv_detalle.SelectedIndex);
                ObtenerDetalle(false);
            }
        }
    }
    #endregion
    #region DropDownList
    protected void ddl_editRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_editRegion.SelectedIndex > 0)
        {
            int regi_id = Convert.ToInt32(ddl_editRegion.SelectedValue);
            DataTable dt = new CiudadBC().ObtenerTodo(regi_id);
            utils.CargaDrop(ddl_editCiudad, "CIUD_ID", "CIUD_NOMBRE", dt);
            ddl_editCiudad.Enabled = true;
            ddl_editCiudad_SelectedIndexChanged(null, null);
        }
        else
        {
            ddl_editCiudad.ClearSelection();
            ddl_editCiudad.Enabled = false;
            ddl_editComuna.ClearSelection();
            ddl_editComuna.Enabled = false;
        }
        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "map", "mapa();", true);
    }
    protected void ddl_editCiudad_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_editCiudad.SelectedIndex > 0)
        {
            int ciud_id = Convert.ToInt32(ddl_editCiudad.SelectedValue);
            DataTable dt = new ComunaBC().ObtenerTodo(ciud_id: ciud_id);
            utils.CargaDrop(ddl_editComuna, "COMU_ID", "COMU_NOMBRE", dt);
            ddl_editComuna.Enabled = true;
        }
        else
        {
            ddl_editComuna.ClearSelection();
            ddl_editComuna.Enabled = false;
        }
        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "map", "mapa();", true);
    }
    protected void ddl_buscarRegion_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        int regi_id = Convert.ToInt32(ddl_buscarRegion.SelectedValue);
        DataTable dt = new CiudadBC().ObtenerTodo(regi_id);
        utils.CargaDropTodos(ddl_buscarCiudad, "CIUD_ID", "CIUD_NOMBRE", dt);
        ddl_buscarCiudad_SelectedIndexChanged(null, null);
    }
    protected void ddl_buscarCiudad_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        int regi_id = Convert.ToInt32(ddl_buscarRegion.SelectedValue);
        int ciud_id = Convert.ToInt32(ddl_buscarCiudad.SelectedValue);
        DataTable dt = new ComunaBC().ObtenerTodo(regi_id, ciud_id);
        utils.CargaDropNormal(ddl_buscarComuna, "COMU_ID", "COMU_NOMBRE", dt);
    }
    protected void ddl_detalleRegionCl_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_detalleRegionCl.SelectedIndex > 0)
        {
            int regi_id = Convert.ToInt32(ddl_detalleRegionCl.SelectedValue);
            DataTable dt = new CiudadBC().ObtenerTodo(regi_id);
            utils.CargaDrop(ddl_detalleCiudadCl, "CIUD_ID", "CIUD_NOMBRE", dt);
            ddl_detalleCiudadCl_SelectedIndexChanged(null, null);
            ddl_detalleCiudadCl.Enabled = true;
        }
        else
        {
            ddl_detalleCiudadCl.ClearSelection();
            ddl_detalleCiudadCl.Enabled = false;

            ddl_detalleComunaCl.ClearSelection();
            ddl_detalleComunaCl.Enabled = false;
        }
    }
    protected void ddl_detalleCiudadCl_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_detalleCiudadCl.SelectedIndex > 0)
        {
            int ciud_id = Convert.ToInt32(ddl_detalleCiudadCl.SelectedValue);
            DataTable dt = new ComunaBC().ObtenerTodo(ciud_id: ciud_id);
            utils.CargaDrop(ddl_detalleComunaCl, "COMU_ID", "COMU_NOMBRE", dt);
            ddl_detalleComunaCl.Enabled = true;
        }
        else
        {
            ddl_detalleComunaCl.ClearSelection();
            ddl_detalleComunaCl.Enabled = false;
        }
    }
    #endregion
    #region boton
    protected void btncorreo_Click(object sender, EventArgs e)
    {
        utils.ShowMessage(this, "Cedible enviada.", "success", true);
        utils.CerrarModal(this, "modalFOTO");
    }
    protected void btnenviar_Click(object sender, EventArgs e)
    {
        PedidoBC gd = new PedidoBC();
        int id = 0;
        try
        {
            if (gd.CrearEnvio(hseleccionado.Value.ToString(), user.USUA_ID, out id))
            {
                hf_idEnvio.Value = id.ToString();
                lbl_cedible.Text = string.Format("Envío cedible N°{0}", id);
                Session["ID_Seleccionados_1"] = hseleccionado.Value;
                UpdatePanel1.Update();
                // utils.AbrirModal(this, "modalFOTO");
                //  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "zip_script", "zip1();", true);
                btnzip_Click(null, null);
            }
            else
            {
                utils.ShowMessage(this, "Error", "error", false);
            }
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
            ObtenerPedidos(true);
        }
    }
    protected void btnzip_Click(object sender, EventArgs e)
    {
        int id = int.Parse(hf_idEnvio.Value);

        EnvioBC envio = new EnvioBC(id);
        DataTable enviados = envio.detalle();
        String mimeType = "";
        envio.archivo(enviados);
        try
        {
            envio.archivo2(enviados, id);
            utils.ShowMessage(this, "Pedidos enviados correctamente", "success", true);
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this.Page, ex.Message, "error", true, "msg_error");
            return;
        }



        return;


        UtilsWeb.AddFileToZip("C:\\ViewState\\multiple.zip", "C:\\ViewState\\cliente.txt");
        UtilsWeb.AddFileToZip("C:\\ViewState\\multiple.zip", "C:\\ViewState\\pedido.txt");

        Response.Clear();
        Response.ContentType = mimeType;
        Response.AddHeader("content-disposition", "attachment; filename=" + "descarga_multiple.zip");
        Response.BinaryWrite(File.ReadAllBytes("C:\\ViewState\\multiple.zip"));
        //   File.Delete(Server.MapPath("./cargadefotos/Output.zip"));
        File.Delete("C:\\ViewState\\multiple.zip");

        Response.End();


    }
    protected void btn_Agendar_Click(object sender, EventArgs e)
    {
        this.txt_fechaAg.Text = DateTime.Now.ToShortDateString();
        this.ddl_edithorario.SelectedIndex = 0;
        utils.AbrirModal(this, "modalFechaAg");
    }
    protected void btn_AgendarMasivo_Click(object sender, EventArgs e)
    {
        DateTime desde, hasta;
        if (ddl_edithorario.SelectedValue != "0")
            desde = Convert.ToDateTime(string.Format("{0} {1}", this.txt_fechaAg.Text, this.ddl_edithorario.SelectedItem.Text));
        else
            desde = Convert.ToDateTime(string.Format("{0}", this.txt_fechaAg.Text));

        PedidoBC pedido = new PedidoBC();

        if (pedido.ModificarFechaAgendamiento(hseleccionado.Value, desde, int.Parse(ddl_edithorario.SelectedValue)))
        {
            utils.ShowMessage(this, "Fecha Modificada correctamente.", "success", true);
            utils.CerrarModal(this, "modalFechaAg");
        }
        else
        {
            utils.ShowMessage(this, "Ocurrió un error al agendar. Revise los datos.", "error", false);
        }
        this.btnBuscar_Click(null, null);
    }
    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        Limpiar();
        ObtenerDetalle(false);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "map", "mapa();", true);
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        ObtenerPedidos(true);
    }
    protected void btn_editLatLon_Click(object sender, EventArgs e)
    {
        UtilsWeb.ObtenerCoordenadas(txt_editDireccion.Text + " ", ddl_editComuna.SelectedItem.Text, txt_editLat, txt_editLon);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "map", "mapa();", true);
    }
    protected void btn_editGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            PedidoBC p = new PedidoBC()
            {
                PERU_NUMERO = txt_editNumero.Text,
                PERU_CODIGO = txt_editCodigo.Text,
                PERU_FECHA = Convert.ToDateTime(txt_editFecha.Text),
                PERU_PESO = txt_editPeso.Text,
                PERU_TIEMPO = txt_editTiempo.Text,
                PERU_DIRECCION = txt_editDireccion.Text,
                PERU_LATITUD = Convert.ToDecimal(txt_editLat.Text.Replace(".", ",")),
                PERU_LONGITUD = Convert.ToDecimal(txt_editLon.Text.Replace(".", ","))
            };
            p.HORA_SALIDA.HORA_ID = Convert.ToInt32(rb_editHorario.SelectedValue);
            p.COMUNA.COMU_ID = Convert.ToInt32(ddl_editComuna.SelectedValue);
            p.USUARIO_PEDIDO = user;
            if (string.IsNullOrEmpty(hf_idPeru.Value))
            {
                DataTable dt = ((DataTable)ViewState["detalle"]).DefaultView.ToTable(false
                , "PEDE_ID"
                , "PERU_ID"
                , "CODIGO_PRODUCTO"
                , "PEDE_DESC_PRODUCTO"
                , "CODIGO_CLIENTE"
                , "DIRECCION_CLIENTE"
                , "NOMBRE_CLIENTE"
                , "ID_COMUNA_CLIENTE"
                , "NUMERO_GUIA"
                , "PESO_PEDIDO"
                , "PEDE_CANTIDAD");
                p.Guardar(dt);
                utils.ShowMessage2(this, "guardar", "success_nuevo");
                utils.CerrarModal(this, "modalEdit");
            }
            else
            {
                p.PERU_ID = Convert.ToInt64(hf_idPeru.Value);
                p.Guardar();
                ObtenerDetalle(true);
                utils.ShowMessage2(this, "guardar", "success_modificar");
            }
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", true);
        }
        finally
        {
            ObtenerPedidos(true);
        }
    }
    protected void btn_recarga_Click(object sender, EventArgs e)
    {
        lbl_reload.Text = "Se recargó la página a las " + DateTime.Now.ToShortTimeString();
    }
    protected void btn_confEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            int peru_id = Convert.ToInt32(hf_idPeru.Value);
            if (new PedidoBC().Eliminar(peru_id))
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
            ObtenerPedidos(true);
        }
    }
    protected void btn_detalleGuardar_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(hf_idPeru.Value))
        {
            try
            {
                PedidoDetalleBC dp = new PedidoDetalleBC()
                {
                    CODIGO_PRODUCTO = txt_detalleCod.Text,
                    PEDE_DESC_PRODUCTO = txt_detalleDesc.Text,
                    CODIGO_CLIENTE = txt_detalleCodCl.Text,
                    DIRECCION_CLIENTE = txt_detalleDireccionCl.Text,
                    NOMBRE_CLIENTE = txt_detalleNomCl.Text,
                    NUMERO_GUIA = txt_detalleNro.Text,
                    PESO_PEDIDO = Convert.ToDecimal(txt_detallePeso.Text),
                    PEDE_CANTIDAD = Convert.ToDecimal(txt_detalleCant.Text)
                };
                if (gv_detalle.SelectedIndex != -1)
                {
                    dp.PEDE_ID = Convert.ToInt64(gv_detalle.SelectedDataKey.Value);
                }
                dp.PEDIDO.PERU_ID = Convert.ToInt64(hf_idPeru.Value);
                dp.COMUNA_CLIENTE.COMU_ID = Convert.ToInt32(ddl_detalleComunaCl.SelectedValue);
                dp.Guardar();
            }
            catch(Exception ex)
            {
                utils.ShowMessage(this, ex.Message, "error", false);
            }
            finally
            {
                utils.CerrarModal(this, "modalDetalle");
                ObtenerDetalle(true);
            }
        }
        else
        {
            DataTable dt = (DataTable)ViewState["detalle"];
            DataRow dr;
            if (gv_detalle.SelectedIndex == -1)
            {
                dr = dt.NewRow();
            }
            else
            {
                dr = dt.Rows[gv_detalle.SelectedIndex];
            }
            //if (!string.IsNullOrEmpty(hf_idPede.Value))
            //{
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        if (row["PEDE_ID"].ToString() == hf_idPede.Value)
            //        {
            //            dr = row;
            //            break;
            //        }
            //    }
            //}
            //dr["PERU_ID"] = Convert.ToInt64(hf_idPeru.Value);
            dr["CODIGO_PRODUCTO"] = txt_detalleCod.Text;
            dr["PEDE_DESC_PRODUCTO"] = txt_detalleDesc.Text;
            dr["CODIGO_CLIENTE"] = txt_detalleCodCl.Text;
            dr["DIRECCION_CLIENTE"] = txt_detalleDireccionCl.Text;
            dr["NOMBRE_CLIENTE"] = txt_detalleNomCl.Text;
            dr["ID_COMUNA_CLIENTE"] = Convert.ToInt32(ddl_detalleComunaCl.SelectedValue);
            dr["COMUNA_CLIENTE"] = ddl_detalleComunaCl.SelectedItem.Text;
            dr["CIUD_ID_CLIENTE"] = Convert.ToInt32(ddl_detalleCiudadCl.SelectedValue);
            dr["CIUD_NOMBRE_CLIENTE"] = ddl_detalleCiudadCl.SelectedItem.Text;
            dr["REGI_ID_CLIENTE"] = Convert.ToInt32(ddl_detalleRegionCl.SelectedValue);
            dr["REGI_NOMBRE_CLIENTE"] = ddl_detalleRegionCl.SelectedItem.Text;
            dr["NUMERO_GUIA"] = txt_detalleNro.Text;
            dr["PESO_PEDIDO"] = Convert.ToDecimal(txt_detallePeso.Text);
            dr["PEDE_CANTIDAD"] = Convert.ToDecimal(txt_detalleCant.Text);
            if (gv_detalle.SelectedIndex == -1)
            {
                dt.Rows.Add(dr);
            }
            ObtenerDetalle(false);
            utils.CerrarModal(this, "modalDetalle");
        }
    }
    protected void btn_editDetalle_Click(object sender, EventArgs e)
    {
        LimpiarDetalle();
        //utils.AbrirModal(this, "modalDetalle");
    }
    #endregion
    #region utilsPaginas
    private void ObtenerPedidos(bool forzarBD)
    {
        if (ViewState["listar"] == null || forzarBD)
        {
            hseleccionado.Value = "";
            DateTime desde = (string.IsNullOrEmpty(txt_buscarDesde.Text)) ? DateTime.MinValue : Convert.ToDateTime(txt_buscarDesde.Text);
            DateTime hasta = (string.IsNullOrEmpty(txt_buscarHasta.Text)) ? DateTime.MinValue : Convert.ToDateTime(txt_buscarHasta.Text);
            int regi_id = Convert.ToInt32(ddl_buscarRegion.SelectedValue);
            int ciud_id = Convert.ToInt32(ddl_buscarCiudad.SelectedValue);
            StringBuilder comu_id = new StringBuilder();
            foreach (RadComboBoxItem item in ddl_buscarComuna.CheckedItems)
            {
                if (comu_id.Length > 0)
                    comu_id.Append(',');
                comu_id.Append(item.Value);
            }
            int hora_id = Convert.ToInt32(ddl_buscarHorario.SelectedValue);
            ViewState["listar"] = new PedidoBC().ObtenerTodo(desde: desde
                                        , hasta: hasta
                                        , hora_id: hora_id
                                        , regi_id: regi_id
                                        , ciud_id: ciud_id
                                        , comu_id: comu_id.ToString()
                                        , usua_id: user.USUA_ID
                                        , peru_numero: txt_buscarNro.Text);
        }
        DataView dw = new DataView((DataTable)ViewState["listar"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)ViewState["sortExpresion"];
        }
        gv_listar.DataSource = dw.ToTable();
        gv_listar.DataBind();
        utils.TableSPag(this, gv_listar);
    }
    private void ObtenerDetalle(bool forzarBD)
    {
        if (ViewState["detalle"] == null || forzarBD)
        {
            long peru_id = Convert.ToInt64(hf_idPeru.Value);
            ViewState["detalle"] = new PedidoDetalleBC().ObtenerTodo(peru_id: peru_id);
        }
        gv_detalle.DataSource = ViewState["detalle"];
        gv_detalle.DataBind();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "dibujaTable", "drawTableDetalles();", true);
    }
    private void CargaDrops()
    {
        DataTable dt = new RegionBC().ObtenerTodo();
        utils.CargaDrop(ddl_editRegion, "REGI_ID", "REGI_NOMBRE", dt);
        ddl_editRegion_SelectedIndexChanged(null, null);
        utils.CargaDropTodos(ddl_buscarRegion, "REGI_ID", "REGI_NOMBRE", dt);
        ddl_buscarRegion_SelectedIndexChanged(null, null);
        utils.CargaDrop(ddl_detalleRegionCl, "REGI_ID", "REGI_NOMBRE", dt);
        ddl_detalleRegionCl_SelectedIndexChanged(null, null);
        dt = new HorarioBC().ObtenerTodo();
        utils.CargaDropNormal(ddl_buscarHorario, "HORA_ID", "HORA_COD", dt);
        utils.CargaDropNormal(ddl_edithorario, "HORA_ID", "HORA_COD", dt);
        ddl_edithorario.Items.Insert(0, new ListItem("No Cambiar", "0"));
        rb_editHorario.DataValueField = "HORA_ID";
        rb_editHorario.DataTextField = "HORA_COD";
        rb_editHorario.DataSource = dt;
        rb_editHorario.DataBind();
    }
    private void LimpiarDetalle()
    {
        gv_detalle.SelectedIndex = -1;
        txt_detalleCodCl.Text = "";
        txt_detalleNomCl.Text = "";
        ddl_detalleRegionCl.ClearSelection();
        ddl_detalleRegionCl_SelectedIndexChanged(null, null);
        txt_detalleDireccionCl.Text = "";
        txt_detalleNro.Text = "";
        txt_detalleCod.Text = "";
        txt_detalleDesc.Text = "";
        txt_detalleCant.Text = "";
        txt_detallePeso.Text = ""; 
    }
    private void Limpiar()
    {
        hf_idPeru.Value = "";
        txt_editNumero.Text = "";
        txt_editCodigo.Text = "";
        txt_editFecha.Text = "";
        txt_editPeso.Text = "";
        txt_editTiempo.Text = "";
        txt_editDireccion.Text = "";
        txt_editLat.Text = "";
        txt_editLon.Text = "";
        //rb_editHoraAm.Checked = false;
        //rb_editHoraPm.Checked = false;
        rb_editHorario.ClearSelection();
        ddl_editRegion.ClearSelection();
        ddl_editRegion_SelectedIndexChanged(null, null);
        DataTable dt = new DataTable();
        dt.Columns.Add("PEDE_ID");
        dt.Columns.Add("PERU_ID");
        dt.Columns.Add("PERU_NUMERO");
        dt.Columns.Add("PERU_NOMBRE");
        dt.Columns.Add("CODIGO_PRODUCTO");
        dt.Columns.Add("PEDE_DESC_PRODUCTO");
        dt.Columns.Add("CODIGO_CLIENTE");
        dt.Columns.Add("DIRECCION_CLIENTE");
        dt.Columns.Add("NOMBRE_CLIENTE");
        dt.Columns.Add("COMUNA_CLIENTE");
        dt.Columns.Add("ID_COMUNA_CLIENTE");
        dt.Columns.Add("CIUD_ID_CLIENTE");
        dt.Columns.Add("CIUD_NOMBRE_CLIENTE");
        dt.Columns.Add("REGI_ID_CLIENTE");
        dt.Columns.Add("REGI_NOMBRE_CLIENTE");
        dt.Columns.Add("NUMERO_GUIA");
        dt.Columns.Add("PESO_PEDIDO");
        dt.Columns.Add("PEDE_CANTIDAD");
        ViewState["detalle"] = dt;
    }
    private void LlenarDatos(PedidoBC p)
    {
        hf_idPeru.Value = p.PERU_ID.ToString();
        txt_editNumero.Text = p.PERU_NUMERO;
        txt_editCodigo.Text = p.PERU_CODIGO;
        txt_editFecha.Text = p.PERU_FECHA.ToShortDateString();
        txt_editPeso.Text = p.PERU_PESO;
        txt_editTiempo.Text = p.PERU_TIEMPO;
        txt_editDireccion.Text = p.PERU_DIRECCION;
        ddl_editRegion.SelectedValue = p.COMUNA.CIUDAD.REGION.REGI_ID.ToString();
        ddl_editRegion_SelectedIndexChanged(null, null);
        ddl_editCiudad.SelectedValue = p.COMUNA.CIUDAD.CIUD_ID.ToString();
        ddl_editCiudad_SelectedIndexChanged(null, null);
        ddl_editComuna.SelectedValue = p.COMUNA.COMU_ID.ToString();
        txt_editLat.Text = p.PERU_LATITUD.ToString();
        txt_editLon.Text = p.PERU_LONGITUD.ToString();
        rb_editHorario.SelectedValue = p.HORA_SALIDA.HORA_ID.ToString();

        int detalle = int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["usa_Detalle"]);
        if (detalle == 1)
        {
            ObtenerDetalle(true);
            gv_detalle.Visible = true;
            btn_editDetalle.Visible = true;
        }
        else
        {
            gv_detalle.Visible = false;
            btn_editDetalle.Visible = false;
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "map", "mapa();", true);
    }
    #endregion
    // viewstate
    protected override void SavePageStateToPersistenceMedium(object state)
    {
        string file = this.GenerateFileName();

        FileStream filestream = new FileStream(file, FileMode.Create);

        LosFormatter formator = new LosFormatter();

        formator.Serialize(filestream, state);

        filestream.Flush();

        filestream.Close();

        filestream = null;
    }
    protected override object LoadPageStateFromPersistenceMedium()
    {
        object state = null;
        try
        {
            StreamReader reader = new StreamReader(this.GenerateFileName());

            LosFormatter formator = new LosFormatter();

            state = formator.Deserialize(reader);

            reader.Close();
        }
        catch (Exception)
        {
            Response.Redirect(string.Format("{0}.aspx", Path.GetFileNameWithoutExtension(this.Page.AppRelativeVirtualPath)));
        }
        return state;
    }
    private string GenerateFileName()
    {
        string pageName = Path.GetFileNameWithoutExtension(this.Page.AppRelativeVirtualPath);

        string file = string.Format("{0}{1}.txt", pageName, Session.SessionID.ToString());

        //       file = Path.Combine(Server.MapPath("~/ViewStateFiles") + "/" + file);  
        file = string.Format("{0}\\{1}", this.utils.pathviewstate(), file);

        return file;
    }

}
