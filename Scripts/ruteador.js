var map;
var directionsService = new google.maps.DirectionsService();
var jsonRuta;
var jsonPedidos;
var jsonOrigenes;
const infowindow = new google.maps.InfoWindow();
function cargamapa(zoom1, mapObj, lat, lon) {
    var center = (!lat || !lon) ? new google.maps.LatLng(-33.45238466, -70.65735526) : new google.maps.LatLng(lat, lon);
    map = new google.maps.Map(mapObj, {
        center: center,
        zoom: zoom1,
        gestureHandling: 'greedy'
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
    var contenido  ;
   


    if (pedido.HORA_SALIDA){
    contenido = `<div id="content">
                <p>
                Código: ${pedido.PERU_CODIGO}
                <br />
                Número: ${pedido.PERU_NUMERO}
                <br />
                Hora: ${pedido.HORA_SALIDA.HORA_COD}
                <br />
                Dirección: ${pedido.PERU_DIRECCION}
                </p>
                </div>
                <button id="btn_puntoAgregar" title="Agregar al final de la ruta" onclick="moverPedidoRuta(${pedido.PERU_ID});" type="button" class="btn btn-sm btn-success">
                    <span class="glyphicon glyphicon-plus" />
                </button>`;
    }
    else
    {
        contenido = `<div id="content">
                <p>
                Código: ${pedido.PERU_CODIGO}
                <br />
                Número: ${pedido.PERU_NUMERO}
                <br />
                Hora: ${pedido.HORA_COD}
                <br />
                Dirección: ${pedido.PERU_DIRECCION}
                </p>
                </div>
                <button id="btn_puntoAgregar" title="Agregar al final de la ruta" onclick="moverPedidoRuta(${pedido.PERU_ID});" type="button" class="btn btn-sm btn-success">
                    <span class="glyphicon glyphicon-plus" />
                </button>`;


    }


    pedido.marcador.addListener('click', function () {
        infowindow.setContent(contenido);
        infowindow.open(map, pedido.marcador);
        seleccionarPedido(pedido, true);
    });
    return pedido;
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
            , icon: icon
        });
    });
}

function crearPoligono() {
    if (jsonRuta.triangle)
        jsonRuta.triangle.setMap(null);
    jsonRuta.triangle = null;
    var coord = [];
    coord.push({ lat: jsonRuta.ORIGEN.LAT_PE, lng: jsonRuta.ORIGEN.LON_PE });
    coord = coord.concat(jsonRuta.PEDIDOS.map((pedido) => new google.maps.LatLng({ lat: pedido.PERU_LATITUD, lng: pedido.PERU_LONGITUD })));
    if (jsonRuta.RETORNO.toUpperCase() === 'S') {
        coord.push({ lat: jsonRuta.ORIGEN.LAT_PE, lng: jsonRuta.ORIGEN.LON_PE })
    }
    jsonRuta.triangle = new google.maps.Polygon({
        paths: coord,
        strokeColor: jsonRuta.RUTA_COLOR,
        strokeOpacity: 0.8,
        strokeWeight: 2,
        fillOpacity: 0.0,
        fillColor: jsonRuta.RUTA_COLOR
    });
    const poligono = $('#rb_poligono:checked,#rb_ambos:checked').length > 0;
    jsonRuta.triangle.setMap((poligono) ? map : null);
}

Object.defineProperty(Array.prototype, 'chunk', {
    value: function (chunkSize) {
        var R = [];
        for (var i = 0; i < this.length; i += chunkSize)
            R.push(this.slice(i, i + chunkSize));
        return R;
    }
});

var contador_global = 0;
var tiempos_globales = [];
var duracion_total = 0;

