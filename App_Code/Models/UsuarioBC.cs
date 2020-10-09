using System.Collections.Generic;
using System.Data;

namespace Ruteador.App_Code.Models
{
    public partial class UsuarioBC : Usuario
    {
        SqlTransaccion tran = new SqlTransaccion();
        public UsuarioBC()
        {
            this.USUARIO_TIPO = new UsuarioTipoBC();
            this.OPERACION = new List<OperacionBC>();
        }
        public DataTable ObtenerTodo(bool usua_activos, int usti_id = 0, string usua_rut = null, string usua_nombre = null, string usua_apellido = null, string usua_username = null)
        {
            return tran.Usuario_ObtenerTodo(usua_activos, usti_id, usua_rut, usua_nombre, usua_apellido, usua_username);
        }
        public DataTable ObtenerTodo(int usti_id = 0, string usua_rut = null, string usua_nombre = null, string usua_apellido = null, string usua_username = null)
        {
            return tran.Usuario_ObtenerTodo(usti_id, usua_rut, usua_nombre, usua_apellido, usua_username);
        }
        public UsuarioBC ObtenerXId()
        {
            UsuarioBC u = tran.Usuario_ObtenerXId(this.USUA_ID);
            u.OPERACION = tran.Operacion_ObtenerList(u.USUA_ID);
            return u;
        }
        public UsuarioBC ObtenerXId(int usua_id)
        {
            UsuarioBC u = tran.Usuario_ObtenerXId(usua_id);
            u.OPERACION = tran.Operacion_ObtenerList(u.USUA_ID);
            return u;
        }
        public UsuarioBC ObtenerXRut()
        {
            UsuarioBC u = tran.Usuario_ObtenerXRut(this.USUA_RUT);
            if (u.USUA_ID != 0)
            {
                u.OPERACION = tran.Operacion_ObtenerList(u.USUA_ID);
            }
            return u;
        }
        public UsuarioBC ObtenerXRut(string usua_rut)
        {
            UsuarioBC u = tran.Usuario_ObtenerXRut(usua_rut);
            if (u.USUA_ID != 0)
            {
                u.OPERACION = tran.Operacion_ObtenerList(u.USUA_ID);
            }
            return u;
        }
        public UsuarioBC Login(string usua_username, string usua_password)
        {
            UsuarioBC u = tran.Usuario_Login(usua_username, usua_password);
            u.OPERACION = tran.Operacion_ObtenerList(u.USUA_ID);
            return u;
        }
        public UsuarioBC Login()
        {
            UsuarioBC u = tran.Usuario_Login(this.USUA_USERNAME, this.USUA_PASSWORD);
            u.OPERACION = tran.Operacion_ObtenerList(u.USUA_ID);
            return u;
        }
        public bool Guardar()
        {
            return tran.Usuario_Guardar(this);
        }
        public bool Guardar(UsuarioBC u)
        {
            return tran.Usuario_Guardar(u);
        }
        public bool Eliminar()
        {
            return tran.Usuario_Eliminar(this.USUA_ID);
        }
        public bool Eliminar(int usua_id)
        {
            return tran.Usuario_Eliminar(usua_id);
        }
        public bool Activar()
        {
            return tran.Usuario_Activar(this.USUA_ID);
        }
        public bool Activar(int usua_id)
        {
            return tran.Usuario_Activar(usua_id);
        }
    }
    public partial class Usuario
    {
        public int USUA_ID { get; set; }
        public string USUA_COD { get; set; }
        public string USUA_DESC { get; set; }
        public string USUA_NOMBRE { get; set; }
        public string USUA_APELLIDO { get; set; }
        public string USUA_RUT { get; set; }
        public string USUA_CORREO { get; set; }
        public string USUA_USERNAME { get; set; }
        public string USUA_PASSWORD { get; set; }
        public bool USUA_ESTADO { get; set; }
        public string USUA_OBSERVACION { get; set; }
        public UsuarioTipoBC USUARIO_TIPO { get; set; }
        public string OPER_ID { get; set; }
        public List<OperacionBC> OPERACION { get; set; }
    }
}