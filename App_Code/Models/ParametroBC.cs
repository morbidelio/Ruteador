using System;
using System.Data;

namespace Ruteador.App_Code.Models
{
    public partial class ParametroBC : Parametro
    {
        SqlTransaccion tran = new SqlTransaccion();
        public DataTable ObtenerTodo(string para_nombre = null, string para_obs = null)
        {
            return tran.Parametro_ObtenerTodo(para_nombre, para_obs);
        }
        public ParametroBC ObtenerXId()
        {
            return tran.Parametro_ObtenerXId(this.PARA_ID);
        }
        public ParametroBC ObtenerXId(int para_id)
        {
            return tran.Parametro_ObtenerXId(para_id);
        }
        public bool Guardar()
        {
            return tran.Parametro_Guardar(this);
        }
        public bool Guardar(ParametroBC p)
        {
            return tran.Parametro_Guardar(p);
        }
        public bool Eliminar()
        {
            return tran.Parametro_Eliminar(this.PARA_ID);
        }
        public bool Eliminar(int para_id)
        {
            return tran.Parametro_Eliminar(para_id);
        }
    }
    public partial class Parametro
    {
        public int PARA_ID { get; set; }
        public string PARA_NOMBRE { get; set; }
        public string PARA_OBS { get; set; }
        public string PARA_VALOR { get; set; }
        public int USUA_ID_CREACION { get; set; }
        public int USUA_ID_MODIFICACION { get; set; }
        public DateTime PARA_FH_CREACION { get; set; }
        public DateTime PARA_FH_MODIFICACION { get; set; }
    }
}