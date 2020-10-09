<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Usuario.aspx.cs" Inherits="App_Usuario" %>

<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="server">
    <div class="col-lg-12 separador"></div>
    <h2>Maestro Usuario</h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Filtro" runat="server">
    <div class="col-lg-12 separador"></div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-lg-1">
                Rut
            </div>
            <div class="col-lg-2">
                <asp:TextBox ID="txt_buscarRut" CssClass="form-control" runat="server" />
            </div>
            <div class="col-lg-1">
                Nombre
            </div>
            <div class="col-lg-2">
                <asp:TextBox ID="txt_buscarNombre" CssClass="form-control" runat="server" />
            </div>
            <div class="col-lg-1">
                Apellido
            </div>
            <div class="col-lg-2">
                <asp:TextBox ID="txt_buscarApellido" CssClass="form-control" runat="server" />
            </div>
            <div class="col-lg-12 separador"></div>
            <div class="col-lg-1">
                Username
            </div>
            <div class="col-lg-2">
                <asp:TextBox ID="txt_buscarUsername" CssClass="form-control" runat="server" />
            </div>
            <div class="col-lg-1">
                Tipo
            </div>
            <div class="col-lg-2">
                <asp:DropDownList ID="ddl_buscarTipo" CssClass="form-control" runat="server" />
            </div>
            <div class="col-lg-1 col-lg-push-4">
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
                    EmptyDataText="No hay registros" OnRowCreated="gv_listar_RowCreated" OnRowDataBound="gv_listar_RowDataBound" OnRowCommand="gv_listar_RowCommand" OnSorting="gv_listar_Sorting" DataKeyNames="USUA_ID,USUA_ESTADO,USTI_NIVEL_PERMISOS">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_modificar" CssClass="btn btn-warning btn-xs" CommandArgument='<%#Eval("USUA_ID")%>' CommandName="EDITAR" runat="server">
                                    <span class="glyphicon glyphicon-edit" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_eliminar" CssClass="btn btn-danger btn-xs" CommandArgument='<%#Eval("USUA_ID")%>' CommandName="ELIMINAR" runat="server">
                                    <span class="glyphicon glyphicon-remove" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_activar" CommandArgument='<%#Container.DataItemIndex%>' CommandName="ACTIVAR" runat="server">
                                    <span class="glyphicon glyphicon-off" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="USUA_COD" SortExpression="USUA_COD" HeaderText="Código" />
                        <asp:BoundField DataField="USUA_DESC" SortExpression="USUA_DESC" HeaderText="Descripción" />
                        <asp:BoundField DataField="USUA_NOMBRE" SortExpression="USUA_NOMBRE" HeaderText="Nombre" />
                        <asp:BoundField DataField="USUA_APELLIDO" SortExpression="USUA_APELLIDO" HeaderText="Apellido" />
                        <asp:BoundField DataField="USUA_RUT" SortExpression="USUA_RUT" HeaderText="Rut" />
                        <asp:BoundField DataField="USUA_CORREO" SortExpression="USUA_CORREO" HeaderText="Correo" />
                        <asp:BoundField DataField="USUA_USERNAME" SortExpression="USUA_USERNAME" HeaderText="Username" />
                        <asp:BoundField DataField="USUA_OBSERVACION" SortExpression="USUA_OBSERVACION" HeaderText="Observacion" />
                        <asp:BoundField DataField="USTI_NOMBRE" SortExpression="USTI_NOMBRE" HeaderText="Nombre" />
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
                                Apellido
                                <br />
                                <asp:TextBox ID="txt_editApellido" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-lg-4">
                                Rut
                                <br />
                                <asp:TextBox ID="txt_editRut" OnTextChanged="txt_editRut_TextChanged" AutoPostBack="true" CssClass="form-control input-rut" runat="server" />
                            </div>
                            <div class="col-lg-4">
                                Correo
                                <br />
                                <asp:TextBox ID="txt_editCorreo" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-lg-4">
                                Username
                                <br />
                                <asp:TextBox ID="txt_editUsername" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-lg-3">
                                Contraseña
                                <br />
                                <asp:TextBox ID="txt_editPassword" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-xs-1">
                                <br />
                                <asp:LinkButton ID="btn_editGenerar" CssClass="btn btn-warning" ToolTip="Generar Automáticamente" runat="server" OnClick="btn_editGenerar_Click">
                  <span class="glyphicon glyphicon-certificate" />
                                </asp:LinkButton>
                            </div>
                            <div class="col-lg-4">
                                Observación
                                <br />
                                <asp:TextBox ID="txt_editObservacion" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-lg-4">
                                Tipo Usuario
                                <br />
                                <asp:DropDownList ID="ddl_editTipo" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-lg-4">
                                Operación
                                <br />
                                <div style="max-height:100px;overflow-y:auto;border:solid 1px lightgray;padding-left:5px;border-radius:5px;">
                                    <asp:CheckBoxList ID="chklst_editOp" CssClass="checklist" runat="server"></asp:CheckBoxList>
                                </div>
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
                        <div class="modal-body">
                            <asp:Label id="lbl_confMensaje" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btn_confEliminar" CssClass="btn btn-success" OnClick="btn_confEliminar_Click" runat="server">
                <span class="glyphicon glyphicon-ok" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="btn_confActivar" CssClass="btn btn-success" OnClick="btn_confActivar_Click" runat="server">
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
                    $('#<%=txt_editNombre.ClientID%>').val() == null) {
                    showAlertClass('guardar', 'warn_nombreVacio');
                    return false;
                }
                if ($('#<%=txt_editApellido.ClientID%>').val() == '' ||
                    $('#<%=txt_editApellido.ClientID%>').val() == null) {
                    showAlertClass('guardar', 'warn_apellidoVacio');
                    return false;
                }
                if ($('#<%=txt_editRut.ClientID%>').val() == '' ||
                    $('#<%=txt_editRut.ClientID%>').val() == null) {
                    showAlertClass('guardar', 'warn_rutVacio');
                    return false;
                }
                if ($('#<%=txt_editUsername.ClientID%>').val() == '' ||
                    $('#<%=txt_editUsername.ClientID%>').val() == null) {
                    showAlertClass('guardar', 'warn_usernameVacio');
                    return false;
                }
                if ($('#<%=txt_editPassword.ClientID%>').val() == '' ||
                    $('#<%=txt_editPassword.ClientID%>').val() == null) {
                    showAlertClass('guardar', 'warn_passwordVacio');
                    return false;
                }
                if ($('#<%=ddl_editTipo.ClientID%>').val() == '' ||
                    $('#<%=ddl_editTipo.ClientID%>').val() == '0' ||
                    $('#<%=ddl_editTipo.ClientID%>').val() == null) {
                    showAlertClass('guardar', 'warn_tipoVacio');
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
