// Example header text. Can be configured in the options.
using Ruteador.App_Code.Models;
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Tipo_Trailer : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ObtenerTipoTrailer(true);
        }
    }

    #region GridView
    protected void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                string color = DataBinder.Eval(e.Row.DataItem, "TRTI_COLOR").ToString();
                if (!string.IsNullOrEmpty(color))
                {
                    foreach(TableCell tc in e.Row.Cells)
                    {
                        tc.BackColor = System.Drawing.ColorTranslator.FromHtml(color);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = this.utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = string.Format("{0} {1}", e.SortExpression, direccion);
        ObtenerTipoTrailer(false);
    }
    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EDITAR")
        {
            hf_id.Value = e.CommandArgument.ToString();
            TrailerTipoBC tipo_trailer = new TrailerTipoBC().ObtenerXId(Convert.ToInt32(e.CommandArgument));
            txt_editDesc.Text = tipo_trailer.TRTI_DESC;
            txt_editColor.Text = tipo_trailer.TRTI_COLOR;
            utils.AbrirModal(this, "modalEdit");
        }
        if (e.CommandName == "ELIMINAR")
        {
            hf_id.Value = e.CommandArgument.ToString();
            utils.AbrirModal(this, "modalConf");
        }
    }
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
    #endregion
    #region Buttons
    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        hf_id.Value = "";
        txt_editDesc.Text = "";
        txt_editColor.Text = "#000000";
        utils.AbrirModal(this, "modalEdit");
    }
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        ObtenerTipoTrailer(true);
    }
    protected void btn_editGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            TrailerTipoBC tt = new TrailerTipoBC();
            tt.TRTI_DESC = txt_editDesc.Text;
            tt.TRTI_COLOR = txt_editColor.Text;

            if (hf_id.Value == "")
            {
                if (tt.Guardar())
                {
                    utils.ShowMessage2(this, "guardar", "success_nuevo");
                    utils.CerrarModal(this, "modalEdit");
                }
                else
                {
                    utils.ShowMessage2(this, "guardar", "error_nuevo");
                }
            }
            else
            {
                tt.TRTI_ID = Convert.ToInt32(hf_id.Value);
                if (tt.Guardar())
                {
                    utils.ShowMessage2(this, "guardar", "success_modificar");
                }
                else
                {
                    utils.ShowMessage2(this, "guardar", "error_modificar");
                }
            }
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
            ObtenerTipoTrailer(true);
        }
    }
    protected void btn_confEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            TrailerTipoBC tipo_trailer = new TrailerTipoBC();
            if (tipo_trailer.Eliminar(Convert.ToInt32(hf_id.Value)))
            {
                utils.ShowMessage2(this, "eliminar", "success");
                utils.CerrarModal(this, "modalConf");
            }
            else
            {
                utils.ShowMessage2(this, "eliminar", "error");
            }
        }
        catch(Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
            ObtenerTipoTrailer(true);
        }

    }
    #endregion
    #region UtilsPagina
    private void ObtenerTipoTrailer(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            TrailerTipoBC tt = new TrailerTipoBC();
            ViewState["lista"] = tt.ObtenerTodo(txt_buscarNombre.Text);
        }
        DataView dw = new DataView((DataTable)ViewState["lista"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)ViewState["sortExpresion"];
        }
        gv_listar.DataSource = dw.ToTable();
        gv_listar.DataBind();
    }
    #endregion
}