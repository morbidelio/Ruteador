<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Conductor.aspx.cs" Inherits="App_Conductor" %>

<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="server">
    <div class="col-lg-12 separador"></div>
    <h2>Maestro Conductor</h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Filtro" runat="server">
    <div class="col-lg-12 separador"></div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-lg-1 text-right">
                Rut
            </div>
            <div class="col-lg-1">
                <asp:TextBox ID="txt_buscarRut" CssClass="form-control" runat="server" />
            </div>
            <div class="col-lg-1 text-right">
                Nombre
            </div>
            <div class="col-lg-2">
                <asp:TextBox ID="txt_buscarNombre" CssClass="form-control" runat="server" />
            </div>
            <div class="col-lg-1">
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
            <div class="col-lg-12">
                <asp:GridView ID="gv_listar" AutoGenerateColumns="False" AllowSorting="true" Width="100%" CssClass="table table-bordered table-hover tablita tab-pag" runat="server"
                    EmptyDataText="No hay registros" OnRowCreated="gv_listar_RowCreated" OnRowDataBound="gv_listar_RowDataBound" OnRowCommand="gv_listar_RowCommand" OnSorting="gv_listar_Sorting" DataKeyNames="COND_ID,COND_ACTIVO,COND_BLOQUEADO">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_modificar" CssClass="btn btn-warning btn-xs" CommandArgument='<%#Eval("COND_ID")%>' CommandName="EDITAR" runat="server">
                                    <span class="glyphicon glyphicon-edit" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_eliminar" CssClass="btn btn-danger btn-xs" CommandArgument='<%#Eval("COND_ID")%>' CommandName="ELIMINAR" runat="server">
                                    <span class="glyphicon glyphicon-remove" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_activar" CommandArgument='<%#Container.DataItemIndex%>' CommandName="ACTIVAR" runat="server" Visible="false">
                                    <span class="glyphicon glyphicon-off" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_bloquear" CommandArgument='<%#Container.DataItemIndex%>' CommandName="BLOQUEAR" runat="server">
                                    <span class="glyphicon glyphicon-lock" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="COND_RUT" SortExpression="COND_RUT" HeaderText="Rut" />
                        <asp:BoundField DataField="COND_NOMBRE" SortExpression="COND_NOMBRE" HeaderText="Nombre" />
                        <asp:BoundField DataField="COND_ACTIVO" SortExpression="COND_ACTIVO" HeaderText="Activo" />
                        <asp:BoundField DataField="COND_TELEFONO" SortExpression="COND_TELEFONO" HeaderText="Teléfono" />
                        <asp:BoundField DataField="COND_EXTRANJERO" SortExpression="COND_EXTRANJERO" HeaderText="Extranjero" />
                        <asp:BoundField DataField="COND_BLOQUEADO" SortExpression="COND_BLOQUEADO" HeaderText="Bloqueado" />
                        <asp:BoundField DataField="COND_MOTIVO_BLOQUEO" SortExpression="COND_MOTIVO_BLOQUEO" HeaderText="Motivo bloqueo" />
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
                            <h4 class="modal-title">Datos Usuario
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <div class="col-lg-4">
                                Nombre
                                <br />
                                <asp:TextBox ID="txt_editNombre" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-lg-4">
                                Rut
                                <br />
                                <asp:TextBox ID="txt_editRut" OnTextChanged="txt_editRut_TextChanged" AutoPostBack="true" CssClass="form-control input-rut" runat="server" />
                            </div>
                            <div class="col-lg-4">
                                Telefono
                                <br />
                                <asp:TextBox ID="txt_editTelefono" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-lg-4">
                                Extranjero
                                <br />
                                <asp:CheckBox id="chk_editExtranjero" runat="server" />
                            </div>
                            <div class="col-lg-12 separador"></div>
                            <div class="col-lg-12" style="text-align: center">
                                <asp:LinkButton ID="btn_editGuardar" OnClick="btn_editGuardar_Click" CssClass="btn btn-primary" runat="server">
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
                        <div class="modal-body" style="height:auto; overflow-y:auto;">
                            <asp:Label id="lbl_confMensaje" runat="server" />
                            <br />
                            <div id="dv_bloquear" class="col-xs-6" style="margin-top:10px;" runat="server">
                                Motivo bloqueo
                                <br />
                                <asp:TextBox ID="txt_confBloquear" CssClass="form-control" runat="server" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btn_confEliminar" CssClass="btn btn-success" OnClick="btn_confEliminar_Click" runat="server">
                <span class="glyphicon glyphicon-ok" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="btn_confActivar" CssClass="btn btn-success" OnClick="btn_confActivar_Click" runat="server">
                <span class="glyphicon glyphicon-ok" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="btn_confBloquear" CssClass="btn btn-success" OnClick="btn_confBloquear_Click" runat="server">
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
    <script>
        function EndRequestHandler1(sender, args) {
            $('#<%=btn_editGuardar.ClientID%>').click(function (e) {
                if ($('#<%=txt_editNombre.ClientID%>').val() == '' ||
                    !$('#<%=txt_editNombre.ClientID%>').val()) {
                    showAlertClass('guardar', 'warn_nombreVacio');
                    return false;
                }
                if ($('#<%=txt_editRut.ClientID%>').val() == '' ||
                    !$('#<%=txt_editRut.ClientID%>').val()) {
                    showAlertClass('guardar', 'warn_rutVacio');
                    return false;
                }
            });
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler1);
    </script>
    <style>
        .checklist > tbody > tr > td{
            text-align:left;
            color:inherit;
            font-size:10px;
            font-weight:bold;
        }
        .checklist > tbody > tr > td > input{
            margin-right:5px;
        }
    </style>
</asp:Content>
