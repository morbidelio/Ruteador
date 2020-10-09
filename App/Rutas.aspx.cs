using Newtonsoft.Json;
using Ruteador.App_Code.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Rutas : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsCallback)
            return;

        ObtenerRutas(true);

        DataTable dt = new PuntoEntregaBC().ObtenerTodo();
        hf_todos.Value = JsonConvert.SerializeObject(dt);
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

    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "PUNTOS")
        {
            utils.AbrirModal(this.Page, "modalPuntos");
            hf_idRuta.Value = "";
            hf_idPunto.Value = "";
            hf_puntosruta.Value = "";
            hf_idOrigen.Value = "";
            txt_puntoNombre.Text = "";
            gv_listar.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int id_ruta = Convert.ToInt32(gv_listar.SelectedDataKey.Values[0]);
            int id_origen = Convert.ToInt32(gv_listar.SelectedDataKey.Values[1]);
            hf_idRuta.Value = id_ruta.ToString();
            hf_idOrigen.Value = id_origen.ToString();
            ObtenerPuntosRuta(true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "map", "mapa();", true);
        }
    }
    private void ObtenerRutas(bool forzarBD)
    {
        if (ViewState["listar"] == null || forzarBD)
        {
            ViewState["listar"] = new RutaBC().ObtenerTodo();
        }
        DataView dw = new DataView((DataTable)ViewState["listar"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)ViewState["sortExpresion"];
        }
        gv_listar.DataSource = dw;
        gv_listar.DataBind();
    }
    private void ObtenerPuntosRuta(bool forzarBD)
    {
        int id_ruta = Convert.ToInt32(hf_idRuta.Value);
        DataTable dt = new RutaBC().ObtenerPuntos(id_ruta);
        hf_puntosruta.Value = JsonConvert.SerializeObject(dt);
    }

    protected void btn_guardar_Click(object sender, EventArgs e)
    {
        try
        {
            int id_ruta = Convert.ToInt32(hf_idRuta.Value);
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(hf_puntosruta.Value);
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                if (sb.Length != 0)
                    sb.Append(",");
                sb.Append(dr["ID"]);
            }
            new RutaBC().Guardar(id_ruta, sb.ToString());
            utils.ShowMessage(this, "Ruta modificada correctamente", "success", true);
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
            ObtenerRutas(true);
        }
    }

    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerRutas(false);
    }
}