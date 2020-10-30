<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.Master" AutoEventWireup="true" CodeFile="Pre_Rutas.aspx.cs" Inherits="App_Pre_Rutas" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="server">
    <div class="col-xs-12 separador"></div>
    <h2>PROPUESTA RUTAS</h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Filtro" runat="server">
    <div class="col-xs-12 separador"></div>
    <asp:UpdatePanel ID="up_reload" runat="server">
        <ContentTemplate>
            <div class="col-xs-9 oculta">
                <div class="col-xs-1">
                    Region
                </div>
                <div class="col-xs-2">
                    <asp:DropDownList ID="ddl_buscarRegion" OnSelectedIndexChanged="ddl_buscarRegion_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                        <asp:ListItem Value="0">Todos...</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-1">
                    Ciudad
                </div>
                <div class="col-xs-2">
                    <asp:DropDownList ID="ddl_buscarCiudad" OnSelectedIndexChanged="ddl_buscarCiudad_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                        <asp:ListItem Value="0">Todos...</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-1">
                    Comuna
                </div>
                <div class="col-xs-2">
                    <asp:DropDownList ID="ddl_buscarComuna" CssClass="form-control" runat="server">
                        <asp:ListItem Value="0">Todos...</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-xs-3">
                <asp:Label ID="lbl_reload" runat="server" />
            </div>
            <div class="col-xs-12 separador"></div>
            <div class="col-xs-1 text-right">
                Fecha
            </div>
            <div class="col-xs-1">
                <asp:TextBox ID="txt_buscarFecha" runat="server" CssClass="form-control input-fecha" />
            </div>
            <div class="col-xs-1 text-right">
                Hora Salida
            </div>
            <div class="col-xs-1">
                <asp:DropDownList ID="ddl_buscarHorario" CssClass="form-control" runat="server" ClientIDMode="Static">
                    <asp:ListItem Value="0">Todos...</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-xs-1 text-right">
                N° Ruta
            </div>
            <div class="col-xs-1">
                <asp:TextBox ID="txt_buscarNro" runat="server" AutoCompleteType="Search" CssClass="form-control" />
            </div>
            <div class="col-xs-1 text-right">
                N° envío
            </div>
            <div class="col-xs-1">
                <asp:TextBox ID="txt_buscaenvio" runat="server" AutoCompleteType="Search" CssClass="form-control" />
            </div>
            <div class="col-xs-2">
                <asp:LinkButton ID="btn_buscar" runat="server" CssClass="btn btn-primary" OnClick="btn_buscar_Click" ToolTip="Buscar">
      <span class="glyphicon glyphicon-search" />
                </asp:LinkButton>
                <asp:LinkButton ID="btn_nuevo" runat="server" CssClass="btn btn-success" OnClick="btn_nuevo_Click" ToolTip="Nuevo">
      <span class="glyphicon glyphicon-plus" />
                </asp:LinkButton>
                <asp:LinkButton ID="btn_enviar" runat="server" CssClass="btn btn-info" OnClick="btn_enviar_Click" ToolTip="Enviar rutas">
            <span class="glyphicon glyphicon-send" /> 
                </asp:LinkButton>
                <asp:LinkButton ID="btn_pdf" runat="server" CssClass="btn btn-info" OnClick="btn_pre_pdf_click" ToolTip="pdf rutas">
            <span class="glyphicon glyphicon-file" /></asp:LinkButton>
                <asp:Button CssClass="ocultar" ID="pdf_post" runat="server" OnClick="btn_pdf_click" />

            </div>
            <div class="col-xs-12 separador"></div>
            <div class="col-xs-push-10 col-xs-2">
                Seleccionadas:
            <asp:Label ID="lblgds" runat="server" Text="0" CssClass="tituloCh" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddl_buscarRegion" />
            <asp:AsyncPostBackTrigger ControlID="ddl_buscarCiudad" />
            <asp:PostBackTrigger ControlID="pdf_post" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Contenedor" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:GridView ID="gv_listar" AutoGenerateColumns="false" AllowSorting="true" Width="100%" CssClass="table table-bordered table-hover tablita tab-nopag" runat="server"
                EmptyDataText="No hay registros" OnRowCreated="gv_listar_RowCreated" OnRowCommand="gv_listar_RowCommand" OnSorting="gv_listar_Sorting" DataKeyNames="ID,ID_ORIGEN">
                <Columns>
                    <asp:TemplateField HeaderText="Todas" ShowHeader="False" ItemStyle-Width="1%">
                        <HeaderTemplate>
                            <input type="checkbox" id="chkTodos" class="chkTodos" onclick="checkAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type="checkbox" data-id='<%#Eval("ID")%>' class="gridCB chk" onclick="checkIndividual(this);" runat="server" id="check" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="btn_puntos" CssClass="btn btn-xs btn-primary" CommandArgument='<%#Container.DataItemIndex%>' CommandName="PUNTOS" runat="server">
                                <span class="glyphicon glyphicon-map-marker" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="btn_color" CssClass="btn btn-xs btn-warning" CommandArgument='<%#Eval("ID")%>' CommandName="COLOR" runat="server">
                                <span class="glyphicon glyphicon-tint" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="btn_detalle" CssClass="btn btn-xs btn-info" CommandArgument='<%#Eval("ID")%>' CommandName="DETALLE" runat="server">
                                <span class="glyphicon glyphicon-list" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="btn_eliminar" CssClass="btn btn-xs btn-danger" CommandArgument='<%#Eval("ID")%>' CommandName="ELIMINAR" runat="server">
                                <span class="glyphicon glyphicon-remove" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="NUMERO" SortExpression="NUMERO" HeaderText="NUMERO" />
                    <asp:BoundField DataField="FECHA_DESPACHOEXP" SortExpression="FECHA_DESPACHOEXP" HeaderText="FECHA_DESPACHO" />
                    <asp:BoundField DataField="OBSERVACION" SortExpression="OBSERVACION" HeaderText="OBSERVACION" Visible="false" />
                    <asp:BoundField DataField="RUTA" SortExpression="RUTA" HeaderText="RUTA" Visible="false" />
                    <asp:BoundField DataField="ORIGEN_NOMBRE" SortExpression="ORIGEN_NOMBRE" HeaderText="CD ORIGEN" />
                    <asp:BoundField DataField="VIAJE_ESTADO" SortExpression="VIAJE_ESTADO" HeaderText="VIAJE_ESTADO" />
                    <asp:BoundField DataField="tipo_vehiculo" SortExpression="tipo_vehiculo" HeaderText="TIPO VEHICULO" />
                    <asp:BoundField DataField="TRAI_PLACA" SortExpression="TRAI_PLACA" HeaderText="VEHICULO" />
                    <asp:BoundField DataField="TRAC_PLACA" SortExpression="TRAC_PLACA" HeaderText="TRACTO" />
                    <asp:BoundField DataField="COND_NOMBRE" SortExpression="COND_NOMBRE" HeaderText="COND" />
                    <asp:BoundField DataField="ENVIO" SortExpression="ENVIO" HeaderText="ENVIO" />
                    <asp:BoundField DataField="HORARIO" SortExpression="HORARIO" HeaderText="HORARIO" />
                    <asp:BoundField DataField="DURACION" SortExpression="DURACION" HeaderText="DURACION" />
                    <asp:BoundField DataField="puntos" SortExpression="puntos" HeaderText="PUNTOS" />
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Modals" runat="server">
    <div style="display: none">
        <asp:Panel ID="pnlReport" runat="server" Visible="false">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="BarCode 128" Font-Size="8pt"
                InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="BarCode 128" WaitMessageFont-Size="14pt">
                <LocalReport ReportPath="Reporte\Pre_Rutas.rdlc" EnableExternalImages="True">
                </LocalReport>
            </rsweb:ReportViewer>
        </asp:Panel>
    </div>
    <div class="modal fade" id="modalPuntos" data-backdrop="static" role="dialog">
        <div class="modal-dialog" style="width: 90%">
            <div class="modal-content">
                <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <div class="col-xs-2">
                                <h4 class="modal-title">PROPUESTA RUTA
                                </h4>
                            </div>
                            <asp:UpdatePanel UpdateMode="Always" runat="server" ID="act_cambia">
                                <ContentTemplate>
                                    <div class="col-xs-1">
                                        <asp:DropDownList ID="ddl_puntosCambiarPreruta" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_puntosCambiarPreruta_SelectedIndexChanged" ForeColor="Blue"></asp:DropDownList>
                                    </div>
                                    <div class="col-xs-2">
                                        <asp:Label ID="lbl_puntoTracto" runat="server" />
                                    </div>
                                    <div class="col-xs-2">
                                        <asp:Label ID="lbl_puntoTrailer" runat="server" />
                                    </div>
                                    <div class="col-xs-2">
                                        <asp:Label ID="lbl_puntoConductor" runat="server" />
                                    </div>
                                    <div class="col-xs-2">
                                        <asp:Label ID="lbl_puntoSalida" runat="server" ClientIDMode="Static" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDjAnu30d80TbLCujKOmvnMKcEj2GI5H3o&sensor=false&lenguage=es&v=3.20"></script>
                            <script src="../Scripts/ruteador.js" type="text/javascript"></script>
                            <div class="col-xs-7" style="height: 65vh" id="map">
                            </div>
                            <div class="col-xs-5">
                                <div class="col-xs-6">
                                    Punto seleccionado
                                </div>
                                <div class="col-xs-3">
                                    <%--<asp:DropDownList ID="ddl_puntoNombre" ClientIDMode="Static" runat="server" />--%>
                                    <%--<telerik:RadComboBox ID="ddl_puntoNombre" OnClientSelectedIndexChanged="ddl_puntoNombre_SelectedIndexChanged" ClientIDMode="Static" AllowCustomText="true" MarkFirstMatch="true" runat="server" />--%>

                                    <asp:UpdatePanel UpdateMode="Always" runat="server" ID="UpdatePanel2">
                                        <ContentTemplate>
                                            <telerik:RadComboBox ID="ddl_puntoNombre" OnClientSelectedIndexChanged="ddl_puntoNombre_SelectedIndexChanged" ClientIDMode="Static" AllowCustomText="true" MarkFirstMatch="true" runat="server" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-xs-12 separador"></div>
                                <div id="tbl_puntos"></div>
                                <div class="col-xs-12 separador"></div>
                                <div class="col-xs-12 text-center" style="text-align: center">
                                    <asp:Label ID="txt_cant_punt" ClientIDMode="Static" runat="server" Text="Cant Puntos" Style="float: left"></asp:Label>
                                    <asp:LinkButton ID="btn_puntosGuardar" OnClick="btn_puntosGuardar_Click" CssClass="btn btn-success" runat="server">
                                        <span class="glyphicon glyphicon-floppy-disk" />
                                    </asp:LinkButton>
                                    <button id="btn_puntosVehiculo" type="button" class="btn btn-info" data-toggle="modal" data-target="#modalVehiculo">
                                        <span class="glyphicon glyphicon-list" />
                                    </button>
                                    <a href="#" onclick="javascript:mostrarRuta()" style="float: right">Mostrar/Ocultar Ruta</a>
                                    <a href="#" onclick="javascript:mostrarPoligono()" style="float: right">Mostrar/Ocultar Polígono</a>
                                </div>
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
    <div class="modal fade" id="modalVehiculo" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Asignar vehículos
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow-y: auto;">
                            <div class="col-xs-4">
                                Tracto
                                <br />
                                <telerik:RadComboBox ID="ddl_vehiculoTracto" OnClientSelectedIndexChanged="ddl_vehiculoTracto_SelectedIndexChanged" AllowCustomText="true" MarkFirstMatch="true" runat="server">
                                </telerik:RadComboBox>
                            </div>
                            <div class="col-xs-4">
                                Tipo Vehículo
                                <br />
                                <telerik:RadComboBox ID="ddl_vehiculoTipo" OnSelectedIndexChanged="ddl_vehiculoTipo_SelectedIndexChanged" runat="server" AutoPostBack="true">
                                </telerik:RadComboBox>
                            </div>
                            <div class="col-xs-4">
                                Vehículo
                                <br />
                                <telerik:RadComboBox ID="ddl_vehiculoTrailer" OnClientSelectedIndexChanged="ddl_vehiculoTrailer_SelectedIndexChanged" AllowCustomText="true" MarkFirstMatch="true" runat="server">
                                </telerik:RadComboBox>
                            </div>
                            <div class="col-xs-4">
                                Conductor
                                <br />
                                <telerik:RadComboBox ID="ddl_vehiculoConductor" OnClientSelectedIndexChanged="ddl_vehiculoConductor_SelectedIndexChanged" AllowCustomText="true" MarkFirstMatch="true" runat="server">
                                </telerik:RadComboBox>
                            </div>
                            <div id="dv_detalle" runat="server">
                                <div class="col-xs-12 separador"></div>
                                <div class="col-xs-12 text-center">
                                    <asp:LinkButton ID="btn_vehiculoGuardar" OnClick="btn_vehiculoGuardar_Click" CssClass="btn btn-success" runat="server">
                                        <span class="glyphicon glyphicon-floppy-disk" />
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn <%=(string.IsNullOrEmpty(hf_idRuta.Value)) ? "btn-success" : "btn-danger"%>" data-dismiss="modal">
                                <span class="glyphicon <%=(string.IsNullOrEmpty(hf_idRuta.Value)) ? "glyphicon-ok" : "glyphicon-remove"%>" />
                            </button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
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
    <div class="modal fade" id="modalenviar" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>
                                <asp:Label ID="lbl_titulo_enviar" runat="server" />
                            </h4>
                        </div>
                        <div class="modal-body" style="height: 70px">
                            <asp:Label ID="Label2" runat="server" />
                            <div class="col-xs-12">
                                <asp:CheckBox runat="server" ID="chk_archivar" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btn_confEnviar" CssClass="btn btn-success" OnClick="btn_confEnviar_Click" runat="server">
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
    <div class="modal fade" id="modalColor" data-backdrop="static" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">Color Ruta
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <div class="col-xs-12">
                                Color
                                <asp:TextBox ID="txt_editColor" CssClass="form-control color" runat="server" />
                                <div id="colorpicker"></div>
                            </div>
                            <div class="col-xs-12 separador">
                            </div>
                            <div class="col-xs-12" style="text-align: center">
                                <asp:LinkButton ID="btn_colorGuardar" runat="server" OnClick="btn_colorGuardar_Click" CssClass="btn btn-success">
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

