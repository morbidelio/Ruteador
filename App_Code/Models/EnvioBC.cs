using Ruteador.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;

/// <summary>
/// Descripción breve de Envio
/// </summary>
/// 
namespace Ruteador.App_Code.Models
{
public partial class EnvioBC:Envio
{
    SqlTransaccion tran = new SqlTransaccion();

    public EnvioBC()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
       // this.Env_ID = id;
    }

    public EnvioBC(int id)
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
        this.Env_ID = id;
	}

    public DataTable ObtenerTodo() //(DateTime desde, DateTime hasta, int hora_id = 0, int regi_id = 0, int ciud_id = 0, int comu_id = 0, int usua_id = 0, string peru_numero = null)
    {
        return tran.Envio_ObtenerTodo(); //desde, hasta, hora_id, regi_id, ciud_id, comu_id, usua_id, peru_numero);
    }

    public DataTable detalle ()
    {
        return tran.envio_detalle(this.Env_ID);
    }



    public bool Eliminar(int Env_ID)
    {
        return tran.envio_Eliminar(Env_ID);
    }


    public void archivo(DataTable dt)
    {

        StringBuilder sb = new StringBuilder();

        DataView view = new DataView(dt);
        DataTable distinctValues = view.ToTable(true, "rut", "nombre", "direccion", "comuna", "lat", "lon", "tEntrega", "Hora_cod_integra");

        IEnumerable<string> columnNames = distinctValues.Columns.Cast<DataColumn>().
                                          Select(column => column.ColumnName);
      //  sb.AppendLine(string.Join(",", columnNames));

        foreach (DataRow row in distinctValues.Rows)
        {
            IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString().Replace(",",".") );
            sb.AppendLine(string.Join(",", fields));
        }

        File.WriteAllText("c:\\ViewState\\cliente.txt", sb.ToString());

        this.str_cabecera = sb.ToString();


      //  tran.Pedido_CrearArchivo_integracion(1, "cabecera_" + DateTime.Now.ToString().Replace(" ", "").Replace(":", "_") + ".txt", sb.ToString());
       //   tran.Pedido_CrearArchivo_integracion(2, "cli_qruta.txt", sb.ToString(),"");


    }


    public void archivo2(DataTable dt, int env_id)
    {

        StringBuilder sb = new StringBuilder();

        DataView view = new DataView(dt);
        DataTable distinctValues = view.ToTable(false, "NumeroPedido", "Rut", "FECHA_INTEGRAV2", "Class", "Set", "envio");

        IEnumerable<string> columnNames = distinctValues.Columns.Cast<DataColumn>().
                                          Select(column => column.ColumnName);
      //  sb.AppendLine(string.Join(",", columnNames));

        foreach (DataRow row in distinctValues.Rows)
        {
            IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString().Replace(",", "."));
            sb.AppendLine(string.Join(",", fields));
        }

        File.WriteAllText("c:\\ViewState\\pedido.txt", sb.ToString());

        //tran.Pedido_CrearArchivo_integracion(1, "detalle_" + DateTime.Now.ToString().Replace(" ","").Replace(":","_") + ".txt", sb.ToString());
        //  tran.Pedido_CrearArchivo_integracion(2, "ped_qruta.txt", sb.ToString(), "CommQruta07");

        this.str_detalle = sb.ToString();

        DataView view3 = new DataView(dt);
        DataTable distinctValues3 = view3.ToTable(true, "COMANDO");
        

        tran.Pedido_CrearArchivo_integracion_v2(2, this.str_cabecera, this.str_detalle,distinctValues3.Rows[0]["COMANDO"].ToString(), env_id);
    }
   
}
   public partial class Envio
    {
        public int Env_ID { get; set; }
        public string str_cabecera { get; set; }
        public string str_detalle { get; set; }

    }

}