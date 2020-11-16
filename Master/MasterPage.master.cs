using Ruteador.App_Code.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Master_MasterPage : System.Web.UI.MasterPage, ICallbackEventHandler
{
    string callbackReturnValue;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsCallback)
            return;
        else
        {
            PreRutaBC preruta = new PreRutaBC();
            string procesos;
            procesos=muestraproceso(preruta.obtenerultimosprocesos());

            if (procesos != "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "muestraproceso", "ClientNameCallback('" + procesos + "');", true);
            }
        }


        if (Session["Usuario"] == null)
            Response.Redirect("~/Inicio.aspx");
        UsuarioBC user = (UsuarioBC)Session["Usuario"];
        bool autorizado = false;
        string nombrepag = this.Page.AppRelativeVirtualPath;
        nombrepag = nombrepag.Substring(nombrepag.LastIndexOf("/") + 1);
        //if (nombrepag.ToLower() != "inicio.aspx")
        //{
        //    foreach (MenuBC m in user.USUARIO_TIPO.MENU)
        //    {
        //        if (m.MENU_LINK == nombrepag)
        //        {
        //            autorizado = true;
        //            break;
        //        }
        //    }
        //    if (!autorizado)
        //        Response.Redirect("~/Inicio.aspx");
        //}

     //   txtClientID.Attributes.Add("onchange", "GetClientNameById('id|' + this.value, 'id');");


        if (IsPostBack == false)
        {
            ParametroBC param = new ParametroBC();
            DataTable datos = param.ObtenerTodo();
            Session["parametros"] = datos;
            int contador = 0;
            while (contador < datos.Rows.Count)
            {

                HiddenField hf = new HiddenField();
                hf.ID = "param_" + datos.Rows[contador]["para_nombre"].ToString();
                hf.Value = datos.Rows[contador]["para_valor"].ToString();
                this.parametros.ContentTemplateContainer.Controls.Add(hf);
                contador = contador + 1;
            }
        }
        string callBackClientID = Page.ClientScript.GetCallbackEventReference(this, "arg", "ClientNameCallback", "context", "ClientNameCallbackError", true);
        string clientIDfunction = "function GetClientNameById(arg,context) { " + callBackClientID + "; }";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetClientNameById", clientIDfunction, true);
//        txtClientName.Attributes.Add("onchange", "GetClientIdByName('name|' + this.value, 'name');");
        //string callBackClientName = Page.ClientScript.GetCallbackEventReference(this, "arg", "ClientIdCallback", "context", "ClientIdCallbackError", true);
        //string clientNamefunction = "function GetClientIdByName(arg, context) { " + callBackClientName + "; }";
        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetClientIdByName", clientNamefunction, true);
    }

    string ICallbackEventHandler.GetCallbackResult()
    {
        return callbackReturnValue;
    }

    void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
    {
        PreRutaBC preruta = new PreRutaBC();
        callbackReturnValue = muestraproceso(preruta.obtenerultimosprocesos());
    }
    string muestraproceso(string procesos)
    {
        if (procesos != "")
        {
            DataTable yamostrados;
            if (Session["procesos"] != null)
            {
                yamostrados = (DataTable)Session["procesos"];
            }
            else
            {
                yamostrados = new DataTable();
                yamostrados.Columns.Add(new DataColumn("mensaje"));
            }
            if (yamostrados.Select("mensaje='" + procesos + "'").Length == 0)
            {
                yamostrados.Rows.Add(procesos);
                Session["procesos"] = yamostrados;
                return procesos;
            }
            else
            {
                return "";
            }
        }
        else
        {
            return procesos;
        }
    }
}


