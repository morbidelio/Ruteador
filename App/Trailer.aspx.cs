// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web;
using Ruteador.App_Code.Models;

public partial class App_Trailer : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            CargaDrops();
            ObtenerTrailer(true);
        }
    }

    #region GridView
    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = this.utils.ConvertSortDirectionToSql((String)this.ViewState["sortOrder"]);
        this.ViewState["sortOrder"] = direccion;
        this.ViewState["sortExpresion"] = string.Format("{0} {1}", e.SortExpression, direccion);
        ObtenerTrailer(false);
    }
    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EDITAR")
        {
            Limpiar();
            hf_idTrailer.Value = e.CommandArgument.ToString();
            TrailerBC trailer = new TrailerBC().ObtenerXId(Convert.ToInt32(e.CommandArgument));
            llenarForm(trailer);
            utils.AbrirModal(this, "modalEdit");
        }
        if (e.CommandName == "ELIMINAR")
        {
            hf_idTrailer.Value = e.CommandArgument.ToString();
            lbl_confTitulo.Text = "Eliminar Trailer";
            lbl_confMensaje.Text = "Se eliminará el trailer seleccionado, ¿desea continuar?";
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
    #region TextBox
    protected void txt_editNumero_TextChanged(object sender, EventArgs e)
    {
        TrailerBC t = new TrailerBC().ObtenerXPlaca(trai_nro: txt_editNumero.Text);
        int trai_id = (string.IsNullOrEmpty(hf_idTrailer.Value)) ? 0 : Convert.ToInt32(hf_idTrailer.Value);
        if (t.TRAI_ID != trai_id && t.TRAI_ID != 0)
        {
            utils.ShowMessage2(this, "guardar", "warn_placaexiste");
            txt_editNumero.Text = "";
            txt_editNumero.Focus();
        }
        else
        {
            ddl_editTipo.Focus();
        }
    }
    protected void txt_editPlaca_TextChanged(object sender, EventArgs e)
    {
        if (funcion.ValidaPlaca(txt_editPlaca.Text))
        {
            TrailerBC t = new TrailerBC().ObtenerXPlaca(trai_placa: txt_editPlaca.Text);
            int trai_id = (string.IsNullOrEmpty(hf_idTrailer.Value)) ? 0 : Convert.ToInt32(hf_idTrailer.Value);
            if (t.TRAI_ID != trai_id && t.TRAI_ID != 0)
            {
                utils.ShowMessage2(this, "guardar", "warn_placaexiste");
                txt_editPlaca.Text = "";
                txt_editPlaca.Focus();
            }
            else
            {
                txt_editNumero.Focus();
            }
        }
        else
        {
            utils.ShowMessage2(this, "guardar", "warn_placaInvalida");
            txt_editPlaca.Text = "";
            txt_editPlaca.Focus();
        }
    }
    #endregion
    #region Buttons
    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        Limpiar();
        utils.AbrirModal(this, "modalEdit");
    }
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        ObtenerTrailer(true);
    }
    protected void btn_editGuardar_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txt_editPlaca.Text)) { utils.ShowMessage2(this, "modificar", "warn_placaVacia");return; }
        if (ddl_editTipo.SelectedIndex < 1) { utils.ShowMessage2(this, "modificar", "warn_tipoVacio");return; }
        try
        {
            TrailerBC trailer = new TrailerBC();
            trailer.TRAI_PLACA = txt_editPlaca.Text;
            trailer.TRAI_NUMERO = txt_editNumero.Text;
            if (ddl_editTipo.SelectedIndex > 0)
                trailer.TRAILER_TIPO.TRTI_ID = Convert.ToInt32(ddl_editTipo.SelectedValue);
            trailer.TRAI_COD = txt_editPlaca.Text;
            if (hf_idTrailer.Value == "")
            {
                if (trailer.Guardar())
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
                trailer.TRAI_ID = Convert.ToInt32(hf_idTrailer.Value);
                if (trailer.Guardar())
                {
                    utils.ShowMessage2(this, "guardar", "success_modificar");
                }
                else
                {
                    utils.ShowMessage2(this, "guardar", "error_modificar");
                }
            }
        }
        catch(Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
            ObtenerTrailer(true);
        }
    }
    protected void btn_confEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            TrailerBC trailer = new TrailerBC();
            if (trailer.Eliminar(Convert.ToInt32(hf_idTrailer.Value)))
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
            ObtenerTrailer(true);
        }
    }
    #endregion
    #region UtilsPagina
    private void CargaDrops()
    {
        DataTable dt = new TrailerTipoBC().ObtenerTodo();
        utils.CargaDrop(ddl_editTipo, "TRTI_ID", "TRTI_DESC", dt);
        utils.CargaDropTodos(ddl_buscarTipo, "TRTI_ID", "TRTI_DESC", dt);
    }
    private void ObtenerTrailer(bool forzarBD)
    {
        if (this.ViewState["lista"] == null || forzarBD)
        {
            TrailerBC trailer = new TrailerBC();
            DataTable dt = trailer.ObtenerTodo(txt_buscarNro.Text, txt_buscarPlaca.Text, Convert.ToInt32(ddl_buscarTipo.SelectedValue));
            ViewState["lista"] = dt;
        }
        DataView dw = new DataView((DataTable)this.ViewState["lista"]);
        if (this.ViewState["sortExpresion"] != null && this.ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)this.ViewState["sortExpresion"];
        }
        this.gv_listar.DataSource = dw.ToTable();
        this.gv_listar.DataBind();
    }
    private void Limpiar()
    {
        hf_excluyentes.Value = "";
        hf_noexcluyentes.Value = "";
        hf_idTrailer.Value = "";
        txt_editPlaca.Text = "";
        txt_editNumero.Text = "";
        ddl_editTipo.ClearSelection();
    }
    private void llenarForm(TrailerBC trailer)
    {
        hf_idTrailer.Value = trailer.TRAI_ID.ToString();
        txt_editPlaca.Text = trailer.TRAI_PLACA;
        txt_editNumero.Text = trailer.TRAI_NUMERO;
        ddl_editTipo.SelectedValue = trailer.TRAILER_TIPO.TRTI_ID.ToString();
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



}