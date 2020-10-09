<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.Master" AutoEventWireup="true" CodeFile="Rutas.aspx.cs" Inherits="App_Rutas" %>

<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="server">
    <div class="col-lg-12 separador"></div>
    <h2>Maestro Rutas
    </h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Filtro" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Contenedor" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:GridView ID="gv_listar" AutoGenerateColumns="False" AllowSorting="true" Width="100%" CssClass="table table-bordered table-hover tablita tab-pag" runat="server"
                EmptyDataText="No hay registros" OnRowCreated="gv_listar_RowCreated" OnRowCommand="gv_listar_RowCommand" OnSorting="gv_listar_Sorting" DataKeyNames="ID,ID_ORIGEN">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="btn_puntos" CssClass="btn btn-xs btn-info" CommandArgument='<%#Container.DataItemIndex%>' CommandName="PUNTOS" runat="server">
                                <span class="glyphicon glyphicon-map-marker" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="NUMERO" SortExpression="NUMERO" HeaderText="NUMERO" />
                    <asp:BoundField DataField="FH_VIAJE" SortExpression="FH_VIAJE" HeaderText="FH_VIAJE" />
                    <asp:BoundField DataField="OBSERVACION" SortExpression="OBSERVACION" HeaderText="OBSERVACION" />
                    <asp:BoundField DataField="PATENTE_TRACTO" SortExpression="PATENTE_TRACTO" HeaderText="PATENTE_TRACTO" />
                    <asp:BoundField DataField="RUTA" SortExpression="RUTA" HeaderText="RUTA" />
                    <asp:BoundField DataField="ORIGEN_NOMBRE" SortExpression="ORIGEN_NOMBRE" HeaderText="ORIGEN" />
                    <asp:BoundField DataField="VIAJE_ESTADO" SortExpression="VIAJE_ESTADO" HeaderText="VIAJE_ESTADO" />
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Modals" runat="server">
    <div class="modal fade" id="modalPuntos" data-backdrop="static" role="dialog">
        <div class="modal-dialog" style="width: 90%">
            <div class="modal-content">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">Ubicación Trailer
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDjAnu30d80TbLCujKOmvnMKcEj2GI5H3o&sensor=false&lenguage=es&v=3.20"></script>
                            <script src="../Scripts/ruteador.js" type="text/javascript"></script>
                            <div class="col-lg-7" style="height: 65vh" id="map">
                            </div>
                            <div class="col-lg-5">
                                <%--<asp:GridView ID="gv_puntos" CssClass="table table-bordered table-hover tablita" AutoGenerateColumns="false" runat="server"
                                    onrowcommand="gv_puntos_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="NOMBRE_PE" HeaderText="Punto entrega" />
                                        <asp:BoundField DataField="DIRECCION_PE" HeaderText="Dirección" />
                                    </Columns>
                                </asp:GridView>--%>
                                Punto seleccionado
                                <br />
                                <asp:TextBox ID="txt_puntoNombre" Enabled="false" ClientIDMode="Static" runat="server" />
                                <div id="tbl_puntos"></div>
                                <div class="col-lg-12 separador"></div>
                                <div class="col-lg-12" style="text-align: center">
                                    <asp:LinkButton ID="btn_guardar" OnClick="btn_guardar_Click" CssClass="btn btn-primary" runat="server">
                                        <span class="glyphicon glyphicon-floppy-disk" />
                                    </asp:LinkButton>
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
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ocultos" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_todos" runat="server" />
            <asp:HiddenField ID="hf_puntosruta" runat="server" />
            <asp:HiddenField ID="hf_idRuta" runat="server" />
            <asp:HiddenField ID="hf_idPunto" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hf_idOrigen" ClientIDMode="Static" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="scripts" runat="server">
    <script type="text/javascript">
        var puntosTodos;
        var puntosRuta;
        function EndRequestHandler1(sender, args) {
            setTimeout(tabla2, 100);
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
                var table = $('#gv_puntos').DataTable({
                    "scrollY": "45vh",
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
        function mapa() {
            limpiarWaypoints();
            var id_origen = parseInt($('#hf_idOrigen').val());
            puntosTodos = JSON.parse($('#<%=hf_todos.ClientID%>').val());
            puntosRuta = JSON.parse($('#<%=hf_puntosruta.ClientID%>').val());
            var mapa = document.getElementById('map');
            cargamapa(12, mapa);
            $('#tbl_puntos').html(jsonToTable(puntosRuta));
            if (!hayMarcadores()) {
                for (var i = 0; i < puntosTodos.length; i++) {
                    insertarMarcador(puntosTodos[i]["ID"], puntosTodos[i]["LAT_PE"], puntosTodos[i]["LON_PE"], 'icon_pe.png', puntosTodos[i]["NOMBRE_PE"]);
                }
            }
            else {
                refrescarMarcadores();
            }
            setOrigen(id_origen);
            setDestino(id_origen);
            for (var i = 0; i < puntosRuta.length; i++) {
                insertarPuntoRuta(puntosRuta[i]["ID"]);
            }
            crearPoligono();
            crearRuta();
        }
        function jsonToTable() {
            var id_origen = parseInt($('#hf_idOrigen').val());
            var origen = buscarPuntosTodos(id_origen);
            var output = '<table id="gv_puntos" style="width:100%" class="table table-border table-hover tablita">';
            output += '<thead>';
            output += '<tr>';
            output += '<th>';
            output += 'Ordenar';
            output += '</th>';
            output += '<th>';
            output += '<span class="glyphicon glyphicon-remove" />';
            output += '</th>';
            output += '<th>';
            output += 'Punto Entrega';
            output += '</th>';
            output += '<th>';
            output += 'Dirección';
            output += '</th>';
            output += '</tr>';
            output += '<tr>';
            output += '<td>';
            output += '</td>';
            output += '<td>';
            output += '</td>';
            output += '<td>';
            output += origen["NOMBRE_PE"];
            output += '</td>';
            output += '<td>';
            output += origen["DIRECCION_PE"];
            output += '</td>';
            output += '</tr>';
            output += '</thead>';
            output += '<tbody>';
            output += '<tr onclick="moverPunto(0);refrescar();" class="sel-between">';
            output += '<td></td>';
            output += '<td></td>';
            output += '<td></td>';
            output += '<td></td>';
            output += '</tr>';
            for (var i = 0; i < puntosRuta.length; i++) {
                output += '<tr>';
                output += '<td>';
                if (i > 0) {
                    output += '<span style="font-size:medium" onclick="moverPunto(' + (i - 1) + ',' + puntosRuta[i]["ID"] + ');refrescar();" class="glyphicon glyphicon-menu-up"></span>';
                }
                if (i != puntosRuta.length - 1) {
                    output += '<span style="font-size:medium" onclick="moverPunto(' + (i + 1) + ',' + puntosRuta[i]["ID"] + ');refrescar();" class="glyphicon glyphicon-menu-down"></span>';
                }
                output += '</td>';
                if (puntosRuta.length > 1) {
                    output += '<td onclick="quitarPunto(' + puntosRuta[i]["ID"] + ');refrescar();">';
                    output += '<span style="font-size:medium" class="glyphicon glyphicon-remove" />';
                    output += '</td>';
                }
                else {
                    output += '<td>';
                    output += '</td>';
                }
                output += '<td>';
                output += puntosRuta[i]["NOMBRE_PE"];
                output += '</td>';
                output += '<td>';
                output += puntosRuta[i]["DIRECCION_PE"];
                output += '</td>';
                output += '</tr>';
                output += '<tr onclick="moverPunto(' + (i + 1) + ');refrescar();" class="sel-between">'
                output += '<td></td>';
                output += '<td></td>';
                output += '<td></td>';
                output += '<td></td>';
                output += '</tr> ';
            }
            output += '</tbody>';
            output += '<tfoot>';
            output += '<tr>';
            output += '<td>';
            output += '</td>';
            output += '<td>';
            output += '</td>';
            output += '<td>';
            output += origen["NOMBRE_PE"];
            output += '</td>';
            output += '<td>';
            output += origen["DIRECCION_PE"];
            output += '</td>';
            output += '</tr>';
            output += '</tfoot>';
            output += '</table>';
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
            $('#hf_idPunto').val('');
            $('#txt_puntoNombre').val('');
            $('.sel-between').removeClass('enabled');
            console.log('funcionó');
        }
        function quitarPunto(id) {
            for (var i = 0; i < puntosRuta.length; i++) {
                if (puntosRuta[i]["ID"] == id) {
                    puntosRuta.splice(i, 1);
                    break;
                }
            }
            quitarPuntoRuta(id);
        }
        function refrescar() {
            $('#<%=hf_puntosruta.ClientID%>').val(JSON.stringify(puntosRuta));
            $('#tbl_puntos').html(jsonToTable());
            tabla2();
            refrescarRuta();
            refrescarPoligono();
            crearRuta();
        }
        function buscarPuntosTodos(id) {
            for (var i = 0; i < puntosTodos.length; i++) {
                if (puntosTodos[i]["ID"] == id) return puntosTodos[i];
            }
            return false;
        }
    </script>
    <style>
        .sel-between > td {
            height: 1px;
            padding: 1px !important;
            border: 1px;
        }

        .sel-between {
            display: none;
        }

            .sel-between.enabled {
                display: block;
            }

                .sel-between.enabled:hover {
                    background-color: red;
                }
    </style>
</asp:Content>
