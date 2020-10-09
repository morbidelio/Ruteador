using System;
using System.Collections.Generic;
using System.Data;

namespace Ruteador.App_Code.Models
{
    public partial class UsuarioTipoBC : UsuarioTipo
    {
        SqlTransaccion tran = new SqlTransaccion();
        public UsuarioTipoBC()
        {
            this.MENU = new List<MenuBC>();
        }
        public DataTable ObtenerTodo(string nombre = null)
        {
            return tran.UsuarioTipo_ObtenerTodo(nombre);
        }
        public UsuarioTipoBC ObtenerXId()
        {
            UsuarioTipoBC u = tran.UsuarioTipo_ObtenerXId(this.USTI_ID);
            u.MENU = new MenuBC().ObtenerList(this.USTI_ID);
            return u;
        }
        public UsuarioTipoBC ObtenerXId(int usti_id)
        {
            UsuarioTipoBC u = tran.UsuarioTipo_ObtenerXId(usti_id);
            u.MENU = new MenuBC().ObtenerList(usti_id);
            return u;
        }
        public bool Guardar()
        {
            return tran.UsuarioTipo_Guardar(this);
        }
        public bool Guardar(UsuarioTipoBC ut)
        {
            return tran.UsuarioTipo_Guardar(ut);
        }
        public bool Eliminar()
        {
            return tran.UsuarioTipo_Eliminar(this.USTI_ID);
        }
        public bool Eliminar(int usti_id)
        {
            return tran.UsuarioTipo_Eliminar(usti_id);
        }
    }
    public partial class UsuarioTipo
    {
        public int USTI_ID { get; set; }
        public string USTI_NOMBRE { get; set; }
        public string USTI_DESC { get; set; }
        public int USTI_NIVEL_PERMISOS { get; set; }
        public string MENU_ID { get; set; }
        public List<MenuBC> MENU { get; set; }
    }
}