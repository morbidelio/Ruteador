using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Descripción breve de OperacionBC
/// </summary>
namespace Ruteador.App_Code.Models
{
    public partial class OperacionBC : Operacion
    {
        SqlTransaccion tran = new SqlTransaccion();
        public OperacionBC()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public OperacionBC ObtenerXId()
        {
            return tran.Operacion_ObtenerXId(this.OPER_ID);
        }
        public OperacionBC ObtenerXId(int oper_id)
        {
            return tran.Operacion_ObtenerXId(oper_id);
        }
        public DataTable ObtenerTodo(int usua_id = 0)
        {
            return tran.Operacion_ObtenerTodo(usua_id);
        }
    }
    public partial class Operacion
    {
        public int OPER_ID { get; set; }
        public string OPER_NOMBRE { get; set; }
    }
}