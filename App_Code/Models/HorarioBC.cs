using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de HorarioBC
/// </summary>
namespace Ruteador.App_Code.Models
{
    public partial class HorarioBC : Horario
    {
        SqlTransaccion tran = new SqlTransaccion();
        public HorarioBC()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public DataTable ObtenerTodo()
        {
            return tran.Horario_ObtenerTodo();
        }
    }
    public partial class Horario
    {
        public int HORA_ID { get; set; }
        public string HORA_COD { get; set; }
        public string HORA_DESC { get; set; }
    }
}