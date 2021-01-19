var map;
const mapObject = document.getElementById('map');
var jsonOrigenes = [];
var jsonPedidos = [];
var jsonRutas = [];
var directionsService = new google.maps.DirectionsService();
var espera = 100;
const infowindow = new google.maps.InfoWindow();
var esperando = 0;

Object.defineProperty(Array.prototype, 'chunk', {
    value: function (chunkSize) {
        var R = [];
        for (var i = 0; i < this.length; i += chunkSize)
            R.push(this.slice(i, i + chunkSize));
        return R;
    }
});

function cargamapa(mapObj) {
    var center = new google.maps.LatLng(-33.45238466, -70.65735526);
    map = new google.maps.Map(mapObj, {
        center: center,
        zoom: 10,
        gestureHandling: 'greedy'
    });
    setTimeout(reOffset1, 100);
}

function crearCabecera() {
    return `<ul id="ul_rutaPills" class="nav nav-pills">
            </ul>
            <div id="dv_pillsScrolls" class="col-xs-12 text-center">
            </div>`;
}
function jsonToPills() {
    const c = $('<ul id="ul_rutaPills" class="nav nav-pills"></ul>');

    var cabecera = `<ul id="ul_rutaPills" class="nav nav-pills">`;
    jsonRutas.forEach(function (ruta) {
        c.append(rutaToPill(ruta, true, true));
        cabecera += rutaToPill(ruta, true, true);
    });
    cabecera += `</ul>`;
    return c;
}
function rutaToPill(ruta, activa, ver) {
    var pill = $(`<li class="pillRuta" id="li_tab${ruta.ID}" data-id="${ruta.ID}" data-color="${ruta.RUTA_COLOR}"></li>`);
    $(pill).click(function () {
        $('#btn_rutaEnfocar').attr('data-id', ruta.ID);
        $('#hf_idRuta').val(ruta.ID.toString());
        $('#chk_rutaCircular').prop('checked', (ruta.RETORNO === 'S'));
        $('#btn_rutaColor').attr('data-color', ruta.RUTA_COLOR);
        $('#btn_rutaColor').css('color', ruta.RUTA_COLOR);
        $('#txt_editNombre').val(ruta.NUMERO);
        $('#lbl_rutaNumero').html(ruta.NUMERO);
        $('#sp_rutaConductor').html((ruta.CONDUCTOR.COND_ID) ? ruta.CONDUCTOR.COND_NOMBRE : 'Sin conductor');
        $('#sp_rutaTrailer').html((ruta.TRAILER.TRAI_ID) ? ruta.TRAILER.TRAI_PLACA : 'Sin vehículo');
        $('#btn_detalleAbrir').click();
        setTimeout(tablaPuntos, 200);
    });
    var link = $(`<a id="lnk_tab${ruta.ID}" href="#dv_tabRuta${ruta.ID}" style="color:${ruta.RUTA_COLOR}"></a>`);
    var btnActivar = $(`<button id="btn_rutaActiva${ruta.ID}" data-id="${ruta.ID}" type="button" title="Ver/Ocultar ruta" class="btn btn-xs btn-ruta" style="color:${ruta.RUTA_COLOR}">
                            <span class="glyphicon glyphicon-eye-open" />
                        </button>`);
    btnActivar.click(function () {
        const activo = btnActivar.hasClass('btn-success');
        if (activo) {
            btnActivar.removeClass('btn-success');
            btnActivar.addClass('btn-default');
        }
        else {
            btnActivar.removeClass('btn-default');
            btnActivar.addClass('btn-success');
        }

        activarRuta(buscarRutaXId(ruta.ID), !activo);
    });
    var lblNumero = $(`<span id="sp_tabNumero${ruta.ID}">${ruta.NUMERO}</span>`);
    var lblCantidad = $(`<span id="sp_tabCantidad${ruta.ID}" class="badge">${ruta.PEDIDOS.length}</span>`);
    var lblTiempo = $(`<center>
                            <span class="label label-alt" id="sp_tabTiempos${ruta.ID}">${secondsToString(calcularTiempoTotal(ruta))}</span>
                        </center>`);

    if (activa) {
        link.attr('data-toggle', 'tab');
    }
    else {
        pill.addClass('disabled');
        btnActivar.prop('disabled', true);
    }
    btnActivar.addClass((ver) ? 'btn-success' : 'btn-default');
    link.append(btnActivar).append(lblNumero).append(lblCantidad);
    pill.append(link).append(lblTiempo);

    return pill;
}

