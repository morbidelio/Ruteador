using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de TrailerBC
/// </summary>
namespace Ruteador.App_Code.Models
{
    public class TrailerBC : Trailer
    {
        readonly SqlTransaccion tran = new SqlTransaccion();
        public TrailerBC()
        {
            TRAILER_TIPO = new TrailerTipoBC();
        }
        public DataTable ObtenerTodo(DateTime fecha = new DateTime(), int hora_id = 0, string trai_numero = null, string trai_placa = null, int trti_id = 0)
        {
            return tran.Trailer_ObtenerTodo(fecha, hora_id, trai_numero, trai_placa, trti_id);
        }
        public TrailerBC ObtenerXId(int trai_id)
        {
            return tran.Trailer_ObtenerXId(trai_id);
        }
        public TrailerBC ObtenerXPlaca(string trai_nro = null, string trai_placa = null)
        {
            return tran.Trailer_ObtenerXPlaca(trai_nro, trai_placa);
        }
        public bool Guardar()
        {
            return tran.Trailer_Guardar(this);
        }
        public bool Guardar(TrailerBC t)
        {
            return tran.Trailer_Guardar(t);
        }
        public bool Eliminar()
        {
            return tran.Trailer_Eliminar(this.TRAI_ID);
        }
        public bool Eliminar(int trai_id)
        {
            return tran.Trailer_Eliminar(trai_id);
        }
    }
    public class Trailer
    {
        public int TRAI_ID { get; set; }
        public string TRAI_COD { get; set; }
        public string TRAI_NUMERO { get; set; }
        public string TRAI_PLACA { get; set; }
        public TrailerTipoBC TRAILER_TIPO { get; set; }
    }
    public class TrailerTipoBC : TrailerTipo
    {
        readonly SqlTransaccion tran = new SqlTransaccion();
        public DataTable ObtenerTodo(string trti_desc = null) {
            return tran.TrailerTipo_ObtenerTodo(trti_desc);
        }
        public TrailerTipoBC ObtenerXId(int trti_id = 0)
        {
            return tran.TrailerTipo_ObtenerXId(trti_id);
        }
        public bool Guardar()
        {
            return tran.TrailerTipo_Guardar(this);
        }
        public bool Guardar(TrailerTipoBC t)
        {
            return tran.TrailerTipo_Guardar(t);
        }
        public bool Eliminar()
        {
            return tran.TrailerTipo_Eliminar(this.TRTI_ID);
        }
        public bool Eliminar(int trti_id)
        {
            return tran.TrailerTipo_Eliminar(trti_id);
        }
    }
    public class TrailerTipo
    {
        public int TRTI_ID { get; set; }
        public string TRTI_DESC { get; set; }
        public string TRTI_COLOR { get; set; }
    }
}