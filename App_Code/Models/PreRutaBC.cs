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
    public partial class PreRutaBC
    {
        SqlTransaccion tran = new SqlTransaccion();
        public PreRutaBC()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public DataTable ObtenerPuntos(int id_ruta)
        {
            return tran.Puntos_ObtenerXPreRuta(id_ruta);
        }
        public bool Guardar(int id_ruta, string id_destinos, string tiempos, string hora_salida)
        {
            return tran.PreRuta_Guardar(id_ruta, id_destinos, tiempos, hora_salida);
        }

        public DataTable IngresarExcel(DataTable dt, int usua_id)
        {
            return tran.Pre_ruta_IngresarExcel(dt, usua_id);
        }
        public DataTable ProcesarExcel(int usua_id)
        {
            return tran.Pre_ruta_ProcesarExcel(usua_id);
        }

        public bool Eliminar(long id_ruta)
        {
            return tran.Pre_ruta_Eliminar(id_ruta);
        }

        public string obtenerultimosprocesos()
        {
            return tran.obtenerultimosprocesos();
        }

        public DataTable CrearEnvio(string pedidos, int usua_id, bool archivar)
        {
            return tran.pre_ruta_CrearEnvio(pedidos, usua_id, archivar); 
        }

    }
    public partial class PreRuta
    {

    }
}