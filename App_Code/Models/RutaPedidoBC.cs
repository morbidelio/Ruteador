using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de RutaPedidoBC
/// </summary>
namespace Ruteador.App_Code.Models
{
    public partial class RutaPedidoBC : RutaPedido
    {
        public RutaPedidoBC()
        {
        }
    }
    public partial class RutaPedido
    {
        public int RPPE_ID { get; set; }
        public DateTime FH_PLANIFICA { get; set; }
        public DateTime FH_LLEGADA { get; set; }
        public DateTime FH_SALIDA { get; set; }
        public int SECUENCIA { get; set; }
        public int tiempo { get; set; }
    }
}