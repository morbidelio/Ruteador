
class Ruta {
    ID: number;
    NUMERO: string;
    FH_VIAJE: Date;
    ID_MOVIL: number;
    ID_ESTADO: number;
    ID_CLIENTE_GPS: number;
    ID_TIPOVIAJE: number;
    FH_SALIDA: Date;
    FH_RETORNO: Date;
    OBSERVACION: string;
    RETORNO: string;
    FH_ULT_REPORTE: Date;
    FH_CREACION: Date;
    CORREO_GPS: boolean;
    FH_UPDATE: Date;
    TOTAL_KG: number;
    RUTA: string;
    FECHA_PRESENTACION: Date;
    FECHA_INICIOCARGA: Date;
    FECHA_FINCARGA: Date;
    FECHA_DESPACHOEXP: Date;
    FECHA_INICIOEXP: Date;
    FECHA_FINEXP: Date;
    RUTA_COLOR: string;
    CONDUCTOR: Conductor;
    ORIGEN: Origen;
    ENVIO: Envio;
    OPERACION: Operacion;
    TRAILER: Trailer;
    TRACTO: Tracto;
    PEDIDOS: Pedido[];
    HORARIO: Horario;
    direcciones: google.maps.DirectionsRenderer[];
    procesado: boolean;
    TIEMPO_RETORNO: number;
}
class Conductor {
    COND_ID: number;
    COND_IMAGEN: string;
    COND_RUT: string;
    COND_NOMBRE: string;
    COND_ACTIVO: boolean;
    COND_BLOQUEADO: boolean;
    COND_TELEFONO: string;
    COND_MOTIVO_BLOQUEO: string;
    COND_EXTRANJERO: boolean;
}
class Origen {
    ID: number;
    ID_PE: string;
    TIPO_ID: number;
    NOMBRE_PE: string;
    DIRECCION_PE: string;
    COMUNA_PE: string;
    CIUDAD_PE: string;
    LAT_PE: number;
    LON_PE: number;
    RADIO_PE: number;
    IS_POLIGONO: boolean;
    ID_CLIENTE: number;
    REGION: string;
    FH_CREA: Date;
    FH_UPDATE: Date;
    ID_MERCADO: number;
    ID_ZONA: number;
    invalido: number;
    PERU_LLEGADA: string;
    OPERACION: Operacion;
    COMUNA: Comuna;
    marcador: google.maps.Marker;
}
class Envio {
    Env_ID: number;
    str_cabecera: string;
    str_detalle: string;
}
class Ciudad {
    CIUD_ID: number;
    CIUD_NOMBRE: string;
    REGION: Region;
}
class Region {
    REGI_ID: number;
    REGI_NOMBRE: string;
    REGI_DESCRIPCION: string;
    REGI_ORDEN: number;
}
class Comuna {
    COMU_ID: number;
    COMU_NOMBRE: string;
    CIUDAD: Ciudad
}
class Operacion {
    OPER_ID: number;
    OPER_NOMBRE: string;
}
class Trailer {
    TRAI_ID: number;
    TRAI_COD: string;
    TRAI_NUMERO: string;
    TRAI_PLACA: string;
    TRAILER_TIPO: TrailerTipo;
}
class TrailerTipo {
    TRTI_ID: number;
    TRTI_DESC: string;
    TRTI_COLOR: string;
}

class Tracto {
    TRAC_ID: number;
    TRAC_PLACA: string;
    TRAC_FH_CREACION: Date;
}
class Pedido {
    PERU_ID: number;
    PERU_NUMERO: string;
    PERU_CODIGO: string;
    PERU_FECHA: Date;
    PERU_PESO: string;
    PERU_TIEMPO: string;
    PERU_DIRECCION: string;
    PERU_LATITUD: number;
    PERU_LONGITUD: number;
    PERU_ENVIADO_RUTEADOR: boolean;
    PERU_FH_ENVIO: Date;
    PERU_FH_CREACION: Date;
    COMUNA: Comuna;
    USUARIO_PEDIDO: Usuario;
    USUARIO_ENVIO: Usuario;
    HORA_SALIDA: Horario;
    RUTA_PEDIDO: RutaPedido;
    marcador: google.maps.Marker;
    tiempo: number;
}
class Horario {
    HORA_ID: number;
    HORA_COD: string;
    HORA_DESC: string;
}
class RutaPedido {
    RPPE_ID: number;
    FH_PLANIFICA: Date;
    FH_LLEGADA: Date;
    FH_SALIDA: Date;
    SECUENCIA: number;
    tiempo: number;
}
class UsuarioTipo {
    USTI_ID: number;
    USTI_NOMBRE: string;
    USTI_DESC: string;
    USTI_NIVEL_PERMISOS: number;
    MENU_ID: string;
    MENU: Menu[];
}
class Usuario {
    USUA_ID: number;
    USUA_COD: string;
    USUA_DESC: string;
    USUA_NOMBRE: string;
    USUA_APELLIDO: string;
    USUA_RUT: string;
    USUA_CORREO: string;
    USUA_USERNAME: string;
    USUA_PASSWORD: string;
    USUA_ESTADO: boolean;
    USUA_OBSERVACION: string;
    USUARIO_TIPO: UsuarioTipo;
    OPER_ID: string;
    OPERACION: Operacion[];
}
class Menu {
    MENU_ID: number;
    MENU_TITULO: string;
    MENU_LINK: string;
    MENU_ORDEN: number;
    MENU_PID: number;
    MENU_CLASE: string;
    MENU_ICONO: string;
    MENU_HIJOS: Menu[];
}