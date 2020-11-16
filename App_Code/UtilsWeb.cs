using Qanalytics.Data.Access.SqlClient;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO.Packaging;
using System.Data.SqlClient;
using Telerik.Web.UI;

/// <summary>
/// Descripción breve de UtilsWeb
/// </summary>
public class UtilsWeb
{
    readonly int intervalo_preingreso = 6;
    public UtilsWeb()
    {
    }
    public int Intervalo_preingreso
    {
        get
        {
            return this.intervalo_preingreso;
        }
    }
    public void ShowMessage(Page p, string msj, string clase, bool hide, string codigo)
    {
        string script = string.Format("msj(\"{0}\",\"{1}\",{2});", msj.Replace("\"", "'").Replace("\r\n", "</br>"), clase, hide.ToString().ToLower());
        ScriptManager.RegisterStartupScript(p, p.GetType(), codigo, script, true);
    }

    public void ShowMessage(Page p, string msj, string clase, bool hide)
    {
        string script = string.Format("msj(\"{0}\",\"{1}\",{2});", msj.Replace("\"", "'").Replace("\r\n", "</br>"), clase, hide.ToString().ToLower());
        ScriptManager.RegisterStartupScript(p, p.GetType(), "msj", script, true);
    }
    public void ShowMessage2(Page p, string accion, string clase)
    {
        ScriptManager.RegisterStartupScript(p.Page, p.GetType(), "msj", string.Format("showAlertClass(\"{0}\",\"{1}\");", accion, clase), true);
    }
    public void AbrirModal(Page p, string nombreModal)
    {
        ScriptManager.RegisterStartupScript(p.Page, p.GetType(), "modal", string.Format("abrirModal('{0}');", nombreModal), true);
    }
    public void CerrarModal(Page p, string nombreModal)
    {
        ScriptManager.RegisterStartupScript(p.Page, p.GetType(), "modal", string.Format("cerrarModal('{0}');", nombreModal), true);
    }
    public string rutANumero(string rut)
    {
        rut = rut.Replace(".", "");
        rut = rut.Replace("-", "");
        return rut;
    }
    public string formatearRut(string rut)
    {
        if (!rut.Contains("-"))
        {
            string r = rut.Substring(0, rut.Length - 1);
            string id = rut.Substring(rut.Length - 1);
            return r + "-" + id;
        }
        else
            return rut;
    }
    public bool validarRut(string rut)
    {
        bool validacion = false;
        try
        {
            rut = rut.ToUpper();
            rut = rut.Replace(".", "");
            rut = rut.Replace("-", "");
            int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

            char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

            int m = 0, s = 1;
            for (; rutAux != 0; rutAux /= 10)
            {
                s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
            }
            if (dv == (char)(s != 0 ? s + 47 : 75))
            {
                validacion = true;
            }
        }
        catch (Exception e)
        {
            validacion = false;
        }
        return validacion;
    }
    public void CargaDropCliente(object nombreDrop, string value, string text, DataTable dt)
    {

        DropDownList drop = (DropDownList)nombreDrop;
        drop.DataSource = null;
        drop.DataBind();
        //drop.SelectedIndex = 0;
        drop.Items.Clear();
        ListItem li = new ListItem("Todos", "0");
        drop.Items.Add(li);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            ListItem l2 = new ListItem();
            l2.Value = dt.Rows[i][value].ToString();
            l2.Text = dt.Rows[i][text].ToString();
            drop.Items.Add(l2);
        }
        drop.SelectedIndex = 0;
    }
    public void CargaDrop(object nombreDrop, string value, string text, DataTable dt, string[] atributos = null)
    {
        if (nombreDrop.GetType() == typeof(DropDownList))
        {
            DropDownList drop = (DropDownList)nombreDrop;
            drop.DataSource = dt;
            drop.DataValueField = value;
            drop.DataTextField = text;
            drop.DataBind();
            ListItem li = new ListItem("Seleccione...", "0");
            drop.Items.Insert(0, li);
            drop.ClearSelection();
        }
        if (nombreDrop.GetType() == typeof(RadComboBox))
        {
            RadComboBox drop = (RadComboBox)nombreDrop;
            drop.DataSource = dt;
            drop.DataValueField = value;
            drop.DataTextField = text;
            drop.DataBind();
            RadComboBoxItem li = new RadComboBoxItem("Seleccione...", "0");
            drop.Items.Insert(0, li);
            drop.SelectedIndex = 0;
        }
    }
    public void CargaDrop_patentes(object nombreDrop, string value, string text, DataTable dt, string[] atributos = null, string campo_marca = null, string valor_marca = null)
    {
        DropDownList drop = (DropDownList)nombreDrop;
        drop.DataSource = null;
        drop.SelectedIndex = -1;
        drop.ClearSelection();
        drop.SelectedValue = null;
        drop.DataBind();
        drop.Items.Clear();
        ListItem li = new ListItem("Seleccione...", "0");
        drop.Items.Add(li);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            ListItem l2 = new ListItem();
            l2.Value = dt.Rows[i][value].ToString();
            l2.Text = dt.Rows[i][text].ToString().ToUpper(); //+ " (" + dt.Rows[i][campo_marca].ToString().ToUpper() + ")";
            drop.Items.Add(l2);
            if (atributos != null)
                for (int j = 0; j < atributos.Length; j++)
                {
                    l2.Attributes.Add(atributos[j], dt.Rows[i][atributos[j]].ToString());
                }
            if (campo_marca != null)
            {
                if (dt.Rows[i][campo_marca].ToString() == valor_marca)
                {
                    l2.Attributes.Add("class", "marcado");
                }
            }
        }


        drop.SelectedIndex = 0;


        if (drop.Items.Count == 2)
        {
            drop.SelectedIndex = 1;
        }
    }
    public void CargaDropDefaultValue(object nombreDrop, string value, string text, DataTable dt, string[] atributos = null)
    {
        DropDownList drop = (DropDownList)nombreDrop;
        drop.DataSource = null;
        drop.DataBind();
        //drop.SelectedIndex = 0;
        drop.Items.Clear();
        ListItem li = new ListItem("Seleccione...", "default");
        drop.Items.Add(li);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            ListItem l2 = new ListItem();
            l2.Value = dt.Rows[i][value].ToString();
            l2.Text = dt.Rows[i][text].ToString().ToUpper();
            drop.Items.Add(l2);
            if (atributos != null)
                for (int j = 0; j < atributos.Length; j++)
                {
                    l2.Attributes.Add(atributos[j], dt.Rows[i][atributos[j]].ToString());
                }


        }
        drop.SelectedIndex = 0;


        if (drop.Items.Count == 2)
        {
            drop.SelectedIndex = 1;
        }
    }
    public void CargaDropNormal(object nombreDrop, string value, string text, DataTable dt)
    {
        if (nombreDrop.GetType() == typeof(DropDownList))
        {
            DropDownList drop = (DropDownList)nombreDrop;
            drop.DataSource = dt;
            drop.DataValueField = value;
            drop.DataTextField = text;
            drop.DataBind();
            drop.ClearSelection();
        }
        if (nombreDrop.GetType() == typeof(RadComboBox))
        {
            RadComboBox drop = (RadComboBox)nombreDrop;
            drop.DataSource = dt;
            drop.DataValueField = value;
            drop.DataTextField = text;
            drop.DataBind();
            drop.ClearSelection();
        }
    }
    public void CargaDropTodos(object nombreDrop, string value, string text, DataTable dt)
    {
        if (nombreDrop.GetType() == typeof(DropDownList))
        {
            DropDownList drop = (DropDownList)nombreDrop;
            drop.DataSource = dt;
            drop.DataValueField = value;
            drop.DataTextField = text;
            drop.DataBind();
            ListItem li = new ListItem("Todos...", "0");
            drop.Items.Insert(0, li);
            drop.ClearSelection();
        }
        if (nombreDrop.GetType() == typeof(RadComboBox))
        {
            RadComboBox drop = (RadComboBox)nombreDrop;
            drop.DataSource = dt;
            drop.DataValueField = value;
            drop.DataTextField = text;
            drop.DataBind();
            RadComboBoxItem li = new RadComboBoxItem("Todos...", "0");
            drop.Items.Insert(0, li);
            drop.ClearSelection();
        }
    }
    public void LimpiarDrop(object nombreDrop)
    {
        CargaDrop(nombreDrop, "ID", "NOMBRE", new DataTable());
    }
    public string ConvertSortDirectionToSql(String order)
    {
        string newSortDirection = String.Empty;
        switch (order)
        {
            case "ASC":
                newSortDirection = "DESC";
                break;
            case "DESC":
                newSortDirection = "ASC";
                break;
            default:
                newSortDirection = "DESC";
                break;
        }

        return newSortDirection;
    }
    public static bool ObtenerCoordenadas(string xDireccion, string COMUNA, TextBox lat, TextBox lon)
    {
        try
        {
            string l = "";
            SqlAccesoDatos data = new SqlAccesoDatos("Route");
            DataTable dt = new DataTable();
            data.CargarSqlComando("[dbo].[SP_BuscaLatLon_API]");
            data.AgregarSqlParametro("@DIRECCION", xDireccion);
            data.AgregarSqlParametro("@COMUNA", COMUNA);
            SqlParameter param = data.AgregarSqlParametroOUT("@rESULT", l);

            data.AgregarSqlParametro("@IP", "10.151.61.153,153");
            data.AgregarSqlParametro("@BD", "QROUTE");
            data.AgregarSqlParametro("@SP", "REFERENCIAR_ORIGEN");
            data.AgregarSqlParametro("@DLL", "");
            data.EjecutarSqlEscritura();

            //while (data.SqlLectorDatos.Read())
            //{
            //    l = Convert.ToString(data.SqlLectorDatos["DATO"]);
            //}
            l = param.Value.ToString();
            lat.Text = l.Split(",".ToCharArray())[0];
            lon.Text = l.Split(",".ToCharArray())[1];

            data.LimpiarSqlParametros();
            data.CerrarSqlConeccion();
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void LLenarDropNumeros(DropDownList combo, int cantidad)
    {
        DropDownList drop = (DropDownList)combo;
        drop.Items.Clear();
        for (int i = 0; i < cantidad; i++)
        {
            ListItem list = new ListItem();
            list.Value = i.ToString("D2");
            list.Text = i.ToString("D2");
            drop.Items.Add(list);
        }
        drop.SelectedIndex = 0;
    }
    public String llenarCerosIzquierda(long number, int cantidadDigitos)
    {
        return number.ToString("D" + cantidadDigitos);
    }
    public DateTime sumarHorasFecha(string fechaConHora, int horas)
    {
        int dia = Convert.ToInt32(fechaConHora.Substring(0, 2));
        int mes = Convert.ToInt32(fechaConHora.Substring(3, 2));
        int agno = Convert.ToInt32(fechaConHora.Substring(6, 4));
        int hora = Convert.ToInt32(fechaConHora.Substring(11, 2));
        int minu = Convert.ToInt32(fechaConHora.Substring(14, 2));
        DateTime fecha = new DateTime(agno, mes, dia, hora, minu, 0);
        fecha = fecha.AddHours(horas);
        return fecha;
    }
    public string buscarArchivo(string fileName)
    {
        string path = System.Web.Configuration.WebConfigurationManager.AppSettings["pathFiles"];
        // Create a reference to the current directory.
        DirectoryInfo di = new DirectoryInfo(path);
        // Create an array representing the files in the current directory.
        FileInfo[] fi = di.GetFiles();
        //Console.WriteLine("The following files exist in the current directory:");
        // Print out the names of the files in the current directory.
        foreach (FileInfo fiTemp in fi)
        {
            if (fiTemp.Name.Contains(fileName + "."))
            {
                return fiTemp.Extension;
            }
        }

        return null;
    }
    public string pathviewstate()
    {
        string path = System.Web.Configuration.WebConfigurationManager.AppSettings["viewstatefiles"];


        return path;
    }

    public string id_cliente_roadshow()
    {
        string path = System.Web.Configuration.WebConfigurationManager.AppSettings["id_cliente_roadshow"];


        return path;


    }
    public static void AddFileToZip(string zipFilename, string fileToAdd)
    {
        using (Package zip = System.IO.Packaging.Package.Open(zipFilename, FileMode.OpenOrCreate))
        {
            string destFilename = @".\" + Path.GetFileName(fileToAdd);
            Uri uri = PackUriHelper.CreatePartUri(new Uri(destFilename, UriKind.Relative));

            if (zip.PartExists(uri))
                zip.DeletePart(uri);

            PackagePart part = zip.CreatePart(uri, "", CompressionOption.Normal);

            using (FileStream fileStream = new FileStream(fileToAdd, FileMode.Open, FileAccess.Read))
            {
                using (Stream dest = part.GetStream())
                {
                    CopyStream(fileStream, dest);
                }
            }
        }
    }

    private static void CopyStream(System.IO.FileStream inputStream, System.IO.Stream outputStream)
    {

        // Dim bufferSize As Long = If(inputStream.Length < BUFFER_SIZE, inputStream.Length, BUFFER_SIZE)
        // Dim buffer As Byte() = New Byte(bufferSize - 1) {}
        // '  Dim bytesRead As Integer = 0
        // Dim bytesWritten As Long = 0

        byte[] bytes = new byte[inputStream.Length - 1 + 1];
        int numBytesToRead = System.Convert.ToInt32(inputStream.Length);
        int numBytesRead = 0;

        while (numBytesToRead > 0)
        {
            int n = inputStream.Read(bytes, numBytesRead, numBytesToRead);


            numBytesRead += n;
            numBytesToRead -= n;
            outputStream.Write(bytes, 0, numBytesRead);
            if (n == 0)
                break;
        }
    }


    public static void AddStreamToZip(string zipFilename, System.IO.Stream contenido, string filename)
    {
        using (Package zip = System.IO.Packaging.Package.Open(zipFilename, FileMode.OpenOrCreate))
        {
            string destFilename = @".\" + Path.GetFileName(filename);
            Uri uri = PackUriHelper.CreatePartUri(new Uri(destFilename, UriKind.Relative));

            if (zip.PartExists(uri))
                zip.DeletePart(uri);

            PackagePart part = zip.CreatePart(uri, "", CompressionOption.Normal);

            using (contenido)
            {
                using (Stream dest = part.GetStream())
                {
                    CopyMemoryStream(contenido as MemoryStream, dest);
                }
            }
        }
    }

    private static void CopyMemoryStream(System.IO.MemoryStream inputStream, System.IO.Stream outputStream)
    {

        // Dim bufferSize As Long = If(inputStream.Length < BUFFER_SIZE, inputStream.Length, BUFFER_SIZE)
        // Dim buffer As Byte() = New Byte(bufferSize - 1) {}
        // '  Dim bytesRead As Integer = 0
        // Dim bytesWritten As Long = 0

        byte[] bytes = new byte[inputStream.Length - 1 + 1];
        int numBytesToRead = System.Convert.ToInt32(inputStream.Length);
        int numBytesRead = 0;

        while (numBytesToRead > 0)
        {
            int n = inputStream.Read(bytes, numBytesRead, numBytesToRead);


            numBytesRead += n;
            numBytesToRead -= n;
            outputStream.Write(bytes, 0, numBytesRead);
            if (n == 0)
                break;
        }
    }

}