<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.Master" AutoEventWireup="true" CodeFile="Pre_Rutas.aspx.cs" Inherits="App_Pre_Rutas" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
            <div class="col-xs-2">
                Fecha Desde
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txt_buscarDesde" runat="server" CssClass="form-control input-fecha" />
            </div>
            <div class="col-xs-2">
                Fecha Hasta
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txt_buscarHasta" runat="server" CssClass="form-control input-fecha" />
            </div>
            <div class="col-xs-2">
                Hora Salida
            </div>
            <div class="col-xs-2">
                <asp:DropDownList ID="ddl_buscarHorario" CssClass="form-control" runat="server" ClientIDMode="Static">
                    <asp:ListItem Value="0">Todos...</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-xs-12 separador"></div>
            <div class="col-xs-2">
                N° Ruta
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txt_buscarNro" runat="server" AutoCompleteType="Search" CssClass="form-control" />
            </div>
            <div class="col-xs-2">
                N° envío
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txt_buscaenvio" runat="server" AutoCompleteType="Search" CssClass="form-control" />
            </div>
            <div class="col-xs-2">
                Seleccionadas:
            <asp:Label ID="lblgds" runat="server" Text="0" CssClass="tituloCh" />
            </div>
            <div class="col-xs-2">
                <asp:LinkButton ID="btn_buscar" runat="server" CssClass="btn btn-primary" OnClick="btn_buscar_Click" ToolTip="Buscar">
      <span class="glyphicon glyphicon-search" />
                </asp:LinkButton>
                <asp:LinkButton ID="btn_nuevo" runat="server" CssClass="btn btn-success" OnClick="btn_nuevo_Click" ToolTip="Nuevo">
      <span class="glyphicon glyphicon-plus" />
                </asp:LinkButton>
                <asp:LinkButton ID="btn_enviar" runat="server" CssClass="btn btn-info" OnClick="btn_enviar_Click" ToolTip="Enviar rutas">
            <span class="glyphicon glyphicon-send" /> </asp:LinkButton>
                <asp:LinkButton ID="btn_pdf" runat="server" CssClass="btn btn-info" OnClick="btn_pdf_click" ToolTip="pdf rutas">
            <span class="glyphicon glyphicon-send" /></asp:LinkButton>
                </asp:LinkButton>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddl_buscarRegion" />
            <asp:AsyncPostBackTrigger ControlID="ddl_buscarCiudad" />
            <asp:PostBackTrigger ControlID="btn_pdf" />
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
                            <asp:LinkButton ID="btn_puntos" CssClass="btn btn-xs btn-info" CommandArgument='<%#Container.DataItemIndex%>' CommandName="PUNTOS" runat="server">
                                <span class="glyphicon glyphicon-map-marker" />
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
                    <asp:BoundField DataField="NUMERO" SortExpression="NUMERO" HeaderText="NUMERO" />
                    <asp:BoundField DataField="FH_VIAJE" SortExpression="FH_VIAJE" HeaderText="FH_VIAJE" Visible="false" />
                    <asp:BoundField DataField="OBSERVACION" SortExpression="OBSERVACION" HeaderText="OBSERVACION" Visible="false" />
                    <asp:BoundField DataField="RUTA" SortExpression="RUTA" HeaderText="RUTA" Visible="false" />
                    <asp:BoundField DataField="ORIGEN_NOMBRE" SortExpression="ORIGEN_NOMBRE" HeaderText="CD ORIGEN" />
                    <asp:BoundField DataField="VIAJE_ESTADO" SortExpression="VIAJE_ESTADO" HeaderText="VIAJE_ESTADO" />
                    <asp:BoundField DataField="ENVIO" SortExpression="ENVIO" HeaderText="ENVIO" />
                    <asp:BoundField DataField="HORARIO" SortExpression="HORARIO" HeaderText="HORARIO" />
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
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <div class="col-xs-2">
                                <h4 class="modal-title">PROPUESTA RUTA
                                </h4>
                            </div>
                            <div class="col-xs-2">
                                <asp:UpdatePanel runat="server" ID="act_cambia">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddl_puntosCambiarPreruta" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_puntosCambiarPreruta_SelectedIndexChanged" ForeColor="Blue"></asp:DropDownList>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDjAnu30d80TbLCujKOmvnMKcEj2GI5H3o&sensor=false&lenguage=es&v=3.20"></script>
                            <script src="../Scripts/ruteador.js" type="text/javascript"></script>
                            <div class="col-xs-7" style="height: 65vh" id="map">
                            </div>
                            <div class="col-xs-5">
                                <%--<asp:GridView ID="gv_puntos" CssClass="table table-bordered table-hover tablita" AutoGenerateColumns="false" runat="server"
                                    onrowcommand="gv_puntos_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="PERU_CODIGO" HeaderText="Punto entrega" />
                                        <asp:BoundField DataField="PERU_DIRECCION" HeaderText="Dirección" />
                                    </Columns>
                                </asp:GridView>--%>
                                <div class="col-xs-6">
                                    Punto seleccionado
                                </div>
                                <div class="col-xs-3">
                                    <asp:TextBox ID="txt_puntoNombre" Enabled="false" ClientIDMode="Static" runat="server" />
                                </div>
                                <div class="col-xs-12 separador"></div>
                                <div id="tbl_puntos"></div>
                                <div class="col-xs-12 separador"></div>
                                <div class="col-xs-12" style="text-align: center">
                                    <asp:Label ID="txt_cant_punt" ClientIDMode="Static" runat="server" Text="Cant Puntos" Style="float: left"> </asp:Label>
                                    <button id="btn_puntosGuardar" type="button" class="btn btn-success" data-toggle="modal" data-target="#modalVehiculo">
                                        <span class="glyphicon glyphicon-floppy-disk" />
                                    </button>
                                    <a href="#" onclick="javascript:cambia_direccion()" style="float: right">Mostrar/Ocultar Letras</a>
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
                            <h4>
                                Asignar vehículos
                            </h4>
                        </div>
                        <div class="modal-body" style="height:auto;overflow-y:auto;">
                            <div class="col-xs-4">
                                Tracto
                                <br />
                                <asp:TextBox ID="txt_vehiculoTracto" CssClass="form-control input-mayus" runat="server" />
                            </div>
                            <div class="col-xs-4">
                                Trailer
                                <br />
                                <asp:TextBox ID="txt_vehiculoTrailer" CssClass="form-control input-mayus" runat="server" />
                            </div>
                            <div class="col-xs-4">
                                Conductor
                                <br />
                                <asp:TextBox ID="txt_vehiculoConductor" CssClass="form-control input-mayus" runat="server" />
                            </div>
                            <div class="col-xs-12 separador"></div>
                            <div class="col-xs-12 text-center">
                                <asp:LinkButton ID="btn_guardar" OnClick="btn_guardar_Click" CssClass="btn btn-success" runat="server">
                                    <span class="glyphicon glyphicon-floppy-disk" />
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-dismiss="modal">
                                <span class="glyphicon glyphicon-remove" />
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

