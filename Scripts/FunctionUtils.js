function validaRut(rut) {
    var tmpstr = "";
    var intlargo = rut;
       if (intlargo.length > 0) {
        crut = rut;
       
        largo = crut.length;
        if (largo < 2) {
            return false;
        }
        var contPuntos = 0;
        var contGuiones = 0;
        for (i = 0; i < crut.length; i++) {
            if (crut.charAt(i) != ' ' && crut.charAt(i) != '.' && crut.charAt(i) != '-') {
                tmpstr = tmpstr + crut.charAt(i);
            }
            if (crut.charAt(i) == '.')
                contPuntos++;
            if (crut.charAt(i) == '-')
                contGuiones++;
        }
        if (contGuiones > 1)
            return false;
        if (contPuntos > 3)
            return false;
        rut = tmpstr;
        crut = tmpstr;
        largo = crut.length;

        if (largo > 2)
            rut = crut.substring(0, largo - 1);
        else
            rut = crut.charAt(0);

        dv = crut.charAt(largo - 1);

        if (rut == null || dv == null)
            return 0;

        var dvr = '0';
        suma = 0;
        mul = 2;

        for (i = rut.length - 1; i >= 0; i--) {
            suma = suma + rut.charAt(i) * mul;
            if (mul == 7)
                mul = 2;
            else
                mul++;
        }

        res = suma % 11;
        if (res == 1)
            dvr = 'k';
        else if (res == 0)
            dvr = '0';
        else {
            dvi = 11 - res;
            dvr = dvi + "";
        }

        if (dvr != dv.toLowerCase()) {
            return false;
        }
        return true;
    }
    return false;
}
function formatearRut(rut) {
    var sRut1 = "";
    for (var i = 0; i < rut.value.length; i++) {
        if (rut.value.charAt(i) != '.' && rut.value.charAt(i) != ' ' && rut.value.charAt(i) != '-')
            sRut1 = sRut1 + rut.value.charAt(i);
    }
    var nPos = 0;    //Guarda el rut invertido con los puntos y el guión agregado   
    var sInvertido = "";    //Guarda el resultado final del rut como debe ser   
    var sRut = "";
    for (var i = sRut1.length - 1; i >= 0; i--) {
        sInvertido += sRut1.charAt(i);
        if (i == sRut1.length - 1)
            sInvertido += "-";
        else
            if (nPos == 3) {
                sInvertido += ".";
                nPos = 0;
            }
        nPos++;
    }
    for (var j = sInvertido.length - 1; j >= 0; j--) {
        if (sInvertido.charAt(sInvertido.length - 1) != ".")
            sRut += sInvertido.charAt(j);
        else
            if (j != sInvertido.length - 1)
                sRut += sInvertido.charAt(j);
    }   //Pasamos al campo el valor formateado   
    rut.value = sRut.toUpperCase();
}
function validaPatente(p) {
    var rex = /[a-zA-Z]{4}[\d]{2}|[a-zA-Z]{2}[\d]{4}/g
    return rex.test(p);
}
function secondsToString(seconds) {
    var hour = Math.floor(seconds / 3600);
    hour = (hour < 10) ? '0' + hour : hour;
    var minute = Math.floor((seconds / 60) % 60);
    minute = (minute < 10) ? '0' + minute : minute;
    var second = seconds % 60;
    second = (second < 10) ? '0' + second : second;
    return hour + ':' + minute + ':' + second;
}
function invertColor(hex) {
    if (hex.indexOf('#') === 0) {
        hex = hex.slice(1);
    }
    // convert 3-digit hex to 6-digits.
    if (hex.length === 3) {
        hex = hex[0] + hex[0] + hex[1] + hex[1] + hex[2] + hex[2];
    }
    if (hex.length !== 6) {
        throw new Error('Invalid HEX color.');
    }
    // invert color components
    var r = (255 - parseInt(hex.slice(0, 2), 16)).toString(16),
        g = (255 - parseInt(hex.slice(2, 4), 16)).toString(16),
        b = (255 - parseInt(hex.slice(4, 6), 16)).toString(16);
    // pad each with zeros and return
    return '#' + padZero(r) + padZero(g) + padZero(b);
}

function padZero(str, len) {
    len = len || 2;
    var zeros = new Array(len).join('0');
    return (zeros + str).slice(-len);
}
function showSpinner(obj) {
    $(`#${obj} *`).css('opacity', '0.5');
    $(`#${obj} button,input`).prop('disabled', true);
    $(`#${obj} ul.nav>li`).addClass('disabled');
    $(`#${obj} ul.nav>li>a[data-toggle="tab"]`).removeAttr('data-toggle');
    $(`#${obj}`).append('<div class="spinner"></div>');
}
function closeSpinner(obj) {
    $(`#${obj} *`).css('opacity', '1');
    $(`#${obj} button,input:not(.disabled)`).prop('disabled', false);
    $(`#${obj} ul.nav>li`).removeClass('disabled');
    $(`#${obj} ul.nav>li>a`).attr('data-toggle', 'tab');
    $(`#${obj} .spinner`).remove();
}
function maxScroll(obj) {
    return parseInt($(obj).prop('scrollWidth') - $(obj).width());
} 
function limpiar(obj) {
    $(`#${obj}`).find('input[type="text"]').val('');
    $(`#${obj}`).find('input[type="radio"]').prop('checked', false);
    $(`#${obj}`).find('input[type="check"]').prop('checked', false);
    $(`#${obj}`).find('table>thead,table>tbody').html('');
    $(`#${obj}`).find('select').val('0');
}