<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Tracto.aspx.cs" Inherits="App_Tracto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-lg-12 separador"></div>
    <h2>Maestro Tracto
    </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-lg-12 separador"></div>
    <div class="col-lg-1">
        Placa
    </div>
    <div class="col-lg-1">
        <asp:TextBox ID="txt_buscarPlaca" CssClass="form-control" runat="server" />
    </div>
    <div class="col-lg-1">
        <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_click" CssClass="btn btn-primary" ToolTip="Buscar Tracto" runat="server">
      <span class="glyphicon glyphicon-search" />
        </asp:LinkButton>
        <asp:LinkButton ID="btn_nuevo" OnClick="btn_nuevo_Click" CssClass="btn btn-success" ToolTip="Nuevo Tracto" runat="server">
      <span class="glyphicon glyphicon-plus" />
        </asp:LinkButton>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-xs-6 col-xs-push-3" style="text-align: center;">
                <asp:GridView ID="gv_listar" AutoGenerateColumns="False" AllowSorting="true" Width="100%" CssClass="table table-bordered table-hover tablita tab-pag" runat="server"
                    EmptyDataText="No hay registros" OnRowCreated="gv_listar_RowCreated" OnRowCommand="gv_listar_RowCommand" OnSorting="gv_listar_Sorting">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_modificar" runat="server" CommandName="EDITAR" CommandArgument='<%# Eval("TRAC_ID") %>' CssClass="btn btn-xs btn-warning" ToolTip="Modificar">
                    <span class="glyphicon glyphicon-edit" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_eliminar" runat="server" CommandName="ELIMINAR" CommandArgument='<%# Eval("TRAC_ID") %>' CssClass="btn btn-xs btn-danger" ToolTip="Eliminar">
                    <span class="glyphicon glyphicon-remove" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Patente" DataField="TRAC_PLACA" SortExpression="TRAC_PLACA" />
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" runat="Server">
    <div class="modal fade" id="modalEdit" data-backdrop="static" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">Datos Tracto
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <div class="col-xs-12">
                                Patente
                <br />
                                <asp:TextBox ID="txt_editPatente" OnTextChanged="txt_editPatente_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control input-mayus"></asp:TextBox>
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
                            <button type="button" data-dismiss="modal" class="btn btn-danger">
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
<asp:Content ID="Content5" ContentPlaceHolderID="ocultos" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_id" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">
        function EndRequestHandler(sender, args) {
            $('#<%=btn_editGuardar.ClientID%>').click(function () {
                if ($('#<%=txt_editPatente.ClientID%>').val() == '' ||
                    !$('#<%=txt_editPatente.ClientID%>').val()) {
                    showAlertClass('guardar', 'warn_placaVacio');
                    return false;
                }
            });
        }

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);
    </script>
</asp:Content>

