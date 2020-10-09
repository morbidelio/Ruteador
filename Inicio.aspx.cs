using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using Ruteador.App_Code.Models;

partial class Inicio : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.UsuarioL.Focus();
            Session.Abandon();

            //UsuarioL.Attributes.Add("onkeypress", "return clickButton(event,'" + BtnLogin.ClientID + "')");
            //ContrasenaL.Attributes.Add("onkeypress", "return clickButton(event,'" + BtnLogin.ClientID + "')");
        }
    }

    protected void btnEntrar_Click(object sender, EventArgs e)
    {
        UsuarioBC usuario = new UsuarioBC();
        usuario.USUA_USERNAME = UsuarioL.Value;
        usuario.USUA_PASSWORD = ContrasenaL.Value;
        usuario = usuario.Login(UsuarioL.Value, ContrasenaL.Value);
        if (usuario.USUA_ID == 0)
        {
            utils.ShowMessage(this, "Nombre de usuario o contraseña incorrectos.", "warn", true);
            return;
        }
        if (!usuario.USUA_ESTADO)
        {
            utils.ShowMessage(this, "Usuario inactivo. Contacte al administrador del sistema.", "warn", true);
            return;
        }
        Session["usuario"] = usuario;
        Response.Redirect("~/App/Default.aspx");
    }
}