function crearTabla() {
    var output = `
                <div class="panel panel-info">
                    <div class="panel-heading" style="overflow-y:auto;">
                        <div class="col-xs-4">
                        <h3 id="lbl_rutaNumero"></h3>
                        </div>
                        <div class="col-xs-4 text-center">
                            <span>Conductor</span> 
                            <br />
                            <span class="label label-info" id="sp_rutaConductor"></span>
                        </div>
                        <div class="col-xs-4 text-center">
                            <span>Vehículo</span>
                            <br />
                            <span class="label label-success" id="sp_rutaTrailer"></span>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="col-xs-6">
                            Punto seleccionado
                        </div>
                        <div class="col-xs-6">
                            <input id="txt_puntoSeleccionado" class="form-control disabled" type="text" disabled />
                        </div>
                        <div class="col-xs-12 separador"></div>
                        <div id="dv_rutaTablasCont" class="tab-content">
                        </div>
                        <div class="btn-group" role="group">
                            <button id="btn_rutaColor" onclick="btn_rutaColor_Click();" type="button" class="btn btn-default" title="Cambiar color ruta">
                                <span class="glyphicon glyphicon-tint" />
                            </button>
                            <button id="btn_rutaEnfocar" onclick="btn_rutaEnfocar_Click();" type="button" class="btn btn-default" title="Enfocar ruta">
                                <span class="glyphicon glyphicon-eye-open" />
                            </button>
                            <button id="btn_rutaVehiculo" onclick="btn_rutaVehiculo_Click();" type="button" class="btn btn-primary" title="Editar detalle ruta">
                                <span class="glyphicon glyphicon-list" />
                            </button>
                            <button id="btn_rutaEliminar" onclick="btn_rutaEliminar_Click();" type="button" class="btn btn-danger" title="Eliminar ruta">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                        <input id="chk_rutaCircular" onclick="chk_rutaCircular_Check(this);" type="checkbox" />
                        <label for="chk_rutaCircular">Retorno</label>
                    </div>
                </div>`;
    return output;
}
function rutaToTable(ruta, activo) {
    var tabRuta = $(`<div id="dv_tabRuta${ruta.ID}"  class="tab-pane fade"></div>`);
    if (activo) tabRuta.addClass('active in');
    var gvPuntos = $(`<table id="gv_puntos${ruta.ID}" class="table table-condensed table-hover tablita" style="width:100%"></table>`);
    var head = $(`<thead>
                    <tr>
                        <th>
                        </th>
                        <th>
                        </th>
                        <th>
                        </th>
                        <th>
                        Nombre
                        </th>
                        <th>
                        Hora Llegada
                        </th>
                        <th>
                        Hora Salida
                        </th>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        ${ruta.ORIGEN.NOMBRE_PE}
                        </td>
                        <td>
                        -
                        </td>
                        <td>
                        ${moment(ruta.HORARIO.HORA_COD, 'HH:mm').format('HH:mm:ss')}
                        </td>
                    </tr>
                </thead>`);
    var body = $(`<tbody id="tb_pedidosRuta${ruta.ID}" data-id="${ruta.ID}"></tbody>`);
    if (ruta.PEDIDOS) {
        ruta.PEDIDOS.forEach(function (pedido, i, arr) {
            var selBetween = $(`<tr class="sel-between">
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>`);
            selBetween.click(function () {
                const idPedido = parseInt($('#hf_idPedido').val());

                moverPedidoRuta(idPedido, ruta.ID, i);

                $('.sel-between').removeClass('activo');
                $('#txt_puntoSeleccionado').val('');
            });
            body.append(selBetween);
            body.append(pedidoToRow(arr.length, i, pedido, ruta.PEDIDOS.length));
        });
    }

    var selBetween = $(`<tr class="sel-between">
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>`);
    selBetween.click(function () {
        const idPedido = parseInt($('#hf_idPedido').val());

        moverPedidoRuta(idPedido, ruta.ID);

        $('.sel-between').removeClass('activo');
        $('#txt_puntoSeleccionado').val('');
    });
    body.append(selBetween);

    gvPuntos.append(head);
    gvPuntos.append(body);

    tabRuta.append(gvPuntos);
    return tabRuta;
}
function pedidoToRow(largo, posicion, pedido, selbetween) {
    var rowPedido = $(`<tr data-id="${pedido.PERU_ID}"></tr>`);
    rowPedido.append(`
                        <td></td>
                        <td class="colmover"></td>
                        <td class="colborrar"></td>
                        <td class="colseleccion"></td>`);
    var btnSubir = $(`<button class="btn-borderless btn-subir">
                        <span style="font-weight:bolder;" class="glyphicon glyphicon-chevron-up" />
                        </button>`);
    btnSubir.click(function () {
        const idRuta = parseInt($('#hf_idRuta').val());
        moverPedidoRuta(pedido.PERU_ID, idRuta, posicion - 1);
        return false;
    });
    var btnBajar = $(`<button class="btn-borderless btn-bajar">
                        <span style="font-weight:bolder;" class="glyphicon glyphicon-chevron-down" />
                        </button>`);
    btnBajar.click(function () {
        const idRuta = parseInt($('#hf_idRuta').val());
        moverPedidoRuta(pedido.PERU_ID, idRuta, posicion + 1);
        return false;
    });
    var btnBorrar = $(`<button class="btn-borderless btn-eliminar">
                    <span style="font-weight:bolder;" class="glyphicon glyphicon-remove" />
                    </button>`);
    btnBorrar.click(function () {
        eliminarPedidoRuta(pedido.PERU_ID)
        return false;
    });
    var lnkSeleccion = $(`<a href="#" onclick="btn_puntoSeleccionar_Click(this);">
                        ${pedido.PERU_NUMERO}
                        </a>`);
    lnkSeleccion.click(function () { seleccionarPedido(pedido, selbetween) });
    if (largo > 1) {
        if (posicion !== 0) {
            rowPedido.children('td.colmover').append(btnSubir);
        }
        if (posicion < largo - 1) {
            rowPedido.children('td.colmover').append(btnBajar);
        }
        rowPedido.children('td.colborrar').append(btnBorrar);
    }
    rowPedido.children('td.colseleccion').append(lnkSeleccion);

    rowPedido.append(`
                <td>
                ${moment(pedido.RUTA_PEDIDO.FH_LLEGADA).format('HH:mm:ss')}
                </td>
                <td>
                ${moment(pedido.RUTA_PEDIDO.FH_SALIDA).format('HH:mm:ss')}
                </td>`);
    return rowPedido;
}

function crearOrigenes() {
    jsonOrigenes.forEach((o) => {
        var icon;
        if (o.ICONO.ICON_ID) {
            icon = {
                url: iconPath + '/' + o.ICONO.ICON_URL,
                scaledSize: {
                    height: 25
                    , width: 25
                }
            };
        }
        else {
            icon = {
                path: 'M 0,0 C -2,-20 -10,-22 -10,-30 A 10,10 0 1,1 10,-30 C 10,-22 2,-20 0,0 z',
                fillColor: '#1B18C9',
                fillOpacity: 1,
                strokeColor: '#000',
                strokeWeight: 2,
                labelOrigin: new google.maps.Point(0, -30),
                scale: 0.75
            };
        }
        o.marcador = new google.maps.Marker({
            map: map
            , position: new google.maps.LatLng(o.LAT_PE, o.LON_PE)
            , title: o.NOMBRE_PE

            //, icon: '../img/marker_blue.png'
            , icon: icon
            //, label: {
            //    text: 'O',
            //    fontFamily: 'Britannic',
            //    fontWeight: 'bolder',
            //    fontSize: '11px',
            //    color: invertColor('#1B18C9')
            //}
        });
    });
}

function crearPedidos() {
    jsonPedidos.forEach(function (o) {
        o = crearPedido(o);
    });
}
function crearPedido(pedido) {
    pedido.marcador = new google.maps.Marker({
        map: map
        , position: { lat: pedido.PERU_LATITUD, lng: pedido.PERU_LONGITUD }
        , title: pedido.PERU_NUMERO
        , icon: '../img/icon_pedido.png'
    });
    const contenido = `<div id="content">
                <p>
                Código: ${pedido.PERU_CODIGO}
                <br />
                Número: ${pedido.PERU_NUMERO}
                <br />
                Hora: ${pedido.HORA_SALIDA.HORA_COD}
                <br />
                Dirección: ${pedido.PERU_DIRECCION}
                </p>
                <button id="btn_puntoAgregar" title="Agregar al final de la ruta" onclick="btn_puntoAgregar_Click(this);" data-id="${pedido.PERU_ID}" type="button" class="btn btn-sm btn-success">
                    <span class="glyphicon glyphicon-plus" />
                </button>
                </div>`;
    pedido.marcador.addListener('click', function () {
        infowindow.setContent(contenido);
        infowindow.open(map, pedido.marcador);
        seleccionarPedido(pedido, true);
    });
    return pedido;
}