</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ocultos" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_trailer" runat="server" />
            <asp:HiddenField ID="hf_tracto" runat="server" />
            <asp:HiddenField ID="hf_conductor" runat="server" />
            <asp:HiddenField ID="hf_todos" runat="server" />
            <asp:HiddenField ID="hf_origenes" runat="server" />
            <asp:HiddenField ID="hf_puntosruta" runat="server" />
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
        .ui-front {
            z-index: 9999999 !important;
        }
        .sel-between > td {
            height: 0px;
            border: 0px;
            padding: 0px !important;
        }

        .sel-between {
        }

        .sel-between.enabled>td {
            height: 2px;
            border: 1px;
            padding: 1px !important;
        }

        .sel-between.enabled:hover {
            background-color: red;
        }
    </style>
    <script type="text/javascript">
        var puntosTodos;
        var puntosRuta;
        var puntosOrigenes;
        function EndRequestHandler1(sender, args) {
            setTimeout(tabla2, 100);
            var jsonTracto = JSON.parse($('#<%=hf_tracto.ClientID%>').val());
            var jsonTrailer = JSON.parse($('#<%=hf_trailer.ClientID%>').val());
            var jsonConductores = JSON.parse($('#<%=hf_conductor.ClientID%>').val());
            var tractos = [];
            var trailers = [];
            var conductores = [];
            jsonTracto.map((o) => {
                tractos.push(o.TRAC_PLACA)
            });
            jsonTrailer.map((o) => {
                trailers.push(o.TRAI_PLACA)
            });
            jsonConductores.map((o) => {
                conductores.push(o.COND_RUT);
            });
            $("#<%=txt_vehiculoTracto.ClientID%>").autocomplete({
                source: tractos,
            });
            $("#<%=txt_vehiculoTracto.ClientID%>").change(function () {
                if (!tractos.find((o) => o === this.value)) {
                    showAlertClass('guardar', 'warn_tractoNoexiste');
                    $("#<%=txt_vehiculoTracto.ClientID%>").val('');
                }
            });
            $("#<%=txt_vehiculoTrailer.ClientID%>").autocomplete({
                source: trailers,
            });
            $("#<%=txt_vehiculoTrailer.ClientID%>").change(function () {
                if (!trailers.find((o) => o === this.value)) {
                    showAlertClass('guardar', 'warn_trailerNoexiste');
                    $("#<%=txt_vehiculoTrailer.ClientID%>").val('');
                }
            });
            $("#<%=txt_vehiculoConductor.ClientID%>").autocomplete({
                source: conductores,
            });
            $("#<%=txt_vehiculoConductor.ClientID%>").change(function () {
                if (!conductores.find((o) => o === this.value)) {
                    showAlertClass('guardar', 'warn_conductorNoexiste');
                    $("#<%=txt_vehiculoConductor.ClientID%>").val('');
                }
            });
<%--            $("#<%=btn_guardar.ClientID%>").click(function () {
                if ($("#<%=txt_vehiculoTrailer.ClientID%>").val() == '' ||
                    !$("#<%=txt_vehiculoTrailer.ClientID%>").val()) {
                    return false;
                }
                if ($("#<%=txt_vehiculoTracto.ClientID%>").val() == '' ||
                    !$("#<%=txt_vehiculoTracto.ClientID%>").val()) {
                    return false;
                }
            });--%>
            $('#<%= btn_enviar.ClientID%>').click(function () {
                $("#<%= hseleccionado.ClientID %>").val(ids.toString());
                if ($("#<%=hseleccionado.ClientID %>").val() == '') {
                    showAlertClass("enviar", "warn_noSeleccionados");
                    return false;
                }
            });
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler1);


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
                    "scrollY": "39vh",
                    "scrollX": true,
                    "scrollCollapse": true,
                    "paging": false,
                    "ordering": false,
                    "searching": false,
                    "lengthChange": false,
                    "info": false
                });
            }
        }

        function mapanuevo() {
            limpiarWaypoints();
            puntosTodos = JSON.parse($('#<%=hf_todos.ClientID%>').val());
            puntosRuta = JSON.parse($('#<%=hf_puntosruta.ClientID%>').val());
            puntosOrigenes = JSON.parse($('#<%=hf_origenes.ClientID%>').val());
            var json_origen = JSON.parse($('#hf_origen').val());
            setCustomOrigen(json_origen["LAT_PE"], json_origen["LON_PE"], "icon_pedido.png", json_origen["NOMBRE_PE"]);
            setCustomDestino(json_origen["LAT_PE"], json_origen["LON_PE"], "icon_pedido.png", json_origen["NOMBRE_PE"]);

            var mapObject = document.getElementById('map');
            cargamapa(12, mapObject);
            $('#tbl_puntos').html(jsonToTable(puntosRuta));
            limpiarMarcadores();
            puntosTodos.map((o) => {
                insertarMarcador(o.PERU_ID, o.PERU_LATITUD, o.PERU_LONGITUD, 'icon_pedido.png', o.PERU_CODIGO);
            });
            puntosOrigenes.map((o) => {
                insertarOrigen(o.ID_PE, o.LAT_PE, o.LON_PE, 'icon_pe.png', o.NOMBRE_PE);
            });

            crearPoligono();
            //crearRuta();
        }




        function mapa2() {
            limpiarWaypoints();
            puntosTodos = JSON.parse($('#<%=hf_todos.ClientID%>').val());
            puntosRuta = JSON.parse($('#<%=hf_puntosruta.ClientID%>').val());
            puntosOrigenes = JSON.parse($('#<%=hf_origenes.ClientID%>').val());
            var json_origen = JSON.parse($('#hf_origen').val());
            setCustomOrigen(json_origen["LAT_PE"], json_origen["LON_PE"], "icon_pedido.png", json_origen["NOMBRE_PE"]);
            setCustomDestino(json_origen["LAT_PE"], json_origen["LON_PE"], "icon_pedido.png", json_origen["NOMBRE_PE"]);

            var mapObject = document.getElementById('map');
            cargamapa(12, mapObject);
            $('#tbl_puntos').html(jsonToTable(puntosRuta));
            limpiarMarcadores();

            puntosTodos.map((o) => {
                insertarMarcador(o.PERU_ID, o.PERU_LATITUD, o.PERU_LONGITUD, 'icon_pedido.png', o.PERU_CODIGO);
            });
            puntosOrigenes.map((o) => {
                insertarOrigen(o.ID_PE, o.LAT_PE, o.LON_PE, 'icon_pe.png', o.NOMBRE_PE);
            });
            puntosRuta.map((o) => {
                insertarPuntoRuta(o.PERU_ID);
            });
            crearPoligono();
            crearRuta();
        }


        function mapa() {
            limpiarWaypoints();
            var json_origen = JSON.parse($('#hf_origen').val());
            setCustomOrigen(json_origen["LAT_PE"], json_origen["LON_PE"], "icon_pedido.png", json_origen["NOMBRE_PE"]);
            setCustomDestino(json_origen["LAT_PE"], json_origen["LON_PE"], "icon_pedido.png", json_origen["NOMBRE_PE"]);
            puntosTodos = JSON.parse($('#<%=hf_todos.ClientID%>').val());
            puntosRuta = JSON.parse($('#<%=hf_puntosruta.ClientID%>').val());
            puntosOrigenes = JSON.parse($('#<%=hf_origenes.ClientID%>').val());
            var mapObject = document.getElementById('map');
            cargamapa(12, mapObject);
            $('#tbl_puntos').html(jsonToTable(puntosRuta));
            if (!hayMarcadores()) {
                puntosTodos.map((o) => {
                    insertarMarcador(o.PERU_ID, o.PERU_LATITUD, o.PERU_LONGITUD, 'icon_pedido.png', o.PERU_CODIGO);
                });
            }
            else {
                refrescarMarcadores();
            }
            puntosRuta.map((o) => {
                insertarPuntoRuta(o.PERU_ID);
            });
            puntosOrigenes.map((o) => {
                insertarOrigen(o.ID_PE, o.LAT_PE, o.LON_PE, 'icon_pe.png', o.NOMBRE_PE);
            });

            crearPoligono();
            crearRuta();
        }
        function jsonToTable() {
            var json_origen = JSON.parse($('#hf_origen').val());
            var output = '<table id="gv_puntos" style="width:100%" class="table table-border table-hover tablita">';
            output += '<thead>';
            output += '<tr>';
            output += '<th>';
            output += '<span class="glyphicon glyphicon-move" />';
            output += '</th>';
            output += '<th>';
            output += '<span class="glyphicon glyphicon-remove text-danger" />';
            output += '</th>';
            output += '<th>';
            output += '<span class="glyphicon glyphicon-flag" />';
            output += '</th>';
            output += '<th>';
            output += 'Cliente';
            output += '</th>';
            output += '<th>';
            output += 'Dirección';
            output += '</th>';
            output += '<th>';
            output += 'Llegada';
            output += '</th>';
            output += '</tr>';
            output += '<tr>';
            output += '<td>';
            output += '</td>';
            output += '<td>';
            output += '</td>';
            output += '<td class="letra" onclick="centrarPunto(' + json_origen["LAT_PE"] + ',' + json_origen["LON_PE"] + ');">A';
            output += '</td>';
            output += '<td>';
            output += json_origen["NOMBRE_PE"];
            output += '</td>';
            output += '<td>';
            output += json_origen["DIRECCION_PE"];
            output += '</td>';
            output += '<td id="t_origen">';
            output += json_origen["PERU_LLEGADA"];
            output += '</td>';
            output += '</tr>';
            output += '</thead>';
            output += '<tbody>';
            output += '<tr onclick="moverPunto(0);refrescar();" class="sel-between">';
            output += '<td></td>';
            output += '<td></td>';
            output += '<td></td>';
            output += '<td></td>';
            output += '<td></td>';
            output += '<td></td>';
            output += '</tr>';
            puntosRuta.map((o, i) => {
                output += '<tr>';
                output += '<td>';
                if (i > 0) {
                    output += '<a href="#" onclick="moverPunto(' + (i - 1) + ',' + o.PERU_ID + ');refrescar();" data-id=' + o.PERU_ID + '><span style="font-size:medium" class="glyphicon glyphicon-menu-up"></span></a>';
                }
                if (i != puntosRuta.length - 1) {
                    output += ' <a href="#" onclick="moverPunto(' + (i + 1) + ',' + o.PERU_ID + ');refrescar();"><span style="font-size:medium" class="glyphicon glyphicon-menu-down"></span></a>';
                }
                output += '</td>';
                if (puntosRuta.length > 1) {
                    output += '<td>';
                    output += '<a href="#" onclick="quitarPunto(' + o.PERU_ID + ');refrescar();"><span style="font-size:medium" class="glyphicon glyphicon-remove text-danger" /></a>';
                    output += '</td>';
                }
                else {
                    output += '<td>';
                    output += '</td>';
                }
                output += '<td class="letra">';
                output += '<a class="btn btn-xs btn-primary" style="width:22px" onclick="centrarPunto(' + o.PERU_LATITUD + ',' + o.PERU_LONGITUD + ');">' + (i + 2 + 9).toString(36).toUpperCase() + '</a>';
                //output += '<span style="font-size:medium" class="glyphicon glyphicon-map-marker" />';
                output += '</td>';
                output += '<td onclick="selecciona(\'' + o.PERU_ID + '\',\'' + o.PERU_CODIGO + '\');"  >';
                output += o.PERU_CODIGO;
                output += '</td>';
                output += '<td>';
                output += o.PERU_DIRECCION;
                output += '</td>';
                output += '<td id="t_' + o.PERU_CODIGO + '"  >';
                output += o.PERU_LLEGADA;
                output += '</td>';

                output += '</tr>';
                output += '<tr onclick="moverPunto(' + (i + 1) + ');refrescar();" class="sel-between">'
                output += '<td></td>';
                output += '<td></td>';
                output += '<td></td>';
                output += '<td></td>';
                output += '<td></td>';
                output += '<td></td>';
                output += '</tr> ';
            });
            output += '</tbody>';
            output += '<tfoot>';
            output += '<tr>';
            output += '<td>';
            output += '</td>';
            output += '<td>';
            output += '</td>';
            output += '<td class="letra" onclick="centrarPunto(' + json_origen["LAT_PE"] + ',' + json_origen["LON_PE"] + ');">A';
            //output += '<span style="font-size:medium" class="glyphicon glyphicon-map-marker" />';
            output += '</td>';
            output += '<td>';
            output += json_origen["NOMBRE_PE"];
            output += '</td>';
            output += '<td>';
            output += json_origen["DIRECCION_PE"];
            output += '</td>';
            output += '<td>&nbsp;';
            output += '</td>';
            output += '</tr>';
            output += '</tfoot>';
            output += '</table>';
            cantidad_puntos(i);

            return output;
        }
        function moverPunto(pos, id) {
            if (id) $('#hf_idPunto').val(id);
            if ($('#hf_idPunto').val() == '') return false;
            var id = parseInt($('#hf_idPunto').val());
            var punto = buscarPuntosTodos(id);
            quitarPunto(id);
            insertarPuntoRuta(id, pos);
            puntosRuta.splice(pos, 0, punto);
        }
        function quitarPunto(id) {
            for (var i = 0; i < puntosRuta.length; i++) {
                if (puntosRuta[i].PERU_ID === id) {
                    puntosRuta.splice(i, 1);
                    break;
                }
            }
            quitarPuntoRuta(id);
        }
        function centrarPunto(lat, lon) {
            var latLon = new google.maps.LatLng(lat, lon);
            map.setCenter(latLon);
            // map.setZoom(13);
        }
        function refrescar() {
            $('#<%=hf_puntosruta.ClientID%>').val(JSON.stringify(puntosRuta));
            $('#tbl_puntos').html(jsonToTable());
            tabla2();
            refrescarRuta();
            refrescarPoligono();
            $('#hf_idPunto').val('');
            $('#txt_puntoNombre').val('');
            $('.sel-between').removeClass('enabled');
        }
        function buscarPuntosTodos(id) {
            for (var i = 0; i < puntosTodos.length; i++) {
                if (puntosTodos[i]["PERU_ID"] == id) return puntosTodos[i];
            }
            return false;
        }

        function selecciona(id, titulo) {

            $('#hf_idPunto').val(id);
            $('#txt_puntoNombre').val(titulo);
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

    </script>
</asp:Content>
