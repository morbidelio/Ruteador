var map;
var bermudaTriangle;
var markers = [];
var waypoints = [];
var coord = [];
var origen;
var destino;
var direcciones = [];
var directionsService;
var color = 'purple';
const infowindow = new google.maps.InfoWindow();
markers.ID = [];
markers.MARKERS = [];
waypoints.ID = [];
waypoints.WAYPOINTS = [];
waypoints.POLYGON = [];
function hayMarcadores() {
    return (markers.MARKERS.length > 0);
}
function setOrigen(lat, lon, icono, titulo, label) {
    origen = new google.maps.Marker({
        map: map
        , position: new google.maps.LatLng(lat, lon)
        , title: titulo
        , icon: (icono) ? '../img/' + icono : null
        , label: (label) ? {
            text: label,
            color: 'white',
            fontFamily: 'Britannic',
            fontWeight: 'bolder',
            fontSize: '14px',
        } : null
    });
}
function setDestino(lat, lon, icono, titulo, label) {
    destino = new google.maps.Marker({
        map: map
        , position: new google.maps.LatLng(lat, lon)
        , title: titulo
        , icon: (icono) ? '../img/' + icono : null
        , label: (label) ? {
            text: label,
            color: 'white',
            fontFamily: 'Britannic',
            fontWeight: 'bolder',
            fontSize: '14px',
        } : null
    });
}
function setColor(color) {
    this.color = color;
}
function getOrigen() {
    return origen;
}
function getDestino() {
    return destino;
}
function getColor() {
    return this.color;
}
function cargamapa(zoom1, mapObj, lat, lon) {
    var center = (!lat || !lon) ? new google.maps.LatLng(-33.45238466, -70.65735526) : new google.maps.LatLng(lat, lon);
    map = new google.maps.Map(mapObj, {
        center: center,
        zoom: zoom1,
        gestureHandling: 'greedy'
    });
}
function limpiarMarcadores() {
    markers.ID = [];
    markers.MARKERS = [];
}
function limpiarWaypoints() {
    waypoints.ID = [];
    waypoints.WAYPOINTS = [];
    waypoints.POLYGON = [];
}
function buscarPuntoRutaXId(id) {
    return waypoints.ID.indexOf(id);
}
function buscarMarcadorXId(id) {
    return markers.ID.indexOf(id);
}
function cantidad_puntos(cantidad) {
    $('#txt_cant_punt').text('Destinos: ' + cantidad);
}
function insertarMarcador(id, lat, lon, icono, titulo, contenido) {
    const index = buscarMarcadorXId(id);
    if (index == -1) {
        markers.ID.push(id);
        const marker = new google.maps.Marker({
            map: map
            , position: new google.maps.LatLng(lat, lon)
            , title: titulo
            , icon: (icono) ? '../img/' + icono : null
        });
        marker.addListener('click', function () {
            if (contenido) {
                infowindow.setContent(contenido);
                infowindow.open(map, marker);
            }
            $('#hf_idPunto').val(id);
            $find('ddl_puntoNombre').findItemByValue(id).select();
            $('.sel-between').addClass('enabled');
        });
        markers.MARKERS.push(marker);
    }
    else {
        markers.MARKERS[index] = new google.maps.Marker({
            map: map
            , position: new google.maps.LatLng(lat, lon)
            , title: titulo
            , icon: '../img/' + icono
        });
    }
}
function insertarOrigen(id, lat, lon, icono, titulo, label) {
    origen = new google.maps.Marker({
        map: map
        , position: new google.maps.LatLng(lat, lon)
        , title: titulo
        , icon: (icono) ? '../img/' + icono : null
        , label: (label) ? {
            text: label,
            color: 'white',
            fontFamily: 'Britannic',
            fontWeight: 'bolder',
            fontSize: '14px',
        } : null
    });
}
function insertarPuntoRuta(id, pos, icono) {
    const indexMarcador = buscarMarcadorXId(id);
    const marcador = markers.MARKERS[indexMarcador];
    const loc = marcador.getPosition();
    if (!isNaN(pos)) {
        waypoints.ID.splice(pos, 0, id);
        waypoints.WAYPOINTS.splice(pos, 0, {
            location: loc
        });
        waypoints.POLYGON.splice(pos, 0, loc);
        for (var i = pos + 1; i < waypoints.ID.length; i++) {
            const indexMarcador2 = buscarMarcadorXId(waypoints.ID[i]);
            const marcador2 = markers.MARKERS[indexMarcador2];
            marcador2.setLabel({
                text: (i + 1).toString(),
                fontFamily: 'Britannic',
                fontWeight: 'bolder',
                fontSize: '14px',
                color: 'white'
            });
        }
    }
    else {
        waypoints.ID.push(id);
        waypoints.WAYPOINTS.push({
            location: loc
        });
        waypoints.POLYGON.push(loc);
        pos = waypoints.ID.length - 1;
    }
    marcador.setLabel({
        text: (pos + 1).toString(),
        fontFamily: 'Britannic',
        fontWeight: 'bolder',
        fontSize: '14px',
        color: 'white'
    });
    marcador.setIcon((icono) ? '../img/' + icono : null);
    //if (!isNaN(pos)) {
    //pos = pos + 1;
    //while (pos < waypoints.ID.length) {
    //    const indexMarcador2 = buscarMarcadorXId(waypoints.ID[pos]);
    //    const marcador2 = markers.MARKERS[indexMarcador2];
    //    const label2 = {
    //        text: (pos + 1).toString(),
    //        fontFamily: 'Britannic',
    //        fontWeight: 'bolder',
    //        fontSize: '14px',
    //        color: 'white'
    //    };
    //    marcador2.setLabel(label2);

    //    pos = pos + 1;
    //}
    //}
}
function quitarMarcador(id) {
    const index = buscarMarcadorXId(id);
    markers.ID.splice(index, 1);
    markers.MARCADORES.splice(index, 1);
    index = buscarPuntoRutaXId(id);
    while (id != -1) {
        waypoints.ID.splice(index, 1);
        waypoints.WAYPOINTS.splice(index, 1);
        waypoints.POLYGON.splice(index, 1);
        index = buscarPuntoRutaXId(id);
    }
}
function quitarPuntoRuta(id) {
    const index = buscarPuntoRutaXId(id);
    if (index != -1) {
        const indexMarcador = buscarMarcadorXId(id);
        const marcador = markers.MARKERS[indexMarcador];
        waypoints.ID.splice(index, 1);
        waypoints.WAYPOINTS.splice(index, 1);
        waypoints.POLYGON.splice(index, 1);
        marcador.setLabel(null);
        marcador.setIcon('../img/icon_pedido.png');
        for (var i = index; i < waypoints.ID.length; i++) {
            const indexMarcador2 = buscarMarcadorXId(waypoints.ID[i]);
            const marcador2 = markers.MARKERS[indexMarcador2];
            marcador2.setLabel({
                text: (i + 1).toString(),
                fontFamily: 'Britannic',
                fontWeight: 'bolder',
                fontSize: '14px',
                color: 'white'
            });
        }
    }
}
function crearPoligono() {
    var coord = [];
    coord.push(origen.getPosition());
    coord = coord.concat(waypoints.POLYGON);
    coord.push(destino.getPosition());
    bermudaTriangle = new google.maps.Polygon({
        paths: coord,
        strokeColor: (color) ? color : 'purple',
        strokeOpacity: 0.8,
        strokeWeight: 2,
        fillOpacity: 0.0,
        fillColor: (color) ? color : 'purple'
    });
    bermudaTriangle.setMap(map);
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
//var bounds = new google.maps.LatLngBounds();

function crearRuta(circular) {
    directionsService = new google.maps.DirectionsService();
    direcciones.map((o) => {
        o.setMap(null);
    });
    direcciones = [];
    var Horasalida = new Date(0);
    const time = $("#ddl_buscarHorario option:selected").text().split(/\:|\-/g);
    Horasalida.setHours(time[0]);
    Horasalida.setMinutes(time[1]);
    $("#t_origen").html(Horasalida.toLocaleTimeString());
    contador_global = 0;
    tiempos_globales = [];
    var chinkos = Array.from(waypoints["WAYPOINTS"]).chunk(20); // chinkos[x] = waypoints["WAYPOINTS"][0 - 20]
    var origen_relativo = origen;
    duracion_total = 0;
    var desway; // = wayway.pop();
    for (var cont_ = 0; cont_ < chinkos.length; cont_++) {
        desway = chinkos[cont_].pop();

        var req = {
            origin: origen_relativo.getPosition(),
            destination: desway, //destino.getPosition(),
            travelMode: "DRIVING",
            waypoints: chinkos[cont_], //waypoints["WAYPOINTS"],
            optimizeWaypoints: false,
        };
        console.log(req);
        const directionsRenderer = new google.maps.DirectionsRenderer({
            preserveViewport: true,
            suppressMarkers: true,
            map: map,
            polylineOptions: {
                strokeColor: (color) ? color : 'purple'
            }
        });
        console.log(directionsRenderer);
        directionsService.route(req, function (response, status) {
            if (status === "OK") {
                directionsRenderer.setDirections(response);
                var tiempos = [];

                response.routes[0].legs.map((o, i) => {
                    tiempos_globales.push(o.duration);
                    tiempos.push(o.duration);

                    Horasalida.setMinutes(
                        Horasalida.getMinutes() + tiempos[i]["value"] / 60
                    );
                    puntosRuta[contador_global].PERU_LLEGADA = `${('0' + Horasalida.getHours()).slice(-2)}:${("0" + Horasalida.getMinutes()).slice(-2)}`;
                    $("#t_" + puntosRuta[contador_global].PERU_CODIGO).html(
                        puntosRuta[contador_global].PERU_LLEGADA
                    );
                    Horasalida.setMinutes(
                        Horasalida.getMinutes() +
                        parseInt(puntosRuta[contador_global].PERU_TIEMPO)
                    );
                    hora_salida = puntosRuta[contador_global].PERU_LLEGADA;
                    duracion_total += parseInt(o.duration.value) +
                        parseInt(puntosRuta[contador_global].PERU_TIEMPO * 60);
                    contador_global = contador_global + 1;
                });
                $("#respuesta_direcction").val(JSON.stringify(tiempos_globales));
                tiempo = moment.duration(duracion_total * 1000);
                $("#lbl_puntoSalida").text("Duración :" + tiempo.format("HH:mm"));
            } else {
                msj("No se pudo crear una ruta: " + status, "warn", true);
            }
        });
        direcciones.push(directionsRenderer);
        origen_relativo = new google.maps.Marker({
            map: map,
            position: desway.location,
            title: "aaaa",
            icon: "../img/" + "icon_pedido.png",
        });
    }
    if (circular) {
        var req = {
            origin: origen_relativo.getPosition(),
            destination: origen.getPosition(), //destino.getPosition(),
            travelMode: "DRIVING",
            optimizeWaypoints: false
        };
        const directionsRenderer = new google.maps.DirectionsRenderer({
            preserveViewport: true,
            suppressMarkers: true,
            map: map,
            polylineOptions: {
                strokeColor: (color) ? color : 'purple'
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

        direcciones.push(directionsRenderer);
    }
}
function mostrar(ruta, poligono) {
    direcciones.map((o) => {
        if (ruta) o.setMap(map);
        else o.setMap(null)
    });
    if (poligono) bermudaTriangle.setMap(map);
    else bermudaTriangle.setMap(null);
}
function refrescarMarcadores() {
    markers.MARKERS.map((o) => {
        o.setMap(map);
    });
}
function refrescarPoligono(show) {
    var coord = [];
    coord.push(origen.getPosition());
    coord = coord.concat(waypoints.POLYGON);
    coord.push(destino.getPosition());
    bermudaTriangle.setPaths(coord);
    bermudaTriangle.setMap((show) ? map : null);
}
function refrescarRuta(show, circular) {
    directionsService = new google.maps.DirectionsService();
    direcciones.map((o) => {
        o.setMap(null);
    });
    direcciones = [];
    contador_global = 0;
    tiempos_globales = [];
    var Horasalida = new Date(0);

    var json_origen = JSON.parse($('#hf_origen').val());
    try {
        time = json_origen["PERU_LLEGADA"].split(/\:|\-/g);
        Horasalida.setHours(time[0]);
        Horasalida.setMinutes(time[1]);
    }
    catch (error) {
        time = $("#ddl_buscarHorario option:selected").text().split(/\:|\-/g);
        Horasalida.setHours(time[0]);
        Horasalida.setMinutes(time[1]);
        $('#t_origen').html(Horasalida.toLocaleTimeString());
    }
    var cont_ = 0;
    var chinkos = Array.from(waypoints.WAYPOINTS).chunk(20);
    var origen_relativo = origen;
    duracion_total = 0;
    var desway; // = wayway.pop();


    while (cont_ < chinkos.length) {
        desway = chinkos[cont_].pop();
        var req = {
            origin: origen_relativo.getPosition(),
            destination: desway, //destino.getPosition(),
            travelMode: "DRIVING",
            waypoints: chinkos[cont_], //waypoints.WAYPOINTS,
            optimizeWaypoints: false
        };
        const directionsRenderer = new google.maps.DirectionsRenderer({
            preserveViewport: true,
            suppressMarkers: true,
            map: (show) ? map : null,
            polylineOptions: {
                strokeColor: (color) ? color : 'purple'
            }
        });
        directionsService.route(req,
            function (response, status) {
                if (status === "OK") {
                    directionsRenderer.setDirections(response);
                    var tiempos = [];

                    response.routes[0].legs.map((o, i) => {
                        tiempos_globales.push(o.duration);
                        tiempos.push(o.duration);

                        Horasalida.setMinutes(Horasalida.getMinutes() + tiempos[i]["value"] / 60)
                        puntosRuta[contador_global].PERU_LLEGADA = ("0" + Horasalida.getHours()).slice(-2) + ':' + ("0" + Horasalida.getMinutes()).slice(-2);
                        $('#t_' + puntosRuta[contador_global].PERU_CODIGO).html(puntosRuta[contador_global].PERU_LLEGADA);
                        Horasalida.setMinutes(Horasalida.getMinutes() + parseInt(puntosRuta[contador_global].PERU_TIEMPO));
                        duracion_total += parseInt(o.duration.value) + parseInt(puntosRuta[contador_global].PERU_TIEMPO * 60);
                        contador_global += 1;
                    });
                    $('#respuesta_direcction').val(JSON.stringify(tiempos_globales));
                    tiempo = moment.duration(duracion_total * 1000);
                    $('#lbl_puntoSalida').text('Duración :' + tiempo.format('HH:mm'));
                }
                else {
                    msj("No se pudo crear una ruta: " + status, "warn", true);
                }
            }
        );

        direcciones.push(directionsRenderer);

        origen_relativo = new google.maps.Marker({
            map: map
            , position: desway.location
            , title: 'aaaa'
            , icon: '../img/' + 'icon_pedido.png'
        });
        cont_ += 1;
    }
    if (circular) {
        //debugger;
        var req = {
            origin: origen_relativo.getPosition(),
            destination: origen.getPosition(), //destino.getPosition(),
            travelMode: "DRIVING",
            optimizeWaypoints: false
        };
        const directionsRenderer = new google.maps.DirectionsRenderer({
            preserveViewport: true,
            suppressMarkers: true,
            map: (show) ? map : null,
            polylineOptions: {
                strokeColor: (color) ? color : 'purple'
            }
        });
        directionsService.route(req,
            function (response, status) {
                if (status === "OK") {
                    directionsRenderer.setDirections(response);
                    //var tiempos = [];

                    //response.routes[0].legs.map((o, i) => {
                    //    tiempos_globales.push(o.duration);
                    //    tiempos.push(o.duration);

                    //    Horasalida.setMinutes(Horasalida.getMinutes() + tiempos[i]["value"] / 60)
                    //    puntosRuta[contador_global].PERU_LLEGADA = ("0" + Horasalida.getHours()).slice(-2) + ':' + ("0" + Horasalida.getMinutes()).slice(-2);
                    //    $('#t_' + puntosRuta[contador_global].PERU_CODIGO).html(puntosRuta[contador_global].PERU_LLEGADA);
                    //    Horasalida.setMinutes(Horasalida.getMinutes() + parseInt(puntosRuta[contador_global].PERU_TIEMPO));
                    //    duracion_total += parseInt(o.duration.value) + parseInt(puntosRuta[contador_global].PERU_TIEMPO * 60);
                    //});
                    //$('#respuesta_direcction').val(JSON.stringify(tiempos_globales));
                    //tiempo = moment.duration(duracion_total * 1000);
                    //$('#lbl_puntoSalida').text('Duración :' + tiempo.format('HH:mm'));
                }
                else {
                    msj("No se pudo crear una ruta: " + status, "warn", true);
                }
            }
        );

        direcciones.push(directionsRenderer);
    }
}