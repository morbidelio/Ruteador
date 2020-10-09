using System;
using System.Net;

public partial class App_Inicio : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsCallback)
            return;

        if (Session["Usuario"] == null)
        {
            Response.Redirect("~/Inicio.aspx");
        }
    }


    public static void DownloadString (string address)
{
    WebClient client = new WebClient ();
    string reply = client.DownloadString ("http://www.itruck.cl/telemetrica/ppp/fepasakmz.asp?TM=302909");
    }
}
