﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Carga_Pedido_Xls.aspx.cs" Inherits="App_Carga_Pedido_Xls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-lg-12 separador"></div>
    <h2>Carga de pedidos</h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-xs-12 separador"></div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-xs-2">
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txt_archivo" CssClass="form-control" runat="server" placeholder="Seleccione un archivo..." Visible="false" />
            </div>
            <div class="col-xs-1">
                <asp:LinkButton ID="btn_subir" CssClass="btn btn-primary" runat="server" OnClick="UploadBtn_Click">
                    <span class="glyphicon glyphicon-upload" />
                </asp:LinkButton>
            </div>
            <div class="col-xs-1">
                <asp:LinkButton ID="btn_procesar" OnClick="btn_procesar_Click" CssClass="btn btn-lg btn-primary" runat="server"  >
                    <span class="glyphicon glyphicon-ok" />
                </asp:LinkButton>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_subir" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div id="dv_ejemplo" runat="server">
                <h4>Formato Ejemplo</h4>
                <table class="table table-bordered table-hover tablita">
                    <tr>
                        <th>NumeroPedido</th>
                        <th>CodigoCliente</th>
                        <th>NombreCliente</th>
                        <th>Direccion</th>
                        <th>Comuna</th>
                        <th>lat</th>
                        <th>lon</th>
                        <th>Fecha</th>
                        <th>Horario</th>
                        <th>Peso</th>
                        <th>TEntrega</th>
                        <th style="visibility:hidden; display:none" >Hora de Salida CD</th>
                        <th style="visibility:hidden; display:none">Hora de Apertura</th>
                        <th style="visibility:hidden; display:none">Hora de Cierre</th>
                        <th style="visibility:hidden; display:none">IdCamion</th>
                        
                    </tr>
                </table>
            </div>
            <asp:GridView ID="gv_listar" CssClass="table table-bordered table-hover tablita tab-nopag" AutoGenerateColumns="TRUE" Width="100%" runat="server"
                OnRowCreated="gv_listar_RowCreated">
                <Columns>
 
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ocultos" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