async function crearRuta(refrescar) {
    if (refrescar) {
        if (jsonRuta.direcciones)
            jsonRuta.direcciones.forEach((direccion) => direccion.setMap(null));
        jsonRuta.PEDIDOS.forEach((pedido) => pedido.marcador.setMap(null));
    }
    jsonRuta.direcciones = [];
    var cont_global = 0;
    var chinkos = [];
    
    var origen_relativo = new google.maps.LatLng({ lat: jsonRuta.ORIGEN.LAT_PE, lng: jsonRuta.ORIGEN.LON_PE });
    var destino_relativo;
    if (jsonRuta.PEDIDOS) {
        chinkos=Array.from(jsonRuta.PEDIDOS).chunk(22);
        jsonRuta.PEDIDOS.forEach((pedido, indexPedido) => {
            pedido.marcador = new google.maps.Marker({
                map: map
                , position: new google.maps.LatLng(pedido.PERU_LATITUD, pedido.PERU_LONGITUD)
                , title: jsonRuta.NUMERO + ' / ' + pedido.PERU_NUMERO
                , icon: {
                    path: 'M 0,0 C -2,-20 -10,-22 -10,-30 A 10,10 0 1,1 10,-30 C 10,-22 2,-20 0,0 z',
                    fillColor: jsonRuta.RUTA_COLOR,
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
                    color: invertColor(jsonRuta.RUTA_COLOR)
                }
            });
            const contenido = `<div id="content">
                <p>
                    Código: ${pedido.PERU_CODIGO}
                    <br />
                    Número: ${pedido.PERU_NUMERO}
                    <br />
                    Dirección: ${pedido.PERU_DIRECCION}
                </p>
                    <div class="btn-group" role="group">
                        <button id="btn_puntoEliminar" title="Eliminar pedido de ruta" onclick="eliminarPedidoRuta(${pedido.PERU_ID});" type="button" class="btn btn-sm btn-danger" ${(jsonRuta.PEDIDOS.length > 1) ? '' : 'disabled'}>
                            <span class="glyphicon glyphicon-remove" />
                        </button>
                        </div>
                    </div>`;
            pedido.marcador.addListener('click', function () {
                infowindow.setContent(contenido);
                infowindow.open(map, pedido.marcador);
                seleccionarPedido(pedido, (jsonRuta.PEDIDOS.length > 1));
            });
        });
    }
    else {
        jsonRuta.PEDIDOS = [];
    }
    var fechaInicial = moment(jsonRuta.FECHA_DESPACHOEXP);
    var HoraInicialRelativa = moment(jsonRuta.HORARIO.HORA_COD, 'HH:mm');
    fechaInicial.set({ 'h': HoraInicialRelativa.hour(), 'm': HoraInicialRelativa.minute() })
    const ruta = $('#rb_ruta:checked,#rb_ambos:checked').length > 0;
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
            map: (ruta) ? map : null,
            polylineOptions: {
                strokeColor: jsonRuta.RUTA_COLOR
            }
        });
        await directionsService.route(req).
            then((response) => {
                directionsRenderer.setDirections(response);
                if (refrescar) {
                    for (const tramo of response.routes[0].legs) {
                        jsonRuta.PEDIDOS[cont_global].tiempo = Math.round(tramo.duration.value / 60);
                        fechaInicial = fechaInicial.add(tramo.duration.value, 's');
                        jsonRuta.PEDIDOS[cont_global].RUTA_PEDIDO.FH_LLEGADA = fechaInicial.format();
                        HoraInicialRelativa = fechaInicial.add(parseInt(jsonRuta.PEDIDOS[cont_global].PERU_TIEMPO), 'm');
                        jsonRuta.PEDIDOS[cont_global].RUTA_PEDIDO.FH_SALIDA = fechaInicial.format();
                        cont_global++;
                    }
                }
            });
        jsonRuta.direcciones.push(directionsRenderer);
        origen_relativo = destino_relativo;
    }
    if (jsonRuta.RETORNO.toUpperCase() === 'S') {
        directionsService = new google.maps.DirectionsService();
        const req = {
            origin: origen_relativo
            , destination: new google.maps.LatLng(jsonRuta.ORIGEN.LAT_PE, jsonRuta.ORIGEN.LON_PE)
            , travelMode: "DRIVING"
            , optimizeWaypoints: false
        };
        const directionsRenderer = new google.maps.DirectionsRenderer({
            preserveViewport: true
            , suppressMarkers: true
            , map: (ruta) ? map : null
            , polylineOptions: {
                strokeColor: jsonRuta.RUTA_COLOR
            }
        });
        await directionsService.route(req,
            function (response, status) {
                if (status === "OK") {
                    directionsRenderer.setDirections(response);
                    jsonRuta.TIEMPO_RETORNO = Math.round(response.routes[0].legs[0].duration.value / 60);
                }
                else {
                    msj("No se pudo crear una ruta: " + status, "warn", true);
                }
            }
        );
        jsonRuta.direcciones.push(directionsRenderer);
    }
    $('#lbl_puntoSalida').text('Duración: ' + secondsToString(calcularTiempoTotal()));
    return new Promise(resolve => resolve(jsonRuta));
}
async function moverPedidoRuta(idPedido, posicion) {

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
        for (var i = 0; i < jsonRuta.PEDIDOS.length; i++) {
            if (jsonRuta.PEDIDOS[i].PERU_ID === idPedido) {
                pedido = jsonRuta.PEDIDOS[i];
                pedido.RUTA_PEDIDO.RPPE_ID = 0;
                pedido.RUTA_PEDIDO.SECUENCIA = 0;
                pedido.RUTA_PEDIDO.TIEMPO = 0;
                if (pedido.marcador)
                    pedido.marcador.setMap(null);
                jsonRuta.PEDIDOS.splice(i, 1);
                break;
            }
        }
    }

      if (!pedido.HORA_SALIDA){

             pedido.HORA_SALIDA=jsonRuta.HORARIO;
     }


    if (!pedido.RUTA_PEDIDO)
    {
    pedido.RUTA_PEDIDO={SECUENCIA:0,RPPE_ID:0,TIEMPO:0,FH_LLEGADA:new Date(),FH_PLANIFICA:new Date(),FH_SALIDA:new Date()};
    }



    if (posicion === null || posicion === undefined) {
        jsonRuta.PEDIDOS.push(pedido);
    }
    else {
        jsonRuta.PEDIDOS.splice(posicion, 0, pedido);
    }
    jsonRuta.PEDIDOS.forEach((p, i) => p.RUTA_PEDIDO.SECUENCIA = i + 1);
    crearPoligono();
    await crearRuta(true);
    $('#tbl_puntos').html(jsonToTable());
    tabla2();
}
async function eliminarPedidoRuta(idPedido) {
    var pedido;
    for (var i = 0; i < jsonRuta.PEDIDOS.length; i++) {
        if (jsonRuta.PEDIDOS[i].PERU_ID === idPedido) {
            pedido = jsonRuta.PEDIDOS[i];
            pedido.RUTA_PEDIDO.RPPE_ID = 0;
            pedido.RUTA_PEDIDO.SECUENCIA = 0;
            pedido.RUTA_PEDIDO.TIEMPO = 0;
            if (pedido.marcador)
                pedido.marcador.setMap(null);
            jsonRuta.PEDIDOS.splice(i, 1);
            break;
        }
    }
    jsonPedidos.push(crearPedido(pedido));
    crearPoligono();
    await crearRuta(true);
    $('#tbl_puntos').html(jsonToTable());
    tabla2();
}

