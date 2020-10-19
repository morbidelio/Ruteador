<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.Master" AutoEventWireup="true" CodeFile="Parametro.aspx.cs" Inherits="App_Parametro" %>

<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="server">
    <div class="col-xs-12 separador"></div>
    <h2>Maestro parámetros</h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Filtro" runat="server">
    <div class="col-xs-12 separador"></div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-xs-1 text-right">
                Nombre
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txt_buscarNombre" CssClass="form-control" runat="server" />
            </div>
            <div class="col-xs-1 text-right">
                Descripción
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txt_buscarDescripcion" CssClass="form-control" runat="server" />
            </div>
            <div class="col-xs-1">
                <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary" runat="server">
                    <span class="glyphicon glyphicon-search" />
                </asp:LinkButton>
                <asp:LinkButton ID="btn_nuevo" OnClick="btn_nuevo_Click" CssClass="btn btn-success" runat="server">
                    <span class="glyphicon glyphicon-plus" />
                </asp:LinkButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Contenedor" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-xs-12">
                <asp:GridView ID="gv_listar" AutoGenerateColumns="False" AllowSorting="true" Width="100%" CssClass="table table-bordered table-hover tablita tab-pag" runat="server"
                    EmptyDataText="No hay registros" OnRowCreated="gv_listar_RowCreated" OnRowCommand="gv_listar_RowCommand" OnSorting="gv_listar_Sorting">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton id="btn_modificar" CssClass="btn btn-warning btn-xs" CommandArgument='<%#Eval("PARA_ID")%>' CommandName="EDITAR" runat="server">
                                    <span class="glyphicon glyphicon-edit" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton id="btn_eliminar" CssClass="btn btn-danger btn-xs" CommandArgument='<%#Eval("PARA_ID")%>' CommandName="ELIMINAR" runat="server">
                                    <span class="glyphicon glyphicon-remove" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PARA_NOMBRE" SortExpression="PARA_NOMBRE" HeaderText="Nombre" />
                        <asp:BoundField DataField="PARA_OBS" SortExpression="PARA_OBS" HeaderText="Descripción" />
                        <asp:BoundField DataField="PARA_VALOR" SortExpression="PARA_VALOR" HeaderText="Valor" />
                        <asp:BoundField DataField="USR_CREACION" SortExpression="USR_CREACION" HeaderText="Usuario creación" />
                        <asp:BoundField DataField="USR_MODIFICACION" SortExpression="USR_MODIFICACION" HeaderText="Usuario última modificación" />
                        <asp:BoundField DataField="PARA_FH_CREACION" SortExpression="PARA_FH_CREACION" HeaderText="FH creación" />
                        <asp:BoundField DataField="PARA_FH_MODIFICACION" SortExpression="PARA_FH_MODIFICACION" HeaderText="FH última modificación" />
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Modals" runat="server">
    <div class="modal fade" id="modalEdit" data-backdrop="static" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">
                                Datos Parámetro
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <div class="col-xs-4">
                                Nombre 
                                <br />
                                <asp:TextBox ID="txt_editNombre" cssClass="form-control" runat="server" />
                            </div>
                            <div class="col-xs-4">
                                Observación 
                                <br />
                                <asp:TextBox ID="txt_editObs" cssClass="form-control" runat="server" />
                            </div>
                            <div class="col-xs-4">
                                Valor 
                                <br />
                                <asp:TextBox ID="txt_editValor" cssClass="form-control" runat="server" />
                            </div>
                            <div class="col-xs-12 separador"></div>
                            <div class="col-xs-12" style="text-align:center">
                                <asp:LinkButton id="btn_editGuardar" OnClick="btn_editGuardar_Click" CssClass="btn btn-primary" runat="server">
                                    <span class="glyphicon glyphicon-floppy-disk" />
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-dismiss="modal">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalConf" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>
                                <asp:Label id="lbl_confTitulo" runat="server" />
                            </h4>
                        </div>
                        <div class="modal-body">
                            <asp:Label id="lbl_confMensaje" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btn_confEliminar" CssClass="btn btn-success" OnClick="btn_confEliminar_Click" runat="server">
                <span class="glyphicon glyphicon-ok" />
                            </asp:LinkButton>
                            <button type="button" class="btn btn-danger" data-dismiss="modal">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ocultos" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_id" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
