var markers = [];
const infowindow = new google.maps.InfoWindow();
var json;

var map2;
function mapa2() {
    var mapa = document.getElementById('map2');
    var center = new google.maps.LatLng(-33.45238466, -70.65735526);
    if (map2 == null) {
        map2 = new google.maps.Map(mapa, {
            center: center,
            zoom: 12,
            gestureHandling: 'greedy'
        });
    }

    cargatodos();
    setTimeout(function () {
        $('[title]').tooltip({ container: 'body' });
    }, 1000);
}
function mapa() {
    var mapa = document.getElementById('map');
    var lat = ($('#txt_editLat').val() == '') ? undefined : parseFloat($('#txt_editLat').val().replace(',', '.'));
    var lon = ($('#txt_editLon').val() == '') ? undefined : parseFloat($('#txt_editLon').val().replace(',', '.'));

    cargamapa(12, mapa, lat, lon);
    insertarMarcador(lat, lon, 'icon_pedido.png', '');
}
function cargamapa(zoom1, mapObj, lat, lon) {
    var center = (lat == undefined || lon == undefined) ? new google.maps.LatLng(-33.45238466, -70.65735526) : new google.maps.LatLng(lat, lon);
    map = new google.maps.Map(mapObj, {
        center: center,
        zoom: zoom1
    });
}
function insertarMarcador(lat, lon, icono, titulo) {
    var marker = new google.maps.Marker({
        map: map
        , position: new google.maps.LatLng(lat, lon)
        , title: titulo
        , draggable: true
        , icon: '../img/' + icono
    });
    marker.addListener('dragend', function (event) {
        $('#txt_editLat').val(event.latLng.lat().toFixed(8).toString().replace('.', ','));
        $('#txt_editLon').val(event.latLng.lng().toFixed(8).toString().replace('.', ','));
    });
}
function insertarMarcadorGlobal(pedido) {
    var marker = new google.maps.Marker({
        map: map2
        , position: { lat: pedido.PERU_LATITUD, lng: pedido.PERU_LONGITUD }
        , title: pedido.PERU_NUMERO
        , icon: '../img/icon_pedido.png'
    });
    const contenido = `<div id="content">
                <p>
                Número: ${pedido.PERU_NUMERO}
                <br />
                Código: ${pedido.PERU_CODIGO}
                <br />
                Nombre: ${pedido.PERU_NOMBRE}
                <br />
                Dirección: ${pedido.PERU_DIRECCION}
                </p>
                </div>`;
    marker.addListener('click', function () {
        infowindow.setContent(contenido);
        infowindow.open(map, marker);
        //seleccionarPedido(pedido, true);
        seleccionaPedido(pedido.PERU_ID);
    });
    markersArray.push(marker);
}
function zoomin2() {
    map2.setZoom(12);
}

function centrar2(id) {
    map2.setZoom(12);
    var json = JSON.parse($('#hf_jsonPedidos').val());
    for (var pedido of json) {
        if (pedido.PERU_ID === id) {
            map2.setCenter({ lat: pedido.PERU_LATITUD, lng: pedido.PERU_LONGITUD });
            break;
        }
    }
}

function clearOverlays() {
    markersArray.forEach((o) => o.setMap(null));
    markersArray = [];
}

function cargatodos() {
    clearOverlays();
    json = JSON.parse($('#hf_jsonPedidos').val());
    for (var pedido of json) {
        insertarMarcadorGlobal(pedido);
    }
}