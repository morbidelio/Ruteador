// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruteador.App_Code.Models;

public partial class App2_envio_ruteador : System.Web.UI.Page
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
            txt_buscarDesde.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
            ObtenerEnvio(true);
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
            //bool enviado = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "PERU_ENVIADO_RUTEADOR"));
            //if (enviado)
            //{
            //    ((LinkButton)e.Row.FindControl("btn_modificar")).Enabled = false;
            //    ((LinkButton)e.Row.FindControl("btn_eliminar")).Enabled = false;
            //    ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Row.FindControl("check")).Visible = false;
            //}
         //   ScriptManager.GetCurrent(this).RegisterPostBackControl((LinkButton)e.Row.FindControl("btn_enviar"));  
          //  PostBackTrigger pst = new PostBackTrigger();
          //  pst.ControlID = e.Row.FindControl("btn_modificar").ID.ToString();
            
         //   upd1.Triggers.Add(pst);
        }
    }
    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "enviar")
        {
            //Limpiar();
            String mimeType="";
           int id = Convert.ToInt32(e.CommandArgument);
            //PedidoBC p = new PedidoBC().ObtenerXId(peru_id);
            //LlenarDatos(p);
            //utils.AbrirModal(this, "modalEdit");
           EnvioBC envio = new EnvioBC(id);
           DataTable enviados = envio.detalle();
           envio.archivo(enviados);
           try
           {
               envio.archivo2(enviados, id);
           }
           catch (Exception ex)
           {
               utils.ShowMessage(this.Page, ex.Message, "error", true,"msg_error");
               return;
           }
           
           UtilsWeb.AddFileToZip("C:\\ViewState\\multiple.zip", "C:\\ViewState\\cliente.txt");
           UtilsWeb.AddFileToZip("C:\\ViewState\\multiple.zip", "C:\\ViewState\\pedido.txt");

           utils.ShowMessage(this.Page, "Pedidos enviados correctamente", "sucess", true);
           return;
           

           Response.Clear();
           Response.ContentType = mimeType;
           Response.AddHeader("content-disposition", "attachment; filename=" + "descarga_multiple.zip");
           Response.BinaryWrite(File.ReadAllBytes("C:\\ViewState\\multiple.zip"));
        //   File.Delete(Server.MapPath("./cargadefotos/Output.zip"));
            File.Delete("C:\\ViewState\\multiple.zip");
           
            Response.End();

           utils.ShowMessage(this.Page, "Archivos Generados Correctamente", "success", true);

        }
        if (e.CommandName == "ELIMINAR")
        {
            hf_idPeru.Value = e.CommandArgument.ToString();
            lbl_confTitulo.Text = "Eliminar Envio";
            lbl_confMensaje.Text = "Se eliminará el Envio seleccionado ¿Desea continuar?";
            utils.AbrirModal(this, "modalConf");
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
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "map", "mapa();", true);
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
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "map", "mapa();", true);
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
                //lbl_cedible.Text = string.Format("Envío rut N°{0}",id);
                Session["ID_Seleccionados_1"] = hseleccionado.Value;
                UpdatePanel1.Update();
               // utils.AbrirModal(this, "modalFOTO");
                EnvioBC envio = new EnvioBC(id);
                DataTable enviados = envio.detalle();
                envio.archivo(enviados);
                try
                {
                    envio.archivo2(enviados, id);
                }
                catch (Exception ex)
                {
                    utils.ShowMessage(this.Page, ex.Message, "error", true, "msg_error");
                    return;
                }

            }
            else
            {
                utils.ShowMessage(this, "Error", "error", false);
            }
        }
        catch(Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
            ObtenerEnvio(true);
        }
    }
    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        Limpiar();
        utils.AbrirModal(this, "modalEdit");
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "map", "mapa();", true);
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        ObtenerEnvio(true);
    }
    protected void btn_editLatLon_Click(object sender, EventArgs e)
    {
        UtilsWeb.ObtenerCoordenadas(txt_editDireccion.Text + " " , ddl_editComuna.SelectedItem.Text, txt_editLat, txt_editLon);
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
                PERU_LATITUD = Convert.ToDecimal(txt_editLat.Text),
                PERU_LONGITUD = Convert.ToDecimal(txt_editLon.Text)
            };
            p.HORA_SALIDA.HORA_ID = Convert.ToInt32(rb_editHorario.SelectedValue);
            //if (rb_editHoraAm.Checked) p.PERU_HORASALIDA = "AM";
            //else if (rb_editHoraPm.Checked) p.PERU_HORASALIDA = "PM";
            p.COMUNA.COMU_ID = Convert.ToInt32(ddl_editComuna.SelectedValue);
            p.USUARIO_PEDIDO = user;
            if (string.IsNullOrEmpty(hf_idPeru.Value))
            {
                if (p.Guardar())
                {
                    utils.ShowMessage2(this, "guardar", "success_nuevo");
                    utils.CerrarModal(this, "modalEdit");
                }
                else
                    utils.ShowMessage2(this, "guardar", "error");
            }
            else
            {
                p.PERU_ID = Convert.ToInt64(hf_idPeru.Value);

                if (p.Guardar())
                {
                    utils.ShowMessage2(this, "guardar", "success_nuevo");
                    utils.CerrarModal(this, "modalEdit");
                }
                else
                    utils.ShowMessage2(this, "guardar", "error");
            }
        }
        catch(Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", true);
        }
        finally
        {
            ObtenerEnvio(true);
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
            if (new EnvioBC().Eliminar(peru_id))
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
            btnBuscar_Click(null, null);
        }
    }
    #endregion
    #region utilsPaginas
    private void ObtenerEnvio(bool forzarBD)
    {
        if (ViewState["listar"] == null || forzarBD)
        {
            EnvioBC p = new EnvioBC();
            hseleccionado.Value = "";
            //DateTime desde = (string.IsNullOrEmpty(txt_buscarDesde.Text)) ? DateTime.MinValue : Convert.ToDateTime(txt_buscarDesde.Text);
            //DateTime hasta = (string.IsNullOrEmpty(txt_buscarHasta.Text)) ? DateTime.MinValue : Convert.ToDateTime(txt_buscarHasta.Text);
            //int regi_id = Convert.ToInt32(ddl_buscarRegion.SelectedValue);
            //int ciud_id = Convert.ToInt32(ddl_buscarCiudad.SelectedValue);
            //int comu_id = Convert.ToInt32(ddl_buscarComuna.SelectedValue);
            //int hora_id = Convert.ToInt32(ddl_buscarHorario.SelectedValue);
            //DataTable dt = p.ObtenerTodo(desde: desde
            //                            , hasta: hasta
            //                            , regi_id: regi_id
            //                            , ciud_id: ciud_id
            //                            , comu_id: comu_id
            //                            , usua_id: user.USUA_ID
            //                            , peru_numero: txt_buscarNro.Text
            //                            , hora_id: hora_id);   //guiaDespacho.ObtenerGuiasEnviarContabilidad(id_bodega, rut_cliente, fechaInicio, fechaFin, this.txtBuscarNumero.Text, this.id_operacion);
            DataTable dt = p.ObtenerTodo();
            ViewState["listar"] = dt;
        }
        DataView dw = new DataView((DataTable)ViewState["listar"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)ViewState["sortExpresion"];
        }
        gv_listar.DataSource = dw.ToTable();
        gv_listar.DataBind();
        upd1.Update();
    }
    private void CargaDrops()
    {
        DataTable dt = new RegionBC().ObtenerTodo();
        utils.CargaDrop(ddl_editRegion, "REGI_ID", "REGI_NOMBRE", dt);
        ddl_editRegion_SelectedIndexChanged(null, null);
        utils.CargaDropTodos(ddl_buscarRegion, "REGI_ID", "REGI_NOMBRE", dt);
        ddl_buscarRegion_SelectedIndexChanged(null, null);
        dt = new HorarioBC().ObtenerTodo();
        utils.CargaDropTodos(ddl_buscarHorario, "HORA_ID", "HORA_COD", dt);
        rb_editHorario.DataValueField = "HORA_ID";
        rb_editHorario.DataTextField = "HORA_COD";
        rb_editHorario.DataSource = dt;
        rb_editHorario.DataBind();
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
        //if (p.PERU_HORASALIDA == "AM") rb_editHoraAm.Checked = true;
        //else rb_editHoraPm.Checked = true;
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

    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerEnvio(false);
    }
}
