using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de TractoBC
/// </summary>
namespace Ruteador.App_Code.Models
{
    public class TractoBC : Tracto
    {
        readonly SqlTransaccion tran = new SqlTransaccion();
        public TractoBC()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public DataTable ObtenerTodo(DateTime fecha = new DateTime(), int hora_id = 0, string trac_placa = null)
        {
            return tran.Tracto_ObtenerTodo(fecha, hora_id, trac_placa);
        }
        public TractoBC ObtenerXId(int trac_id)
        {
            return tran.Tracto_ObtenerXId(trac_id);
        }
        public TractoBC ObtenerXPlaca(string trac_placa)
        {
            return tran.Tracto_ObtenerXPlaca(trac_placa);
        }
        public bool Guardar()
        {
            return tran.Tracto_Guardar(this);
        }
        public bool Guardar(TractoBC t)
        {
            return tran.Tracto_Guardar(t);
        }
        public bool Eliminar()
        {
            return tran.Tracto_Eliminar(this.TRAC_ID);
        }
        public bool Eliminar(int trac_id)
        {
            return tran.Tracto_Eliminar(trac_id);
        }
    }
    public class Tracto
    {
        public int TRAC_ID { get; set; }
        public string TRAC_PLACA { get; set; }
        public DateTime TRAC_FH_CREACION { get; set; }
    }
}