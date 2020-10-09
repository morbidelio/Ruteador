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
    var rex1 = /[a-zA-Z]{4}[\d]{2}/g
    var rex2 = /[a-zA-Z]{2}[\d]{4}/g
    var ex = false;
    if (rex1.test(p)) {
        ex = true;
    }
    if (rex2.test(p)) {
        ex = true;
    }
    return ex;
}