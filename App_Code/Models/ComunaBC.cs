using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ComunaBC
/// </summary>
namespace Ruteador.App_Code.Models
{
    public partial class ComunaBC : Comuna
    {
        SqlTransaccion tran = new SqlTransaccion();
        public ComunaBC()
        {
            this.CIUDAD = new CiudadBC();
        }
        public DataTable ObtenerTodo(int regi_id = 0, int ciud_id = 0)
        {
            return tran.Comuna_ObtenerTodo(regi_id, ciud_id);
        }
        public ComunaBC ObtenerXId()
        {
            return tran.Comuna_ObtenerXId(this.COMU_ID);
        }
        public ComunaBC ObtenerXId(int comu_id)
        {
            return tran.Comuna_ObtenerXId(comu_id);
        }
    }
    public partial class Comuna
    {
        public int COMU_ID { get; set; }
        public string COMU_NOMBRE { get; set; }
        public CiudadBC CIUDAD { get; set; }
    }
}