function mostrar(ruta, poligono) {
    jsonRuta.direcciones.forEach((direccion) => {
        if (ruta) direccion.setMap(map);
        else direccion.setMap(null)
    });
    if (poligono) jsonRuta.triangle.setMap(map);
    else jsonRuta.triangle.setMap(null);
}
function limpiarRuta() {
    if (jsonRuta.direcciones) {
        jsonRuta.direcciones.forEach((dir) => dir.setMap(null));
    }
    if (jsonRuta.triangle) {
        jsonRuta.triangle.setMap(null);
          jsonRuta.triangle.setPaths(null);
    }
    jsonRuta.direcciones = [];
  

    jsonRuta.PEDIDOS.forEach((pedido) => {
        if (pedido.marcador) {
            pedido.marcador.setMap(null);
        }
    });
}

function jsonToTable() {
    var fecha0 = moment(jsonRuta.FECHA_DESPACHOEXP);
    var table = $(`<table id="gv_puntos" style="width:100%" class="table table-condensed table-hover tablita"></table>`);
    var head = $(`<thead>
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
                    <td class="colselecciona">
                    </td>
                    <td>
                    ${jsonRuta.ORIGEN.NOMBRE_PE}
                    </td>
                    <td>
                    ${jsonRuta.ORIGEN.DIRECCION_PE}
                    </td>
                    <td id="t_origen">
                    ${moment(jsonRuta.HORARIO.HORA_COD, 'HH:mm').format('HH:mm:ss')}
                    </td>
                    </tr>
                    </thead>`);

    var btnseleccionar = $(`<button type="button" class="btn-borderless" style="font-weight:bold">
                            0
                        </button>`);
    btnseleccionar.click(function () {
        centrarLatLon(jsonRuta.ORIGEN.LAT_PE, jsonRuta.ORIGEN.LON_PE);
        return false;
    });
    head.children('colselecciona').append(btnseleccionar);
    table.append(head);
    var body = $(`<tbody></tbody>`);
    jsonRuta.PEDIDOS.forEach((o, i) => {
        tiempoFIN = moment(o.RUTA_PEDIDO.FH_LLEGADA);
        i1 = i;
        var selBetween = $(`<tr class="sel-between">
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            </tr>`);
        selBetween.click(function () {
            var id = parseInt($('#hf_idPedido').val());
            moverPedidoRuta(id, i);
            $('#tbl_puntos').html(jsonToTable());
            $('.sel-between').removeClass('enabled');
        });
        body.append(selBetween);
        body.append(pedidoToRow(o, i));
    });
    var selBetween = $(`<tr class="sel-between">
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            </tr>`);
    selBetween.click(function () {
        var id = parseInt($('#hf_idPedido').val());
        moverPedidoRuta(id, 0);
        $('#tbl_puntos').html(jsonToTable());
        $('.sel-between').removeClass('enabled');
    });
    body.append(selBetween);
    const circular = $('#hf_circular').val() === 'S';
    var footer = $(`<tfoot>
                        <tr>
                        <td>
                        <input id="chk_circular" onclick="chk_rutaCircular_Check(this);" type="checkbox" ${(circular) ? 'checked' : ''} />
                        </td>
                        <td>
                        </td>
                        <td>
                        <a style="font-weight:bold" href="#" onclick="centrarLatLon(${jsonRuta.ORIGEN.LAT_PE}, ${jsonRuta.ORIGEN.LON_PE});">0</a>
                        </td>
                        <td>
                        ${jsonRuta.ORIGEN.NOMBRE_PE}
                        </td>
                        <td>
                        ${jsonRuta.ORIGEN.DIRECCION_PE}
                        </td>
                        <td>&nbsp;
                        </td>
                        </tr>
                        </tfoot>`);
    table.append(body);
    table.append(footer);

    $('#txt_cant_punt').text('Destinos: ' + jsonRuta.PEDIDOS.length);
    return table;
}
function pedidoToRow(pedido, posicion) {
    var row = $(`<tr>
                <td class="colmover"></td>
                <td class="colborrar"></td>
                <td class="colseleccionar letra"></td>
                </tr>`);
    var btnmoverArriba = $(`<button type="button" class="btn-borderless" data-id="${pedido.PERU_ID}">
                                <span style="font-size:small" class="glyphicon glyphicon-menu-up"></span>
                                </button>`);
    btnmoverArriba.click(function () {
        moverPedidoRuta(pedido.PERU_ID, posicion - 1);
        return false;
    });
    var btnmoverAbajo = $(`<button type="button" class="btn-borderless" data-id="${pedido.PERU_ID}">
                                <span style="font-size:small" class="glyphicon glyphicon-menu-down"></span>
                                </button>`);
    btnmoverAbajo.click(function () {
        moverPedidoRuta(pedido.PERU_ID, posicion + 1);
        return false;
    });
    var btnborrar = $(`<button type="button" class="btn-borderless" >
                        <span style="font-size:small" class="glyphicon glyphicon-remove text-danger" />
                        </button>`);
    btnborrar.click(function () {
        eliminarPedidoRuta(pedido.PERU_ID);
        $('#tbl_puntos').html(jsonToTable());
        rutaToJson();
        return false;
    });
    if (jsonRuta.PEDIDOS.length > 1) {
        if (posicion > 0) {
            row.children('td.colmover').append(btnmoverArriba);
        }
        if (posicion != jsonRuta.PEDIDOS.length - 1) {
            row.children('td.colmover').append(btnmoverAbajo);
        }
        row.children('td.colborrar').append(btnborrar);
    }
    var btnseleccionar = $(`<button type="button" class="btn-borderless" style="font-weight:bold">
                        ${(posicion + 1).toString()}
                        </button>`);
    btnseleccionar.click(function () {
        seleccionarPedido(pedido, (jsonRuta.PEDIDOS > 1))
        centrarLatLon(pedido.PERU_LATITUD, pedido.PERU_LONGITUD);
        return false;
    });
    row.children('td.colseleccionar').append(btnseleccionar);
    row.append(`<td onclick="selecciona(${pedido.PERU_ID});">
                ${pedido.PERU_CODIGO}
                </td>
                <td>
                ${pedido.PERU_DIRECCION}
                </td>
                <td id="t_${pedido.PERU_CODIGO}">
                ${moment(pedido.RUTA_PEDIDO.FH_LLEGADA).format('HH:mm:ss')}
                </td>`);
    return row;
}

