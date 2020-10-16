using Ruteador.App_Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ConductorBC
/// </summary>
namespace Ruteador.App_Code.Models
{
    public partial class ConductorBC : Conductor
    {
        SqlTransaccion tran = new SqlTransaccion();
        public ConductorBC()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public DataTable ObtenerTodo(bool cond_activo, bool cond_bloqueado, string cond_rut = null, string cond_nombre = null)
        {
            return tran.Conductor_ObtenerTodo(cond_activo, cond_bloqueado, cond_rut, cond_nombre);
        }
        public DataTable ObtenerTodo(string cond_rut = null, string cond_nombre = null)
        {
            return tran.Conductor_ObtenerTodo(cond_rut, cond_nombre);
        }
        public ConductorBC ObtenerXId(int cond_id)
        {
            return tran.Conductor_ObtenerXId(cond_id);
        }
        public ConductorBC ObtenerXRut(string cond_rut)
        {
            return tran.Conductor_ObtenerXRut(cond_rut);
        }
        public bool Guardar()
        {
            return tran.Conductor_Guardar(this);
        }
        public bool Guardar(ConductorBC c)
        {
            return tran.Conductor_Guardar(c);
        }
        public bool Eliminar()
        {
            return tran.Conductor_Eliminar(this.COND_ID);
        }
        public bool Eliminar(int cond_id)
        {
            return tran.Conductor_Eliminar(cond_id);
        }
        public bool Activar()
        {
            return tran.Conductor_Activar(this.COND_ID);
        }
        public bool Activar(int cond_id)
        {
            return tran.Conductor_Activar(cond_id);
        }
        public bool Bloquear(int cond_id, string cond_motivo_bloqueo = null)
        {
            return tran.Conductor_Bloquear(cond_id, cond_motivo_bloqueo);
        }
    }
    public partial class Conductor
    {
        public int COND_ID { get; set; }
        public string COND_IMAGEN { get; set; }
        public string COND_RUT { get; set; }
        public string COND_NOMBRE { get; set; }
        public bool COND_ACTIVO { get; set; }
        public bool COND_BLOQUEADO { get; set; }
        public string COND_TELEFONO { get; set; }
        public string COND_MOTIVO_BLOQUEO { get; set; }
        public bool COND_EXTRANJERO { get; set; }
    }
}