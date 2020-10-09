<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Carga_Pedido_Xls.aspx.cs" Inherits="App_Carga_Pedido_Xls" %>

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
            <asp:GridView ID="gv_listar" CssClass="table table-bordered table-hover tablita tab-principal" AutoGenerateColumns="false" Width="100%" runat="server"
                OnRowCreated="gv_listar_RowCreated">
                <Columns>
                    <asp:BoundField DataField="PEDI_NUMERO" HeaderText="Número" />
                    <asp:BoundField DataField="PEDI_CODIGO" HeaderText="Código" />
                    <asp:BoundField DataField="PEDI_FECHA" HeaderText="Fecha" />
                    <asp:BoundField DataField="PEDI_PESO" HeaderText="Peso" />
                    <asp:BoundField DataField="PEDI_TIEMPO" HeaderText="Tiempo entrega" />
                    <asp:BoundField DataField="PEDI_DIRECCION" HeaderText="Dirección" />
                    <asp:BoundField DataField="PEDI_COMUNA" HeaderText="Comuna" />
                    <asp:BoundField DataField="PEDI_LATITUD" HeaderText="Latitud" />
                    <asp:BoundField DataField="PEDI_LONGITUD" HeaderText="Longitud" />
                    <asp:BoundField DataField="PEDI_HORASALIDA" HeaderText="Hora salida" />
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

