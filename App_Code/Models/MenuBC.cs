using System.Data;
using System.Collections.Generic;
using System;

namespace Ruteador.App_Code.Models
{
    public partial class MenuBC : Menu
    {
        SqlTransaccion tran = new SqlTransaccion();
        public MenuBC()
        {
            MENU_HIJOS = new List<MenuBC>();
        }
        public DataSet ObtenerTodo(int usti_id = 0)
        {
            return tran.Menu_ObtenerTodo(usti_id);
        }
        public List<MenuBC> ObtenerList(int usti_id = 0)
        {
            List<MenuBC> output = new List<MenuBC>();
            DataSet ds = ObtenerTodo(usti_id);
            foreach (DataRow dr in ds.Tables["MENU_PADRES"].Rows)
            {
                DataView dw = new DataView(ds.Tables["MENU_HIJOS"]);
                dw.RowFilter = "MENU_PID = " + dr["MENU_ID"].ToString();
                MenuBC menu = new MenuBC()
                {
                    MENU_ID = Convert.ToInt32(dr["MENU_ID"]),
                    MENU_TITULO = Convert.ToString(dr["MENU_TITULO"]),
                    MENU_LINK = Convert.ToString(dr["MENU_LINK"]),
                    MENU_ORDEN = Convert.ToInt32(dr["MENU_ORDEN"]),
                    MENU_CLASE = Convert.ToString(dr["MENU_CLASE"]),
                    MENU_ICONO = Convert.ToString(dr["MENU_ICONO"])
                };
                foreach (DataRow dr2 in dw.ToTable().Rows)
                {
                    menu.MENU_HIJOS.Add(new MenuBC()
                    {
                        MENU_ID = Convert.ToInt32(dr2["MENU_ID"]),
                        MENU_TITULO = Convert.ToString(dr2["MENU_TITULO"]),
                        MENU_LINK = Convert.ToString(dr2["MENU_LINK"]),
                        MENU_ORDEN = Convert.ToInt32(dr2["MENU_ORDEN"]),
                        MENU_PID = Convert.ToInt32(dr2["MENU_PID"]),
                        MENU_CLASE = Convert.ToString(dr2["MENU_CLASE"]),
                        MENU_ICONO = Convert.ToString(dr2["MENU_ICONO"])
                    });
                }
                output.Add(menu);
            }
            return output;
        }
    }
    public partial class Menu
    {
        public int MENU_ID { get; set; }
        public string MENU_TITULO { get; set; }
        public string MENU_LINK { get; set; }
        public int MENU_ORDEN { get; set; }
        public int MENU_PID { get; set; }
        public string MENU_CLASE { get; set; }
        public string MENU_ICONO { get; set; }
        public List<MenuBC> MENU_HIJOS { get; set; }
    }
}