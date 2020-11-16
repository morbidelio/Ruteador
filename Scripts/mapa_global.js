var map;
var jsonOrigenes = [];
var jsonPedidos = [];
var jsonRutas = [];
var directionsService = new google.maps.DirectionsService();
const infowindow = new google.maps.InfoWindow();

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
}
function crearPedidos() {
    jsonPedidos.forEach(function (o) {
        o = crearPedido(o);
    });
}
function crearPedido(pedido) {
    pedido.marcador = new google.maps.Marker({
        map: map
        , position: new google.maps.LatLng(pedido.PERU_LATITUD, pedido.PERU_LONGITUD)
        , title: pedido.PERU_NUMERO
        , icon: '../img/icon_pedido.png'
    });
    const contenido = `<div id="content">
                <p>
                Código: ${pedido.PERU_CODIGO}
                <br />
                Número: ${pedido.PERU_NUMERO}
                <br />
                Hora: ${pedido.HORA_COD}
                <br />
                Dirección: ${pedido.PERU_DIRECCION}
                </p>
                <button id="btn_rutaAgregar" onclick="btn_rutaAgregar_Click(${pedido.PERU_ID});" type="button" class="btn btn-success">
                    <span class="glyphicon glyphicon-plus" />
                </button>
                </div>`;
    pedido.marcador.addListener('click', function () {
        infowindow.setContent(contenido);
        infowindow.open(map, pedido.marcador);
    });
    return pedido;
}
async function crearRutas() {
    for (var ruta of jsonRutas) {
        ruta = await crearRuta(ruta);
    }
}
async function crearRuta(ruta, refrescar) {
    if (refrescar) {
        ruta.direcciones.forEach((direccion) => direccion.setMap(null));
        ruta.PEDIDOS.forEach((pedido) => pedido.marcador.setMap(null));
    }
    var cont_global = 0;
    var chinkos = Array.from(ruta.PEDIDOS).chunk(22);
    var origen_relativo = ruta.ORIGEN;
    var destino_relativo;
    ruta.direcciones = [];
    ruta.PEDIDOS.forEach((pedido, indexPedido) => 
        pedido.marcador = new google.maps.Marker({
            map: map
            , position: new google.maps.LatLng(pedido.PERU_LATITUD, pedido.PERU_LONGITUD)
            , title: pedido.PERU_CODIGO + ' / ' + pedido.PERU_NUMERO
            , icon: '../img/marker_red.png'
            , label: {
                text: (indexPedido + 1).toString(),
                fontFamily: 'Britannic',
                fontWeight: 'bolder',
                fontSize: '14px',
                color: 'white'
            }
        }));
    var fechaInicial = moment(ruta.FECHA_DESPACHOEXP);
    var HoraInicialRelativa = moment(ruta.PEDIDOS[0].HORA_SALIDA.HORA_COD, 'HH:mm');
    fechaInicial.set({ 'h': HoraInicialRelativa.hour(), 'm': HoraInicialRelativa.minute()})
    for (const chink of chinkos) {
        destino_relativo = chink.pop();
        const waypoints = chink.map((ch) => {
            return { location: new google.maps.LatLng(ch.PERU_LATITUD, ch.PERU_LONGITUD) }
        });
        const req = {
            origin: new google.maps.LatLng(origen_relativo.LAT_PE, origen_relativo.LON_PE)
            , destination: new google.maps.LatLng(destino_relativo.PERU_LATITUD, destino_relativo.PERU_LONGITUD)
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
                if (refrescar) {
                    for (const tramo of response.routes[0].legs) {
                        ruta.PEDIDOS[cont_global].tiempo = tramo.duration.value/60;
                        fechaInicial = fechaInicial.add(tramo.duration.value, 's');
                        ruta.PEDIDOS[cont_global].RUTA_PEDIDO.FH_LLEGADA = fechaInicial.format();
                        HoraInicialRelativa = fechaInicial.add(parseInt(ruta.PEDIDOS[cont_global].PERU_TIEMPO), 'm');
                        ruta.PEDIDOS[cont_global].RUTA_PEDIDO.FH_SALIDA = fechaInicial.format();
                        cont_global++;
                    }
                }
            }).
            catch((error) => {
                msj("No se pudo crear una ruta: " + error, "warn", true);
            });
        ruta.direcciones.push(directionsRenderer);
        origen_relativo = destino_relativo;
    }
    if (ruta.RETORNO.toUpperCase() === 'S') {
        directionsService = new google.maps.DirectionsService();
        const req = {
            origin: new google.maps.LatLng(origen_relativo.LAT_PE, origen_relativo.LON_PE)
            , destination: new google.maps.LatLng(origen.LAT_PE, origen.LON_PE)
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
        directionsService.route(req,
            function (response, status) {
                if (status === "OK") {
                    directionsRenderer.setDirections(response);
                }
                else {
                    msj("No se pudo crear una ruta: " + status, "warn", true);
                }
            }
        );
        ruta.direcciones.push(directionsRenderer);
    }
    return new Promise(resolve => resolve(ruta));
}
function crearOrigenes() {
    jsonOrigenes.forEach((o) => {
        o.marcador = new google.maps.Marker({
            map: map
            , position: new google.maps.LatLng(o.LAT_PE, o.LON_PE)
            , title: o.NOMBRE_PE
            , icon: '../img/marker_blue.png'
        });
    });
}
function activarRuta(id, activar) {
    var ruta = buscarRutaXId(id);
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
            pedido.marcador.setMap(null);
        }
        else {
            pedido.marcador.setMap(map);
        }
    });
}
function enfocarRuta(id) {
    var bounds = new google.maps.LatLngBounds();
    jsonRutas.forEach((ruta) => {
        if (ruta.ID !== id) {
            ruta.direcciones.forEach((direccion) => {
                direccion.setMap(null);
            });
            ruta.PEDIDOS.forEach((pedido) => {
                pedido.marcador.setMap(null);
            });
        }
        else {
            bounds.extend({ lat: ruta.ORIGEN.LAT_PE, lng: ruta.ORIGEN.LON_PE });
            ruta.direcciones.forEach((direccion) => {
                direccion.setMap(map);
            });
            ruta.PEDIDOS.forEach((pedido) => {
                pedido.marcador.setMap(map);
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

function jsonToPills() {
    var cabecera = `<ul class="nav nav-pills">`;
    jsonRutas.forEach(function (ruta, rutaI) {
        cabecera += `<li id="li_tab${ruta.ID}" onclick="selecciona(this);" ${(rutaI === 0) ? 'class="active"' : ''} data-id="${ruta.ID}" data-color="${ruta.RUTA_COLOR}">
                                <a href="#dv_tabRuta${ruta.ID}" data-toggle="tab">
                                    <button id="btn_ruta${ruta.ID}" onclick="btn_ruta_Click(this);" type="button" data-id="${ruta.ID}" class="btn btn-xs btn-default btn-ruta activo">
                                    <span style="font-weight:bolder;" class="glyphicon glyphicon-off" />
                                    </button>
                                    ${ruta.NUMERO} <span id="sp_tabCantidad${ruta.ID}" class="badge">${ruta.PEDIDOS.length}</span>
                                </a>
                                <center>
                                    <span class="label label-info" id="sp_tabTiempos${ruta.ID}">${secondsToString(calcularTiempos(ruta))}</span>
                                </center>
                            </li>`;
    });
    cabecera += `</ul>`
    return cabecera;
}
function jsonToTable() {
    var output = '<div class="tab-content">';
    var primero = true;
    for (ruta of jsonRutas) {
        output += `<div id="dv_tabRuta${ruta.ID}" class="tab-pane fade ${(primero) ? 'active in' : ''}">
                            <table class="table table-condensed table-hover tablita">
                            <thead>
                            <tr>
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
                            ${ruta.ORIGEN.NOMBRE_PE}
                            </td>
                            <td>
                            -
                            </td>
                            <td>
                            ${moment(ruta.PEDIDOS[0].HORA_SALIDA.HORA_COD, 'HH:mm').format('HH:mm:ss')}
                            </td>
                            </tr>
                            </thead>
                            <tbody id="tb_pedidosRuta${ruta.ID}">`;
        output += rutaToTable(ruta);
        output += `</tbody>
                            </table>
                            </div>`;
        primero = false;
    }
    output += `</div>
            <div class="btn-group" role="group">
                <button id="btn_rutaColor" onclick="btn_rutaColor_Click();" type="button" class="btn btn-default">
                    <span class="glyphicon glyphicon-tint" />
                </button>
                <button id="btn_rutaEnfocar" onclick="btn_rutaEnfocar_Click();" type="button" class="btn btn-default">
                    <span class="glyphicon glyphicon-eye-open" />
                </button>
                <button id="btn_rutaVehiculo" onclick="btn_rutaVehiculo_Click();" type="button" class="btn btn-primary">
                    <span class="glyphicon glyphicon-list" />
                </button>
                <button id="btn_rutaEliminar" onclick="btn_rutaEliminar_Click();" type="button" class="btn btn-danger">
                    <span class="glyphicon glyphicon-remove" />
                </button>
            </div>
            <input id="chk_rutaCircular" onclick="chk_rutaCircular_Check();" type="checkbox" />
            <label for="chk_rutaCircular">Retorno</label>`
    return output;
}
function rutaToTable(ruta) {
    var output = "";
    ruta.PEDIDOS.forEach(function (pedido, i, arr) {
        output += `<tr>
                                <td data-id="${pedido.PERU_ID}">`;
        if (arr.length > 1) {
            if (i !== 0) {
                output += `<a href="#" class="btn-subir">
                            <span onclick="subirSecuencia(${ruta.ID},${i})" style="font-weight:bolder;" class="glyphicon glyphicon-chevron-up" />
                            </a>`;
            }
            if (i !== arr.length - 1) {
                output += `<a href="#" class="btn-bajar">
                            <span onclick="bajarSecuencia(${ruta.ID},${i})" style="font-weight:bolder;" class="glyphicon glyphicon-chevron-down" />
                            </a>`;
            }
        }
        output += `</td>
                                <td>`;
        if (arr.length > 1) {
            output += `<a href="#" class="btn-subir">
                        <span onclick="eliminarPunto(${ruta.ID},${i})" style="font-weight:bolder;" class="glyphicon glyphicon-remove" />
                        </a>`;
        }
        output += `</td>
                                <td>
                                ${pedido.PERU_NUMERO}
                                </td>
                                <td>
                                ${moment(pedido.RUTA_PEDIDO.FH_LLEGADA).format('HH:mm:ss')}
                                </td>
                                <td>
                                ${moment(pedido.RUTA_PEDIDO.FH_SALIDA).format('HH:mm:ss')}
                                </td>
                                </tr>`;
    });
    return output;
}
function calcularTiempos(ruta) {
    const fechaInicial = moment(ruta.FECHA_DESPACHOEXP);
    const horaInicial = moment(ruta.PEDIDOS[0].HORA_SALIDA.HORA_COD, 'HH:mm');
    fechaInicial.set({ 'hours': horaInicial.hours(), 'minutes': horaInicial.minutes() });

    const fechaFinal = moment(ruta.PEDIDOS[ruta.PEDIDOS.length - 1].RUTA_PEDIDO.FH_SALIDA);
    fechaFinal.year(fechaInicial.year()).dayOfYear(fechaInicial.dayOfYear());
    if (fechaFinal.hours() < fechaInicial.hours()) {
        fechaFinal.add(1, 'd');
    }
    return moment(fechaFinal).diff(fechaInicial, 'seconds');
}