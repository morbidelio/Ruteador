using Ruteador.App_Code.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Parametro : System.Web.UI.Page
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
            ObtenerParametros(true);
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
    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EDITAR")
        {
            Limpiar();
            int id = Convert.ToInt32(e.CommandArgument);
            ParametroBC p = new ParametroBC().ObtenerXId(id);
            txt_editNombre.Text = p.PARA_NOMBRE;
            txt_editObs.Text = p.PARA_OBS;
            txt_editValor.Text = p.PARA_VALOR;
            utils.AbrirModal(this, "modalEdit");
        }
        if (e.CommandName == "ELIMINAR")
        {
            hf_id.Value = e.CommandArgument.ToString();
            lbl_confTitulo.Text = "Eliminar parámetro";
            lbl_confMensaje.Text = "Se eliminará el parámetro seleccionado ¿Está seguro?";
            btn_confEliminar.Visible = true;
            utils.AbrirModal(this, "modalConf");
        }
    }
    #endregion
    #region Buttons
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        ObtenerParametros(true);
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
            ParametroBC p = new ParametroBC();
            p.PARA_NOMBRE = txt_editNombre.Text;
            p.PARA_OBS = txt_editObs.Text;
            p.PARA_VALOR = txt_editValor.Text;
            p.USUA_ID_CREACION = user.USUA_ID;
            p.USUA_ID_MODIFICACION = user.USUA_ID;
            if (string.IsNullOrEmpty(hf_id.Value))
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
                p.PARA_ID = Convert.ToInt32(hf_id.Value);
                if (p.Guardar())
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
            ObtenerParametros(true);
        }
    }
    protected void btn_confEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            int para_id = Convert.ToInt32(hf_id.Value);
            if (new ParametroBC().Eliminar(para_id))
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
            ObtenerParametros(true);
        }
    }
    #endregion
    #region UtilsPagina
    private void ObtenerParametros(bool forzarBD)
    {
        if (ViewState["listar"] == null || forzarBD)
        {
            string para_nombre = txt_buscarNombre.Text;
            string para_obs = txt_buscarDescripcion.Text;
            ViewState["listar"] = new ParametroBC().ObtenerTodo(para_nombre, para_obs);
        }
        DataView dw = new DataView((DataTable)ViewState["listar"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)ViewState["sortExpresion"];
        }
        gv_listar.DataSource = dw;
        gv_listar.DataBind();
    }
    private void Limpiar()
    {
        hf_id.Value = "";
        txt_editNombre.Text = "";
        txt_editObs.Text = "";
        txt_editValor.Text = "";
    }
    #endregion


    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerParametros(false);
    }
}