async function crearRutas(limite) {
    espera = 100;
    for (var i = 0; i < jsonRutas.length; i++) {

        while (esperando == 1) {
        }
        jsonRutas[i].PEDIDOS.forEach((p) => p.marcador = null);
        jsonRutas[i].direcciones = [];
        if (i < limite || !limite)
            jsonRutas[i].procesado = false;
        if (jsonRutas[i].PEDIDOS.length < 25) { espera = espera + 1000; }
        else { espera = espera + 1800 * (Math.ceil(jsonRutas[i].PEDIDOS.length / 25)); }

        if (jsonRutas[i].RETORNO.toUpperCase() === 'S') espera = espera + 1800;
        setTimeout(crearSiguieteRuta, espera);
    }
}
async function crearRuta(ruta, refrescar) {
    while (esperando == 1) {
    }
    if (refrescar) {
        ruta.direcciones.forEach((direccion) => direccion.setMap(null));
        ruta.PEDIDOS.forEach((pedido) => pedido.marcador.setMap(null));
        ruta.RESPUESTA = [];
    }
    ruta.direcciones = [];
    var cont_global = 0;
    var chinkos = Array.from(ruta.PEDIDOS).chunk(22);
    var origen_relativo = new google.maps.LatLng({ lat: ruta.ORIGEN.LAT_PE, lng: ruta.ORIGEN.LON_PE });
    var destino_relativo;
    if (ruta.PEDIDOS) {
        ruta.PEDIDOS.forEach((pedido, indexPedido) => {
            pedido.marcador = new google.maps.Marker({
                map: map
                , position: new google.maps.LatLng(pedido.PERU_LATITUD, pedido.PERU_LONGITUD)
                , title: ruta.NUMERO + ' / ' + pedido.PERU_NUMERO
                , icon: {
                    path: 'M 0,0 C -2,-20 -10,-22 -10,-30 A 10,10 0 1,1 10,-30 C 10,-22 2,-20 0,0 z',
                    fillColor: ruta.RUTA_COLOR,
                    fillOpacity: 1,
                    strokeColor: '#000',
                    strokeWeight: 1,
                    labelOrigin: new google.maps.Point(0, -30),
                    scale: 0.75
                }
                , label: {
                    text: (indexPedido + 1).toString(),
                    fontFamily: 'Britannic',
                    fontWeight: 'bolder',
                    fontSize: '11px',
                    color: invertColor(ruta.RUTA_COLOR)
                }
            });
            const contenido = `<div id="content">
                <p>
                    Ruta: <a onclick="$('#lnk_tab${ruta.ID}').click();btn_rutaEnfocar_Click(this);" href="#" data-id="${ruta.ID}">${ruta.NUMERO}</a>
                    <br />
                    Código: ${pedido.PERU_CODIGO}
                    <br />
                    Número: ${pedido.PERU_NUMERO}
                    <br />
                    Hora: ${pedido.HORA_SALIDA.HORA_COD}
                    <br />
                    Dirección: ${pedido.PERU_DIRECCION}
                </p>
                    <div class="btn-group" role="group">
                        <button id="btn_puntoAgregar" title="Agregar al final de la ruta" onclick="btn_puntoAgregar_Click(this);" data-id="${pedido.PERU_ID}" type="button" class="btn btn-sm btn-success">
                            <span class="glyphicon glyphicon-plus" />
                        </button>
                        <button id="btn_puntoEliminar" title="Eliminar pedido de ruta" onclick="btn_puntoEliminar_Click(${pedido.PERU_ID});" type="button" class="btn btn-sm btn-danger" ${(ruta.PEDIDOS.length > 1) ? '' : 'disabled'}>
                            <span class="glyphicon glyphicon-remove" />
                        </button>
                        </div>
                    </div>`;
            pedido.marcador.addListener('click', function () {
                infowindow.setContent(contenido);
                infowindow.open(map, pedido.marcador);
                seleccionarPedido(pedido, (ruta.PEDIDOS.length > 1));
            });
        });
    }
    else {
        ruta.PEDIDOS = [];
    }
    var fechaInicial = moment(ruta.FECHA_DESPACHOEXP);
    var HoraInicialRelativa = moment(ruta.HORARIO.HORA_COD, 'HH:mm');
    fechaInicial.set({ 'h': HoraInicialRelativa.hour(), 'm': HoraInicialRelativa.minute() })
    for (const chink of chinkos) {
        const chk = chink.pop();
        destino_relativo = new google.maps.LatLng({ lat: chk.PERU_LATITUD, lng: chk.PERU_LONGITUD });
        const waypoints = chink.map((ch) => {
            return { location: new google.maps.LatLng(ch.PERU_LATITUD, ch.PERU_LONGITUD) }
        });
        const req = {
            origin: origen_relativo
            , destination: destino_relativo
            , travelMode: "DRIVING"
            , waypoints: waypoints
            , optimizeWaypoints: false
        };
        const directionsRenderer = new google.maps.DirectionsRenderer({
            preserveViewport: true,
            suppressMarkers: true,
            map: map,
            polylineOptions: {
                strokeColor: ruta.RUTA_COLOR
            }
        });
        while (esperando == 1) {
        }
        await directionsService.route(req).
            then((response) => {
                ruta.RESPUESTA.push(response);
                directionsRenderer.setDirections(response);
                if (refrescar) {
                    for (const tramo of response.routes[0].legs) {
                        ruta.PEDIDOS[cont_global].tiempo = tramo.duration.value / 60;
                        fechaInicial = fechaInicial.add(tramo.duration.value, 's');
                        ruta.PEDIDOS[cont_global].RUTA_PEDIDO.FH_LLEGADA = fechaInicial.format();
                        HoraInicialRelativa = fechaInicial.add(parseInt(ruta.PEDIDOS[cont_global].PERU_TIEMPO), 'm');
                        ruta.PEDIDOS[cont_global].RUTA_PEDIDO.FH_SALIDA = fechaInicial.format();
                        cont_global++;
                    }
                }
            }).
            catch((error) => {

                //    msj("No se pudo crear una ruta... reintentando: " + error, "warn", true);

                setTimeout(crearSiguieteRuta, 1500);
            });
        ruta.direcciones.push(directionsRenderer);
        origen_relativo = destino_relativo;
        sleep(200);
    }
    if (ruta.RETORNO.toUpperCase() === 'S') {
        sleep(200);
        directionsService = new google.maps.DirectionsService();
        const req = {
            origin: origen_relativo
            , destination: new google.maps.LatLng(ruta.ORIGEN.LAT_PE, ruta.ORIGEN.LON_PE)
            , travelMode: "DRIVING"
            , optimizeWaypoints: false
        };
        const directionsRenderer = new google.maps.DirectionsRenderer({
            preserveViewport: true
            , suppressMarkers: true
            , map: map
            , polylineOptions: {
                strokeColor: ruta.RUTA_COLOR
            }
        });
        await directionsService.route(req).
            then((response) => {
                ruta.RESPUESTA.push(response);
                directionsRenderer.setDirections(response);
                ruta.TIEMPO_RETORNO = Math.round(response.routes[0].legs[0].duration.value / 60);
            }).
            catch((error) => {
                msj("No se pudo crear una ruta " + error, "warn", true);
            });
        ruta.direcciones.push(directionsRenderer);
    }
    else {
        ruta.TIEMPO_RETORNO = 0;
    }
    return new Promise(resolve => resolve(ruta));
}


function sleep(milliseconds) {

    esperando = 1;
    var start = new Date().getTime();
    for (var i = 0; i < 1e7; i++) {
        if ((new Date().getTime() - start) > milliseconds) {
            esperando = 0;
            break;
        }
    }
}

