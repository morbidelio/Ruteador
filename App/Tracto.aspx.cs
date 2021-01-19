using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Ruteador.App_Code.Models;

public partial class App_Tracto : System.Web.UI.Page
{
    private static UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();
    private UsuarioBC user = new UsuarioBC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
            Response.Redirect("~/Inicio.aspx");
        user = (UsuarioBC)Session["usuario"];
        if (!IsPostBack)
        {
            ObtenerTracto(true);
        }
    }
    #region GridView
    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerTracto(false);
    }
    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EDITAR")
        {
            hf_id.Value = e.CommandArgument.ToString();
            TractoBC t = new TractoBC().ObtenerXId(Convert.ToInt32(hf_id.Value));
            txt_editPatente.Text = t.TRAC_PLACA;
            utils.AbrirModal(this, "modalEdit");
        }
        if (e.CommandName == "ELIMINAR")
        {
            hf_id.Value = e.CommandArgument.ToString();
            lbl_confTitulo.Text = "Eliminar Tracto";
            lbl_confMensaje.Text = "Se eliminará el tracto seleccionado ¿Desea continuar?";
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
    #region Botones
    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        Limpiar();
        utils.AbrirModal(this, "modalEdit");
    }
    protected void btn_buscar_click(object sender, EventArgs e)
    {
        ObtenerTracto(true);
    }
    protected void btn_editGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            TractoBC t = new TractoBC();
            t.TRAC_PLACA = txt_editPatente.Text;
            if (hf_id.Value == "")
            {
                if (t.Guardar())
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
                t.TRAC_ID = Convert.ToInt32(hf_id.Value);
                if (t.Guardar())
                {
                    utils.ShowMessage2(this, "guardar", "success_modificar");
                    utils.CerrarModal(this, "modalEdit");
                }
                else
                {
                    utils.ShowMessage2(this, "guardar", "error_modificar");
                }
            }
        }
        catch( Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
            ObtenerTracto(true);
        }
    }
    protected void btn_confEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            TractoBC t = new TractoBC();
            if (t.Eliminar(Convert.ToInt32(hf_id.Value)))
            {
                utils.ShowMessage2(this, "eliminar", "success");
                utils.CerrarModal(this, "modalConf");
            }
            else
            {
                utils.ShowMessage2(this, "eliminar", "error");
            }
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
            ObtenerTracto(true);
        }
    }
    #endregion
    #region FuncionesPagina
    private void Limpiar()
    {
        hf_id.Value = "";
        txt_editPatente.Text = "";
    }
    private void ObtenerTracto(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            ViewState["lista"] = new TractoBC().ObtenerTodo(trac_placa: txt_buscarPlaca.Text);
        }

        DataView dw = new DataView((DataTable)ViewState["lista"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
            dw.Sort = (String)ViewState["sortExpresion"];
        gv_listar.DataSource = dw;
        gv_listar.DataBind();
    }
    #endregion

    // viewstate
    protected override void SavePageStateToPersistenceMedium(object state)
    {

        string file = GenerateFileName();

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



            StreamReader reader = new StreamReader(GenerateFileName());

            LosFormatter formator = new LosFormatter();

            state = formator.Deserialize(reader);

            reader.Close();


        }
        catch (Exception ex)
        {
            Console.Write(ex);
            Response.Redirect(Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + ".aspx");
        }
        return state;
    }

    private string GenerateFileName()
    {

        string pageName = Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath);

        string file = pageName + Session.SessionID.ToString() + ".txt";

        //       file = Path.Combine(Server.MapPath("~/ViewStateFiles") + "/" + file);  
        file = utils.pathviewstate() + "\\" + file;

        return file;

    }




    protected void txt_editPatente_TextChanged(object sender, EventArgs e)
    {
        if (funcion.ValidaPlaca(txt_editPatente.Text))
        {
            TractoBC t = new TractoBC().ObtenerXPlaca(trac_placa: txt_editPatente.Text);
            int trac_id = (string.IsNullOrEmpty(hf_id.Value)) ? 0 : Convert.ToInt32(hf_id.Value);
            if (t.TRAC_ID != trac_id && t.TRAC_ID != 0)
            {
                utils.ShowMessage2(this, "guardar", "warn_placaExiste");
                txt_editPatente.Text = "";
                txt_editPatente.Focus();
            }
        }
        else
        {
            utils.ShowMessage2(this, "guardar", "warn_placaInvalida");
            txt_editPatente.Text = "";
            txt_editPatente.Focus();
        }
    }
}