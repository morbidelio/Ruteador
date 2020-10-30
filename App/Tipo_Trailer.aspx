<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Tipo_Trailer.aspx.cs" Inherits="App_Tipo_Trailer" %>

<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-xs-12 separador"></div>
    <h2>Maestro Tipo Vehículo
    </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-xs-12 separador"></div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-xs-1 text-right">
                Descripción
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txt_buscarNombre" runat="server" CssClass="form-control" />
            </div>
            <div class="col-xs-2">
                <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary" ToolTip="Buscar" runat="server">
                <span class="glyphicon glyphicon-search" />
                </asp:LinkButton>
                <asp:LinkButton ID="btn_nuevo" OnClick="btn_nuevo_Click" CssClass="btn btn-success" ToolTip="Nuevo" runat="server">
                <span class="glyphicon glyphicon-plus" />
                </asp:LinkButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-xs-6 col-xs-push-3">
                <asp:GridView ID="gv_listar" AutoGenerateColumns="False" AllowSorting="true" Width="100%" CssClass="table table-bordered table-hover tablita tab-pag" runat="server"
                    EmptyDataText="No hay registros" OnRowCreated="gv_listar_RowCreated" OnRowDataBound="gv_listar_RowDataBound" OnRowCommand="gv_listar_RowCommand" OnSorting="gv_listar_Sorting">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_modificar" runat="server" CommandName="EDITAR" CommandArgument='<%# Eval("TRTI_ID") %>' CssClass="btn btn-xs btn-warning" ToolTip="Modificar">
                  <span class="glyphicon glyphicon-edit" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_eliminar" runat="server" CommandName="ELIMINAR" CommandArgument='<%# Eval("TRTI_ID") %>' CssClass="btn btn-xs btn-danger" ToolTip="Eliminar">
                  <span class="glyphicon glyphicon-remove" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Descripción" DataField="TRTI_DESC" SortExpression="TRTI_DESC" />
                        <%--<asp:BoundField HeaderText="Color" DataField="TRTI_COLOR" SortExpression="TRTI_COLOR" />--%>
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" />
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" runat="Server">
    <div class="modal fade" id="modalEdit" data-backdrop="static" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">Datos Tipo Trailer
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <div class="col-xs-6">
                                Descripción
                <br />
                                <asp:TextBox ID="txt_editDesc" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-xs-6">
                                Color
                                <asp:TextBox ID="txt_editColor" CssClass="form-control color" runat="server" />
                                <div id="colorpicker"></div>
                            </div>
                            <div class="col-xs-12 separador">
                            </div>
                            <div class="col-xs-12" style="text-align: center">
                                <asp:LinkButton ID="btn_editGuardar" runat="server" OnClick="btn_editGuardar_Click" CssClass="btn btn-success">
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
                                <asp:Label ID="lbl_confTitulo" runat="server" />
                            </h4>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="lbl_confMensaje" runat="server" />
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
<asp:Content ID="Hidden" ContentPlaceHolderID="ocultos" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_id" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript" src="../Scripts/farbtastic.js"></script>
    <link rel="stylesheet" href="../Scripts/farbtastic.css" type="text/css" />
    <script type="text/javascript">
        function EndRequestHandler(sender, args) {
            $('#colorpicker').farbtastic('#<%=txt_editColor.ClientID%>');
            $('#<%=btn_editGuardar.ClientID%>').click(function () {
                if ($('#<%=txt_editDesc.ClientID%>').val() == '' ||
                    !$('#<%=txt_editDesc.ClientID%>').val()) {
                    showAlertClass('guardar', 'warn_descVacio');
                    return false;
                }
                if ($('#<%=txt_editColor.ClientID%>').val() == '' ||
                    !$('#<%=txt_editColor.ClientID%>').val()) {
                    showAlertClass('guardar', 'warn_colorVacio');
                    return false;
                }
            });
        }

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);
    </script>
</asp:Content>
