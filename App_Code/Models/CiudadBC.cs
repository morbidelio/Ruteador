using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de CiudadBC
/// </summary>
namespace Ruteador.App_Code.Models
{
    public partial class CiudadBC : Ciudad
    {
        SqlTransaccion tran = new SqlTransaccion();
        public CiudadBC()
        {
            this.REGION = new RegionBC();
        }
        public DataTable ObtenerTodo(int regi_id = 0)
        {
            return tran.Ciudad_ObtenerTodo(regi_id);
        }
        public CiudadBC ObtenerXId()
        {
            return tran.Ciudad_ObtenerXId(this.CIUD_ID);
        }
        public CiudadBC ObtenerXId(int ciud_id)
        {
            return tran.Ciudad_ObtenerXId(ciud_id);
        }
    }
    public partial class Ciudad
    {
        public int CIUD_ID { get; set; }
        public string CIUD_NOMBRE { get; set; }
        public RegionBC REGION { get; set; }
    }
}