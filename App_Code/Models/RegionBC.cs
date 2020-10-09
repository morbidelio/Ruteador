using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de RegionBC
/// </summary>
namespace Ruteador.App_Code.Models
{
    public partial class RegionBC : Region
    {
        SqlTransaccion tran = new SqlTransaccion();
        public RegionBC()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public DataTable ObtenerTodo()
        {
            return tran.Region_ObtenerTodo();
        }
        public RegionBC ObtenerXId()
        {
            return tran.Region_ObtenerXId(this.REGI_ID);
        }
        public RegionBC ObtenerXId(int regi_id)
        {
            return tran.Region_ObtenerXId(regi_id);
        }
    }
    public partial class Region
    {
        public int REGI_ID { get; set; }
        public string REGI_NOMBRE { get; set; }
        public string REGI_DESCRIPCION { get; set; }
        public int REGI_ORDEN { get; set; }
    }
}