async function crearSiguieteRuta() {
    while (esperando == 1) {
    }
    for (var ruta of jsonRutas) {
        if (!ruta.procesado) {
            ruta.procesado = true;
            var refrescar = false;
            if (refrescar) {
                ruta.direcciones.forEach((direccion) => direccion.setMap(null));
                ruta.PEDIDOS.forEach((pedido) => pedido.marcador.setMap(null));
                ruta.RESPUESTA = [];
            }
            ruta.direcciones = [];
            if (ruta.PEDIDOS) {
                ruta.PEDIDOS.forEach((pedido, indexPedido) => {
                    pedido.marcador = new google.maps.Marker({
                        map: map
                        , position: new google.maps.LatLng(pedido.PERU_LATITUD, pedido.PERU_LONGITUD)
                        , title: ruta.NUMERO + ' / ' + pedido.PERU_NUMERO
                        , icon: {
                            path: 'M 0,0 C -2,-20 -10,-22 -10,-30 A 10,10 0 1,1 10,-30 C 10,-22 2,-20 0,0 z',
                            fillColor: ruta.RUTA_COLOR,
                            fillOpacity: 1,
                            strokeColor: '#000',
                            strokeWeight: 1,
                            labelOrigin: new google.maps.Point(0, -30),
                            scale: 0.75
                        }
                        , label: {
                            text: (indexPedido + 1).toString(),
                            fontFamily: 'Britannic',
                            fontWeight: 'bolder',
                            fontSize: '11px',
                            color: invertColor(ruta.RUTA_COLOR)
                        }
                    });
                    const contenido = `<div id="content">
                <p>
                    Ruta: <a onclick="$('#lnk_tab${ruta.ID}').click();btn_rutaEnfocar_Click(this);" href="#" data-id="${ruta.ID}">${ruta.NUMERO}</a>
                    <br />
                    Código: ${pedido.PERU_CODIGO}
                    <br />
                    Número: ${pedido.PERU_NUMERO}
                    <br />
                    Hora: ${pedido.HORA_SALIDA.HORA_COD}
                    <br />
                    Dirección: ${pedido.PERU_DIRECCION}
                </p>
                    <div class="btn-group" role="group">
                        <button id="btn_puntoAgregar" title="Agregar al final de la ruta" onclick="btn_puntoAgregar_Click(this);" data-id="${pedido.PERU_ID}" type="button" class="btn btn-sm btn-success">
                            <span class="glyphicon glyphicon-plus" />
                        </button>
                        <button id="btn_puntoEliminar" title="Eliminar pedido de ruta" onclick="btn_puntoEliminar_Click(${pedido.PERU_ID});" type="button" class="btn btn-sm btn-danger" ${(ruta.PEDIDOS.length > 1) ? '' : 'disabled'}>
                            <span class="glyphicon glyphicon-remove" />
                        </button>
                        </div>
                    </div>`;
                    pedido.marcador.addListener('click', function () {
                        infowindow.setContent(contenido);
                        infowindow.open(map, pedido.marcador);
                        seleccionarPedido(pedido, (ruta.PEDIDOS.length > 1));

                    });
                });
            }
            else {
                ruta.PEDIDOS = [];
            }
            var fechaInicial = moment(ruta.FECHA_DESPACHOEXP);
            var HoraInicialRelativa = moment(ruta.HORARIO.HORA_COD, 'HH:mm');
            fechaInicial.set({ 'h': HoraInicialRelativa.hour(), 'm': HoraInicialRelativa.minute() });
            if (ruta.RESPUESTA.length === 0 || !ruta.RESPUESTA) {
                var origen_relativo = new google.maps.LatLng({ lat: ruta.ORIGEN.LAT_PE, lng: ruta.ORIGEN.LON_PE });
                var destino_relativo;
                var chinkos = Array.from(ruta.PEDIDOS).chunk(22);
                for (const chink of chinkos) {
                    const chk = chink.pop();
                    destino_relativo = new google.maps.LatLng({ lat: chk.PERU_LATITUD, lng: chk.PERU_LONGITUD });
                    const waypoints = chink.map((ch) => {
                        return { location: new google.maps.LatLng(ch.PERU_LATITUD, ch.PERU_LONGITUD) }
                    });
                    const req = {
                        origin: origen_relativo
                        , destination: destino_relativo
                        , travelMode: "DRIVING"
                        , waypoints: waypoints
                        , optimizeWaypoints: false
                    };
                    const directionsRenderer = new google.maps.DirectionsRenderer({
                        preserveViewport: true,
                        suppressMarkers: true,
                        map: map,
                        polylineOptions: {
                            strokeColor: ruta.RUTA_COLOR
                        }
                    });
                    await directionsService.route(req).
                        then((response) => {
                            directionsRenderer.setDirections(response);
                            $.ajax({
                                type: "POST",
                                url: "Mapa_Global.asmx/GuardarResponse",
                                data: "{ response: '" + LZString.compressToBase64(JSON.stringify(response)) + "', id_ruta:" + ruta.ID + " }",

                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                error: function (ex) {
                                    console.error("Error " + ex.status + ": " + ex.responseJSON.Message);
                                }
                            });
                        }).
                        catch((error) => {
                            for (var ruta of jsonRutas) {

                                for (var direccion of ruta.direcciones) {
                                    if (direccion == directionsRenderer) {
                                        for (var pedido of ruta.PEDIDOS) {
                                            if (pedido && pedido.marcador) {
                                                pedido.marcador.setMap(null);
                                                pedido.marcador = null;
                                            }
                                        }

                                        ruta.procesado = false;
                                        setTimeout(crearSiguieteRuta, 1500);
                                        break;
                                    }
                                }

                            }
                        });
                    ruta.direcciones.push(directionsRenderer);
                    origen_relativo = destino_relativo;
                    sleep(200);
                }
                if (ruta.RETORNO.toUpperCase() === 'S') {
                    sleep(200);
                    directionsService = new google.maps.DirectionsService();
                    const req = {
                        origin: origen_relativo
                        , destination: new google.maps.LatLng(ruta.ORIGEN.LAT_PE, ruta.ORIGEN.LON_PE)
                        , travelMode: "DRIVING"
                        , optimizeWaypoints: false
                    };
                    const directionsRenderer = new google.maps.DirectionsRenderer({
                        preserveViewport: true
                        , suppressMarkers: true
                        , map: map
                        , polylineOptions: {
                            strokeColor: ruta.RUTA_COLOR
                        }
                    });
                    await directionsService.route(req).
                        then((response) => {
                            directionsRenderer.setDirections(response);
                            ruta.TIEMPO_RETORNO = Math.round(response.routes[0].legs[0].duration.value / 60);
                            $.ajax({
                                type: "POST",
                                url: "Mapa_Global.asmx/GuardarResponse",
                                data: "{ response: '" + LZString.compressToBase64(JSON.stringify(response)) + "', id_ruta:" + ruta.ID + " }",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                error: function (ex) {
                                    console.error("Error " + ex.status + ": " + ex.responseJSON.Message);
                                }
                            });
                        }).
                        catch(() => {

                        });
                    ruta.direcciones.push(directionsRenderer);
                }
                else {
                    ruta.TIEMPO_RETORNO = 0;
                }
            }
            else {
                ruta.RESPUESTA.forEach((r) => {
                    const directionsRenderer = new google.maps.DirectionsRenderer({
                        preserveViewport: true
                        , suppressMarkers: true
                        , map: map
                        , polylineOptions: {
                            strokeColor: ruta.RUTA_COLOR
                        }
                    });
                    directionsRenderer.setDirections(JSON.parse(LZString.decompressFromBase64(r.RURE_RESPUESTA)));
                    ruta.direcciones.push(directionsRenderer);
                });
            }
            $('#ul_rutaPills').append(rutaToPill(ruta, false, true));
            $('#dv_rutaTablasCont').append(rutaToTable(ruta));
            break;
        }
    }
    var todosProcesados = true;
    for (var ruta of jsonRutas) {
        if (!ruta.procesado) {
            todosProcesados = false;
            break;
        }
    }
    if (todosProcesados) {
        closeSpinner('dv_pills');
        closeSpinner('dv_rutas');
        $('.pillRuta:first').children('a').click();
        $('[title]').tooltip({ container: 'body' });
        $('#dv_pillsScrolls').html(pillScroll());
    }
}

