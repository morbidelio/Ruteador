using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ConductorBC
/// </summary>
public partial class ConductorBC : Conductor
{
    public ConductorBC()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
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