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
            DETALLE = new List<PedidoDetalleBC>();
        }
        public DataTable ObtenerTodo(DateTime desde = new DateTime(), DateTime hasta = new DateTime(), int hora_id = 0, int regi_id = 0, int ciud_id = 0, string comu_id = null, int usua_id = 0, string peru_numero = null, bool solo_sin_ruta = false, long id_ruta=0)
        {
            return tran.Pedido_ObtenerTodo(desde, hasta, hora_id, regi_id, ciud_id, comu_id, usua_id, peru_numero, solo_sin_ruta, id_ruta);
        }
        public List<PedidoBC> ObtenerArray(bool solo_sin_ruta, DateTime desde = new DateTime(), DateTime hasta = new DateTime(), int hora_id = 0, int regi_id = 0, int ciud_id = 0, int comu_id = 0, int usua_id = 0, string peru_numero = null, long id_ruta = 0)
        {
            return tran.Pedido_ObtenerArray(solo_sin_ruta, desde, hasta, hora_id, regi_id, ciud_id, comu_id, usua_id, peru_numero, id_ruta);
        }
        public List<PedidoBC> ObtenerArray(DateTime desde = new DateTime(), DateTime hasta = new DateTime(), int hora_id = 0, int regi_id = 0, int ciud_id = 0, int comu_id = 0, int usua_id = 0, string peru_numero = null, long id_ruta = 0)
        {
            return tran.Pedido_ObtenerArray(desde, hasta, hora_id, regi_id, ciud_id, comu_id, usua_id, peru_numero, id_ruta);
        }
        public PedidoBC ObtenerXId(bool carga_detalle = false)
        {
            return tran.Pedido_ObtenerXId(this.PERU_ID, carga_detalle);
        }
        public PedidoBC ObtenerXId(Int64 peru_id, bool carga_detalle = false)
        {
            return tran.Pedido_ObtenerXId(peru_id, carga_detalle);
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
        public bool Guardar(DataTable dt = null)
        {
            return tran.Pedido_Guardar(this, dt);
        }
        public bool Guardar(PedidoBC p, DataTable dt = null)
        {
            return tran.Pedido_Guardar(p, dt);
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
        public string PERU_NOMBRE { get; set; }
        public bool PERU_ENVIADO_RUTEADOR { get; set; }
        public DateTime PERU_FH_ENVIO { get; set; }
        public DateTime PERU_FH_CREACION { get; set; }
        public ComunaBC COMUNA { get; set; }
        public UsuarioBC USUARIO_PEDIDO { get; set; }
        public UsuarioBC USUARIO_ENVIO { get; set; }
        public HorarioBC HORA_SALIDA { get; set; }
        public RutaPedidoBC RUTA_PEDIDO { get; set; }
        public List<PedidoDetalleBC> DETALLE { get; set; }
    }
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
    public partial class PedidoDetalleBC : PedidoDetalle
    {
        SqlTransaccion tran = new SqlTransaccion();
        public PedidoDetalleBC()
        {
            COMUNA_CLIENTE = new ComunaBC();
            PEDIDO = new PedidoBC();
        }
        public DataTable ObtenerTodo(long peru_id = 0, int regi_id = 0, int ciud_id = 0, int comu_id = 0, long ruta_id = 0)
        {
            return tran.PedidoDetalle_ObtenerTodo(peru_id, regi_id, ciud_id, comu_id, ruta_id);
        }
        public PedidoDetalleBC ObtenerXId(long pede_id = 0)
        {
            return tran.PedidoDetalle_ObtenerXId(pede_id);
        }
        public bool Guardar()
        {
            return tran.PedidoDetalle_Guardar(this);
        }
        public bool Guardar(PedidoDetalleBC dp)
        {
            return tran.PedidoDetalle_Guardar(dp);
        }
        public bool Eliminar()
        {
            return tran.PedidoDetalle_Eliminar(this.PEDE_ID);
        }
        public bool Eliminar(long pede_id)
        {
            return tran.PedidoDetalle_Eliminar(pede_id);
        }
    }
    public partial class PedidoDetalle
    {
        public long PEDE_ID { get; set; }
        public string CODIGO_PRODUCTO { get; set; }
        public string CODIGO_CLIENTE { get; set; }
        public string DIRECCION_CLIENTE { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public string NUMERO_GUIA { get; set; }
        public decimal PESO_PEDIDO { get; set; }
        public decimal PEDE_CANTIDAD { get; set; }
        public string PEDE_DESC_PRODUCTO { get; set; }
        public PedidoBC PEDIDO { get; set; }
        public ComunaBC COMUNA_CLIENTE { get; set; }
    }
}