</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ocultos" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_todos" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hf_origenes" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hf_puntosruta" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hf_idRuta" runat="server" />
            <asp:HiddenField ID="hf_idPunto" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hf_origen" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hseleccionado" runat="server" />
            <asp:HiddenField ID="respuesta_direcction" ClientIDMode="Static" runat="server" />
            <asp:Button ID="btn_exportarExcel" runat="server" OnClick="btn_exportarExcel_Click" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_exportarExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="scripts" runat="server">
    <style>
        .sel-between > td {
            height: 0px;
            border: 0px;
            padding: 0px !important;
        }

        .sel-between {
        }

            .sel-between.enabled > td {
                height: 2px;
                border: 1px;
                padding: 1px !important;
            }

            .sel-between.enabled:hover {
                background-color: red;
            }
    </style>
    <script type="text/javascript" src="../Scripts/farbtastic.js"></script>
    <link rel="stylesheet" href="../Scripts/farbtastic.css" type="text/css" />
    <script type="text/javascript">
        var puntosTodos;
        var puntosRuta;
        var puntosOrigenes;

        // DropDownList
        function ddl_vehiculoConductor_SelectedIndexChanged(sender, args) {
            if (!args.get_item()) {
                sender.findItemByValue('0').select();
                showAlertClass('guardar', 'warn_conductorNoExiste');
                return false;
            }
        }
        function ddl_vehiculoTracto_SelectedIndexChanged(sender, args) {
            if (!args.get_item()) {
                sender.findItemByValue('0').select();
                showAlertClass('guardar', 'warn_tractoNoExiste');
                return false;
            }
        }
        function ddl_vehiculoTrailer_SelectedIndexChanged(sender, args) {
            if (!args.get_item()) {
                sender.findItemByValue('0').select();
                showAlertClass('guardar', 'warn_trailerNoExiste');
                return false;
            }
        }
        function ddl_puntoNombre_SelectedIndexChanged(sender, args) {
            const selectedItem = args.get_item();
            if (!selectedItem) {
                sender.clearSelection();
                $('#hf_idPunto').val('');
                $('.sel-between').removeClass('enabled');
                showAlertClass('guardar', 'warn_pedidoNoExiste');
                return false;
            }
            if (selectedItem.get_index() < 1) {
                $('#hf_idPunto').val('');
                $('.sel-between').removeClass('enabled');
            }
            else {
                const id = parseInt(selectedItem.get_value());
                centrarPunto(id);
                const m = markers["MARKERS"][buscarMarcadorXId(id)];
                $('#hf_idPunto').val(id);
                $('.sel-between').addClass('enabled');
            }
        }
        // PageLoad
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler1);
        function EndRequestHandler1(sender, args) {
            setTimeout(tabla2, 100);
            $('#colorpicker').farbtastic('#<%=txt_editColor.ClientID%>');
            $('#<%=btn_colorGuardar.ClientID%>').click(function () {
                if ($('#<%=txt_editColor.ClientID%>').val() == '' ||
                    !$('#<%=txt_editColor.ClientID%>').val()) {
                    showAlertClass('guardar', 'warn_colorVacio');
                    return false;
                }
            });
            $('#<%= btn_enviar.ClientID%>').click(function () {
                $("#<%= hseleccionado.ClientID %>").val(ids.toString());
                if ($("#<%=hseleccionado.ClientID %>").val() == '') {
                    showAlertClass("enviar", "warn_noSeleccionados");
                    return false;
                }
            });

            $('#<%= btn_pdf.ClientID%>').click(function () {
                $("#<%= hseleccionado.ClientID %>").val(ids.toString());
                if ($("#<%=hseleccionado.ClientID %>").val() == '') {
                    showAlertClass("enviar", "warn_noSeleccionados");
                    return false;
                }
            });
            //$('#ddl_puntoNombre').change(function (e) {
            //    debugger;
            //});

        }

        var calcDataTableHeight = function () {
            return $(window).height() - $("#scrolls").offset().top - 100;
        };
        function reOffset1() {

            $('div.dataTables_scrollBody').height(calcDataTableHeight());
        }
        window.onresize = function (e) {
            reOffset1();
        }
        function tabla2() {
            if ($('#gv_puntos')[0] != undefined && $('#gv_puntos')[0].rows.length > 1) {
                $('#gv_puntos').DataTable({
                    "scrollY": "38vh",
                    "scrollX": true,
                    "scrollCollapse": true,
                    "paging": false,
                    "ordering": false,
                    "searching": false,
                    "lengthChange": false,
                    "info": false
                });
            }
            $('div.dataTables_scrollBody').scrollTop(pageScrollPos);
            $('div.dataTables_scrollBody').scrollLeft(pageScrollPosleft);
        }
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);

        var pageScrollPos = 0;
        var pageScrollPosleft = 0;

        function BeginRequestHandler(sender, args) {
            pageScrollPos = $('div.dataTables_scrollBody').scrollTop();
            pageScrollPosleft = $('div.dataTables_scrollBody').scrollLeft();
        }
        function mapa(nuevo) {
            limpiarWaypoints();
            puntosTodos = JSON.parse($('#hf_todos').val());
            puntosRuta = JSON.parse($('#hf_puntosruta').val());
            puntosOrigenes = JSON.parse($('#hf_origenes').val());
            const json_origen = JSON.parse($('#hf_origen').val());
            setOrigen(json_origen.LAT_PE, json_origen.LON_PE, "marker_blue.png", json_origen.NOMBRE_PE, '0');
            setDestino(json_origen.LAT_PE, json_origen.LON_PE, "marker_blue.png", json_origen.NOMBRE_PE, '0');
            if (nuevo) {
                json_origen.PERU_LLEGADA = $('#<%=ddl_buscarHorario.ClientID%>  option:selected').text();
            }

            const mapObject = document.getElementById('map');
            cargamapa(12, mapObject);
            $('#tbl_puntos').html(jsonToTable(json_origen));
            limpiarMarcadores();

            puntosTodos.map((o) => {
                insertarMarcador(o.PERU_ID, o.PERU_LATITUD, o.PERU_LONGITUD, 'icon_pedido.png', o.PERU_CODIGO, crearInfoWindow(o));
            });
            puntosOrigenes.map((o) => {
                insertarOrigen(o.ID_PE, o.LAT_PE, o.LON_PE, 'marker_blue.png', o.NOMBRE_PE, '0');
            });
            if (!nuevo) {
                puntosRuta.map((o) => {
                    insertarPuntoRuta(o.PERU_ID, NaN, 'marker_red.png');
                });
                crearPoligono();
                crearRuta();
                var bounds = new google.maps.LatLngBounds();
                bounds.extend(origen.position);
                waypoints["WAYPOINTS"].map((o) => {
                    bounds.extend(o.location);
                });
                setTimeout(function () { map.fitBounds(bounds) }, 500);
            }
        }
        function crearInfoWindow(o) {
            const contenido = `<div id="content">
                <p>
                Código: ${o.PERU_CODIGO}
<br />
                Número: ${o.PERU_NUMERO}
<br />
                Hora: ${o.HORA_COD}
<br />
                Dirección: ${o.PERU_DIRECCION}
                </p>
                </div>`;
            return contenido;
        }
        function jsonToTable(json_origen) {
            var tiempo0;
            var tiempoFIN;
            if (!json_origen) {
                json_origen = JSON.parse($('#hf_origen').val());
            }

            var i1;
            tiempo0 = moment.duration(json_origen.PERU_LLEGADA);
            var output = `<table id="gv_puntos" style="width:100%" class="table table-border table-hover tablita">
                            <thead>
                            <tr style="white-space:normal">
                            <th>
                            <span class="glyphicon glyphicon-move" />
                            </th>
                            <th>
                            <span class="glyphicon glyphicon-remove text-danger" />
                            </th>
                            <th>
                            <span class="glyphicon glyphicon-flag" />
                            </th>
                            <th>
                            Cliente
                            </th>
                            <th>
                            Dirección
                            </th>
                            <th>
                            Llegada
                            </th>
                            </tr>
                            <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            <a class="btn btn-xs btn-primary" style="width:22px" onclick="centrarLatLon(${json_origen.LAT_PE},${json_origen.LON_PE});">0</a>
                            </td>
                            <td>
                            ${json_origen.NOMBRE_PE}
                            </td>
                            <td>
                            ${json_origen.DIRECCION_PE}
                            </td>
                            <td id="t_origen">
                            ${json_origen.PERU_LLEGADA}
                            </td>
                            </tr>
                            </thead>
                            <tbody>
                            <tr onclick="moverPunto(0);refrescar()" class="sel-between">
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            </tr>`;
            puntosRuta.map((o, i) => {
                output += '<tr style="white-space:normal">';
                output += '<td>';
                if (i > 0) {
                    output += `<a href="#" onclick="moverPunto(${(i - 1)},${o.PERU_ID});refrescar();" data-id="${o.PERU_ID}">
                                <span style="font-size:small" class="glyphicon glyphicon-menu-up"></span>
                                </a>`;
                }
                if (i != puntosRuta.length - 1) {
                    output += `<a href="#" onclick="moverPunto(${(i + 1)},${o.PERU_ID});refrescar();">
                                <span style="font-size:small" class="glyphicon glyphicon-menu-down"></span >
                                </a>`;
                }
                output += '</td>';
                if (puntosRuta.length > 1) {
                    output += `<td>
                                <a href="#" onclick="quitarPunto(${o.PERU_ID});refrescar();"><span style="font-size:small" class="glyphicon glyphicon-remove text-danger" /></a>
                                </td>`;
                }
                else {
                    output += `<td>
                                </td>`;
                }
                output += `<td class="letra">
                            <a class="btn btn-xs btn-primary" style="width:22px" onclick="selecciona(${o.PERU_ID});centrarLatLon(${o.PERU_LATITUD},${o.PERU_LONGITUD});">${(i + 1).toString()}</a>
                            </td>
                            <td onclick="selecciona(${o.PERU_ID});">
                            ${o.PERU_CODIGO}
                            </td>
                            <td>
                            ${o.PERU_DIRECCION}
                            </td>
                            <td id="t_${o.PERU_CODIGO}">
                            ${o.PERU_LLEGADA}
                            </td>
                            </tr>
                            <tr onclick="moverPunto(${(i + 1)});refrescar();" class="sel-between">
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            </tr>`;
                tiempoFIN = o.PERU_LLEGADA;
                i1 = i;
            });
            output += `</tbody>
                        <tfoot>
                        <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        <a class="btn btn-xs btn-primary" style="width:22px" onclick="centrarLatLon(${json_origen.LAT_PE}, ${json_origen.LON_PE});">0</a>
                        </td>
                        <td>
                        ${json_origen.NOMBRE_PE}
                        </td>
                        <td>
                        ${json_origen.DIRECCION_PE}
                        </td>
                        <td>&nbsp;
                        </td>
                        </tr>
                        </tfoot>
                        </table>`
            cantidad_puntos(i1 + 1);

            if (tiempoFIN != undefined) {
                const tiempoviaje = tiempoFIN.split(':');
                const tiempo = moment.duration(tiempo0.subtract(parseInt(tiempoviaje[0] * 60) + parseInt(tiempoviaje[1]), 'minutes') * -1);
                $('#<%=lbl_puntoSalida.ClientID%>').text('Duración :' + tiempo.format('HH:mm'));
            }
            return output;
        }
        function moverPunto(pos, id) {
            if (id) $('#hf_idPunto').val(id);
            if ($('#hf_idPunto').val() == '') return false;
            var id = parseInt($('#hf_idPunto').val());
            var punto = buscarPuntosTodos(id);
            var elim = quitarPunto(id);
            if (elim > -1 && pos > (elim + 1)) pos = pos - 1;
            insertarPuntoRuta(id, pos, 'marker_red.png');
            puntosRuta.splice(pos, 0, punto);
        }
        function quitarPunto(id) {
            var eliminado = -1;
            for (var i = 0; i < puntosRuta.length; i++) {
                if (puntosRuta[i].PERU_ID === id) {
                    puntosRuta.splice(i, 1);
                    eliminado = i;
                    break;
                }
            }
            quitarPuntoRuta(id);
            return eliminado;
        }
        function centrarLatLon(lat, lon) {
            const latLon = new google.maps.LatLng(lat, lon);
            map.setCenter(latLon);
            // map.setZoom(13);
        }
        function centrarPunto(id) {
            const p = buscarPuntosTodos(id);
            centrarLatLon(p.PERU_LATITUD, p.PERU_LONGITUD);
        }
        function refrescar() {
            $('#hf_puntosruta').val(JSON.stringify(puntosRuta));
            $('#tbl_puntos').html(jsonToTable());
            tabla2();
            refrescarRuta();
            refrescarPoligono();
            $('#hf_idPunto').val('');
            $find('ddl_puntoNombre').clearSelection();
            $('.sel-between').removeClass('enabled');
        }
        function buscarPuntosTodos(id) {
            for (var i = 0; i < puntosTodos.length; i++) {
                if (puntosTodos[i].PERU_ID === id) return puntosTodos[i];
            }
            return false;
        }
        function selecciona(id) {

            $('#hf_idPunto').val(id);
            //$('#txt_puntoNombre').val(titulo);
            $find('ddl_puntoNombre').findItemByValue(id.toString()).select();
            $('.sel-between').addClass('enabled');
        }
        var ids = [];
        function checkAll(a) {
            $(".gridCB:enabled").prop('checked', $(a).prop('checked'))
            $(".gridCB:enabled").each(function (index, e) {
                var id = $(e).attr("data-id");
                var index = ids.indexOf(id);
                if ($(a).prop('checked')) {
                    if (index == -1) ids.push(id);
                    $(e).parent().parent().addClass("seleccionado");
                }
                else {
                    if (index != -1) ids.splice(index, 1);
                    $(e).parent().parent().removeClass("seleccionado");
                }
            });
            $("#<%=lblgds.ClientID %>").html($(".gridCB:checked").length);
            console.log(ids);
        }
        function checkIndividual(objeto) {
            var cant = parseInt($("#<%= lblgds.ClientID %>").html());
            $('#chkTodos').prop('checked', ($('.gridCB:checked').length == $('.gridCB:enabled').length));
            var id = $(objeto).attr("data-id");
            if ($(objeto).prop('checked')) {
                ids.push(id);
                $(objeto).parent().parent().addClass("seleccionado");
            }
            else {
                ids.splice(ids.indexOf(id), 1);
                $(objeto).parent().parent().removeClass("seleccionado");
            }
            $("#<%=lblgds.ClientID %>").html($(".gridCB:checked").length);
            console.log(ids);
        }
        function exportar() {
            ids = [];
            setTimeout(timeexporta, 200);
        }
        function timeexporta() {
            $("#<%= btn_exportarExcel.ClientID %>").click();
        }
        function exportarpdf() {
            setTimeout(timeexportapdf, 200);
        }
        function timeexportapdf() {
            $("#<%= pdf_post.ClientID %>").click();
        }

    </script>
</asp:Content>
