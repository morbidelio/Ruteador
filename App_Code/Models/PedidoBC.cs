using Ruteador.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PedidoBC
/// </summary>
namespace Ruteador.App_Code.Models
{
    public partial class PedidoBC : Pedido
    {
        SqlTransaccion tran = new SqlTransaccion();
        public PedidoBC()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
            COMUNA = new ComunaBC();
            USUARIO_PEDIDO = new UsuarioBC();
            USUARIO_ENVIO = new UsuarioBC();
            HORA_SALIDA = new HorarioBC();
            RUTA_PEDIDO = new RutaPedidoBC();
        }
        public DataTable ObtenerTodo(DateTime desde, DateTime hasta, int hora_id = 0, int regi_id = 0, int ciud_id = 0, int comu_id = 0, int usua_id = 0, string peru_numero = null, bool solo_sin_ruta = false, int id_ruta=0)
        {
            return tran.Pedido_ObtenerTodo(desde, hasta, hora_id, regi_id, ciud_id, comu_id, usua_id, peru_numero, solo_sin_ruta, id_ruta);
        }
        public DataTable ObtenerTodo(int hora_id = 0, int regi_id = 0, int ciud_id = 0, int comu_id = 0, int usua_id = 0, string peru_numero = null, bool solo_sin_ruta = false, int id_ruta = 0)
        {
            return tran.Pedido_ObtenerTodo(DateTime.MinValue, DateTime.MinValue, hora_id, regi_id, ciud_id, comu_id, usua_id, peru_numero, solo_sin_ruta, id_ruta);
        }
        public List<PedidoBC> ObtenerArray(bool solo_sin_ruta, DateTime desde, DateTime hasta, int hora_id = 0, int regi_id = 0, int ciud_id = 0, int comu_id = 0, int usua_id = 0, string peru_numero = null, int id_ruta = 0)
        {
            return tran.Pedido_ObtenerArray(solo_sin_ruta, desde, hasta, hora_id, regi_id, ciud_id, comu_id, usua_id, peru_numero, id_ruta);
        }
        public List<PedidoBC> ObtenerArray(DateTime desde, DateTime hasta, int hora_id = 0, int regi_id = 0, int ciud_id = 0, int comu_id = 0, int usua_id = 0, string peru_numero = null, int id_ruta = 0)
        {
            return tran.Pedido_ObtenerArray(desde, hasta, hora_id, regi_id, ciud_id, comu_id, usua_id, peru_numero, id_ruta);
        }
        public PedidoBC ObtenerXId()
        {
            return tran.Pedido_ObtenerXId(this.PERU_ID);
        }
        public PedidoBC ObtenerXId(Int64 peru_id)
        {
            return tran.Pedido_ObtenerXId(peru_id);
        }
        public DataTable IngresarExcel(DataTable dt, int usua_id)
        {
            return tran.Pedido_IngresarExcel(dt, usua_id);
        }
        public DataTable ProcesarExcel(int usua_id)
        {
            return tran.Pedido_ProcesarExcel(usua_id);
        }
        public bool CrearEnvio(string pedidos, int usua_id, out int id)
        {
            return tran.Pedido_CrearEnvio(pedidos, usua_id, out id);
        }
        public bool Guardar()
        {
            return tran.Pedido_Guardar(this);
        }
        public bool Guardar(PedidoBC p)
        {
            return tran.Pedido_Guardar(p);
        }
        public bool Eliminar()
        {
            return tran.Pedido_Eliminar(this.PERU_ID);
        }
        public bool Eliminar(long peru_id)
        {
            return tran.Pedido_Eliminar(peru_id);
        }

        public bool ModificarFechaAgendamiento(string ids, DateTime desde, int horario)
        {
             return tran.PedidoModificarFechaAgendamiento(ids, desde, horario);
        }

    }
    public partial class Pedido
    {
        public long PERU_ID { get; set; }
        public string PERU_NUMERO { get; set; }
        public string PERU_CODIGO { get; set; }
        public DateTime PERU_FECHA { get; set; }
        public string PERU_PESO { get; set; }
        public string PERU_TIEMPO { get; set; }
        public string PERU_DIRECCION { get; set; }
        public decimal PERU_LATITUD { get; set; }
        public decimal PERU_LONGITUD { get; set; }
        public bool PERU_ENVIADO_RUTEADOR { get; set; }
        public DateTime PERU_FH_ENVIO { get; set; }
        public DateTime PERU_FH_CREACION { get; set; }
        public ComunaBC COMUNA { get; set; }
        public UsuarioBC USUARIO_PEDIDO { get; set; }
        public UsuarioBC USUARIO_ENVIO { get; set; }
        public HorarioBC HORA_SALIDA { get; set; }
        public RutaPedidoBC RUTA_PEDIDO { get; set; }
    }
}