function seleccionarPedido(pedido, selbetween) {
    $('#hf_idPedido').val(pedido.PERU_ID);
    if (selbetween) {
        $('.sel-between').addClass('activo');
        $('#btn_puntoAgregar').prop('disabled', false);
    }
    else {
        $('.sel-between').removeClass('activo');
        $('#btn_puntoAgregar').prop('disabled', true);
    }
    $('#txt_puntoSeleccionado').val(pedido.PERU_NUMERO);
}

function activarRuta(ruta, activar) {
    if (ruta.direcciones.length > 0) {
        ruta.direcciones.forEach((direccion) => {
            if (!activar) {
                direccion.setMap(null);
            }
            else {
                direccion.setMap(map);
            }
        });
        ruta.PEDIDOS.forEach((pedido) => {
            if (!activar) {
                if (pedido && pedido.marcador) {
                    pedido.marcador.setMap(null);

                }
            }
            else {
                pedido.marcador.setMap(map);
            }
        });
    }
    else if (activar) {
        crearRuta(ruta, false);
    }
}
function enfocarRuta(id) {
    var bounds = new google.maps.LatLngBounds();
    jsonRutas.forEach((ruta) => {
        if (ruta.ID !== id) {
            activarRuta(ruta, false);
        }
        else {
            activarRuta(ruta, true);
            bounds.extend({ lat: ruta.ORIGEN.LAT_PE, lng: ruta.ORIGEN.LON_PE });
            ruta.PEDIDOS.forEach((pedido) => {
                bounds.extend(pedido.marcador.getPosition());
            });
            map.fitBounds(bounds);
        }
    });
}

function buscarRutaXId(id) {
    for (const ruta of jsonRutas) {
        if (ruta.ID === id)
            return ruta;
    }
}
function buscarPedidoXId(id, ruta) {
    if (ruta) {
        for (const pedido of ruta.PEDIDOS) {
            if (pedido.PERU_ID === id)
                return pedido;
        }
    }
    else {
        for (const pedido of jsonPedidos) {
            if (pedido.PERU_ID === id)
                return pedido;
        }
        for (const ruta of jsonRutas) {
            for (const pedido of ruta.PEDIDOS) {
                if (pedido.PERU_ID === id)
                    return pedido;
            }
        }
    }
}

function calcularTiempoTotal(ruta) {
    if (ruta.PEDIDOS.length > 0) {
        const fechaInicial = moment(ruta.FECHA_DESPACHOEXP);
        const horaInicial = moment(ruta.PEDIDOS[0].HORA_SALIDA.HORA_COD, 'HH:mm');
        fechaInicial.set({ 'hours': horaInicial.hours(), 'minutes': horaInicial.minutes() });
        const fechaFinal = moment(ruta.PEDIDOS[ruta.PEDIDOS.length - 1].RUTA_PEDIDO.FH_SALIDA);
        if (fechaFinal < fechaInicial) {
            fechaFinal.year(fechaInicial.year()).dayOfYear(fechaInicial.dayOfYear());
            if (fechaFinal.hours() < fechaInicial.hours()) {
                fechaFinal.add(1, 'd');
            }
        }
        if (ruta.RETORNO === 'S') {
            fechaFinal.add(ruta.TIEMPO_RETORNO, 'm');
        }
        return moment(fechaFinal).diff(fechaInicial, 'seconds');
    }
    else
        return 0;
}

async function moverPedidoRuta(idPedido, idRuta, posicion) {           // Mueve pedidos con ruta asignada a nueva posición
    var esRuta = false;
    var mismaRuta;
    var pedido;
    for (var i = 0; i < jsonPedidos.length; i++) {
        if (jsonPedidos[i].PERU_ID === idPedido) {
            mismaRuta = false;
            pedido = jsonPedidos[i];
            if (jsonPedidos[i].marcador)
                jsonPedidos[i].marcador.setMap(null);
            jsonPedidos.splice(i, 1);
            break;
        }
    }
    if (!pedido) {
        for (var ruta of jsonRutas) {
            for (var i = 0; i < ruta.PEDIDOS.length; i++) {
                if (ruta.PEDIDOS[i].PERU_ID === idPedido) {
                    pedido = ruta.PEDIDOS[i];
                    esRuta = true;
                    mismaRuta = (idRuta === ruta.ID)
                    pedido.RUTA_PEDIDO.RPPE_ID = 0;
                    pedido.RUTA_PEDIDO.SECUENCIA = 0;
                    pedido.RUTA_PEDIDO.TIEMPO = 0;
                    if (pedido.marcador)
                        pedido.marcador.setMap(null);
                    ruta.PEDIDOS.splice(i, 1);
                    if (mismaRuta) {
                        if (posicion === null || posicion === undefined) {
                            ruta.PEDIDOS.push(pedido);
                        }
                        else {
                            ruta.PEDIDOS.splice(posicion, 0, pedido);
                        }
                    }
                    break;
                }
            }
            if (esRuta) {
                ruta.PEDIDOS.forEach((p, i) => p.RUTA_PEDIDO.SECUENCIA = i + 1);
                await guardarRuta(ruta);
                break;
            }
        }
    }
    if (!mismaRuta) {
        for (var ruta of jsonRutas) {
            if (ruta.ID === idRuta) {
                if (posicion === null || posicion === undefined) {
                    ruta.PEDIDOS.push(pedido);
                }
                else {
                    ruta.PEDIDOS.splice(posicion, 0, pedido);
                }
                for (var i = posicion; i < ruta.PEDIDOS.length; i++) {
                    ruta.PEDIDOS[i].RUTA_PEDIDO.SECUENCIA = i + 1;
                }
                await guardarRuta(ruta);
                break;
            }
        }
    }
}
async function eliminarPedidoRuta(id) {
    var esRuta = false;
    for (var ruta of jsonRutas) {
        for (var posicion = 0; posicion < ruta.PEDIDOS.length; posicion++) {
            var pedido = ruta.PEDIDOS[posicion];
            if (pedido.PERU_ID === id) {
                esRuta = true;
                pedido.RUTA_PEDIDO.RPPE_ID = 0;
                pedido.RUTA_PEDIDO.SECUENCIA = 0;
                pedido.RUTA_PEDIDO.TIEMPO = 0;
                if (pedido.marcador)
                    pedido.marcador.setMap(null);
                ruta.PEDIDOS.splice(posicion, 1);
                jsonPedidos.push(crearPedido(pedido));
            }
            if (esRuta && ruta.PEDIDOS[posicion])
                ruta.PEDIDOS[posicion].RUTA_PEDIDO.SECUENCIA = posicion + 1;
        }
        if (esRuta) {
            await guardarRuta(ruta);
            break;
        }
    }
}

var ultimoboton = null;
var containerMaxScroll = 0;

