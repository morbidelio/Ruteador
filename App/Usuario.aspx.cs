using Ruteador.App_Code.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Usuario : System.Web.UI.Page
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
            CargaDrops();
            CargaChecks();
            ObtenerUsuarios(true);
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
            LinkButton btn_modificar = (LinkButton)e.Row.FindControl("btn_modificar");
            LinkButton btn_eliminar = (LinkButton)e.Row.FindControl("btn_eliminar");
            int usua_id = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "USUA_ID"));
            int nivel_permisos = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "USTI_NIVEL_PERMISOS"));
            bool estado = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "USUA_ESTADO"));
            btn_activar.CssClass = (estado) ? "btn btn-success btn-xs" : "btn btn-danger btn-xs";
            if (usua_id == user.USUA_ID)
            {
                btn_activar.Enabled = false;
                btn_eliminar.Enabled = false;
            }
            if (nivel_permisos < user.USUARIO_TIPO.USTI_NIVEL_PERMISOS)
            {
                btn_activar.Style.Add("visibility", "hidden");
                btn_modificar.Style.Add("visibility","hidden");
                btn_eliminar.Style.Add("visibility","hidden");
            }
        }
    }
    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EDITAR")
        {
            Limpiar();
            int id = Convert.ToInt32(e.CommandArgument);
            UsuarioBC u = new UsuarioBC().ObtenerXId(id);
            LlenarDatos(u);
            txt_editRut.Enabled = false;
            utils.AbrirModal(this, "modalEdit");
        }
        if (e.CommandName == "ELIMINAR")
        {
            hf_id.Value = e.CommandArgument.ToString();
            lbl_confTitulo.Text = "Eliminar usuario";
            lbl_confMensaje.Text = "Se eliminará el usuario seleccionado ¿Está seguro?";
            btn_confActivar.Visible = false;
            btn_confEliminar.Visible = true;
            utils.AbrirModal(this, "modalConf");
        }
        if (e.CommandName == "ACTIVAR")
        {
            gv_listar.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int usua_id = Convert.ToInt32(gv_listar.SelectedDataKey.Values["USUA_ID"]);
            bool usua_estado = Convert.ToBoolean(gv_listar.SelectedDataKey.Values["USUA_ESTADO"]);
            lbl_confTitulo.Text = (usua_estado) ? "Desactivar usuario" : "Activar usuario";
            lbl_confMensaje.Text = (usua_estado) ? "Se desactivará el usuario seleccionado ¿Está seguro?" : "Se activará el usuario seleccionado ¿Está seguro?";
            btn_confActivar.Visible = true;
            btn_confEliminar.Visible = false;
            utils.AbrirModal(this, "modalConf");
        }
    }
    #endregion
    #region TextBox
    protected void txt_editRut_TextChanged(object sender, EventArgs e)
    {
        if (!funciones.ValidaRut(txt_editRut.Text))
        {
            utils.ShowMessage2(this, "rut", "warn_invalido");
            txt_editRut.Text = "";
            return;
        }
        UsuarioBC u = new UsuarioBC().ObtenerXRut(txt_editRut.Text);
        if (u.USUA_ID != 0)
        {
            if (u.USUARIO_TIPO.USTI_NIVEL_PERMISOS < user.USUARIO_TIPO.USTI_NIVEL_PERMISOS)
            {
                utils.ShowMessage2(this, "rut", "warn_existe");
                Limpiar();
            }
            else
            {
                utils.ShowMessage2(this, "rut", "success_existe");
                LlenarDatos(u);
                txt_editRut.Enabled = false;
            }
        }
    }
    #endregion
    #region Buttons
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        ObtenerUsuarios(true);
    }
    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        Limpiar();
        txt_editRut.Enabled = true;
        utils.AbrirModal(this, "modalEdit");
    }
    protected void btn_editGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            UsuarioBC u = new UsuarioBC()
            {
                USUA_RUT = txt_editRut.Text,
                USUA_NOMBRE = txt_editNombre.Text,
                USUA_APELLIDO = txt_editApellido.Text,
                USUA_USERNAME = txt_editUsername.Text,
                USUA_PASSWORD = txt_editPassword.Text,
                USUA_OBSERVACION = txt_editObservacion.Text,
                USUA_CORREO = txt_editCorreo.Text,
                OPER_ID = ""
            };
            u.USUARIO_TIPO.USTI_ID = Convert.ToInt32(ddl_editTipo.Text);
            foreach (ListItem i in chklst_editOp.Items)
            {
                if (i.Selected)
                {
                    if (!string.IsNullOrEmpty(u.OPER_ID)) u.OPER_ID += ",";
                    u.OPER_ID += i.Value;
                }
            }
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
                u.USUA_ID = Convert.ToInt32(hf_id.Value);
                if (u.Guardar())
                    utils.ShowMessage2(this, "guardar", "success_modificar");
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
            ObtenerUsuarios(true);
        }
    }
    protected void btn_editGenerar_Click(object sender, EventArgs e)
    {
        txt_editPassword.Text = funciones.generarPassword(6);
    }
    protected void btn_confEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            int usua_id = Convert.ToInt32(hf_id.Value);
            if (new UsuarioBC().Eliminar(usua_id))
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
            ObtenerUsuarios(true);
        }
    }
    protected void btn_confActivar_Click(object sender, EventArgs e)
    {
        try
        {
            int usua_id = Convert.ToInt32(gv_listar.SelectedDataKey.Values["USUA_ID"]);
            bool usua_estado = Convert.ToBoolean(gv_listar.SelectedDataKey.Values["USUA_ESTADO"]);
            if (new UsuarioBC().Activar(usua_id))
            {
                utils.ShowMessage2(this, "activar", (!usua_estado) ? "success_activar" : "success_desactivar");
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
            ObtenerUsuarios(true);
        }
    }
    #endregion
    #region UtilsPagina
    private void CargaDrops()
    {
        DataTable dt;
        dt = new UsuarioTipoBC().ObtenerTodo();
        utils.CargaDropTodos(ddl_buscarTipo, "USTI_ID", "USTI_NOMBRE", dt);
        utils.CargaDrop(ddl_editTipo, "USTI_ID", "USTI_NOMBRE", dt);
    }
    private void CargaChecks()
    {
        DataTable dt;
        dt = new OperacionBC().ObtenerTodo();
        chklst_editOp.DataSource = dt;
        chklst_editOp.DataValueField = "OPER_ID";
        chklst_editOp.DataTextField = "OPER_NOMBRE";
        chklst_editOp.DataBind();
    }
    private void Limpiar()
    {
        txt_editRut.Text = "";
        txt_editNombre.Text = "";
        txt_editApellido.Text = "";
        txt_editCorreo.Text = "";
        txt_editPassword.Text = "";
        txt_editObservacion.Text = "";
        txt_editUsername.Text = "";
        ddl_editTipo.ClearSelection();
        chklst_editOp.ClearSelection();
        hf_id.Value = "";
    }
    private void ObtenerUsuarios(bool forzarBD)
    {
        if (ViewState["listar"] == null || forzarBD)
        {
            int usti_id = Convert.ToInt32(ddl_buscarTipo.SelectedValue);
            ViewState["listar"] = new UsuarioBC().ObtenerTodo(
                usti_id: usti_id,
                usua_rut: txt_buscarRut.Text,
                usua_nombre: txt_buscarNombre.Text,
                usua_apellido: txt_buscarApellido.Text,
                usua_username: txt_buscarUsername.Text);
        }
        DataView dw = new DataView((DataTable)ViewState["listar"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)ViewState["sortExpresion"];
        }
        gv_listar.DataSource = dw;
        gv_listar.DataBind();
    }
    private void LlenarDatos(UsuarioBC u)
    {
        hf_id.Value = u.USUA_ID.ToString();
        txt_editRut.Text = u.USUA_RUT;
        txt_editCorreo.Text = u.USUA_CORREO;
        txt_editNombre.Text = u.USUA_NOMBRE;
        txt_editApellido.Text = u.USUA_APELLIDO;
        txt_editUsername.Text = u.USUA_USERNAME;
        txt_editPassword.Text = u.USUA_PASSWORD;
        txt_editObservacion.Text = u.USUA_OBSERVACION;
        ddl_editTipo.SelectedValue = u.USUARIO_TIPO.USTI_ID.ToString();
        foreach (OperacionBC o in u.OPERACION)
        {
            chklst_editOp.Items.FindByValue(o.OPER_ID.ToString()).Selected = true;
        }
    }
    #endregion

    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerUsuarios(false);
    }
}