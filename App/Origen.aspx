<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Origen.aspx.cs" Inherits="App_Origen" %>

<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="server">
    <div class="col-xs-12 separador"></div>
    <h2>Maestro Origen</h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Filtro" runat="server">
    <div class="col-xs-12 separador"></div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-xs-1 text-right">
                Nombre
            </div>
            <div class="col-xs-1">
                <asp:TextBox ID="txt_buscarNombre" CssClass="form-control" runat="server" />
            </div>
            <div class="col-xs-1 text-right">
                Region
            </div>
            <div class="col-xs-1">
                <asp:DropDownList ID="ddl_buscarRegion" OnSelectedIndexChanged="ddl_buscarRegion_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                    <asp:ListItem Value="0">Todos...</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-xs-1 text-right">
                Ciudad
            </div>
            <div class="col-xs-1">
                <asp:DropDownList ID="ddl_buscarCiudad" OnSelectedIndexChanged="ddl_buscarCiudad_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                    <asp:ListItem Value="0">Todos...</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-xs-1 text-right">
                Comuna
            </div>
            <div class="col-xs-1">
                <asp:DropDownList ID="ddl_buscarComuna" CssClass="form-control" runat="server">
                    <asp:ListItem Value="0">Todos...</asp:ListItem>
                </asp:DropDownList>
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
                                <asp:LinkButton ID="btn_modificar" CssClass="btn btn-warning btn-xs" CommandArgument='<%#Eval("ID")%>' CommandName="EDITAR" runat="server">
                                    <span class="glyphicon glyphicon-edit" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_eliminar" CssClass="btn btn-danger btn-xs" CommandArgument='<%#Eval("ID")%>' CommandName="ELIMINAR" runat="server">
                                    <span class="glyphicon glyphicon-remove" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ID_PE" SortExpression="ID_PE" HeaderText="Id_pe" />
                        <asp:BoundField DataField="NOMBRE_PE" SortExpression="NOMBRE_PE" HeaderText="Nombre" />
                        <asp:BoundField DataField="DIRECCION_PE" SortExpression="DIRECCION_PE" HeaderText="Dirección" />
                        <asp:BoundField DataField="COMU_NOMBRE" SortExpression="COMU_NOMBRE" HeaderText="Comuna" />
                        <asp:BoundField DataField="CIUD_NOMBRE" SortExpression="CIUD_NOMBRE" HeaderText="Ciudad" />
                        <asp:BoundField DataField="REGI_NOMBRE" SortExpression="REGI_NOMBRE" HeaderText="Región" />
                        <asp:BoundField DataField="LAT_PE" SortExpression="LAT_PE" HeaderText="Latitud" />
                        <asp:BoundField DataField="LON_PE" SortExpression="LON_PE" HeaderText="Longitud" />
                        <asp:BoundField DataField="RADIO_PE" SortExpression="RADIO_PE" HeaderText="Radio_pe" />
                        <asp:BoundField DataField="IS_POLIGONO" SortExpression="IS_POLIGONO" HeaderText="Is_poligono" />
                        <asp:BoundField DataField="FH_CREA" SortExpression="FH_CREA" HeaderText="FH Creación" />
                        <asp:BoundField DataField="FH_UPDATE" SortExpression="FH_UPDATE" HeaderText="FH Ult Modificación" />
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Modals" runat="server">
    <div class="modal fade" id="modalEdit" data-backdrop="static" role="dialog">
        <div class="modal-dialog" style="width: 90%">
            <div class="modal-content">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">Datos Origen
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDjAnu30d80TbLCujKOmvnMKcEj2GI5H3o&sensor=false&lenguage=es&v=3.20"></script>
                            <script src="../Scripts/origen.js" type="text/javascript"></script>
                            <div class="col-xs-8">
                                <div class="col-xs-2">
                                    Id Pe
                                    <br />
                                    <asp:TextBox ID="txt_editIdPe" CssClass="form-control" runat="server" />
                                </div>
                                <div class="col-xs-4">
                                    Nombre
                                    <br />
                                    <asp:TextBox ID="txt_editNombre" CssClass="form-control" runat="server" />
                                </div>
                                <div class="col-xs-2">
                                    Radio
                                    <br />
                                    <asp:TextBox ID="txt_editRadio" CssClass="form-control input-number" runat="server" />
                                </div>
                                <div class="col-xs-2">
                                    Poligono
                                    <br />
                                    <asp:CheckBox ID="chk_editPoligono" runat="server" />
                                </div>
                                <div class="col-xs-2">
                                    Operación
                                    <br />
                                    <asp:DropDownList ID="ddl_editOpe" CssClass="form-control" runat="server" />
                                </div>
                                <div class="col-xs-4">
                                    Región
                                    <br />
                                    <asp:DropDownList ID="ddl_editRegion" OnSelectedIndexChanged="ddl_editRegion_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0">Seleccione...</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xs-4">
                                    Ciudad
                                    <br />
                                    <asp:DropDownList ID="ddl_editCiudad" OnSelectedIndexChanged="ddl_editCiudad_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0">Seleccione...</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xs-4">
                                    Comuna
                                    <br />
                                    <asp:DropDownList ID="ddl_editComu" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0">Seleccione...</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xs-12 separador"></div>
                                <div class="col-xs-6">
                                    Direccion
                                    <br />
                                    <asp:TextBox ID="txt_editDireccion" CssClass="form-control" runat="server" />
                                </div>
                                <div class="col-xs-2">
                                    <br />
                                    <asp:LinkButton ID="btn_editLatLon" CssClass="btn btn-info" OnClick="btn_editLatLon_Click" runat="server">
                                        <span class="glyphicon glyphicon-map-marker" />
                                    </asp:LinkButton>
                                </div>
                                <div class="col-xs-2">
                                    Latitud
                                    <br />
                                    <asp:TextBox ID="txt_editLat" ClientIDMode="Static" Enabled="false" CssClass="form-control" runat="server" />
                                </div>
                                <div class="col-xs-2">
                                    Longitud
                                    <br />
                                    <asp:TextBox ID="txt_editLon" ClientIDMode="Static" Enabled="false" CssClass="form-control" runat="server" />
                                </div>
                            </div>
                            <div class="col-xs-4" style="height: 65vh" id="map">
                            </div>
                            <div class="col-xs-12 separador"></div>
                            <div class="col-xs-12" style="text-align: center">
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
<asp:Content ID="Content6" ContentPlaceHolderID="ocultos" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_idOrigen" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="scripts" runat="server">
    <script>
        function mapa() {
            var mapa = document.getElementById('map');
            var nombre = $("#<%=txt_editNombre.ClientID%>").val();
            var lat = ($('#txt_editLat').val() == '') ? undefined : parseFloat($('#txt_editLat').val().replace(',','.'));
            var lon = ($('#txt_editLon').val() == '') ? undefined : parseFloat($('#txt_editLon').val().replace(',','.'));
            cargamapa(8, mapa, lat, lon);
            insertarMarcador(lat, lon, 'icon_pe.png', nombre);
        }
        function EndRequestHandler1(sender, args) {
            $('#<%=btn_editGuardar.ClientID%>').click(function (e) {
                if ($("#<%=txt_editIdPe.ClientID%>").val() == '' ||
                    $("#<%=txt_editIdPe.ClientID%>").val() == null) {
                    showAlertClass('guardar', 'warn_idPevacio');
                    return false;
                }
                if ($("#<%=txt_editNombre.ClientID%>").val() == '' ||
                    $("#<%=txt_editNombre.ClientID%>").val() == null) {
                    showAlertClass('guardar', 'warn_nombrevacio');
                    return false;
                }
                if ($("#<%=txt_editDireccion.ClientID%>").val() == '' ||
                    $("#<%=txt_editDireccion.ClientID%>").val() == null) {
                    showAlertClass('guardar', 'warn_direccionvacio');
                    return false;
                }
                if ($("#<%=txt_editLat.ClientID%>").val() == '' ||
                    $("#<%=txt_editLat.ClientID%>").val() == null ||
                    $("#<%=txt_editLon.ClientID%>").val() == '' ||
                    $("#<%=txt_editLon.ClientID%>").val() == null) {
                    showAlertClass('guardar', 'warn_latlonvacio');
                    return false;
                }
                if ($("#<%=ddl_editOpe.ClientID%>").val() == '' ||
                    $("#<%=ddl_editOpe.ClientID%>").val() == null ||
                    $("#<%=ddl_editOpe.ClientID%>").val() == '0') {
                    showAlertClass('guardar', 'warn_opevacio');
                    return false;
                }
                if ($("#<%=ddl_editRegion.ClientID%>").val() == '' ||
                    $("#<%=ddl_editRegion.ClientID%>").val() == null ||
                    $("#<%=ddl_editRegion.ClientID%>").val() == '0') {
                    showAlertClass('guardar', 'warn_regivacio');
                    return false;
                }
                if ($("#<%=ddl_editCiudad.ClientID%>").val() == '' ||
                    $("#<%=ddl_editCiudad.ClientID%>").val() == null ||
                    $("#<%=ddl_editCiudad.ClientID%>").val() == '0') {
                    showAlertClass('guardar', 'warn_ciudadvacio');
                    return false;
                }
                if ($("#<%=ddl_editComu.ClientID%>").val() == '' ||
                    $("#<%=ddl_editComu.ClientID%>").val() == null ||
                    $("#<%=ddl_editComu.ClientID%>").val() == '0') {
                    showAlertClass('guardar', 'warn_comuvacio');
                    return false;
                }
            });
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler1);
    </script>
</asp:Content>
