using Ruteador.App_Code.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Carga_Pedido_Xls : System.Web.UI.Page
{
    UsuarioBC user;
    UtilsWeb utils = new UtilsWeb();
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
            btn_procesar.Enabled = false;
        }
    }
    #region Buttons
    protected void UploadBtn_Click(object sender, EventArgs e)
    {
        string sExt = string.Empty;
        string sName = string.Empty;
        string Ruta1;
        bool ejecuto = false;
        Ruta1 = "../xls/";

        var RUTA = Server.MapPath(Ruta1);
        if (FileUpload1.HasFile)
        {
            sName = FileUpload1.FileName;
            sExt = Path.GetExtension(sName);
            if (ValidaExtension(sExt))
            {
                try
                {
                    FileUpload1.SaveAs(string.Format("{0}{1}", RUTA, sName));
                    foreach (string Archivo in Directory.GetFiles(RUTA, sName))
                    {
                        string Nombre = "";
                        string Extension = "";
                        Nombre = Path.GetFileName(Archivo);
                        Extension = Path.GetExtension(Archivo);
                        if (Extension == ".xlsx" || Extension == ".xls")
                        {
                            ejecuto = LeerArchivoExcel(string.Format("{0}{1}", RUTA, Nombre), "Datos", Extension);
                        }
                    }
                    string[] xlsx = Directory.GetFiles(RUTA, sName);
                    foreach (string f in xlsx)
                    {
                        File.Delete(f);
                    }
                }
                catch (Exception ex)
                {
                    utils.ShowMessage(this, ex.Message, "error", false);
                    ejecuto = false;
                }

                if (ejecuto)
                {
                    utils.ShowMessage(this, "Pedidos cargados correctamente", "success", true);
                }
            }
        }
    }
    protected void btn_procesar_Click(object sender, EventArgs e)
    {
        new PedidoBC().ProcesarExcel(user.USUA_ID);
        Response.Redirect("./Pedido_Ruteador.aspx");
    }
    #endregion
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
    #endregion
    #region Utils
    private bool ValidaExtension(string sExtension)
    {
        switch (sExtension)
        {
            case ".xlsx":
                return true;
            case ".xls":
                return true;
            default:
                return false;
        }
    }
    private bool LeerArchivoExcel(string RutaCompleta, string Hoja, string extension)
    {
        try
        {
            string ConexionString = string.Format("Data Source={0}", RutaCompleta);

            if (extension == ".xls")
            {
                ConexionString += string.Format(";Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties='Excel 8.0;HDR=NO;IMEX=1'");
            }
            else
            {
                ConexionString += string.Format(";Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties='Excel 12.0 Xml;HDR=NO;IMEX=1'");
            }
            

            using (OleDbConnection Conexion = new OleDbConnection(ConexionString))
            {

                Conexion.Open();
                DataTable dtSchema = Conexion.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                string Sheet1 = dtSchema.Rows[0].Field<string>("TABLE_NAME");
             //   Conexion.Close();

                string Sql = string.Format("SELECT * FROM [{0}]", Sheet1);

                OleDbDataAdapter Adapter = new OleDbDataAdapter(Sql, Conexion);
                DataTable DT = new DataTable("Excel");
                Adapter.Fill(DT);
                DataTable dtIn = new DataTable();
                int y = 0;
                while (y < DT.Columns.Count) //Definir columnas datatable y sus nombres
                {
                    if (String.IsNullOrWhiteSpace(DT.Rows[0][y].ToString())) break;
                    dtIn.Columns.Add(DT.Rows[0][y].ToString(), Type.GetType("System.String"));
                    if (!string.IsNullOrEmpty(DT.Rows[0][y].ToString()))
                        DT.Columns[y].ColumnName = DT.Rows[0][y].ToString();
                    else
                        break;
                    y++;
                }
                DT.Rows.RemoveAt(0);
                foreach (DataRow dr in DT.Rows)
                {
                    int x = 0;
                    while (x < y)
                    {
                        if (!String.IsNullOrWhiteSpace(dr[x].ToString()))
                        {
                            dtIn.ImportRow(dr);
                            break;
                        }
                        x++;
                    }
                }

                DataTable dtOut = new PedidoBC().IngresarExcel(dtIn, user.USUA_ID);
                gv_listar.DataSource = dtOut;
                gv_listar.DataBind();
                btn_procesar.Enabled = true;
                Conexion.Close();
                return true;
            }
        }
        catch (Exception ex)
        {
            btn_procesar.Enabled = false;
            utils.ShowMessage(this, ex.Message, "error", false);
            return false;
        }

    }
    #endregion
}
