using Newtonsoft.Json;
using Ruteador.App_Code.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Perfil : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
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
            DataSet ds = new MenuBC().ObtenerTodo();
            hf_json.Value = JsonConvert.SerializeObject(new MenuBC().ObtenerList());
            ObtenerPerfiles(true);
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
            LinkButton btn_modificar = (LinkButton)e.Row.FindControl("btn_modificar");
            LinkButton btn_eliminar = (LinkButton)e.Row.FindControl("btn_eliminar");
            int usua_id = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "USUA_ID"));
            int nivel_permisos = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "USTI_NIVEL_PERMISOS"));
            bool estado = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "USUA_ESTADO"));
            if (usua_id == user.USUA_ID)
            {
                btn_eliminar.Enabled = false;
            }
            if (nivel_permisos < user.USUARIO_TIPO.USTI_NIVEL_PERMISOS)
            {
                btn_modificar.Style.Add("visibility", "hidden");
                btn_eliminar.Style.Add("visibility", "hidden");
            }
        }
    }
    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EDITAR")
        {
            Limpiar();
            int id = Convert.ToInt32(e.CommandArgument);
            UsuarioTipoBC ut = new UsuarioTipoBC().ObtenerXId(id);
            txt_editNombre.Text = ut.USTI_NOMBRE;
            txt_editDescripcion.Text = ut.USTI_DESC;
            txt_editPermisos.Text = ut.USTI_NIVEL_PERMISOS.ToString();
            hf_id.Value = ut.USTI_ID.ToString();
            hf_menuId.Value = ut.MENU_ID.ToString();
            utils.AbrirModal(this, "modalEdit");
        }
        if (e.CommandName == "ELIMINAR")
        {
            hf_id.Value = e.CommandArgument.ToString();
            utils.AbrirModal(this, "modalConf");
        }
    }
    #endregion
    #region Buttons
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        ObtenerPerfiles(true);
    }
    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        Limpiar();
        utils.AbrirModal(this, "modalEdit");
    }
    protected void btn_editGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            UsuarioTipoBC ut = new UsuarioTipoBC();
            ut.USTI_NOMBRE = txt_editNombre.Text;
            ut.USTI_DESC = txt_editDescripcion.Text;
            ut.USTI_NIVEL_PERMISOS = Convert.ToInt32(txt_editPermisos.Text);
            ut.MENU_ID = hf_menuId.Value;
            if (string.IsNullOrEmpty(hf_id.Value))
            {
                if (ut.Guardar())
                {
                    utils.ShowMessage2(this, "guardar", "success_nuevo");
                    utils.CerrarModal(this, "modalEdit");
                }
                else
                    utils.ShowMessage2(this, "guardar", "error");
            }
            else
            {
                ut.USTI_ID = Convert.ToInt32(hf_id.Value);
                if (ut.Guardar())
                    utils.ShowMessage2(this, "guardar", "success_modificar");
                else
                    utils.ShowMessage2(this, "guardar", "error");
            }
        }
        catch(Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
            ObtenerPerfiles(true);
        }
    }
    protected void btn_confEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            UsuarioTipoBC ut = new UsuarioTipoBC();
            ut.USTI_ID = Convert.ToInt32(hf_id.Value);
            if (ut.Eliminar())
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
            ObtenerPerfiles(true);
        }
    }
    #endregion
    #region UtilsPagina
    private void Limpiar()
    {
        txt_editNombre.Text = "";
        txt_editDescripcion.Text = "";
        txt_editPermisos.Text = "";
        hf_id.Value = "";
        hf_menuId.Value = "";
    }
    private void ObtenerPerfiles(bool forzarBD)
    {
        if (ViewState["listar"] == null || forzarBD)
        {
            string para_nombre = txt_buscarNombre.Text;
            ViewState["listar"] = new UsuarioTipoBC().ObtenerTodo(para_nombre);
        }
        DataView dw = new DataView((DataTable)ViewState["listar"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)ViewState["sortExpresion"];
        }
        gv_listar.DataSource = dw;
        gv_listar.DataBind();
    }
    #endregion

    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerPerfiles(false);
    }
}