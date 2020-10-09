var markers = [];

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