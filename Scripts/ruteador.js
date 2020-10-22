var map;
var bermudaTriangle;
var markers = [];
var waypoints = [];
var coord = [];
var origen;
var destino;
var directionsRenderer;
var directionsService;
const infowindow = new google.maps.InfoWindow();
markers["ID"] = [];
markers["MARKERS"] = [];
waypoints["ID"] = [];
waypoints["WAYPOINTS"] = [];
waypoints["POLYGON"] = [];
function hayMarcadores() {
    return (markers["MARKERS"].length > 0);
}
function setOrigen(lat, lon, icono, titulo) {
    origen = new google.maps.Marker({
        map: map
        , position: new google.maps.LatLng(lat, lon)
        , title: titulo
        , icon: '../img/' + icono
    });
}
function setDestino(lat, lon, icono, titulo) {
    destino = new google.maps.Marker({
        map: map
        , position: new google.maps.LatLng(lat, lon)
        , title: titulo
        , icon: '../img/' + icono
    });
}
function getOrigen() {
    return origen;
}
function getDestino() {
    return destino;
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
    markers["ID"] = [];
    markers["MARKERS"] = [];
}
function limpiarWaypoints() {
    waypoints["ID"] = [];
    waypoints["WAYPOINTS"] = [];
    waypoints["POLYGON"] = [];
}
function buscarPuntoRutaXId(id) {
    return waypoints["ID"].indexOf(id);
}
function buscarMarcadorXId(id) {
    return markers["ID"].indexOf(id);
}
function cantidad_puntos(cantidad) {
    $('#txt_cant_punt').text('Destinos: ' + cantidad);
}
function insertarMarcador(id, lat, lon, icono, titulo, contenido) {
    var index = buscarMarcadorXId(id);
    if (index == -1) {
        markers["ID"].push(id);
        var marker = new google.maps.Marker({
            map: map
            , position: new google.maps.LatLng(lat, lon)
            , title: titulo
            , icon: '../img/' + icono

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
        markers["MARKERS"].push(marker);
    }
    else {
        markers["MARKERS"][index] = new google.maps.Marker({
            map: map
            , position: new google.maps.LatLng(lat, lon)
            , title: titulo
            , icon: '../img/' + icono
        });
    }
}
function insertarOrigen(id, lat, lon, icono, titulo) {
    var origen = new google.maps.Marker({
        map: map
        , position: new google.maps.LatLng(lat, lon)
        , title: titulo
        , icon: '../img/' + icono
    });
}
function insertarPuntoRuta(id, pos) {
    var indexMarcador = buscarMarcadorXId(id);
    var loc = markers["MARKERS"][indexMarcador].getPosition();
    if (!isNaN(pos)) {
        waypoints["ID"].splice(pos, 0, id);
        waypoints["WAYPOINTS"].splice(pos, 0, {
            location: loc
        });
        waypoints["POLYGON"].splice(pos, 0, loc);
    }
    else {
        waypoints["ID"].push(id);
        waypoints["WAYPOINTS"].push({
            location: loc
        });
        waypoints["POLYGON"].push(loc);
    }
}
function quitarMarcador(id) {
    var index = buscarMarcadorXId(id);
    markers["ID"].splice(index, 1);
    markers["MARCADORES"].splice(index, 1);
    index = buscarPuntoRutaXId(id);
    while (id != -1) {
        waypoints["ID"].splice(index, 1);
        waypoints["WAYPOINTS"].splice(index, 1);
        waypoints["POLYGON"].splice(index, 1);
        index = buscarPuntoRutaXId(id);
    }
}
function quitarPuntoRuta(id) {
    var index = buscarPuntoRutaXId(id);
    if (index != -1) {
        waypoints["ID"].splice(index, 1);
        waypoints["WAYPOINTS"].splice(index, 1);
        waypoints["POLYGON"].splice(index, 1);
    }
}
function crearPoligono() {
    var coord = [];
    coord.push(origen.getPosition());
    coord = coord.concat(waypoints["POLYGON"]);
    coord.push(destino.getPosition());
    bermudaTriangle = new google.maps.Polygon({
        paths: coord,
        strokeColor: "#FF0000",
        strokeOpacity: 0.8,
        strokeWeight: 2,
        fillColor: "#FF0000",
        fillOpacity: 0.0
    });
    bermudaTriangle.setMap(map);
}
function crearRuta() {
    directionsService = new google.maps.DirectionsService();
    directionsRenderer = new google.maps.DirectionsRenderer();
    directionsRenderer.setOptions({ preserveViewport: false });
    directionsRenderer.setMap(map);
    var wayway = Array.from(waypoints["WAYPOINTS"]);
    var desway = wayway.pop();

    var Horasalida = new Date(0);
    var json_origen = JSON.parse($('#hf_origen').val());
    time = $("#ddl_buscarHorario option:selected").text().split(/\:|\-/g);
    Horasalida.setHours(time[0]);
    Horasalida.setMinutes(time[1]);
    $('#t_origen').html(Horasalida.toLocaleTimeString());
    var tiempo0 = moment.duration(("0" + Horasalida.getHours()).slice(-2) + ':' + ("0" + Horasalida.getMinutes()).slice(-2));
    var req = {
        origin: origen.getPosition(),
        destination: desway,  //destino.getPosition(),
        travelMode: "DRIVING",
        waypoints: wayway, //waypoints["WAYPOINTS"],
        optimizeWaypoints: false
    };
    directionsService.route(req,
        function (response, status) {
            if (status === "OK") {
                //  $('#respuesta_direcction').val(JSON.stringify(response.routes[0].legs));
                directionsRenderer.setDirections(response);
                //   $('#respuesta_direcction').val(JSON.stringify(response.routes[0].legs));
                var tiempos = [];
                var duracion_total = 0;
                var hora_salida;
                var contador = 0;
                while (contador < (response.routes[0].legs.length)) {
                    tiempos.push(response.routes[0].legs[contador].duration);
                    Horasalida.setMinutes(Horasalida.getMinutes() + tiempos[contador]["value"] / 60)
                    puntosRuta[contador].PERU_LLEGADA = ("0" + Horasalida.getHours()).slice(-2) + ':' + ("0" + Horasalida.getMinutes()).slice(-2);
                    $('#t_' + puntosRuta[contador].PERU_CODIGO).html(puntosRuta[contador].PERU_LLEGADA);
                    hora_salida = puntosRuta[contador].PERU_LLEGADA;
                    Horasalida.setMinutes(Horasalida.getMinutes() + parseInt(puntosRuta[contador].PERU_TIEMPO));
                    duracion_total = duracion_total + parseInt(response.routes[0].legs[contador].duration.value) + parseInt(puntosRuta[contador].PERU_TIEMPO * 60);
                    contador = contador + 1;
                }
                $('#respuesta_direcction').val(JSON.stringify(tiempos));
                tiempo = moment.duration(duracion_total * 1000);
                $('#lbl_puntoSalida').text('Duración :' + tiempo.format('HH:mm'));
            }
            else {
                msj("No se pudo crear una ruta: " + status, "warn", true);
            }
        }
    );
}
function cambia_direccion() {
    directionsRenderer.setOptions({ preserveViewport: true });
    if (directionsRenderer.getMap()) {
        directionsRenderer.setMap(null);
    } else {
        directionsRenderer.setMap(map);
    }
}
function refrescarMarcadores() {
    markers["MARKERS"].map((o) => {
        o.setMap(map);
    });
}
function refrescarPoligono() {
    var coord = [];
    coord.push(origen.getPosition());
    coord = coord.concat(waypoints["POLYGON"]);
    coord.push(destino.getPosition());
    bermudaTriangle.setPaths(coord);
}
function refrescarRuta() {
    if (!directionsRenderer) directionsRenderer = new google.maps.DirectionsRenderer();
    if (!directionsService) directionsService = new google.maps.DirectionsService();
    directionsRenderer.setOptions({ preserveViewport: false });
    directionsRenderer.setDirections({ routes: [] });
    var wayway = Array.from(waypoints["WAYPOINTS"]);
    var desway = wayway.pop();
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
    var tiempo0 = moment.duration(("0" + Horasalida.getHours()).slice(-2) + ':' + ("0" + Horasalida.getMinutes()).slice(-2));
    var req = {
        origin: origen.getPosition(),
        destination: desway, //destino.getPosition(),
        travelMode: "DRIVING",
        waypoints: wayway, //waypoints["WAYPOINTS"],
        optimizeWaypoints: false
    };
    directionsService.route(req,
        function (response, status) {
            if (status === "OK") {
                directionsRenderer.setDirections(response);
                var tiempos = [];
                var hora_salida;
                var duracion_total = 0;
                var contador = 0;
                while (contador < (response.routes[0].legs.length)) {
                    tiempos.push(response.routes[0].legs[contador].duration);

                    Horasalida.setMinutes(Horasalida.getMinutes() + tiempos[contador]["value"] / 60)
                    puntosRuta[contador].PERU_LLEGADA = ("0" + Horasalida.getHours()).slice(-2) + ':' + ("0" + Horasalida.getMinutes()).slice(-2);
                    $('#t_' + puntosRuta[contador].PERU_CODIGO).html(puntosRuta[contador].PERU_LLEGADA);
                    Horasalida.setMinutes(Horasalida.getMinutes() + parseInt(puntosRuta[contador].PERU_TIEMPO));
                    hora_salida = puntosRuta[contador].PERU_LLEGADA;
                    duracion_total = duracion_total + parseInt(response.routes[0].legs[contador].duration.value) + parseInt(puntosRuta[contador].PERU_TIEMPO * 60);
                    contador = contador + 1;
                }
                $('#respuesta_direcction').val(JSON.stringify(tiempos));
                tiempo = moment.duration(duracion_total * 1000);
                $('#lbl_puntoSalida').text('Duración :' + tiempo.format('HH:mm'));
            }
            else {
                msj("No se pudo crear una ruta: " + status, "warn", true);
            }
        }
    );
}