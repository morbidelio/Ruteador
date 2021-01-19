var markers = [];

function cargamapa(zoom1, mapObj, lat, lon) {
    var center = (lat == undefined || lon == undefined) ? new google.maps.LatLng(-33.45238466, -70.65735526) : new google.maps.LatLng(lat, lon);
    map = new google.maps.Map(mapObj, {
        center: center,
        zoom: zoom1
    });
}
function insertarMarcador(lat, lon, titulo, icono ) {
    var icon;
    if (!icono)
        icon = {
            path: 'M 0,0 C -2,-20 -10,-22 -10,-30 A 10,10 0,1,1 10,-30 C 10,-22 2,-20 0,0 z',
            fillColor: '#1B18C9',
            fillOpacity: 1,
            strokeColor: '#000',
            strokeWeight: 2,
            labelOrigin: new google.maps.Point(0, -30),
            scale: 0.75
        };
    else
        icon = {
            url: icono,
            scaledSize: {
                height: 25
                , width: 25
            }
        };
    const marker = new google.maps.Marker({
        map: map
        , position: new google.maps.LatLng(lat, lon)
        , title: titulo
        , draggable: true
        , icon: icon
    });
    marker.addListener('dragend', function (event) {
        $('#txt_editLat').val(event.latLng.lat().toFixed(8).toString().replace('.', ','));
        $('#txt_editLon').val(event.latLng.lng().toFixed(8).toString().replace('.', ','));
    });
}