function seleccionarPedido(pedido, selbetween) {
    $('#hf_idPedido').val(pedido.PERU_ID);
    if (selbetween) {
        $('.sel-between').addClass('enabled');
        $('#btn_puntoAgregar').prop('disabled', false);
    }
    else {
        $('.sel-between').removeClass('enabled');
        $('#btn_puntoAgregar').prop('disabled', true);
    }
    $find('ddl_puntoNombre').findItemByValue(pedido.PERU_ID.toString()).select();
}

function centrarPunto(id) {
    for (var pedido of jsonRuta.PEDIDOS) {
        if (id === pedido.PERU_ID) {
            const latLon = { lat: pedido.PERU_LATITUD, lng: pedido.PERU_LONGITUD };
            map.setCenter(latLon);
            break;
        }
    }
}
function centrarLatLon(lat, lon) {
    const latLon = new google.maps.LatLng(lat, lon);
    map.setCenter(latLon);
}

function rutaToJson() {
    $('#hf_jsonRuta').val(JSON.stringify(jsonRuta, (key, value) => {
        if (key === 'direcciones')
            return undefined;
        else if (key === 'marcador')
            return undefined;
        else if (key === 'triangle')
            return undefined;
        else
            return value;
    }));
}
function calcularTiempoTotal() {
    if (jsonRuta.PEDIDOS.length > 0) {
        const fechaInicial = moment(jsonRuta.FECHA_DESPACHOEXP);
        const horaInicial = moment(jsonRuta.PEDIDOS[0].HORA_SALIDA.HORA_COD, 'HH:mm');
        fechaInicial.set({ 'hours': horaInicial.hours(), 'minutes': horaInicial.minutes() });
        const fechaFinal = moment(jsonRuta.PEDIDOS[jsonRuta.PEDIDOS.length - 1].RUTA_PEDIDO.FH_SALIDA);
        if (fechaFinal < fechaInicial) {
            fechaFinal.year(fechaInicial.year()).dayOfYear(fechaInicial.dayOfYear());
            if (fechaFinal.hours() < fechaInicial.hours()) {
                fechaFinal.add(1, 'd');
            }
        }
        if (jsonRuta.RETORNO === 'S') {
            fechaFinal.add(jsonRuta.TIEMPO_RETORNO, 'm');
        }
        return moment(fechaFinal).diff(fechaInicial, 'seconds');
    }
    else
        return 0;
}

