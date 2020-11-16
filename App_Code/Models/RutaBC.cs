using System;
using System.Data;

namespace Ruteador.App_Code.Models
{
    public partial class RutaBC : Ruta
    {
        SqlTransaccion tran = new SqlTransaccion();
        public RutaBC()
        {

        }
        public DataTable ObtenerTodo()
        {
            return tran.Ruta_ObtenerTodo();
        }
        public DataTable ObtenerPuntos(int id_ruta)
        {
            return tran.Puntos_ObtenerXRuta(id_ruta);   
        }

        public bool Guardar(int id_ruta, string id_destinos)
        {
            return tran.Ruta_Guardar(id_ruta, id_destinos);
        }

    }
    public partial class Ruta
    {
        public int ID { get; set; }
        public string NUMERO { get; set; }
        public DateTime FH_VIAJE { get; set; }
        public int ID_ORIGEN { get; set; }
        public int ID_MOVIL { get; set; }
        public int ID_ESTADO { get; set; }
        public int ID_ESTADO_ERR { get; set; }
        public int ID_CLIENTE_GPS { get; set; }
        public int ID_CONDUCTOR { get; set; }
        public DateTime FH_SALIDA { get; set; }
        public DateTime FH_RETORNO { get; set; }
        public string OBSERVACION { get; set; }
        public string PATENTE_TRACTO { get; set; }
        public string PATENTE_CARRO { get; set; }
        public string CONDUCTOR { get; set; }
        public int ID_TIPOVIAJE { get; set; }
        public int ID_OPE { get; set; }
        public int ID_TRAILER { get; set; }
        public bool CORREO_PROXIMIDAD { get; set; }
        public string RETORNO { get; set; }
        public string TRANSPORTE { get; set; }
        public string INSERTA_TIPO { get; set; }
        public DateTime FH_LLEGA_CD { get; set; }
        public DateTime FH_ULT_REPORTE { get; set; }
        public DateTime FH_CREACION { get; set; }
        public bool CORREO_GPS { get; set; }
        public DateTime FH_UPDATE { get; set; }
        public int CIERRE_MANUAL { get; set; }
        public string MOTIVO_CIERRE { get; set; }
        public decimal TOTAL_KG { get; set; }
        public int ID_RUTA_RETORNO { get; set; }
        public int ID_LAPA { get; set; }
        public int ID_RETORNO { get; set; }
        public string RUTA { get; set; }
        public DateTime FECHA_PRESENTACION { get; set; }
        public DateTime FECHA_INICIOCARGA { get; set; }
        public DateTime FECHA_FINCARGA { get; set; }
        public DateTime FECHA_DESPACHOEXP { get; set; }
        public DateTime FECHA_INICIOEXP { get; set; }
        public DateTime FECHA_FINEXP { get; set; }
        public int SAP_ENVIO { get; set; }
        public DateTime SAP_FH_UPDATE { get; set; }
        public string SAP_RESPUESTA { get; set; }
        public string CONDUCTOR_RUT { get; set; }
        public string CONDUCTOR_NOMBRE { get; set; }
        public int IS_MULTI { get; set; }
    }
}