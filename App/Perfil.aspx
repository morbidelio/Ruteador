<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Perfil.aspx.cs" Inherits="App_Perfil" %>

<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="server">
    <div class="col-xs-12 separador"></div>
    <h2>Maestro perfil</h2>
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
            <div class="col-xs-push-2 col-xs-8">
                <asp:GridView ID="gv_listar" AutoGenerateColumns="False" AllowSorting="true" Width="100%" CssClass="table table-bordered table-hover tablita tab-nopag" runat="server"
                    EmptyDataText="No hay registros" OnRowCreated="gv_listar_RowCreated" OnRowCommand="gv_listar_RowCommand" OnSorting="gv_listar_Sorting">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton id="btn_modificar" CssClass="btn btn-warning btn-xs" CommandArgument='<%#Eval("USTI_ID")%>' CommandName="EDITAR" runat="server">
                                    <span class="glyphicon glyphicon-edit" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>  
                                <asp:LinkButton id="btn_eliminar" CssClass="btn btn-danger btn-xs" CommandArgument='<%#Eval("USTI_ID")%>' CommandName="ELIMINAR" runat="server">
                                    <span class="glyphicon glyphicon-remove" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="USTI_NOMBRE" SortExpression="USTI_NOMBRE" HeaderText="Nombre" />
                        <asp:BoundField DataField="USTI_DESC" SortExpression="USTI_DESC" HeaderText="Descripción" />
                        <asp:BoundField DataField="USTI_NIVEL_PERMISOS" SortExpression="USTI_NIVEL_PERMISOS" HeaderText="Nivel Permisos" />
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Modals" runat="server">
    <div class="modal fade" id="modalEdit" data-backdrop="static" role="dialog">
        <div class="modal-dialog" style="width:90%;">
            <div class="modal-content">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">
                                Datos Parámetro
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <div class="col-xs-2">
                                Nombre
                                <br />
                                <asp:TextBox id="txt_editNombre" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-xs-4">
                                Descripción
                                <br />
                                <asp:TextBox id="txt_editDescripcion" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-xs-2">
                                Nivel permisos
                                <br />
                                <asp:TextBox id="txt_editPermisos" CssClass="form-control input-number" runat="server" />
                            </div>
                            <div class="col-xs-12 separador"></div>
                            <div id="dv_editMenu" class="col-xs-12">
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
                            <h4>Eliminar perfil
                            </h4>
                        </div>
                        <div class="modal-body">
                            Se eliminará el perfil seleccionado ¿Está seguro?
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
            <asp:HiddenField ID="hf_menuId" runat="server" />
            <asp:HiddenField ID="hf_json" ClientIDMode="Static" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="scripts" runat="server">
    <script>
        var menu_id = [];
        function EndRequestHandler1(sender, args) {
            crearTabla();
            stringToCheck();
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler1);
        function crearTabla() {
            var json = JSON.parse($('#hf_json').val());
            var output = "<table>";
            output += "<tbody>";
            output += "<tr>";
            for (var i = 0; i < json.length; i++) {
                var hijos = json[i].MENU_HIJOS;
                output += "<td class='tab-check'>";
                output += "<table>";
                if (hijos.length > 0) {
                    output += "<thead>";
                    output += "<th>";
                    output += "<input type='checkbox' data-id='" + json[i]["MENU_ID"] + "' class='chk-padre' disabled /> ";
                    output += "</th>";
                    output += "<th>";
                    output += json[i]["MENU_TITULO"];
                    output += "</th>";
                    output += "</thead>"
                    for (var j = 0; j < hijos.length; j++) {
                        output += "<tr>";
                        output += "<td>";
                        output += "<input data-id='";
                        output += hijos[j]["MENU_ID"];
                        output += "' type='checkbox' onclick='check(this)' class='chk-hijo' data-chk-padre='";
                        output += json[i]["MENU_ID"] + "' />";
                        output += "</td>";
                        output += "<td>";
                        output += hijos[j]["MENU_TITULO"];
                        output += "</td>";
                        output += "</tr>";
                    }
                }
                else {
                    output += "<thead>";
                    output += "<th>";
                    output += "<input type='checkbox' data-id='" + json[i]["MENU_ID"] + "' class='chk-padre' onclick='check(this);' /> ";
                    output += "</th>";
                    output += "<th>";
                    output += json[i]["MENU_TITULO"];
                    output += "</th>";
                    output += "</thead>"
                }
                output += "</table>";
                output += "</td>";
            }
            output += "</tr>";
            output += "</tbody>";
            output += "</table>";
            $('#dv_editMenu').html(output);
        }
        function check(e) {
            var id = $(e).attr('data-id');
            if ($(e).prop('checked')) addId(id);
            else removeId(id);
            if ($(e).hasClass('chk-hijo')) {
                var padre_id = $(e).attr('data-chk-padre');
                var checked = ($('.chk-hijo[data-chk-padre=' + padre_id + ']:checked').length > 0);
                $('.chk-padre[data-id=' + padre_id + ']').prop('checked', checked);
                if (checked) addId(padre_id);
                else removeId(padre_id);
            }
            console.log($('#<%=hf_menuId.ClientID%>').val());
        }
        function checkToString() {
            var output = "";
            $('.chk-padre:checked,.chk-hijo:checked').each(function () {
                if (output != "") {
                    output += ",";
                }
                output += $(this).attr('data-id');
            });
            $('#<%=hf_menuId.ClientID%>').val(output);
            console.log($('#<%=hf_menuId.ClientID%>').val());
        }
        function stringToCheck() {
            menu_id = ($('#<%=hf_menuId.ClientID%>').val() == '') ? [] : $('#<%=hf_menuId.ClientID%>').val().split(',');
            for (var i = 0; i < menu_id.length; i++) {
                $('.chk-padre[data-id=' + menu_id[i] + '],.chk-hijo[data-id=' + menu_id[i] + ']').prop('checked', true);
            }
        }
        function addId(id) {
            var index = menu_id.indexOf(id);
            if (index == -1) {
                menu_id.push(id);
            }
            $('#<%=hf_menuId.ClientID%>').val(menu_id.toString());
        }
        function removeId(id) {
            var index = menu_id.indexOf(id);
            if (index != -1) {
                menu_id.splice(index,1);
            }
            $('#<%=hf_menuId.ClientID%>').val(menu_id.toString());
        }
    </script>
    <style>
        .tab-check{
            color: rgb(114, 179, 255);
            vertical-align:top;
        }
        .tab-check > table > thead > tr > th{
            padding: 0px 5px;
        }
        .tab-check > table > tbody > tr > td{
            font-size: small;
        }
    </style>
</asp:Content>