using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de OrigenBC
/// </summary>
namespace Ruteador.App_Code.Models
{
    public partial class OrigenBC : Origen
    {
        SqlTransaccion tran = new SqlTransaccion();
        public OrigenBC()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
            OPERACION = new OperacionBC();
            COMUNA = new ComunaBC();
            RADIO_PE = 50;
            ICONO = new IconoBC();
        }
        public DataTable ObtenerTodo(string orig_nombre = null, int regi_id = 0, int ciud_id = 0, int comu_id = 0)
        {
            return tran.Origen_ObtenerTodo(orig_nombre, regi_id, ciud_id, comu_id);
        }
        public List<OrigenBC> ObtenerArray(string orig_nombre = null, int regi_id = 0, int ciud_id = 0, int comu_id = 0)
        {
            return tran.Origen_ObtenerArray(orig_nombre, regi_id, ciud_id, comu_id);
        }
        public OrigenBC ObtenerXId()
        {
            return tran.Origen_ObtenerXId(this.ID);
        }
        public OrigenBC ObtenerXId(int origen_id)
        {
            return tran.Origen_ObtenerXId(origen_id);
        }

        public OrigenBC ObtenerXIdruta(int ruta_id)
        {
            return tran.Origen_ObtenerXIdruta(ruta_id);
        }
        public bool Guardar()
        {
            return tran.Origen_Guardar(this);
        }
        public bool Guardar(OrigenBC o)
        {
            return tran.Origen_Guardar(o);
        }
        public bool Eliminar()
        {
            return tran.Origen_Eliminar(this.ID);
        }
        public bool Eliminar(int id)
        {
            return tran.Origen_Eliminar(id);
        }
    }
    public partial class Origen
    {
        public int ID { get; set; }
        public string ID_PE { get; set; }
        public int TIPO_ID { get; set; }
        public string NOMBRE_PE { get; set; }
        public string DIRECCION_PE { get; set; }
        public string COMUNA_PE { get; set; }
        public string CIUDAD_PE { get; set; }
        public decimal LAT_PE { get; set; }
        public decimal LON_PE { get; set; }
        public int RADIO_PE { get; set; }
        public bool IS_POLIGONO { get; set; }
        public int ID_CLIENTE { get; set; }
        public string REGION { get; set; }
        public DateTime FH_CREA { get; set; }
        public DateTime FH_UPDATE { get; set; }
        public int ID_MERCADO { get; set; }
        public int ID_ZONA { get; set; }
        public int invalido { get; set; }
        public string PERU_LLEGADA { get; set; }
        public OperacionBC OPERACION { get; set; }
        public ComunaBC COMUNA { get; set; }
        public IconoBC ICONO { get; set; }
    }
}