// DropDownList
function ddl_vehiculoConductor_SelectedIndexChanged(sender, args) {
    const cond_id = parseInt(args.get_item().get_value());
    console.log(cond_id);
    if (jsonRuta)
        jsonRuta.CONDUCTOR.COND_ID = cond_id;
}
function ddl_vehiculoTracto_SelectedIndexChanged(sender, args) {
    const trac_id = parseInt(args.get_item().get_value());
    console.log(trac_id);
    if (jsonRuta)
        jsonRuta.TRACTO.TRAC_ID = trac_id;
}
function ddl_vehiculoTrailer_SelectedIndexChanged(sender, args) {
    const trai_id = parseInt(args.get_item().get_value());
    console.log(trai_id);
    if (jsonRuta)
        jsonRuta.TRAILER.TRAI_ID = trai_id;
}
function ddl_puntoNombre_SelectedIndexChanged(sender, args) {
    const selectedItem = args.get_item();
    if (!selectedItem) {
        sender.clearSelection();
        $('#hf_idPedido').val('');
        $('.sel-between').removeClass('enabled');
        showAlertClass('guardar', 'warn_pedidoNoExiste');
        return false;
    }
    if (selectedItem.get_index() < 1) {
        $('#hf_idPedido').val('');
        $('.sel-between').removeClass('enabled');
    }
    else {
        const id = parseInt(selectedItem.get_value());
        centrarPunto(id);
        $('#hf_idPedido').val(id);
        $('.sel-between').addClass('enabled');
    }
}
// Checkbox
async function chk_rutaCircular_Check(o) {
    const circular = $(o).prop('checked');
    jsonRuta.RETORNO = (circular) ? 'S' : 'N';
    $('#hf_circular').val(jsonRuta.RETORNO);
    crearPoligono();
    await crearRuta(true);
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

    if (!$.fn.DataTable.isDataTable('#gv_puntos')) {
        $('#gv_puntos').DataTable({
            "scrollY": "38vh",
            "scrollX": false,
            "scrollCollapse": true,
            "paging": false,
            "ordering": false,
            "searching": false,
            "lengthChange": false,
            "info": false
        });
    }
    else {
        $('#gv_puntos').DataTable().draw();
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

async function mapa(nuevo) {
    $('#rb_ambos').prop('checked', true);
    const mapObject = document.getElementById('map');
    jsonPedidos = JSON.parse($('#hf_jsonPedidos').val());
    jsonOrigenes = JSON.parse($('#hf_jsonOrigenes').val());
    jsonRuta = JSON.parse($('#hf_jsonRuta').val());
    
    cargamapa(12, mapObject);

   if (nuevo==false) { $('#tbl_puntos').html(jsonToTable()); crearPedidos();}
   else
   {
   jsonRuta.ORIGEN=jsonOrigenes[0];
   jsonRuta.RETORNO='N';
   $('#tbl_puntos').html(jsonToTable());
   crearPedidos();
   }

    crearOrigenes();
    if (!nuevo) {
        crearPoligono();
        await crearRuta(false);
        var bounds = new google.maps.LatLngBounds();
        bounds.extend({ lat: jsonRuta.ORIGEN.LAT_PE, lng: jsonRuta.ORIGEN.LON_PE });
        jsonRuta.PEDIDOS.forEach((pedido) => {
            bounds.extend({ lat: pedido.PERU_LATITUD, lng: pedido.PERU_LONGITUD });
        });
        setTimeout(function () { map.fitBounds(bounds) }, 500);
    }
    else {
        await crearRuta(false);
        limpiarRuta();
    }
}
async function guardarRuta(ruta) {
    await crearRuta(ruta, true);
    rutaToJson();
    $('#btn_puntosGuardar').click();
}
function limpiarPuntos() {
    $('#hf_idPedido').val('');
    $('.sel-between').removeClass('enabled');
    $find('ddl_puntoNombre').findItemByValue('0').select();
}
function selecciona(id) {

    $('#hf_idPedido').val(id);
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
    $("#lblgds").html($(".gridCB:checked").length);
    console.log(ids);
}
function checkIndividual(objeto) {
    var cant = parseInt($("#lblgds").html());
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
    $("#lblgds").html($(".gridCB:checked").length);
    console.log(ids);
}
function exportar() {
    ids = [];
    setTimeout(timeexporta, 200);
}
function timeexporta() {
    $("#btn_exportarExcel").click();
}
function exportarpdf() {
    setTimeout(timeexportapdf, 200);
}
function timeexportapdf() {
    $("#pdf_post").click();
}

function restauracheck() {

    var i = 0;
    while (i < ids.length) {
        $("[data-id=" + ids[i] + "]").find(".gridCB").prop('checked', true);
        i = i + 1;

    }
    $("#lblgds").html($(".gridCB:checked").length);
}


Sys.WebForms.PageRequestManager.getInstance().add_endRequest(restauracheck);