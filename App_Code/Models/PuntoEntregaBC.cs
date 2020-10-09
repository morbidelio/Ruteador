using System.Data;

namespace Ruteador.App_Code.Models
{
    public class PuntoEntregaBC : PuntoEntrega
    {
        SqlTransaccion tran = new SqlTransaccion();
        public DataTable ObtenerTodo(int id_tipo = 0)
        {
            return tran.Puntos_ObtenerTodo(id_tipo);
        }
        public PuntoEntregaBC ObtenerXId()
        {
            return tran.Puntos_ObtenerXId(this.ID);
        }
        public PuntoEntregaBC ObtenerXId(int id)
        {
            return tran.Puntos_ObtenerXId(id);
        }
    }
    public class PuntoEntrega
    {
        public int ID { get; set; }
        public string NOMBRE_PE { get; set; }
        public string DIRECCION_PE { get; set; }
        public decimal LAT_PE { get; set; }
        public decimal LON_PE { get; set; }
        public bool IS_POLIGONO { get; set; }
    }
}