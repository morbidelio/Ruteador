using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de IconoBC
/// </summary>
/// 
namespace Ruteador.App_Code.Models
{
    public partial class IconoBC : Icono
    {
        SqlTransaccion tran = new SqlTransaccion();
        public IconoBC()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public DataTable ObtenerTodo()
        {
            return tran.Icono_ObtenerTodo();
        }
        public IconoBC ObtenerXId(int icon_id)
        {
            return tran.Icono_ObtenerXId(icon_id);
        }
    }
    public partial class Icono
    {
        public int ICON_ID { get; set; }
        public string ICON_URL { get; set; }
    }
}