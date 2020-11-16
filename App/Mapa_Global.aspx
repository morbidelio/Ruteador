<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Mapa_Global.aspx.cs" Inherits="App_Mapa_Global" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-xs-12 separador"></div>
    <div class="col-xs-1 text-right">
        Fecha
    </div>
    <div class="col-xs-1">
        <asp:TextBox ID="txt_buscarFecha" CssClass="form-control input-fecha" runat="server" />
    </div>
    <div class="col-xs-1 text-right">
        Hora Salida
    </div>
    <div class="col-xs-1">
        <asp:DropDownList ID="ddl_buscarHorario" CssClass="form-control" runat="server" ClientIDMode="Static">
            <asp:ListItem Value="0">Todos...</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="col-xs-1">
        <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary" runat="server">
            <span class="glyphicon glyphicon-search" />
        </asp:LinkButton>
    </div>
    <div id="dv_pills" class="col-xs-7">
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel ID="up_contenedor" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="col-xs-8" style="height: 70vh" id="map">
            </div>
            <div id="dv_rutas" class="col-xs-4">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" runat="Server">
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
    <asp:UpdatePanel ID="up_hidden" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_idRuta" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hf_jsonRuta" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hf_jsonRutas" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hf_jsonPedidos" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hf_jsonOrigenes" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hf_retorno" ClientIDMode="Static" runat="server" />
            <asp:Button ID="btn_guardar" ClientIDMode="Static" OnClick="btn_guardar_Click" CssClass="ocultar" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" runat="Server">
    <style type="text/css">
        .nav-pills {
            overflow-x: auto;
            display: -webkit-box;
            display: -moz-box;
        }

            .nav-pills > li {
                float: none;
            }
    </style>
    <%--<script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDjAnu30d80TbLCujKOmvnMKcEj2GI5H3o&sensor=false&lenguage=es&v=3.20"></script>--%>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDjAnu30d80TbLCujKOmvnMKcEj2GI5H3o&sensor=false&lenguage=es&v=beta"></script>
    <script src="../Scripts/moment.js"></script>
    <script src="../Scripts/mapa_global.js"></script>
    <script type="text/javascript">
        const retorno_ruta = <%=ConfigurationManager.AppSettings["retorno_ruta"] %>;
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
        const mapObject = document.getElementById('map');
        async function mapa() {
            jsonRutas = JSON.parse($('#hf_jsonRutas').val());
            jsonPedidos = JSON.parse($('#hf_jsonPedidos').val());
            jsonOrigenes = JSON.parse($('#hf_jsonOrigenes').val());
            cargamapa(mapObject);
            crearOrigenes(jsonOrigenes);
            crearPedidos(jsonPedidos);
            if (jsonRutas.length > 0) {
                await crearRutas();
                $('#dv_pills').html(jsonToPills());

                const tab = document.getElementById(`li_tab${jsonRutas[0].ID}`);
                selecciona(tab);
                $('#dv_rutas').html(jsonToTable());
            }
            else {
                $('#dv_rutas').html('<div class="alert alert-info" role="alert">No hay rutas hoy</div>');
            }
        }
        async function btn_rutaAgregar_Click(idPedido) {
            const idRuta = parseInt($('#hf_idRuta').val());
            var pedido;
            for (var i = 0; i < jsonPedidos.length; i++) {
                if (jsonPedidos[i].PERU_ID === idPedido) {
                    pedido = jsonPedidos[i];
                    pedido.marcador.setMap(null);
                    jsonPedidos.splice(i, 1);
                }
            }
            for (var ruta of jsonRutas) {
                if (ruta.ID === idRuta) {
                    pedido.RUTA_PEDIDO.SECUENCIA = ruta.PEDIDOS.length + 1;
                    ruta.PEDIDOS.push(pedido);
                    ruta = await crearRuta(ruta, true);
                    $(`#tb_pedidosRuta${ruta.ID}`).html(rutaToTable(ruta));
                    $(`#sp_tabTiempos${ruta.ID}`).html(secondsToString(calcularTiempos(ruta)));
                    guardarRuta();
                    return;
                }
            }
        }
        async function subirSecuencia(idRuta, indexPedido) {
            for (var ruta of jsonRutas) {
                if (ruta.ID === idRuta) {
                    var pedido = ruta.PEDIDOS[indexPedido];
                    const secuenciaInicial = pedido.RUTA_PEDIDO.SECUENCIA;
                    ruta.PEDIDOS.splice(indexPedido, 1);
                    pedido.RUTA_PEDIDO.SECUENCIA = (parseInt(secuenciaInicial) - 1).toString();
                    ruta.PEDIDOS[indexPedido - 1].RUTA_PEDIDO.SECUENCIA = secuenciaInicial;
                    ruta.PEDIDOS.splice(indexPedido - 1, 0, pedido);
                    ruta = await crearRuta(ruta, true);
                    $(`#tb_pedidosRuta${ruta.ID}`).html(rutaToTable(ruta));
                    $(`#sp_tabTiempos${ruta.ID}`).html(secondsToString(calcularTiempos(ruta)));
                    guardarRuta();
                    return;
                }
            }
        }
        async function bajarSecuencia(idRuta, indexPedido) {
            for (var ruta of jsonRutas) {
                if (ruta.ID === idRuta) {
                    var pedido = ruta.PEDIDOS[indexPedido];
                    const secuenciaInicial = pedido.RUTA_PEDIDO.SECUENCIA;
                    ruta.PEDIDOS.splice(indexPedido, 1);
                    pedido.RUTA_PEDIDO.SECUENCIA = (parseInt(secuenciaInicial)).toString();
                    ruta.PEDIDOS[indexPedido].RUTA_PEDIDO.SECUENCIA = secuenciaInicial;
                    ruta.PEDIDOS.splice(indexPedido + 1, 0, pedido);
                    ruta = await crearRuta(ruta, true);
                    $(`#tb_pedidosRuta${ruta.ID}`).html(rutaToTable(ruta));
                    $(`#sp_tabTiempos${ruta.ID}`).html(secondsToString(calcularTiempos(ruta)));
                    guardarRuta();
                    return;
                }
            }
        }
        async function eliminarPunto(idRuta, indexPedido) {
            for (var ruta of jsonRutas) {
                if (ruta.ID === idRuta) {
                    var pedido = ruta.PEDIDOS[indexPedido];
                    pedido.RUTA_PEDIDO.RPPE_ID = 0;
                    pedido.RUTA_PEDIDO.SECUENCIA = 0;
                    pedido.RUTA_PEDIDO.TIEMPO = 0;
                    pedido.marcador.setMap(null);
                    ruta.PEDIDOS.splice(indexPedido, 1);
                    jsonPedidos.push(pedido);
                    crearPedido(pedido);
                    for (var i = indexPedido; i < ruta.PEDIDOS.length; i++) {
                        ruta.PEDIDOS[indexPedido].SECUENCIA = (parseInt(ruta.PEDIDOS[indexPedido].SECUENCIA) - 1).toString();
                    }
                    ruta = await crearRuta(ruta, true);
                    $(`#tb_pedidosRuta${ruta.ID}`).html(rutaToTable(ruta));
                    $(`#sp_tabTiempos${ruta.ID}`).html(secondsToString(calcularTiempos(ruta)));
                    $(`#sp_tabCantidad${ruta.ID}`).html(ruta.PEDIDOS.length);
                    guardarRuta();
                    return;
                }
            }
        }
        function guardarRutas() {
            $('#hf_jsonRutas').val(JSON.stringify(jsonRutas, (key, value) => {
                if (key === 'direcciones')
                    return undefined;
                else if (key === 'marcador')
                    return undefined;
                else
                    return value;
            }));
        }
        function guardarRuta() {
            const id = parseInt($('#hf_idRuta').val());
            const ruta = buscarRutaXId(id);
            $('#hf_jsonRuta').val(JSON.stringify(ruta, (key, value) => {
                if (key === 'direcciones')
                    return undefined;
                else if (key === 'marcador')
                    return undefined;
                else
                    return value;
            }));
            $('#btn_guardar').click();
        }
        function selecciona(objeto) {
            if (objeto) {
                console.log(objeto);
                const id = $(objeto).attr('data-id');
                const color = $(objeto).attr('data-color');
                $('#<%=txt_editColor.ClientID%>').val(color);
                $('#btn_rutaColor').css('color', color);
                $('#hf_idRuta').val(id.toString())
            }
            else {
                $('#hf_idRuta').val('')
                console.log('No, nada');
            }
        }
        function btn_rutaColor_Click() {
            $('#modalColor').modal();
        }
        function btn_rutaVehiculo_Click() {
            const id = $('#hf_idRuta').val();
            const ruta = buscarRutaXId(parseInt(id));
            $find('<%=ddl_vehiculoTracto.ClientID%>').findItemByValue(ruta.TRACTO.TRAC_ID.toString()).select();
            $find('<%=ddl_vehiculoTipo.ClientID%>').findItemByValue(ruta.TRAILER.TRAILER_TIPO.TRTI_ID.toString()).select();
            setTimeout(function () {
                $find('<%=ddl_vehiculoTrailer.ClientID%>').findItemByValue(ruta.TRAILER.TRAI_ID.toString()).select();
            }, 500);
            $find('<%=ddl_vehiculoConductor.ClientID%>').findItemByValue(ruta.CONDUCTOR.COND_ID.toString()).select();
            $('#modalVehiculo').modal();
        }
        function btn_rutaEliminar_Click() {
            const id = $('#hf_idRuta').val();
            const ruta = buscarRutaXId(parseInt(id));
            $('#<%=lbl_confTitulo.ClientID%>').html('Eliminar Ruta');
            $('#<%=lbl_confMensaje.ClientID%>').html('Se eliminará la ruta seleccionada ¿Desea continuar?');
            $('#modalConf').modal();
        }
        function btn_ruta_Click(o) {
            const activo = $(o).hasClass('activo');
            const id = $(o).attr('data-id');
            if (activo) {
                $(o).removeClass('activo');
                $(o).css('color', 'inherit');
                activarRuta(parseInt(id), false);
            }
            else {
                $(o).addClass('activo');
                const color = $(o).parent().parent().attr('data-color');
                $(o).css('color', color);
                activarRuta(parseInt(id), true);
            }
        }
        function btn_rutaEnfocar_Click() {
            const id = parseInt($('#hf_idRuta').val());
            const color = buscarRutaXId(id).RUTA_COLOR;
            const itemsActivos = $(`.btn-ruta[data-id!=${id}]`);
            const o = $(`#btn_ruta${id}`);
            itemsActivos.removeClass('activo');
            itemsActivos.css('color', 'inherit');
            o.addClass('activo');
            o.css('color', color);
            enfocarRuta(id);
        }
        function chk_rutaCircular_Check(o) {
            if ($(o).prop('checked')) $('#hf_retorno').val('S')
            else $('#hf_retorno').val('N');
            $('#btn_guardar').click();
        }
        // PageLoad
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler1);
        function EndRequestHandler1(sender, args) {
            $('.activo').each(function (o) {
                const color = $(this).parent().parent().attr('data-color');
                $(this).css('color', color);
            });
        }
    </script>
</asp:Content>

