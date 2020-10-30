using Ruteador.App_Code.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Conductor : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    FuncionesGenerales funciones = new FuncionesGenerales();
    UsuarioBC user;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsCallback)
            return;

        if (Session["usuario"] == null)
        {
            Response.Redirect("~/Inicio.aspx");
        }
        user = (UsuarioBC)Session["usuario"];
        if (!IsPostBack)
        {
            ObtenerConductores(true);
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
    protected void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btn_activar = (LinkButton)e.Row.FindControl("btn_activar");
            LinkButton btn_bloquear = (LinkButton)e.Row.FindControl("btn_bloquear");
            bool activo = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "COND_ACTIVO"));
            bool bloqueado = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "COND_BLOQUEADO"));
            btn_activar.CssClass = (activo) ? "btn btn-success btn-xs" : "btn btn-danger btn-xs";
            btn_bloquear.CssClass = (!bloqueado) ? "btn btn-success btn-xs" : "btn btn-danger btn-xs";
        }
    }
    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EDITAR")
        {
            Limpiar();
            int id = Convert.ToInt32(e.CommandArgument);
            ConductorBC u = new ConductorBC().ObtenerXId(id);
            LlenarDatos(u);
            chk_editExtranjero.Enabled = false;
            txt_editRut.Enabled = false;
            utils.AbrirModal(this, "modalEdit");
        }
        if (e.CommandName == "ELIMINAR")
        {
            hf_id.Value = e.CommandArgument.ToString();
            lbl_confTitulo.Text = "Eliminar conductor";
            lbl_confMensaje.Text = "Se eliminará el conductor seleccionado ¿Está seguro?";
            btn_confActivar.Visible = false;
            btn_confBloquear.Visible = false;
            btn_confEliminar.Visible = true;
            dv_bloquear.Visible = false;
            utils.AbrirModal(this, "modalConf");
        }
        if (e.CommandName == "ACTIVAR")
        {
            gv_listar.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int cond_id = Convert.ToInt32(gv_listar.SelectedDataKey.Values["COND_ID"]);
            bool cond_activo = Convert.ToBoolean(gv_listar.SelectedDataKey.Values["COND_ACTIVO"]);
            lbl_confTitulo.Text = (cond_activo) ? "Desactivar conductor" : "Activar conductor";
            lbl_confMensaje.Text = (cond_activo) ? "Se desactivará el conductor seleccionado ¿Está seguro?" : "Se activará el conductor seleccionado ¿Está seguro?";
            btn_confActivar.Visible = true;
            btn_confBloquear.Visible = false;
            btn_confEliminar.Visible = false;
            dv_bloquear.Visible = false;
            utils.AbrirModal(this, "modalConf");
        }
        if (e.CommandName == "BLOQUEAR")
        {
            gv_listar.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int cond_id = Convert.ToInt32(gv_listar.SelectedDataKey.Values["COND_ID"]);
            bool cond_bloqueado = Convert.ToBoolean(gv_listar.SelectedDataKey.Values["COND_BLOQUEADO"]);
            lbl_confTitulo.Text = (cond_bloqueado) ? "Desbloquear conductor" : "Bloquear conductor";
            lbl_confMensaje.Text = (cond_bloqueado) ? "Se desbloqueará el conductor seleccionado ¿Está seguro?" : "Se bloqueará el conductor seleccionado ¿Está seguro?";
            btn_confActivar.Visible = false;
            btn_confBloquear.Visible = true;
            btn_confEliminar.Visible = false;
            dv_bloquear.Visible = !cond_bloqueado;
            txt_confBloquear.Text = "";
            utils.AbrirModal(this, "modalConf");
        }
    }
    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerConductores(false);
    }
    #endregion
    #region TextBox
    protected void txt_editRut_TextChanged(object sender, EventArgs e)
    {
        if (!funciones.ValidaRut(txt_editRut.Text))
        {
            utils.ShowMessage2(this, "guardar", "warn_rutInvalido");
            txt_editRut.Text = "";
            txt_editRut.Focus();
            return;
        }
        else
        {
            txt_editTelefono.Focus();
        }
        ConductorBC c = new ConductorBC().ObtenerXRut(txt_editRut.Text);
        if (c.COND_ID != 0)
        {
            utils.ShowMessage2(this, "guardar", "success_rutExiste");
            LlenarDatos(c);
            chk_editExtranjero.Enabled = false;
            txt_editRut.Enabled = false;
        }
    }
    #endregion
    #region Buttons
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        ObtenerConductores(true);
    }
    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        Limpiar();
        txt_editRut.Enabled = true;
        chk_editExtranjero.Enabled = true;
        utils.AbrirModal(this, "modalEdit");
    }
    protected void btn_editGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            ConductorBC u = new ConductorBC()
            {
                COND_RUT = txt_editRut.Text,
                COND_NOMBRE = txt_editNombre.Text,
                COND_TELEFONO = txt_editTelefono.Text,
                COND_EXTRANJERO = chk_editExtranjero.Checked
            };
            if (string.IsNullOrEmpty(hf_id.Value))
            {
                if (u.Guardar())
                {
                    utils.ShowMessage2(this, "guardar", "success_nuevo");
                    utils.CerrarModal(this, "modalEdit");
                }
                else
                    utils.ShowMessage2(this, "guardar", "error");
            }
            else
            {
                u.COND_ID = Convert.ToInt32(hf_id.Value);
                if (u.Guardar())
                {
                    utils.ShowMessage2(this, "guardar", "success_modificar");
                    utils.CerrarModal(this, "modalEdit");
                }
                else
                    utils.ShowMessage2(this, "guardar", "error");
            }
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
            ObtenerConductores(true);
        }
    }
    protected void btn_confEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            int cond_id = Convert.ToInt32(hf_id.Value);
            if (new ConductorBC().Eliminar(cond_id))
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
            ObtenerConductores(true);
        }
    }
    protected void btn_confActivar_Click(object sender, EventArgs e)
    {
        try
        {
            int cond_id = Convert.ToInt32(gv_listar.SelectedDataKey.Values["COND_ID"]);
            bool cond_activo = Convert.ToBoolean(gv_listar.SelectedDataKey.Values["COND_ACTIVO"]);
            if (new ConductorBC().Activar(cond_id))
            {
                utils.ShowMessage2(this, "activar", (!cond_activo) ? "success_activar" : "success_desactivar");
                utils.CerrarModal(this, "modalConf");
            }
            else
                utils.ShowMessage2(this, "activar", "error");
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
            ObtenerConductores(true);
        }
    }
    protected void btn_confBloquear_Click(object sender, EventArgs e)
    {
        try
        {
            int cond_id = Convert.ToInt32(gv_listar.SelectedDataKey.Values["COND_ID"]);
            bool bloqueado = Convert.ToBoolean(gv_listar.SelectedDataKey.Values["COND_BLOQUEADO"]);
            if (new ConductorBC().Bloquear(cond_id))
            {
                utils.ShowMessage2(this, "bloquear", (bloqueado) ? "success_desbloquear" : "success_bloquear");
                utils.CerrarModal(this, "modalConf");
            }
            else
                utils.ShowMessage2(this, "bloquear", "error");
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
            ObtenerConductores(true);
        }
    }
    #endregion
    #region UtilsPagina
    private void Limpiar()
    {
        txt_editRut.Text = "";
        txt_editNombre.Text = "";
        txt_editTelefono.Text = "";
        chk_editExtranjero.Checked = false;
        hf_id.Value = "";
    }
    private void ObtenerConductores(bool forzarBD)
    {
        if (ViewState["listar"] == null || forzarBD)
        {
            ViewState["listar"] = new ConductorBC().ObtenerTodo(
                cond_rut: txt_buscarRut.Text,
                cond_nombre: txt_buscarNombre.Text);
        }
        DataView dw = new DataView((DataTable)ViewState["listar"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)ViewState["sortExpresion"];
        }
        gv_listar.DataSource = dw;
        gv_listar.DataBind();
    }
    private void LlenarDatos(ConductorBC c)
    {
        hf_id.Value = c.COND_ID.ToString();
        txt_editRut.Text = c.COND_RUT;
        txt_editTelefono.Text = c.COND_TELEFONO;
        txt_editNombre.Text = c.COND_NOMBRE;
        chk_editExtranjero.Checked = c.COND_EXTRANJERO;
    }
    #endregion
}