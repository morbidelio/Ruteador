using Ruteador.App_Code.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Origen : System.Web.UI.Page
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
            CargaDrops();
            ObtenerOrigenes(true);
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
            OrigenBC o = new OrigenBC().ObtenerXId(id);
            LlenarDatos(o);
            utils.AbrirModal(this, "modalEdit");
        }
        if (e.CommandName == "ELIMINAR")
        {
            hf_idOrigen.Value = e.CommandArgument.ToString();
            lbl_confTitulo.Text = "Eliminar origen";
            lbl_confMensaje.Text = "Se eliminará el origen seleccionado ¿Está seguro?";
            btn_confEliminar.Visible = true;
            utils.AbrirModal(this, "modalConf");
        }
    }
    #endregion
    protected void btn_editLatLon_Click(object sender, EventArgs e)
    {
        UtilsWeb.ObtenerCoordenadas(txt_editDireccion.Text + " " , ddl_editComu.SelectedItem.Text, txt_editLat, txt_editLon);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "map", "mapa();", true);
    }
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
            ddl_editComu.ClearSelection();
            ddl_editComu.Enabled = false;
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "map", "mapa();", true);
    }
    protected void ddl_editCiudad_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_editCiudad.SelectedIndex > 0)
        {
            int ciud_id = Convert.ToInt32(ddl_editCiudad.SelectedValue);
            DataTable dt = new ComunaBC().ObtenerTodo(ciud_id: ciud_id);
            utils.CargaDrop(ddl_editComu, "COMU_ID", "COMU_NOMBRE", dt);
            ddl_editComu.Enabled = true;
        }
        else
        {
            ddl_editComu.ClearSelection();
            ddl_editComu.Enabled = false;
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
        DataTable dt = new ComunaBC().ObtenerTodo(regi_id,ciud_id);
        utils.CargaDropTodos(ddl_buscarComuna, "COMU_ID", "COMU_NOMBRE", dt);
    }
    #endregion
    #region Buttons
    protected void btn_editGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            OrigenBC o = new OrigenBC()
            {
                ID_PE = txt_editIdPe.Text,
                NOMBRE_PE = txt_editNombre.Text,
                DIRECCION_PE = txt_editDireccion.Text,
                LAT_PE = Convert.ToDecimal(txt_editLat.Text.Replace(".",",")),
                LON_PE = Convert.ToDecimal(txt_editLon.Text.Replace(".", ",")),
                RADIO_PE = Convert.ToInt32(txt_editRadio.Text),
                IS_POLIGONO = chk_editPoligono.Checked,
            };
            o.COMUNA.CIUDAD.REGION.REGI_ID = Convert.ToInt32(ddl_editRegion.SelectedValue);
            o.COMUNA.CIUDAD.CIUD_ID = Convert.ToInt32(ddl_editCiudad.SelectedValue);
            o.COMUNA.COMU_ID = Convert.ToInt32(ddl_editComu.SelectedValue);
            o.OPERACION.OPER_ID = Convert.ToInt32(ddl_editOpe.SelectedValue);
            if (string.IsNullOrEmpty(hf_idOrigen.Value))
            {
                if (o.Guardar())
                {
                    utils.ShowMessage2(this, "guardar", "success_nuevo");
                    utils.CerrarModal(this, "modalEdit");
                }
                else
                    utils.ShowMessage2(this, "guardar", "error");
            }
            else
            {
                o.ID = Convert.ToInt32(hf_idOrigen.Value);
                if (o.Guardar())
                {
                    utils.ShowMessage2(this, "guardar", "success_modificar");
                    utils.CerrarModal(this, "modalEdit");
                }
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
            ObtenerOrigenes(true);
        }
    }
    protected void btn_confEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            int orig_id = Convert.ToInt32(hf_idOrigen.Value);
            if (new OrigenBC().Eliminar(orig_id))
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
            ObtenerOrigenes(true);
        }
    }
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        ObtenerOrigenes(true);
    }
    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        Limpiar();
        utils.AbrirModal(this, "modalEdit");
    }
    #endregion
    #region UtilsPagina
    private void ObtenerOrigenes(bool forzarBD)
    {
        if (ViewState["listar"] == null || forzarBD)
        {
            int regi_id = Convert.ToInt32(ddl_buscarRegion.SelectedValue);
            int ciud_id = Convert.ToInt32(ddl_buscarCiudad.SelectedValue);
            int comu_id = Convert.ToInt32(ddl_buscarComuna.SelectedValue);
            ViewState["listar"] = new OrigenBC().ObtenerTodo(txt_buscarNombre.Text,regi_id,ciud_id,comu_id);
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
        hf_idOrigen.Value = "";
        txt_editIdPe.Text = "";
        txt_editNombre.Text = "";
        ddl_editRegion.ClearSelection();
        ddl_editRegion_SelectedIndexChanged(null, null);
        txt_editDireccion.Text = "";
        txt_editLat.Text = "";
        txt_editLon.Text = "";
        txt_editRadio.Text = "";
        chk_editPoligono.Checked = false;
        ddl_editOpe.ClearSelection();
    }
    private void LlenarDatos(OrigenBC o)
    {
        hf_idOrigen.Value = o.ID.ToString();
        txt_editIdPe.Text = o.ID_PE;
        txt_editNombre.Text = o.NOMBRE_PE;
        ddl_editRegion.SelectedValue = o.COMUNA.CIUDAD.REGION.REGI_ID.ToString();
        ddl_editRegion_SelectedIndexChanged(null, null);
        ddl_editCiudad.SelectedValue = o.COMUNA.CIUDAD.CIUD_ID.ToString();
        ddl_editCiudad_SelectedIndexChanged(null, null);
        ddl_editComu.SelectedValue = o.COMUNA.COMU_ID.ToString();
        txt_editDireccion.Text = o.DIRECCION_PE;
        txt_editLat.Text = o.LAT_PE.ToString();
        txt_editLon.Text = o.LON_PE.ToString();
        txt_editRadio.Text = o.RADIO_PE.ToString();
        chk_editPoligono.Checked = o.IS_POLIGONO;
        ddl_editOpe.SelectedValue = o.OPERACION.OPER_ID.ToString();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "map", "mapa();", true);
    }
    private void CargaDrops()
    {
        DataTable dt = new RegionBC().ObtenerTodo();
        utils.CargaDrop(ddl_editRegion, "REGI_ID", "REGI_NOMBRE", dt);
        ddl_editRegion_SelectedIndexChanged(null, null);
        utils.CargaDropTodos(ddl_buscarRegion, "REGI_ID", "REGI_NOMBRE", dt);
        ddl_buscarRegion_SelectedIndexChanged(null, null);
        dt = new OperacionBC().ObtenerTodo();
        utils.CargaDrop(ddl_editOpe, "OPER_ID", "OPER_NOMBRE", dt);
    }
    #endregion

    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerOrigenes(false);
    }
}