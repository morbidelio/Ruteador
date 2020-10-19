<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true"
    CodeFile="Trailer.aspx.cs" Inherits="App_Trailer" %>

<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-xs-12 separador">
    </div>
    <h2>Maestro Trailer
    </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-xs-12 separador">
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-xs-2 text-right">
                Tipo Transporte
            </div>
            <div class="col-xs-2">
                <asp:DropDownList ID="ddl_buscarTipo" CssClass="form-control" runat="server">
                </asp:DropDownList>
            </div>
            <div class="col-xs-1 text-right">
                N° 
            </div>
            <div class="col-xs-1">
                <asp:TextBox ID="txt_buscarNro" runat="server" CssClass="form-control" />
            </div>
            <div class="col-xs-1 text-right">
                Placa
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txt_buscarPlaca" runat="server" CssClass="form-control" />
            </div>
            <div class="col-xs-2">
                <asp:LinkButton ID="btn_buscarTrailer" OnClick="btn_buscar_Click" CssClass="btn btn-primary" ToolTip="Buscar Trailer" runat="server">
      <span class="glyphicon glyphicon-search" />
                </asp:LinkButton>
                <asp:LinkButton ID="btn_nuevoTrailer" OnClick="btn_nuevo_Click" CssClass="btn btn-success" ToolTip="Nuevo Trailer" runat="server">
      <span class="glyphicon glyphicon-plus" />
                </asp:LinkButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-xs-8 col-xs-push-2">
                <asp:GridView ID="gv_listar" AutoGenerateColumns="False" AllowSorting="true" Width="100%" CssClass="table table-bordered table-hover tablita tab-pag" runat="server"
                    EmptyDataText="No hay registros" OnRowCreated="gv_listar_RowCreated" OnRowCommand="gv_listar_RowCommand" OnSorting="gv_listar_Sorting">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_modificar" runat="server" CommandName="EDITAR" CommandArgument='<%# Eval("TRAI_ID") %>' CssClass="btn btn-xs btn-warning" ToolTip="Modificar">
                    <span class="glyphicon glyphicon-edit" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_eliminar" runat="server" CommandName="ELIMINAR" CommandArgument='<%# Eval("TRAI_ID") %>' CssClass="btn btn-xs btn-danger" ToolTip="Eliminar">
                    <span class="glyphicon glyphicon-remove" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TRAI_COD" SortExpression="TRAI_COD" HeaderText="Código" />
                        <asp:BoundField DataField="TRAI_NUMERO" SortExpression="TRAI_NUMERO" HeaderText="Número" />
                        <asp:BoundField DataField="TRAI_PLACA" SortExpression="TRAI_PLACA" HeaderText="Placa" />
                        <asp:BoundField DataField="TRTI_DESC" SortExpression="TRTI_DESC" HeaderText="Tipo" />
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" runat="Server">
    <div class="modal fade" id="modalEdit" data-backdrop="static" role="dialog">
        <div class="modal-dialog ">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">Datos Trailer
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <div class="col-xs-4">
                                Placa
                <br />
                                <asp:TextBox ID="txt_editPlaca" OnTextChanged="txt_editPlaca_TextChanged" AutoPostBack="true" CssClass="form-control input-mayus" runat="server" />
                            </div>
                            <div class="col-xs-4">
                                N° Flota
                <br />
                                <asp:TextBox ID="txt_editNumero" OnTextChanged="txt_editNumero_TextChanged" AutoPostBack="true" CssClass="form-control input-number" runat="server" />
                            </div>
                            <div class="col-xs-4">
                                Tipo
                <br />
                                <asp:DropDownList ID="ddl_editTipo" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-12 separador">
                            </div>
                            <div class="col-xs-12" style="text-align: center;">
                                <asp:LinkButton ID="btn_editGuardar" OnClick="btn_editGuardar_Click" CssClass="btn btn-success" runat="server">
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
<asp:Content ID="Hidden" ContentPlaceHolderID="ocultos" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_idTrailer" runat="server" />
            <asp:HiddenField ID="hf_excluyentes" Value="" runat="server" />
            <asp:HiddenField ID="hf_noexcluyentes" Value="" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">
        function EndRequestHandler(sender, args) {
            $('#<%=btn_editGuardar.ClientID%>').click(function () {
                if ($('#<%=txt_editPlaca.ClientID%>').val() == '' ||
                    !$('#<%=txt_editPlaca.ClientID%>').val()) {
                    showAlertClass('guardar', 'warn_placaVacio');
                    return false;
                }
                if ($('#<%=txt_editNumero.ClientID%>').val() == '' ||
                    !$('#<%=txt_editNumero.ClientID%>').val()) {
                    showAlertClass('guardar', 'warn_numeroVacio');
                    return false;
                }
                if ($('#<%=ddl_editTipo.ClientID%>').val() == '0' ||
                    $('#<%=ddl_editTipo.ClientID%>').val() == '' ||
                    !$('#<%=ddl_editTipo.ClientID%>').val()) {
                    showAlertClass('guardar', 'warn_tipoVacio');
                    return false;
                }
            });
        }

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);
    </script>
</asp:Content>