$.fn.extend({
    list2Columns: function (numCols) {
        var listItems = $(this).find('li'); /* get the list data */
        var listHeader = $(this);
        var numListItems = listItems.length;
        var numItemsPerCol = Math.ceil(numListItems / numCols); /* divide by the number of columns requires */
        var currentColNum = 1, currentItemNumber = 1, returnHtml = '', i = 0;

        /* append the columns */
        for (i = 1; i <= numCols; i++) {
            $(this).parent().append('<ul class="column list-column-' + i + '"></ul>');
        }

        /* append the items to the columns */
        $.each(listItems, function (i, v) {
            if (currentItemNumber <= numItemsPerCol) {
                currentItemNumber++;
            } else {
                currentItemNumber = 1;
                currentColNum++;
            }
            $('.list-column-' + currentColNum).append(v);
        });
        $(this).remove();
    }
});

function pillScroll() {
    ultimoboton = null;
    const container = document.getElementById('ul_rutaPills');
    const containerWidth = $(container).width();
    $('.pillRuta').each(function (i, obj) {
        containerMaxScroll = containerMaxScroll + $(obj).outerWidth(true);
    });

    var widthAcumulado = 0;
    var buttons = [];

    buttons.push($(`<button class="scroll-pager activo" type="button" data-scroll="0"></button>`));
    buttons[0].click(function () {
        var scr = parseInt($(this).attr('data-scroll'));
        $('#ul_rutaPills').scrollLeft(scr);
        return false;
    });

    var button = $(`<button class="scroll-pager" type="button"></button>`);
    $('.pillRuta').each(function (i, obj) {
        const pillWidth = $(obj).outerWidth(true);



        if (widthAcumulado + pillWidth > containerWidth * buttons.length) {
            if (widthAcumulado > containerMaxScroll) {
                button.attr('data-scroll', containerMaxScroll);
            }
            else {
                button.attr('data-scroll', Math.round(widthAcumulado));
            }
            $(button).click(function () {
                var scr = parseInt($(this).attr('data-scroll'));
                $('#ul_rutaPills').scrollLeft(scr);
                return false;
            });
            buttons.push(button);
            ultimoboton = button;
            button = $(`<button class="scroll-pager" type="button"></button>`);
        }
        widthAcumulado += pillWidth;
    });
    $('#ul_rutaPills').scroll(function () {
        var final;
        const thisScroll = $(this).scrollLeft();
        $('.scroll-pager').each(function () {
            var pagerScroll = parseInt($(this).attr('data-scroll'));
            if (thisScroll >= (pagerScroll - 150)) {
                final = pagerScroll;
            }


        });
        if (thisScroll >= maxScroll(this) - 150 && ultimoboton != null) {

            $(`.scroll-pager`).removeClass('activo');
            $(ultimoboton).addClass('activo');
        }
        else {

            $(`.scroll-pager[data-scroll="${parseInt(final)}"]`).addClass('activo');
            $(`.scroll-pager[data-scroll!="${parseInt(final)}"]`).removeClass('activo');
        }
    });

    buttons.splice(0, 0, $(`<button class="scroll-pager2" type="button"></button>`));
    buttons[0].click(function () {
        $("#ctl00_Filtro_ctl00").toggle();
        if ($("#dv_pills").hasClass("col-xs-12")) {
            $("#dv_pills").removeClass("col-xs-12")
            $("#dv_pills").addClass("col-xs-9")
        }
        else {
            $("#dv_pills").removeClass("col-xs-9")
            $("#dv_pills").addClass("col-xs-12")

        }
        $('#dv_pillsScrolls').html(pillScroll());
        return false;
    });
    return buttons;
}

