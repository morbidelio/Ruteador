using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Web;
using Ruteador.App_Code.Models;

public partial class Master_menu_json2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
       
    }

    [System.Web.Services.WebMethod(true)]
    public static string SendMessage()
    {
        UsuarioBC usuario = (UsuarioBC)HttpContext.Current.Session["usuario"];
        DataSet ds = new MenuBC().ObtenerTodo(usuario.USUARIO_TIPO.USTI_ID);
        Console.WriteLine("algo");
        return JsonConvert.SerializeObject(new MenuBC().ObtenerList(usuario.USUARIO_TIPO.USTI_ID));
    }
}
