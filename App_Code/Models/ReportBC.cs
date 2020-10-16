using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Descripción breve de CiudadBC
/// </summary>
namespace Ruteador.App_Code.Models
{

    public partial class ReportBC : Report
    {
        SqlTransaccion tran = new SqlTransaccion();


        public ReportBC()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

       public DataTable obrenerReporteDespachoViaje(string ids)
       {
           return tran.obrenerReporteDespachoViaje(ids);

       }



    }

    public partial class Report
    {
    }

        
}