// Procesar datos
function btn_nuevo_Click() {
    const hora_id = $('#ddl_buscarHorario').val();
    const fecha_despacho = $('#txt_buscarFecha').val();
    $.ajax({
        type: "POST",
        url: "Mapa_Global.asmx/NuevaRuta",
        data: `{ hora_id: '${hora_id}', fecha_despacho: '${fecha_despacho}'}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var ruta = JSON.parse(data.d);
            console.log(ruta);
            ruta.direcciones = [];
            ruta.PEDIDOS = [];
            ruta.RESPUESTA = [];
            jsonRutas.push(ruta);
            if ($('.pillRuta').length) {
                $('#ul_rutaPills').append(rutaToPill(ruta, true, true));
                $('#dv_rutaTablasCont').append(rutaToTable(ruta, true));
                $(`#sp_tabTiempos${ruta.ID}`).html(secondsToString(0));
                $(`#sp_tabCantidad${ruta.ID}`).html(ruta.PEDIDOS.length);
                $(`#lnk_tab${ruta.ID}`).tab('show');
                $('#hf_idRuta').val(ruta.ID.toString());
                $('#lbl_rutaNumero').html(ruta.NUMERO);

                $('#sp_rutaConductor').html((ruta.CONDUCTOR.COND_ID) ? ruta.CONDUCTOR.COND_NOMBRE : 'Sin conductor');
                $('#sp_rutaTrailer').html((ruta.TRAILER.TRAI_ID) ? ruta.TRAILER.TRAI_PLACA : 'Sin vehículo');
                tablaPuntos();
                $('#dv_pillsScrolls').html(pillScroll());
            }
            else {
                mapa();
            }
        },
        error: function (ex) {
            console.log(moment(fecha_despacho, 'dd-mm-yyyy'));
            console.error("Error " + ex.status + ": " + ex.statusText);
        },
        complete: function () {
        }
    });
}
function eliminarRuta() {
    var id = parseInt($('.pillRuta.active').attr('data-id'));
    var nextId;
    $.ajax({
        type: "POST",
        url: "Mapa_Global.asmx/EliminarRuta",
        data: `{ ruta_id: ${id} }`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            nextId = ($(`#li_tab${id}`).next().length) ? parseInt($(`#li_tab${id}`).next().attr('data-id')) : parseInt($(`#li_tab${id}`).prev().attr('data-id'));
            $(`#dv_tabRuta${id}`).remove();
            $(`#li_tab${id}`).remove();
            $('#modalConf').modal('hide');
            for (var posicion = 0; posicion < jsonRutas.length; posicion++) {
                const ruta = jsonRutas[posicion];
                if (ruta.ID === id) {
                    jsonRutas[posicion].PEDIDOS.forEach((pedido) => {
                        pedido.RUTA_PEDIDO.RPPE_ID = 0;
                        pedido.RUTA_PEDIDO.SECUENCIA = 0;
                        pedido.RUTA_PEDIDO.TIEMPO = 0;
                        if (pedido.marcador)
                            pedido.marcador.setMap(null);
                        ruta.PEDIDOS.splice(posicion, 1);
                        jsonPedidos.push(crearPedido(pedido));
                    });
                    jsonRutas[posicion].direcciones.forEach((direccion) => direccion.setMap(null));
                    jsonRutas.splice(posicion, 1);
                    break;
                }
            }
            if (jsonRutas.length) {
                id = parseInt(nextId);
                $('#hf_idRuta').val(nextId.toString());
                $(`#lnk_tab${nextId}`).tab('show');
                for (var posicion = 0; posicion < jsonRutas.length; posicion++) {
                    const ruta = jsonRutas[posicion];
                    if (ruta.ID === nextId) {
                        $('#lbl_rutaNumero').html(ruta.NUMERO);
                        $('#sp_rutaConductor').html((ruta.CONDUCTOR.COND_ID) ? ruta.CONDUCTOR.COND_NOMBRE : 'Sin conductor');
                        $('#sp_rutaTrailer').html((ruta.TRAILER.TRAI_ID) ? ruta.TRAILER.TRAI_PLACA : 'Sin vehículo');
                        break;
                    }
                }
                tablaPuntos();
                $('#dv_pillsScrolls').html(pillScroll());
            }
            else {
                $('#dv_pills').html('<div class="alert alert-info" role="alert">No hay rutas hoy</div>');
                $('#dv_rutas').html('');
            }
        },
        error: function (ex) {
            nextId = id;
            console.error("Error " + ex.status + ": " + ex.statusText);
        },
        complete: function () {
        }
    });
}
function eliminarRuta2() {
    const id = parseInt($('#hf_idRuta').val());
    const tabAnterior = $(`#lnk_tab${id}`).parent().prev();
    $(`#dv_tabRuta${id}`).remove();
    $(`#lnk_tab${id}`).parent().remove();
    for (var posicion = 0; posicion < jsonRutas.length; posicion++) {
        const ruta = jsonRutas[posicion];
        if (ruta.ID === id) {
            jsonRutas[posicion].PEDIDOS.forEach((pedido) => {
                pedido.RUTA_PEDIDO.RPPE_ID = 0;
                pedido.RUTA_PEDIDO.SECUENCIA = 0;
                pedido.RUTA_PEDIDO.TIEMPO = 0;
                if (pedido.marcador)
                    pedido.marcador.setMap(null);
                ruta.PEDIDOS.splice(posicion, 1);
                jsonPedidos.push(crearPedido(pedido));
            });
            jsonRutas[posicion].direcciones.forEach((direccion) => direccion.setMap(null));
            jsonRutas.splice(posicion, 1);
            break;
        }
    }
    $('#modalConf').modal('hide');
    $('#hf_idRuta').val(tabAnterior.attr('data-id'));
    tabAnterior.children('a').tab('show');
    tablaPuntos();
    $('#dv_pillsScrolls').html(pillScroll());
}
async function guardarRuta(ruta) {
    ruta = await crearRuta(ruta, true);
    $(`#dv_tabRuta${ruta.ID}`).html(rutaToTable(ruta, true));
    $('#lbl_rutaNumero').html(ruta.NUMERO);
    $(`#sp_tabNumero${ruta.ID}`).html(ruta.NUMERO);
    $(`#sp_tabTiempos${ruta.ID}`).html(secondsToString(calcularTiempoTotal(ruta)));
    $(`#sp_tabCantidad${ruta.ID}`).html(ruta.PEDIDOS.length);
    $('#sp_rutaConductor').html((ruta.CONDUCTOR.COND_ID) ? ruta.CONDUCTOR.COND_NOMBRE : 'Sin conductor');
    $('#sp_rutaTrailer').html((ruta.TRAILER.TRAI_ID) ? ruta.TRAILER.TRAI_PLACA : 'Sin vehículo');
    const jsonRuta = JSON.stringify(ruta, (key, value) => {
        if (key === 'direcciones')
            return undefined;
        else if (key === 'marcador')
            return undefined;
        else if (key === 'RESPUESTA')
            return undefined;
        else
            return value;
    });
    $.ajax({
        type: "POST",
        url: "Mapa_Global.asmx/GuardarRuta",
        data: "{ jsonRuta: '" + jsonRuta + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (ex) {
            console.error("Error " + ex.status + ": " + ex.responseJSON.Message);
        }
    });
    tablaPuntos();
}
async function nuevaRuta(jsonRuta) {
    const ruta = JSON.parse(jsonRuta);
    ruta.direcciones = [];
    ruta.PEDIDOS = [];
    jsonRutas.push(ruta);
    $('#ul_rutaPills').append(rutaToPill(ruta, true, true));
    $('#dv_rutaTablasCont').append(rutaToTable(ruta, false));
    $(`#sp_tabTiempos${ruta.ID}`).html(secondsToString(calcularTiempoTotal(ruta)));
    $(`#sp_tabCantidad${ruta.ID}`).html(ruta.PEDIDOS.length);
    $(`#lnk_tab${ruta.ID}`).tab('show');
    $('#dv_pillsScrolls').html(pillScroll());
}
async function colorRuta() {
    const idRuta = parseInt($('#hf_idRuta').val());
    const color = $('#txt_editColor').val();
    for (var ruta of jsonRutas) {
        if (ruta.ID === idRuta) {
            ruta.RUTA_COLOR = color;
            $(`#btn_rutaActiva${ruta.ID}`).attr('data-color', ruta.RUTA_COLOR);
            $(`#btn_rutaActiva${ruta.ID}`).css('color', ruta.RUTA_COLOR);
            $(`#li_tab${ruta.ID}`).attr('data-color', ruta.RUTA_COLOR);
            $('#btn_rutaColor').css('color', ruta.RUTA_COLOR);
            await guardarRuta(ruta);
        }
    }
}
async function guardarResponse(response) {

}
// Checkbox
async function chk_rutaCircular_Check(o) {
    const id = parseInt($('#hf_idRuta').val());
    for (var ruta of jsonRutas) {
        if (ruta.ID === id) {
            ruta.RETORNO = ($(o).prop('checked')) ? 'S' : 'N';
            $('#hf_retorno').val(ruta.RETORNO);
            await guardarRuta(ruta);
            break;
        }
    }
}

