using System;


namespace Qanalytics.Data.Access
{
    internal sealed class Configuracion
    {
        public static String CadenaConeccion(String nombreConeccion)
        {
            String cadenaConeccion;

            try
            {
                cadenaConeccion = System.Configuration.ConfigurationManager.ConnectionStrings[nombreConeccion].ToString();
            }
            catch (Exception excepcion)
            {
                throw (excepcion);
            }
            return cadenaConeccion;
        }
    }
}
