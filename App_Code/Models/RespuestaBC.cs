using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de RespuestaBC
/// </summary>
namespace Ruteador.App_Code.Models
{
    public class RespuestaBC : Respuesta
    {
        SqlTransaccion tran = new SqlTransaccion();
        public RespuestaBC()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public bool Guardar(DataTable dt, long id_ruta)
        {
            return tran.Respuesta_Guardar(dt, id_ruta);
        }
        public bool Guardar(string response, long id_ruta)
        {
            return tran.Respuesta_Guardar(response, id_ruta);
        }
        public bool Eliminar(long rure_id = 0, long ruta_id = 0)
        {
            return tran.Respuesta_Eliminar(rure_id, ruta_id);
        }
    }
    public class Respuesta
    {
        public long RURE_ID { get; set; }
        public string RURE_RESPUESTA { get; set; }
        public int RURE_ORDEN { get; set; }
        public PreRutaBC RUTA { get; set; }
    }
}