using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PreRutaBC
/// </summary>
namespace Ruteador.App_Code.Models
{
    public partial class PreRutaBC : PreRuta
    {
        SqlTransaccion tran = new SqlTransaccion();
        public PreRutaBC()
        {
            CONDUCTOR = new ConductorBC();
            ENVIO = new EnvioBC();
            OPERACION = new OperacionBC();
            ORIGEN = new OrigenBC();
            TRAILER = new TrailerBC();
            TRACTO = new TractoBC();
        }
        public DataTable ObtenerTodo(DateTime desde, DateTime hasta, int hora_id = 0, int regi_id = 0, int ciud_id = 0, int comu_id = 0, int usua_id = 0, string peru_numero = null, string envio = null)
        {
            return tran.PreRuta_ObtenerTodo(desde, hasta, hora_id, regi_id, ciud_id, comu_id, usua_id, peru_numero, envio);
        }
        public List<PreRutaBC> ObtenerArray(DateTime desde, DateTime hasta, int hora_id = 0, int regi_id = 0, int ciud_id = 0, int comu_id = 0, int usua_id = 0, string peru_numero = null, string envio = null, bool puntos_ruta = false)
        {
            List<PreRutaBC> listado = tran.PreRuta_ObtenerArray(desde, hasta, hora_id, regi_id, ciud_id, comu_id, usua_id, peru_numero, envio);
            if (puntos_ruta)
            {
                foreach(PreRutaBC pr in listado)
                {
                    pr.PEDIDOS = new PedidoBC().ObtenerArray(desde: DateTime.MinValue, hasta:DateTime.MinValue, id_ruta: pr.ID);
                }
            }
            return listado;
        }
        public DataTable ObtenerPuntos(int id_ruta = 0)
        {
            return tran.Puntos_ObtenerXPreRuta(id_ruta);
        }
        public PreRutaBC ObtenerXId(int id_preruta)
        {
            return tran.PreRuta_ObtenerXId(id_preruta);
        }
        public bool GuardarPuntos(PreRutaBC p, string id_destinos, string tiempos, string hora_salida)
        {
            return tran.PreRuta_GuardarPuntos(p, id_destinos, tiempos, hora_salida);
        }
        public bool GuardarPuntos(string id_destinos, string tiempos, string hora_salida)
        {
            return tran.PreRuta_GuardarPuntos(this, id_destinos, tiempos, hora_salida);
        }
        public bool GuardarPuntos()
        {
            return tran.PreRuta_GuardarPuntos(this);
        }
        public bool GuardarPuntos(PreRutaBC p)
        {
            return tran.PreRuta_GuardarPuntos(p);
        }
        public bool GuardarDetalle()
        {
            return tran.PreRuta_GuardarDetalle(this);
        }
        public bool GuardarDetalle(PreRutaBC p)
        {
            return tran.PreRuta_GuardarDetalle(p);
        }
        public DataTable IngresarExcel(DataTable dt, int usua_id)
        {
            return tran.PreRuta_IngresarExcel(dt, usua_id);
        }
        public DataTable ProcesarExcel(int usua_id)
        {
            return tran.PreRuta_ProcesarExcel(usua_id);
        }
        public bool Eliminar(long id_ruta)
        {
            return tran.PreRuta_Eliminar(id_ruta);
        }
        public string obtenerultimosprocesos()
        {
            return tran.PreRuta_ObtenerUltimosProcesos();
        }
        public DataTable CrearEnvio(string pedidos, int usua_id, bool archivar)
        {
            return tran.PreRuta_CrearEnvio(pedidos, usua_id, archivar);
        }
    }
    public partial class PreRuta
    {
        public int ID { get; set; }
        public string NUMERO { get; set; }
        public DateTime FH_VIAJE { get; set; }
        public int ID_MOVIL { get; set; }
        public int ID_ESTADO { get; set; }
        public int ID_CLIENTE_GPS { get; set; }
        public int ID_TIPOVIAJE { get; set; }
        public DateTime FH_SALIDA { get; set; }
        public DateTime FH_RETORNO { get; set; }
        public string OBSERVACION { get; set; }
        public string RETORNO { get; set; }
        public DateTime FH_ULT_REPORTE { get; set; }
        public DateTime FH_CREACION { get; set; }
        public bool CORREO_GPS { get; set; }
        public DateTime FH_UPDATE { get; set; }
        public decimal TOTAL_KG { get; set; }
        public string RUTA { get; set; }
        public DateTime FECHA_PRESENTACION { get; set; }
        public DateTime FECHA_INICIOCARGA { get; set; }
        public DateTime FECHA_FINCARGA { get; set; }
        public DateTime FECHA_DESPACHOEXP { get; set; }
        public DateTime FECHA_INICIOEXP { get; set; }
        public DateTime FECHA_FINEXP { get; set; }
        public string RUTA_COLOR { get; set; }
        public ConductorBC CONDUCTOR { get; set; }
        public EnvioBC ENVIO { get; set; }
        public OperacionBC OPERACION { get; set; }
        public OrigenBC ORIGEN { get; set; }
        public TrailerBC TRAILER { get; set; }
        public TractoBC TRACTO { get; set; }
        public List<PedidoBC> PEDIDOS { get; set; }
    }
}