async function mapa() {
    cargamapa(mapObject);
    crearOrigenes();
    crearPedidos(jsonPedidos);
    if (jsonRutas.length) {
        $('#dv_pills').html(crearCabecera());
        $('#dv_rutas').html(crearTabla());
        showSpinner('dv_pills');
        showSpinner('dv_rutas');
        await crearRutas(10);
        tablaPuntos();
    }
    else {
        $('#dv_pills').html('<div class="alert alert-info" role="alert">No hay rutas hoy</div>');
        $('#dv_rutas').html('');
    }
}
function tablaPuntos() {
    if ($.fn.DataTable.isDataTable(`.tab-pane.active>table`)) {
        $(`.tab-pane.active>table`).DataTable().draw();
    }
    else if ($(`.tab-pane.active>table>thead`).length > 0) {
        $('.tab-pane.active>table').DataTable({
            "scrollY": dtHeight(),
            "scrollX": false,
            "scrollCollapse": true,
            "paging": false,
            "ordering": false,
            "searching": false,
            "lengthChange": false,
            "info": false
        });
    }
    reOffset1();
}
// DropDownList
async function ddl_editConductor_SelectedIndexChanged(sender, args) {
    const item = sender.findItemByText(sender.get_text());
    if (sender.get_text() == '') {
        sender.findItemByValue('0').select();
        return false;
    }
    if (!item) {
        sender.findItemByValue('0').select();
        showAlertClass('guardar', 'warn_conductorNoExiste');
        return false;
    }
    if (!item.get_enabled()) {
        sender.findItemByValue('0').select();
        showAlertClass('guardar', 'warn_conductorEnUso');
        return false;
    }
    const idRuta = parseInt($('#hf_idRuta').val());
    const cond_id = parseInt(args.get_item().get_value());
    for (var ruta of jsonRutas) {
        if (ruta.ID === idRuta) {
            ruta.CONDUCTOR.COND_ID = cond_id;
            if (cond_id === 0) {
                ruta.CONDUCTOR.COND_NOMBRE = null;
                ruta.CONDUCTOR.COND_RUT = null;
            }
            else {
                const cond = args.get_item().get_text();
                ruta.CONDUCTOR.COND_RUT = cond.split('/')[0];
                ruta.CONDUCTOR.COND_NOMBRE = cond.split('/')[1];
            }
            await guardarRuta(ruta);
        }
    }
}
async function ddl_editTracto_SelectedIndexChanged(sender, args) {
    const item = sender.findItemByText(sender.get_text());
    if (sender.get_text() == '') {
        sender.findItemByValue('0').select();
        return false;
    }
    if (!item) {
        sender.findItemByValue('0').select();
        showAlertClass('guardar', 'warn_tractoNoExiste');
        return false;
    }
    if (!item.get_enabled()) {
        sender.findItemByValue('0').select();
        showAlertClass('guardar', 'warn_tractoEnUso');
        return false;
    }
    const idRuta = parseInt($('#hf_idRuta').val());
    const trac_id = parseInt(args.get_item().get_value());
    for (var ruta of jsonRutas) {
        if (ruta.ID === idRuta) {
            ruta.TRACTO.TRAC_ID = trac_id;
            await guardarRuta(ruta);
        }
    }
}
async function ddl_editTrailer_SelectedIndexChanged(sender, args) {
    const item = sender.findItemByText(sender.get_text());
    if (sender.get_text() == '') {
        sender.findItemByValue('0').select();
        return false;
    }
    if (!item) {
        sender.findItemByValue('0').select();
        showAlertClass('guardar', 'warn_trailerNoExiste');
        return false;
    }
    if (!item.get_enabled()) {
        sender.findItemByValue('0').select();
        showAlertClass('guardar', 'warn_trailerEnUso');
        return false;
    }
    const idRuta = parseInt($('#hf_idRuta').val());
    const trai_id = parseInt(args.get_item().get_value());
    for (var ruta of jsonRutas) {
        if (ruta.ID === idRuta) {
            ruta.TRAILER.TRAI_ID = trai_id;
            if (trai_id === 0) {
                ruta.TRAILER.TRAI_PLACA = null;
            }
            else {
                const trai = args.get_item().get_text();
                ruta.TRAILER.TRAI_PLACA = trai;
            }
            await guardarRuta(ruta);
        }
    }
}
async function txt_editNombre_TextChanged(sender, args) {
    const idRuta = parseInt($('#hf_idRuta').val());
    for (var ruta of jsonRutas) {
        if (ruta.ID === idRuta) {
            if (args === '' || !args) {
                sender.value = ruta.NUMERO;
                showAlertClass('guardar', 'warn_numeroVacio');
                return false;
            }
            ruta.NUMERO = args;
            await guardarRuta(ruta);
        }
    }
}
// Botones puntos
async function btn_puntoAgregar_Click(objeto) {
    const idPedido = parseInt($(objeto).attr('data-id'));
    const idRuta = parseInt($('#hf_idRuta').val());
    moverPedidoRuta(idPedido, idRuta);
    $('.sel-between').removeClass('activo');
}
function btn_puntoSeleccionar_Click(objeto) {
    const rutaId = parseInt($(objeto).parent().parent().parent().attr('data-id'));
    const pedidoId = parseInt($(objeto).parent().parent().attr('data-id'));
    const ruta = buscarRutaXId(rutaId);
    const pedido = buscarPedidoXId(pedidoId, ruta);
    seleccionarPedido(pedido, (ruta.PEDIDOS.length > 1));
}
async function btn_selBetween_Click(objeto) {
    const idPedido = parseInt($('#hf_idPedido').val());
    const idRuta = parseInt($(objeto).parent().attr('data-id'));
    const pos = parseInt($(objeto).attr('data-pos'));
    if (isNaN(pos))
        moverPedidoRuta(idPedido, idRuta);
    else
        moverPedidoRuta(idPedido, idRuta, pos);
    $('.sel-between').removeClass('activo');
    $('#txt_puntoSeleccionado').val('');
}
async function btn_puntoSubir_Click(indexPedido, idPedido) {
    const idRuta = parseInt($('#hf_idRuta').val());
    moverPedidoRuta(idPedido, idRuta, indexPedido - 1);
}
async function btn_puntoBajar_Click(indexPedido, idPedido) {
    const idRuta = parseInt($('#hf_idRuta').val());
    moverPedidoRuta(idPedido, idRuta, indexPedido + 1);
}
async function btn_puntoEliminar_Click(id) {
    eliminarPedidoRuta(id);
}
// Botones footer
function btn_rutaColor_Click() {
    const color = $('#btn_rutaColor').attr('data-color');
    $('#txt_editColor').val(color);
    var colorpicker = $.farbtastic('#colorpicker');
    colorpicker.setColor(color);
    $('#modalColor').modal();
}
function btn_rutaVehiculo_Click() {
    $('#modalVehiculo').modal();
}
function btn_rutaEnfocar_Click(sender) {
    var id;
    if (sender) {
        id = parseInt($(sender).attr('data-id'));
        $('#hf_idRuta').val(id);
    }
    else
        id = parseInt($('#hf_idRuta').val());
    const itemsActivos = $(`.btn-ruta[data-id!=${id}]`);
    const o = $(`#btn_rutaActiva${id}`);
    o.removeClass('btn-default');
    o.addClass('btn-success');
    itemsActivos.removeClass('btn-success');
    itemsActivos.addClass('btn-default');
    enfocarRuta(id, true);
}
// Botones cabecera
function btn_todos_Click(mostrar) {
    if (mostrar) {
        $('.btn-ruta.btn-default').each((i, btn) => btn_rutaActiva_Click(btn));
    }
    else {
        $('.btn-ruta.btn-success').each((i, btn) => btn_rutaActiva_Click(btn));
    }
}
function btn_rutaActiva_Click(o) {
    const activo = $(o).hasClass('btn-success');
    const id = parseInt($(o).attr('data-id'));
    if (activo) {
        $(o).removeClass('btn-success');
        $(o).addClass('btn-default');
    }
    else {
        $(o).removeClass('btn-default');
        $(o).addClass('btn-success');
    }

    activarRuta(buscarRutaXId(id), !activo);
}

// PageLoad
Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler1);
function EndRequestHandler1(sender, args) {
    $('#colorpicker').farbtastic('#txt_editColor');
}
$(window).resize(function () {
    $('#dv_pillsScrolls').html(pillScroll());
    reOffset1();
});
var dtHeight = function () {
    return $(window).height() - $(".tab-pane.fade.active.in").offset().top - 180;
};
var mapHeight = function () {
    return $(window).height() - $("#map").offset().top - 35;
};
function reOffset1() {
    if ($('div.dataTables_scrollBody').length > 0)
        $('div.dataTables_scrollBody').css('maxHeight', dtHeight());
    $('#map').height(mapHeight());
}