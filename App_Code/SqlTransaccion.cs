using Qanalytics.Data.Access.SqlClient;
using Ruteador.App_Code.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace Ruteador.App_Code
{
    public class SqlTransaccion
    {
        SqlAccesoDatos data = new SqlAccesoDatos("CsString");
        #region Horario
        internal DataTable Horario_ObtenerTodo()
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_HORARIO]");
                return data.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        #endregion
        #region Menu
        internal DataSet Menu_ObtenerTodo(int usti_id)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_MENU]");
                if (usti_id != 0)
                    data.AgregarSqlParametro("@usti_id", usti_id);
                DataSet ds = data.EjecutarSqlquery3();
                ds.Tables[0].TableName = "MENU_PADRES";
                ds.Tables[1].TableName = "MENU_HIJOS";
                ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["MENU_ID"] };
                ds.Tables[1].PrimaryKey = new DataColumn[] { ds.Tables[1].Columns["MENU_ID"] };
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        #endregion
        #region Operacion
        internal DataTable Operacion_ObtenerTodo(int usua_id)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_OPERACION]");
                if (usua_id != 0)
                data.AgregarSqlParametro("@usua_id", usua_id);
                return data.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal OperacionBC Operacion_ObtenerXId(int oper_id)
        {
            OperacionBC o = new OperacionBC();
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_OPERACION]");
                    data.AgregarSqlParametro("@OPER_ID", oper_id);
                data.EjecutarSqlLector();
                while (data.SqlLectorDatos.Read())
                {
                    if (data.SqlLectorDatos["OPER_ID"] != DBNull.Value)
                        o.OPER_ID = Convert.ToInt32(data.SqlLectorDatos["OPER_ID"]);
                    if (data.SqlLectorDatos["OPER_NOMBRE"] != DBNull.Value)
                        o.OPER_NOMBRE = Convert.ToString(data.SqlLectorDatos["OPER_NOMBRE"]);
                }
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal List<OperacionBC> Operacion_ObtenerList(int usua_id)
        {

            try
            {
                List<OperacionBC> o = new List<OperacionBC>();
                data.CargarSqlComando("[dbo].[LISTAR_OPERACION]");
                if (usua_id != 0)
                    data.AgregarSqlParametro("@usua_id", usua_id);
                data.EjecutarSqlLector();
                while (data.SqlLectorDatos.Read())
                {
                    o.Add(new OperacionBC()
                    {
                        OPER_ID = Convert.ToInt32(data.SqlLectorDatos["OPER_ID"]),
                        OPER_NOMBRE = Convert.ToString(data.SqlLectorDatos["OPER_NOMBRE"])
                    });
                }
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        #endregion
        #region Origen
        internal DataTable Origen_ObtenerTodo(string orig_nombre, int regi_id, int ciud_id, int comu_id)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_ORIGEN]");
                if (!string.IsNullOrEmpty(orig_nombre))
                    data.AgregarSqlParametro("@orig_nombre", orig_nombre);
                if (regi_id != 0)
                    data.AgregarSqlParametro("@regi_id", regi_id);
                if (ciud_id != 0)
                    data.AgregarSqlParametro("@ciud_id", ciud_id);
                if (comu_id != 0)
                    data.AgregarSqlParametro("@comu_id", comu_id);
                return data.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal OrigenBC Origen_ObtenerXId(int origen_id)
        {
            OrigenBC o = new OrigenBC();
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_ORIGEN]");
                if (origen_id != 0)
                    data.AgregarSqlParametro("@origen_id", origen_id);
                data.EjecutarSqlLector();
                while (data.SqlLectorDatos.Read())
                {
                    if (data.SqlLectorDatos["ID"] != DBNull.Value)
                        o.ID = Convert.ToInt32(data.SqlLectorDatos["ID"]);
                    if (data.SqlLectorDatos["ID_PE"] != DBNull.Value)
                        o.ID_PE = Convert.ToString(data.SqlLectorDatos["ID_PE"]);
                    if (data.SqlLectorDatos["NOMBRE_PE"] != DBNull.Value)
                        o.NOMBRE_PE = Convert.ToString(data.SqlLectorDatos["NOMBRE_PE"]);
                    if (data.SqlLectorDatos["DIRECCION_PE"] != DBNull.Value)
                        o.DIRECCION_PE = Convert.ToString(data.SqlLectorDatos["DIRECCION_PE"]);
                    if (data.SqlLectorDatos["LAT_PE"] != DBNull.Value)
                        o.LAT_PE = Convert.ToDecimal(data.SqlLectorDatos["LAT_PE"]);
                    if (data.SqlLectorDatos["LON_PE"] != DBNull.Value)
                        o.LON_PE = Convert.ToDecimal(data.SqlLectorDatos["LON_PE"]);
                    if (data.SqlLectorDatos["RADIO_PE"] != DBNull.Value)
                        o.RADIO_PE = Convert.ToInt32(data.SqlLectorDatos["RADIO_PE"]);
                    if (data.SqlLectorDatos["IS_POLIGONO"] != DBNull.Value)
                        o.IS_POLIGONO = Convert.ToBoolean(data.SqlLectorDatos["IS_POLIGONO"]);
                    if (data.SqlLectorDatos["ID_OPE"] != DBNull.Value)
                        o.OPERACION.OPER_ID = Convert.ToInt32(data.SqlLectorDatos["ID_OPE"]);
                    if (data.SqlLectorDatos["FH_CREA"] != DBNull.Value)
                        o.FH_CREA = Convert.ToDateTime(data.SqlLectorDatos["FH_CREA"]);
                    if (data.SqlLectorDatos["FH_UPDATE"] != DBNull.Value)
                        o.FH_UPDATE = Convert.ToDateTime(data.SqlLectorDatos["FH_UPDATE"]);
                    if (data.SqlLectorDatos["COMU_ID"] != DBNull.Value)
                        o.COMUNA.COMU_ID = Convert.ToInt32(data.SqlLectorDatos["COMU_ID"]);
                    if (data.SqlLectorDatos["COMU_NOMBRE"] != DBNull.Value)
                        o.COMUNA.COMU_NOMBRE = Convert.ToString(data.SqlLectorDatos["COMU_NOMBRE"]);
                    if (data.SqlLectorDatos["CIUD_ID"] != DBNull.Value)
                        o.COMUNA.CIUDAD.CIUD_ID = Convert.ToInt32(data.SqlLectorDatos["CIUD_ID"]);
                    if (data.SqlLectorDatos["CIUD_NOMBRE"] != DBNull.Value)
                        o.COMUNA.CIUDAD.CIUD_NOMBRE = Convert.ToString(data.SqlLectorDatos["CIUD_NOMBRE"]);
                    if (data.SqlLectorDatos["REGI_ID"] != DBNull.Value)
                        o.COMUNA.CIUDAD.REGION.REGI_ID = Convert.ToInt32(data.SqlLectorDatos["REGI_ID"]);
                    if (data.SqlLectorDatos["REGI_NOMBRE"] != DBNull.Value)
                        o.COMUNA.CIUDAD.REGION.REGI_NOMBRE = Convert.ToString(data.SqlLectorDatos["REGI_NOMBRE"]);
                }
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }


        internal OrigenBC Origen_ObtenerXIdruta(int ruta_id)
        {
            OrigenBC o = new OrigenBC();
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_ORIGEN]");
                if (ruta_id != 0)
                    data.AgregarSqlParametro("@ruta_id", ruta_id);
                data.EjecutarSqlLector();
                while (data.SqlLectorDatos.Read())
                {
                    if (data.SqlLectorDatos["ID"] != DBNull.Value)
                        o.ID = Convert.ToInt32(data.SqlLectorDatos["ID"]);
                    if (data.SqlLectorDatos["ID_PE"] != DBNull.Value)
                        o.ID_PE = Convert.ToString(data.SqlLectorDatos["ID_PE"]);
                    if (data.SqlLectorDatos["NOMBRE_PE"] != DBNull.Value)
                        o.NOMBRE_PE = Convert.ToString(data.SqlLectorDatos["NOMBRE_PE"]);
                    if (data.SqlLectorDatos["DIRECCION_PE"] != DBNull.Value)
                        o.DIRECCION_PE = Convert.ToString(data.SqlLectorDatos["DIRECCION_PE"]);
                    if (data.SqlLectorDatos["LAT_PE"] != DBNull.Value)
                        o.LAT_PE = Convert.ToDecimal(data.SqlLectorDatos["LAT_PE"]);
                    if (data.SqlLectorDatos["LON_PE"] != DBNull.Value)
                        o.LON_PE = Convert.ToDecimal(data.SqlLectorDatos["LON_PE"]);
                    if (data.SqlLectorDatos["RADIO_PE"] != DBNull.Value)
                        o.RADIO_PE = Convert.ToInt32(data.SqlLectorDatos["RADIO_PE"]);
                    if (data.SqlLectorDatos["IS_POLIGONO"] != DBNull.Value)
                        o.IS_POLIGONO = Convert.ToBoolean(data.SqlLectorDatos["IS_POLIGONO"]);
                    if (data.SqlLectorDatos["ID_OPE"] != DBNull.Value)
                        o.OPERACION.OPER_ID = Convert.ToInt32(data.SqlLectorDatos["ID_OPE"]);
                    if (data.SqlLectorDatos["FH_CREA"] != DBNull.Value)
                        o.FH_CREA = Convert.ToDateTime(data.SqlLectorDatos["FH_CREA"]);
                    if (data.SqlLectorDatos["FH_UPDATE"] != DBNull.Value)
                        o.FH_UPDATE = Convert.ToDateTime(data.SqlLectorDatos["FH_UPDATE"]);
                    if (data.SqlLectorDatos["PERU_LLEGADA"] != DBNull.Value)
                        o.PERU_LLEGADA = data.SqlLectorDatos["PERU_LLEGADA"].ToString();
                    if (data.SqlLectorDatos["COMU_ID"] != DBNull.Value)
                        o.COMUNA.COMU_ID = Convert.ToInt32(data.SqlLectorDatos["COMU_ID"]);
                    if (data.SqlLectorDatos["COMU_NOMBRE"] != DBNull.Value)
                        o.COMUNA.COMU_NOMBRE = Convert.ToString(data.SqlLectorDatos["COMU_NOMBRE"]);
                    if (data.SqlLectorDatos["CIUD_ID"] != DBNull.Value)
                        o.COMUNA.CIUDAD.CIUD_ID = Convert.ToInt32(data.SqlLectorDatos["CIUD_ID"]);
                    if (data.SqlLectorDatos["CIUD_NOMBRE"] != DBNull.Value)
                        o.COMUNA.CIUDAD.CIUD_NOMBRE = Convert.ToString(data.SqlLectorDatos["CIUD_NOMBRE"]);
                    if (data.SqlLectorDatos["REGI_ID"] != DBNull.Value)
                        o.COMUNA.CIUDAD.REGION.REGI_ID = Convert.ToInt32(data.SqlLectorDatos["REGI_ID"]);
                    if (data.SqlLectorDatos["REGI_NOMBRE"] != DBNull.Value)
                        o.COMUNA.CIUDAD.REGION.REGI_NOMBRE = Convert.ToString(data.SqlLectorDatos["REGI_NOMBRE"]);
                }
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }

        internal bool Origen_Guardar(OrigenBC o)
        {
            try
            {
                data.CargarSqlComando("[dbo].[GUARDAR_ORIGEN]");
                if (o.ID != 0)
                    data.AgregarSqlParametro("@ID", o.ID);
                data.AgregarSqlParametro("@ID_PE", o.ID_PE);
                data.AgregarSqlParametro("@NOMBRE_PE", o.NOMBRE_PE);
                data.AgregarSqlParametro("@DIRECCION_PE", o.DIRECCION_PE);
                data.AgregarSqlParametro("@LAT_PE", o.LAT_PE);
                data.AgregarSqlParametro("@LON_PE", o.LON_PE);
                data.AgregarSqlParametro("@RADIO_PE", o.RADIO_PE);
                data.AgregarSqlParametro("@IS_POLIGONO", o.IS_POLIGONO);
                data.AgregarSqlParametro("@ID_OPE", o.OPERACION.OPER_ID);
                data.AgregarSqlParametro("@COMU_ID", o.COMUNA.COMU_ID);
                //data.AgregarSqlParametro("@ID_CLIENTE", o.ID_CLIENTE);
                //data.AgregarSqlParametro("@ID_MERCADO", o.ID_MERCADO);
                //data.AgregarSqlParametro("@ID_ZONA", o.ID_ZONA);
                //data.AgregarSqlParametro("@invalido", o.invalido);
                data.EjecutarSqlEscritura();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal bool Origen_Eliminar(int id)
        {
            try
            {
                data.CargarSqlComando("[dbo].[ELIMINAR_ORIGEN]");
                data.AgregarSqlParametro("@ID", id);
                data.EjecutarSqlEscritura();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        #endregion
        #region Parametro
        internal DataTable Puntos_ObtenerTodo(int id_tipo)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_PUNTO]");
                if (id_tipo != 0)
                    data.AgregarSqlParametro("@id_tipo", id_tipo);
                return data.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal PuntoEntregaBC Puntos_ObtenerXId(int id)
        {
            try
            {
                PuntoEntregaBC p = new PuntoEntregaBC();
                data.CargarSqlComando("[dbo].[LISTAR_PUNTO]");
                data.AgregarSqlParametro("@id", id);
                data.EjecutarSqlLector();
                while (data.SqlLectorDatos.Read())
                {
                    p.ID = Convert.ToInt32(data.SqlLectorDatos["ID"]);
                    p.NOMBRE_PE = Convert.ToString(data.SqlLectorDatos["NOMBRE_PE"]);
                    p.DIRECCION_PE = Convert.ToString(data.SqlLectorDatos["DIRECCION_PE"]);
                    p.LAT_PE = Convert.ToDecimal(data.SqlLectorDatos["LAT_PE"]);
                    p.LON_PE = Convert.ToDecimal(data.SqlLectorDatos["LON_PE"]);
                    p.IS_POLIGONO = Convert.ToBoolean(data.SqlLectorDatos["IS_POLIGONO"]);
                }
                return p;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal DataTable Puntos_ObtenerXRuta(int id_ruta)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_PUNTOS_RUTA]");
                data.AgregarSqlParametro("@id_ruta", id_ruta);
                return data.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal DataTable Puntos_ObtenerXPreRuta(int id_preruta)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_PUNTOS_PRE_RUTA]");
                data.AgregarSqlParametro("@id_ruta", id_preruta);
                return data.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        #endregion
        #region Pedido
        internal DataTable Pedido_IngresarExcel(DataTable dt, int usua_id)
        {
            try
            {
                data.CargarSqlComando("[dbo].[GUARDAR_INTEGRA_PEDIDOS]");
                data.AgregarSqlParametro("@PEDIDOS", dt);
                data.AgregarSqlParametro("@id_usuario", usua_id);
                return data.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal DataTable Pedido_ProcesarExcel(int usua_id)
        {
            DataTable dt;
            try
            {
                data.CargarSqlComando("[dbo].[PROCESAR_INTEGRA_PEDIDOS]");
                data.AgregarSqlParametro("@id_usuario", usua_id);
                dt=data.EjecutarSqlquery2();
  
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
            return dt;
        }
        internal bool Pedido_CrearEnvio(string gd, int usua_id, out int id)
        {



            try
            {
                data.CargarSqlComando("[dbo].[CREAR_ENVIO_RUTAS]");
                data.AgregarSqlParametro("@pedidos", gd);
                data.AgregarSqlParametro("@id_usuario", usua_id);
                data.EjecutaSqlInsertIdentity();
                id = data.ID;
               return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }





        }


        internal bool Pedido_CrearArchivo_integracion_old(int cliente_id , string archivo , string contenido , string nombre_comm)
        {


            SqlAccesoDatos data2 = new SqlAccesoDatos("Integra");
            try
            {
                data2.CargarSqlComando("[DBO].[RUTEADOR_Crea_Archivo_RoadShow]");
                data2.AgregarSqlParametro("@ID_CLIENTE", cliente_id);
                data2.AgregarSqlParametro("@RUTA_ARCHIVO", archivo);
                data2.AgregarSqlParametro("@DETALLE_ARCHIVO", contenido);
                if (nombre_comm!="")
                    data2.AgregarSqlParametro("@nombre_comm", nombre_comm);

                data2.EjecutarSqlEscritura();
                
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                data2.LimpiarSqlParametros();
                data2.CerrarSqlConeccion();
            }





        }

        internal bool Pedido_CrearArchivo_integracion_v2(int cliente_id, string cabecera, string detalle, string nombre_comm, int id_envio)
        {

            try
            {
                data.CargarSqlComando("[dbo].[VALIDAR_ENVIO_RUTA]");
                data.AgregarSqlParametro("@ID_ENVIO", id_envio);

                data.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }



            SqlAccesoDatos data2 = new SqlAccesoDatos("Integra");
            try
            {
                data2.CargarSqlComando("[DBO].[RUTEADOR_Crea_Archivo_RoadShow_V2]");
                data2.AgregarSqlParametro("@ID_CLIENTE", cliente_id);
                data2.AgregarSqlParametro("@cabecera_ARCHIVO", cabecera);
                data2.AgregarSqlParametro("@DETALLE_ARCHIVO", detalle);
                if (nombre_comm != "")
                    data2.AgregarSqlParametro("@nombre_comm", nombre_comm);

                data2.EjecutarSqlEscritura();

                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                data2.LimpiarSqlParametros();
                data2.CerrarSqlConeccion();
            }





        }
        internal DataTable Pedido_ObtenerTodo(DateTime desde, DateTime hasta, int hora_id, int regi_id, int ciud_id, int comu_id, int usua_id, string peru_numero, bool solo_sin_ruta, int id_ruta)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_PEDIDOS]");
                if (desde != DateTime.MinValue && hasta != DateTime.MinValue)
                {
                    data.AgregarSqlParametro("@fh_desde", desde);
                    data.AgregarSqlParametro("@fh_hasta", hasta);
                }
                if (usua_id != 0)
                    data.AgregarSqlParametro("@id_usuario", usua_id);
                if (regi_id != 0)
                    data.AgregarSqlParametro("@regi_id", regi_id);
                if (ciud_id != 0)
                    data.AgregarSqlParametro("@ciud_id", ciud_id);
                if (comu_id != 0)
                    data.AgregarSqlParametro("@comu_id", comu_id);
                if (hora_id != 0)
                    data.AgregarSqlParametro("@hora_id", hora_id);
                if (!string.IsNullOrEmpty(peru_numero))
                    data.AgregarSqlParametro("@peru_numero", peru_numero);
                if (solo_sin_ruta==true)
                    data.AgregarSqlParametro("@solo_sin_ruta", solo_sin_ruta);
                if (id_ruta != 0)
                    data.AgregarSqlParametro("@id_ruta", id_ruta);

                return data.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal PedidoBC Pedido_ObtenerXId(long peru_id)
        {
            try
            {
                PedidoBC p = new PedidoBC();
                data.CargarSqlComando("[dbo].[LISTAR_PEDIDOS]");
                data.AgregarSqlParametro("@peru_id", peru_id);
                data.EjecutarSqlLector();
                while (data.SqlLectorDatos.Read())
                {
                    if (data.SqlLectorDatos["PERU_ID"] != DBNull.Value)
                        p.PERU_ID = Convert.ToInt64(data.SqlLectorDatos["PERU_ID"]);
                    if (data.SqlLectorDatos["PERU_NUMERO"] != DBNull.Value)
                        p.PERU_NUMERO = Convert.ToString(data.SqlLectorDatos["PERU_NUMERO"]);
                    if (data.SqlLectorDatos["PERU_CODIGO"] != DBNull.Value)
                        p.PERU_CODIGO = Convert.ToString(data.SqlLectorDatos["PERU_CODIGO"]);
                    if (data.SqlLectorDatos["PERU_FECHA"] != DBNull.Value)
                        p.PERU_FECHA = Convert.ToDateTime(data.SqlLectorDatos["PERU_FECHA"]);
                    if (data.SqlLectorDatos["PERU_PESO"] != DBNull.Value)
                        p.PERU_PESO = Convert.ToString(data.SqlLectorDatos["PERU_PESO"]);
                    if (data.SqlLectorDatos["PERU_TIEMPO"] != DBNull.Value)
                        p.PERU_TIEMPO = Convert.ToString(data.SqlLectorDatos["PERU_TIEMPO"]);
                    if (data.SqlLectorDatos["PERU_DIRECCION"] != DBNull.Value)
                        p.PERU_DIRECCION = Convert.ToString(data.SqlLectorDatos["PERU_DIRECCION"]);
                    if (data.SqlLectorDatos["PERU_LATITUD"] != DBNull.Value)
                        p.PERU_LATITUD = Convert.ToDecimal(data.SqlLectorDatos["PERU_LATITUD"]);
                    if (data.SqlLectorDatos["PERU_LONGITUD"] != DBNull.Value)
                        p.PERU_LONGITUD = Convert.ToDecimal(data.SqlLectorDatos["PERU_LONGITUD"]);
                    if (data.SqlLectorDatos["HORA_ID"] != DBNull.Value)
                        p.HORA_SALIDA.HORA_ID = Convert.ToInt32(data.SqlLectorDatos["HORA_ID"]);
                    if (data.SqlLectorDatos["HORA_COD"] != DBNull.Value)
                        p.HORA_SALIDA.HORA_COD = Convert.ToString(data.SqlLectorDatos["HORA_COD"]);
                    if (data.SqlLectorDatos["PERU_USUA_ID"] != DBNull.Value)
                        p.USUARIO_PEDIDO.USUA_ID = Convert.ToInt32(data.SqlLectorDatos["PERU_USUA_ID"]);
                    if (data.SqlLectorDatos["PERU_ENVIADO_RUTEADOR"] != DBNull.Value)
                        p.PERU_ENVIADO_RUTEADOR = Convert.ToBoolean(data.SqlLectorDatos["PERU_ENVIADO_RUTEADOR"]);
                    if (data.SqlLectorDatos["PERU_FH_ENVIO"] != DBNull.Value)
                        p.PERU_FH_ENVIO = Convert.ToDateTime(data.SqlLectorDatos["PERU_FH_ENVIO"]);
                    if (data.SqlLectorDatos["USR_ENVIO"] != DBNull.Value)
                        p.USUARIO_ENVIO.USUA_ID = Convert.ToInt32(data.SqlLectorDatos["USR_ENVIO"]);
                    if (data.SqlLectorDatos["PERU_FH_CREACION"] != DBNull.Value)
                        p.PERU_FH_CREACION = Convert.ToDateTime(data.SqlLectorDatos["PERU_FH_CREACION"]);
                    if (data.SqlLectorDatos["COMU_ID"] != DBNull.Value)
                        p.COMUNA.COMU_ID = Convert.ToInt32(data.SqlLectorDatos["COMU_ID"]);
                    if (data.SqlLectorDatos["COMU_NOMBRE"] != DBNull.Value)
                        p.COMUNA.COMU_NOMBRE = Convert.ToString(data.SqlLectorDatos["COMU_NOMBRE"]);
                    if (data.SqlLectorDatos["CIUD_ID"] != DBNull.Value)
                        p.COMUNA.CIUDAD.CIUD_ID = Convert.ToInt32(data.SqlLectorDatos["CIUD_ID"]);
                    if (data.SqlLectorDatos["CIUD_NOMBRE"] != DBNull.Value)
                        p.COMUNA.CIUDAD.CIUD_NOMBRE = Convert.ToString(data.SqlLectorDatos["CIUD_NOMBRE"]);
                    if (data.SqlLectorDatos["REGI_ID"] != DBNull.Value)
                        p.COMUNA.CIUDAD.REGION.REGI_ID = Convert.ToInt32(data.SqlLectorDatos["REGI_ID"]);
                    if (data.SqlLectorDatos["REGI_NOMBRE"] != DBNull.Value)
                        p.COMUNA.CIUDAD.REGION.REGI_NOMBRE = Convert.ToString(data.SqlLectorDatos["REGI_NOMBRE"]);
                }
                return p;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal bool Pedido_Guardar(PedidoBC p)
        {
            try
            {
                data.CargarSqlComando("[dbo].[GUARDAR_PEDIDO_RUTA]"); 
                if (p.PERU_ID != 0)
                    data.AgregarSqlParametro("@PERU_ID", p.PERU_ID);
                data.AgregarSqlParametro("@PERU_NUMERO", p.PERU_NUMERO);
                data.AgregarSqlParametro("@PERU_CODIGO", p.PERU_CODIGO);
                data.AgregarSqlParametro("@PERU_FECHA", p.PERU_FECHA);
                data.AgregarSqlParametro("@PERU_PESO", p.PERU_PESO);
                data.AgregarSqlParametro("@PERU_TIEMPO", p.PERU_TIEMPO);
                data.AgregarSqlParametro("@PERU_DIRECCION", p.PERU_DIRECCION);
                data.AgregarSqlParametro("@PERU_LATITUD", p.PERU_LATITUD);
                data.AgregarSqlParametro("@PERU_LONGITUD", p.PERU_LONGITUD);
                data.AgregarSqlParametro("@HORA_ID", p.HORA_SALIDA.HORA_ID);
                data.AgregarSqlParametro("@COMU_ID", p.COMUNA.COMU_ID);
                data.AgregarSqlParametro("@PERU_USUA_ID", p.USUARIO_PEDIDO.USUA_ID);
                data.EjecutarSqlEscritura();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal bool Pedido_Eliminar(long peru_id)
        {
            try
            {
                data.CargarSqlComando("[dbo].[ELIMINAR_PEDIDO]");
                data.AgregarSqlParametro("@peru_id", peru_id);
                data.EjecutarSqlEscritura();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }

        internal bool PedidoModificarFechaAgendamiento(string ids, DateTime desde, int horario)
        {
            bool exito = false;

            data.CargarSqlComando("[dbo].[PEDIDOS_MODIFICAR_FECHA_MASIVA]");
            data.AgregarSqlParametro("@ID", ids);
            data.AgregarSqlParametro("@FECHA_DESDE", desde);
            if (horario>0) data.AgregarSqlParametro("@horario", horario);
            try
            {
                data.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return exito;
        }

        #endregion
        #region PuntoEntrega
        internal DataTable Parametro_ObtenerTodo(string para_nombre, string para_obs)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_PARAMETRO]");
                if (!string.IsNullOrEmpty(para_nombre))
                    data.AgregarSqlParametro("@para_nombre", para_nombre);
                if (!string.IsNullOrEmpty(para_obs))
                    data.AgregarSqlParametro("@para_obs", para_obs);
                return data.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal ParametroBC Parametro_ObtenerXId(int para_id)
        {
            try
            {
                ParametroBC p = new ParametroBC();
                data.CargarSqlComando("[dbo].[LISTAR_PARAMETRO]");
                data.AgregarSqlParametro("@PARA_ID", para_id);
                data.EjecutarSqlLector();
                while (data.SqlLectorDatos.Read())
                {
                    p.PARA_ID = Convert.ToInt32(data.SqlLectorDatos["PARA_ID"]);
                    p.PARA_NOMBRE = Convert.ToString(data.SqlLectorDatos["PARA_NOMBRE"]);
                    p.PARA_OBS = Convert.ToString(data.SqlLectorDatos["PARA_OBS"]);
                    p.PARA_VALOR = Convert.ToString(data.SqlLectorDatos["PARA_VALOR"]);
                    p.USUA_ID_CREACION = Convert.ToInt32(data.SqlLectorDatos["USUA_ID_CREACION"]);
                    p.PARA_FH_CREACION = Convert.ToDateTime(data.SqlLectorDatos["PARA_FH_CREACION"]);
                    if (data.SqlLectorDatos["USUA_ID_MODIFICACION"] != DBNull.Value)
                    {
                        p.USUA_ID_MODIFICACION = Convert.ToInt32(data.SqlLectorDatos["USUA_ID_MODIFICACION"]);
                        p.PARA_FH_MODIFICACION = Convert.ToDateTime(data.SqlLectorDatos["PARA_FH_MODIFICACION"]);
                    }
                }
                return p;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal bool Parametro_Guardar(ParametroBC p)
        {
            try
            {
                data.CargarSqlComando("[dbo].[GUARDAR_PARAMETRO]");
                if (p.PARA_ID != 0)
                    data.AgregarSqlParametro("@PARA_ID", p.PARA_ID);
                data.AgregarSqlParametro("@PARA_NOMBRE", p.PARA_NOMBRE);
                data.AgregarSqlParametro("@PARA_OBS", p.PARA_OBS);
                data.AgregarSqlParametro("@PARA_VALOR", p.PARA_VALOR);
                data.AgregarSqlParametro("@USUA_ID", p.USUA_ID_CREACION);
                data.EjecutarSqlEscritura();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal bool Parametro_Eliminar(int para_id)
        {
            try
            {
                data.CargarSqlComando("[dbo].[ELIMINAR_PARAMETRO]");
                data.AgregarSqlParametro("@PARA_ID", para_id);
                data.EjecutarSqlEscritura();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        #endregion
        #region Region/Ciudad/Comuna
        internal DataTable Region_ObtenerTodo()
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_REGION]");
                return data.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal RegionBC Region_ObtenerXId(int regi_id)
        {
            try
            {
                RegionBC r = new RegionBC();
                data.CargarSqlComando("[dbo].[LISTAR_REGION]");
                data.AgregarSqlParametro("@REGI_ID", regi_id);
                data.EjecutarSqlLector();
                while (data.SqlLectorDatos.Read())
                {
                    if (data.SqlLectorDatos["REGI_ID"] != DBNull.Value)
                        r.REGI_ID = Convert.ToInt32(data.SqlLectorDatos["REGI_ID"]);
                    if (data.SqlLectorDatos["REGI_NOMBRE"] != DBNull.Value)
                        r.REGI_NOMBRE = Convert.ToString(data.SqlLectorDatos["REGI_NOMBRE"]);
                    if (data.SqlLectorDatos["REGI_DESCRIPCION"] != DBNull.Value)
                        r.REGI_DESCRIPCION = Convert.ToString(data.SqlLectorDatos["REGI_DESCRIPCION"]);
                    if (data.SqlLectorDatos["REGI_ORDEN"] != DBNull.Value)
                        r.REGI_ORDEN = Convert.ToInt32(data.SqlLectorDatos["REGI_ORDEN"]);
                }
                return r;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal DataTable Ciudad_ObtenerTodo(int regi_id)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_CIUDAD]");
                if (regi_id != 0)
                    data.AgregarSqlParametro("@REGI_ID", regi_id);
                return data.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal CiudadBC Ciudad_ObtenerXId(int ciud_id)
        {
            try
            {
                CiudadBC c = new CiudadBC();
                data.CargarSqlComando("[dbo].[LISTAR_CIUDAD]");
                data.AgregarSqlParametro("@CIUD_ID", ciud_id);
                data.EjecutarSqlLector();
                while (data.SqlLectorDatos.Read())
                {
                    if (data.SqlLectorDatos["CIUD_ID"] != DBNull.Value)
                        c.CIUD_ID = Convert.ToInt32(data.SqlLectorDatos["CIUD_ID"]);
                    if (data.SqlLectorDatos["CIUD_NOMBRE"] != DBNull.Value)
                        c.CIUD_NOMBRE = Convert.ToString(data.SqlLectorDatos["CIUD_NOMBRE"]);
                    if (data.SqlLectorDatos["REGI_ID"] != DBNull.Value)
                        c.REGION.REGI_ID = Convert.ToInt32(data.SqlLectorDatos["REGI_ID"]);
                    if (data.SqlLectorDatos["REGI_NOMBRE"] != DBNull.Value)
                        c.REGION.REGI_NOMBRE = Convert.ToString(data.SqlLectorDatos["REGI_NOMBRE"]);
                }
                return c;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal DataTable Comuna_ObtenerTodo(int regi_id, int ciud_id)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_COMUNA]");
                if (regi_id != 0)
                    data.AgregarSqlParametro("@REGI_ID", regi_id);
                if (ciud_id != 0)
                    data.AgregarSqlParametro("@CIUD_ID", ciud_id);
                return data.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal ComunaBC Comuna_ObtenerXId(int comu_id)
        {
            try
            {
                ComunaBC c = new ComunaBC();
                data.CargarSqlComando("[dbo].[LISTAR_COMUNA]");
                data.AgregarSqlParametro("@COMU_ID", comu_id);
                data.EjecutarSqlLector();
                while (data.SqlLectorDatos.Read())
                {
                    if (data.SqlLectorDatos["COMU_ID"] != DBNull.Value)
                        c.COMU_ID = Convert.ToInt32(data.SqlLectorDatos["COMU_ID"]);
                    if (data.SqlLectorDatos["COMU_NOMBRE"] != DBNull.Value)
                        c.COMU_NOMBRE = Convert.ToString(data.SqlLectorDatos["COMU_NOMBRE"]);
                    if (data.SqlLectorDatos["CIUD_ID"] != DBNull.Value)
                        c.CIUDAD.CIUD_ID = Convert.ToInt32(data.SqlLectorDatos["CIUD_ID"]);
                    if (data.SqlLectorDatos["CIUD_NOMBRE"] != DBNull.Value)
                        c.CIUDAD.CIUD_NOMBRE = Convert.ToString(data.SqlLectorDatos["CIUD_NOMBRE"]);
                    if (data.SqlLectorDatos["REGI_ID"] != DBNull.Value)
                        c.CIUDAD.REGION.REGI_ID = Convert.ToInt32(data.SqlLectorDatos["REGI_ID"]);
                    if (data.SqlLectorDatos["REGI_NOMBRE"] != DBNull.Value)
                        c.CIUDAD.REGION.REGI_NOMBRE = Convert.ToString(data.SqlLectorDatos["REGI_NOMBRE"]);
                }
                return c;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        #endregion
        #region Ruta
        internal DataTable Ruta_ObtenerTodo()
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_RUTA]");
                return data.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal bool Ruta_Guardar(int id_ruta, string id_destinos)
        {
            try
            {
                data.CargarSqlComando("[dbo].[GUARDAR_PUNTOS_RUTA]");
                data.AgregarSqlParametro("@ID_RUTA", id_ruta);
                data.AgregarSqlParametro("@ID_DESTINOS", id_destinos);
                data.EjecutarSqlEscritura();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }

        internal bool Pre_ruta_Eliminar(long ruta_id)
        {
            try
            {
                data.CargarSqlComando("[dbo].[ELIMINAR_PRE_RUTA]");
                data.AgregarSqlParametro("@ruta_id", ruta_id);
                data.EjecutarSqlEscritura();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }

        internal string obtenerultimosprocesos()
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_ULTIMOS_PROCESOS_PRE_RUTA]");
               // data.AgregarSqlParametro("@ruta_id", ruta_id);
                return data.EjecutaSqlScalar();
                //return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        


        }
  

        #endregion
        #region Pre_Ruta

        internal DataTable  pre_ruta_CrearEnvio(string gd, int usua_id, bool archivar)
        {



            try
            {
                data.CargarSqlComando("[dbo].[CREAR_ENVIO_PRE_RUTAS]");
                data.AgregarSqlParametro("@pedidos", gd);
                data.AgregarSqlParametro("@id_usuario", usua_id);
                data.AgregarSqlParametro("@archivar", archivar);
             return    data.EjecutarSqlquery2();
//                id = data.ID;
//                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }

        }

        internal DataTable PreRuta_ObtenerTodo(DateTime desde, DateTime hasta, int hora_id = 0, int regi_id = 0, int ciud_id = 0, int comu_id = 0, int usua_id = 0, string peru_numero = null, string envio=null)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_PRERUTA]");
                if (desde != DateTime.MinValue && hasta != DateTime.MinValue)
                {
                    data.AgregarSqlParametro("@fh_desde", desde);
                    data.AgregarSqlParametro("@fh_hasta", hasta);
                }

                if (usua_id != 0)
                    data.AgregarSqlParametro("@id_usuario", usua_id);
                if (regi_id != 0)
                    data.AgregarSqlParametro("@regi_id", regi_id);
                if (ciud_id != 0)
                    data.AgregarSqlParametro("@ciud_id", ciud_id);
                if (comu_id != 0)
                    data.AgregarSqlParametro("@comu_id", comu_id);
                if (hora_id != 0)
                    data.AgregarSqlParametro("@hora_id", hora_id);
                if (!string.IsNullOrEmpty(peru_numero))
                             data.AgregarSqlParametro("@numero", peru_numero);
                if (!string.IsNullOrEmpty(envio))
                    data.AgregarSqlParametro("@envio", envio);
                   return data.EjecutarSqlquery2();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal bool PreRuta_Guardar(int id_ruta, string id_destinos, string tiempos, string hora_salida)
        {
            try
            {
                data.CargarSqlComando("[dbo].[GUARDAR_PUNTOS_PRE_RUTA]");
                if (@id_ruta!=0) data.AgregarSqlParametro("@ID_RUTA", id_ruta);

                data.AgregarSqlParametro("@ID_DESTINOS", id_destinos);
                data.AgregarSqlParametro("@id_tiempos", tiempos);
                data.AgregarSqlParametro("@hora_salida", hora_salida);
                data.EjecutarSqlEscritura();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }

        internal DataTable Pre_ruta_IngresarExcel(DataTable dt, int usua_id)
        {
            try
            {
                data.CargarSqlComando("[dbo].[GUARDAR_INTEGRA_RUTAS]");
                data.AgregarSqlParametro("@RUTAS", dt);
                data.AgregarSqlParametro("@id_usuario", usua_id);
                return data.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal DataTable Pre_ruta_ProcesarExcel(int usua_id)
        {
            DataTable dt;
            try
            {
                data.CargarSqlComando("[dbo].[PROCESAR_INTEGRA_PRE_RUTA]");
                data.AgregarSqlParametro("@id_usuario", usua_id);
                dt = data.EjecutarSqlquery2();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
            return dt;
        }

        #endregion
        #region Usuario
        internal DataTable Usuario_ObtenerTodo(bool usua_activos, int usti_id, string usua_rut, string usua_username, string usua_nombre, string usua_apellido)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_USUARIO]");
                data.AgregarSqlParametro("@USUA_ACTIVOS", usua_activos);
                if (usti_id != 0)
                    data.AgregarSqlParametro("@USTI_ID", usti_id);
                if (!string.IsNullOrEmpty(usua_rut))
                    data.AgregarSqlParametro("@USUA_RUT", usua_rut);
                if (!string.IsNullOrEmpty(usua_nombre))
                    data.AgregarSqlParametro("@USUA_NOMBRE", usua_nombre);
                if (!string.IsNullOrEmpty(usua_apellido))
                    data.AgregarSqlParametro("@USUA_APELLIDO", usua_apellido);
                if (!string.IsNullOrEmpty(usua_username))
                    data.AgregarSqlParametro("@USUA_USERNAME", usua_username);
                return data.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal DataTable Usuario_ObtenerTodo(int usti_id, string usua_rut, string usua_username, string usua_nombre, string usua_apellido)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_USUARIO]");
                if (usti_id != 0)
                    data.AgregarSqlParametro("@USTI_ID", usti_id);
                if (!string.IsNullOrEmpty(usua_rut))
                    data.AgregarSqlParametro("@USUA_RUT", usua_rut);
                if (!string.IsNullOrEmpty(usua_nombre))
                    data.AgregarSqlParametro("@USUA_NOMBRE", usua_nombre);
                if (!string.IsNullOrEmpty(usua_apellido))
                    data.AgregarSqlParametro("@USUA_APELLIDO", usua_apellido);
                if (!string.IsNullOrEmpty(usua_username))
                    data.AgregarSqlParametro("@USUA_USERNAME", usua_username);
                return data.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal UsuarioBC Usuario_ObtenerXId(int usua_id)
        {
            try
            {
                UsuarioBC u = new UsuarioBC();
                data.CargarSqlComando("[dbo].[LISTAR_USUARIO]");
                data.AgregarSqlParametro("@USUA_ID", usua_id);
                data.EjecutarSqlLector();
                while (data.SqlLectorDatos.Read())
                {
                    if (data.SqlLectorDatos["USUA_ID"] != DBNull.Value)
                        u.USUA_ID = Convert.ToInt32(data.SqlLectorDatos["USUA_ID"]);
                    if (data.SqlLectorDatos["USUA_COD"] != DBNull.Value)
                        u.USUA_COD = Convert.ToString(data.SqlLectorDatos["USUA_COD"]);
                    if (data.SqlLectorDatos["USUA_DESC"] != DBNull.Value)
                        u.USUA_DESC = Convert.ToString(data.SqlLectorDatos["USUA_DESC"]);
                    if (data.SqlLectorDatos["USUA_NOMBRE"] != DBNull.Value)
                        u.USUA_NOMBRE = Convert.ToString(data.SqlLectorDatos["USUA_NOMBRE"]);
                    if (data.SqlLectorDatos["USUA_APELLIDO"] != DBNull.Value)
                        u.USUA_APELLIDO = Convert.ToString(data.SqlLectorDatos["USUA_APELLIDO"]);
                    if (data.SqlLectorDatos["USUA_RUT"] != DBNull.Value)
                        u.USUA_RUT = Convert.ToString(data.SqlLectorDatos["USUA_RUT"]);
                    if (data.SqlLectorDatos["USUA_CORREO"] != DBNull.Value)
                        u.USUA_CORREO = Convert.ToString(data.SqlLectorDatos["USUA_CORREO"]);
                    if (data.SqlLectorDatos["USUA_USERNAME"] != DBNull.Value)
                        u.USUA_USERNAME = Convert.ToString(data.SqlLectorDatos["USUA_USERNAME"]);
                    if (data.SqlLectorDatos["USUA_PASSWORD"] != DBNull.Value)
                        u.USUA_PASSWORD = new FuncionesGenerales().Desencriptar(Convert.ToString(data.SqlLectorDatos["USUA_PASSWORD"]), u.USUA_USERNAME.ToLower());
                    if (data.SqlLectorDatos["USUA_ESTADO"] != DBNull.Value)
                        u.USUA_ESTADO = Convert.ToBoolean(data.SqlLectorDatos["USUA_ESTADO"]);
                    if (data.SqlLectorDatos["USUA_OBSERVACION"] != DBNull.Value)
                        u.USUA_OBSERVACION = Convert.ToString(data.SqlLectorDatos["USUA_OBSERVACION"]);
                    if (data.SqlLectorDatos["USTI_ID"] != DBNull.Value)
                        u.USUARIO_TIPO.USTI_ID = Convert.ToInt32(data.SqlLectorDatos["USTI_ID"]);
                    if (data.SqlLectorDatos["USTI_NOMBRE"] != DBNull.Value)
                        u.USUARIO_TIPO.USTI_NOMBRE = Convert.ToString(data.SqlLectorDatos["USTI_NOMBRE"]);
                    if (data.SqlLectorDatos["USTI_DESC"] != DBNull.Value)
                        u.USUARIO_TIPO.USTI_DESC = Convert.ToString(data.SqlLectorDatos["USTI_DESC"]);
                    if (data.SqlLectorDatos["USTI_NIVEL_PERMISOS"] != DBNull.Value)
                        u.USUARIO_TIPO.USTI_NIVEL_PERMISOS = Convert.ToInt32(data.SqlLectorDatos["USTI_NIVEL_PERMISOS"]);
                }
                return u;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal UsuarioBC Usuario_ObtenerXRut(string rut)
        {
            try
            {
                UsuarioBC u = new UsuarioBC();
                data.CargarSqlComando("[dbo].[LISTAR_USUARIO_X_RUT]");
                data.AgregarSqlParametro("@USUA_RUT", rut);
                data.EjecutarSqlLector();
                while (data.SqlLectorDatos.Read())
                {
                    if (data.SqlLectorDatos["USUA_ID"] != DBNull.Value)
                        u.USUA_ID = Convert.ToInt32(data.SqlLectorDatos["USUA_ID"]);
                    if (data.SqlLectorDatos["USUA_COD"] != DBNull.Value)
                        u.USUA_COD = Convert.ToString(data.SqlLectorDatos["USUA_COD"]);
                    if (data.SqlLectorDatos["USUA_DESC"] != DBNull.Value)
                        u.USUA_DESC = Convert.ToString(data.SqlLectorDatos["USUA_DESC"]);
                    if (data.SqlLectorDatos["USUA_NOMBRE"] != DBNull.Value)
                        u.USUA_NOMBRE = Convert.ToString(data.SqlLectorDatos["USUA_NOMBRE"]);
                    if (data.SqlLectorDatos["USUA_APELLIDO"] != DBNull.Value)
                        u.USUA_APELLIDO = Convert.ToString(data.SqlLectorDatos["USUA_APELLIDO"]);
                    if (data.SqlLectorDatos["USUA_RUT"] != DBNull.Value)
                        u.USUA_RUT = Convert.ToString(data.SqlLectorDatos["USUA_RUT"]);
                    if (data.SqlLectorDatos["USUA_CORREO"] != DBNull.Value)
                        u.USUA_CORREO = Convert.ToString(data.SqlLectorDatos["USUA_CORREO"]);
                    if (data.SqlLectorDatos["USUA_USERNAME"] != DBNull.Value)
                        u.USUA_USERNAME = Convert.ToString(data.SqlLectorDatos["USUA_USERNAME"]);
                    if (data.SqlLectorDatos["USUA_PASSWORD"] != DBNull.Value)
                        u.USUA_PASSWORD = new FuncionesGenerales().Desencriptar(Convert.ToString(data.SqlLectorDatos["USUA_PASSWORD"]), u.USUA_USERNAME.ToLower());
                    if (data.SqlLectorDatos["USUA_ESTADO"] != DBNull.Value)
                        u.USUA_ESTADO = Convert.ToBoolean(data.SqlLectorDatos["USUA_ESTADO"]);
                    if (data.SqlLectorDatos["USUA_OBSERVACION"] != DBNull.Value)
                        u.USUA_OBSERVACION = Convert.ToString(data.SqlLectorDatos["USUA_OBSERVACION"]);
                    if (data.SqlLectorDatos["USTI_ID"] != DBNull.Value)
                        u.USUARIO_TIPO.USTI_ID = Convert.ToInt32(data.SqlLectorDatos["USTI_ID"]);
                    if (data.SqlLectorDatos["USTI_NOMBRE"] != DBNull.Value)
                        u.USUARIO_TIPO.USTI_NOMBRE = Convert.ToString(data.SqlLectorDatos["USTI_NOMBRE"]);
                    if (data.SqlLectorDatos["USTI_DESC"] != DBNull.Value)
                        u.USUARIO_TIPO.USTI_DESC = Convert.ToString(data.SqlLectorDatos["USTI_DESC"]);
                    if (data.SqlLectorDatos["USTI_NIVEL_PERMISOS"] != DBNull.Value)
                        u.USUARIO_TIPO.USTI_NIVEL_PERMISOS = Convert.ToInt32(data.SqlLectorDatos["USTI_NIVEL_PERMISOS"]);
                }
                return u;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal UsuarioBC Usuario_Login(string usua_username, string usua_password)
        {
            try
            {
                UsuarioBC u = new UsuarioBC();
                data.CargarSqlComando("[dbo].[LOGIN_USUARIO]");
                data.AgregarSqlParametro("@USUA_USERNAME", usua_username.ToLower());
                data.AgregarSqlParametro("@USUA_PASSWORD", new FuncionesGenerales().Encriptar(usua_password, usua_username.ToLower()));
                data.EjecutarSqlLector();
                while (data.SqlLectorDatos.Read())
                {
                    if (data.SqlLectorDatos["USUA_ID"] != DBNull.Value)
                        u.USUA_ID = Convert.ToInt32(data.SqlLectorDatos["USUA_ID"]);
                    if (data.SqlLectorDatos["USUA_COD"] != DBNull.Value)
                        u.USUA_COD = Convert.ToString(data.SqlLectorDatos["USUA_COD"]);
                    if (data.SqlLectorDatos["USUA_DESC"] != DBNull.Value)
                        u.USUA_DESC = Convert.ToString(data.SqlLectorDatos["USUA_DESC"]);
                    if (data.SqlLectorDatos["USUA_NOMBRE"] != DBNull.Value)
                        u.USUA_NOMBRE = Convert.ToString(data.SqlLectorDatos["USUA_NOMBRE"]);
                    if (data.SqlLectorDatos["USUA_APELLIDO"] != DBNull.Value)
                        u.USUA_APELLIDO = Convert.ToString(data.SqlLectorDatos["USUA_APELLIDO"]);
                    if (data.SqlLectorDatos["USUA_RUT"] != DBNull.Value)
                        u.USUA_RUT = Convert.ToString(data.SqlLectorDatos["USUA_RUT"]);
                    if (data.SqlLectorDatos["USUA_CORREO"] != DBNull.Value)
                        u.USUA_CORREO = Convert.ToString(data.SqlLectorDatos["USUA_CORREO"]);
                    if (data.SqlLectorDatos["USUA_USERNAME"] != DBNull.Value)
                        u.USUA_USERNAME = Convert.ToString(data.SqlLectorDatos["USUA_USERNAME"]);
                    if (data.SqlLectorDatos["USUA_ESTADO"] != DBNull.Value)
                        u.USUA_ESTADO = Convert.ToBoolean(data.SqlLectorDatos["USUA_ESTADO"]);
                    if (data.SqlLectorDatos["USUA_OBSERVACION"] != DBNull.Value)
                        u.USUA_OBSERVACION = Convert.ToString(data.SqlLectorDatos["USUA_OBSERVACION"]);
                    if (data.SqlLectorDatos["USTI_ID"] != DBNull.Value)
                        u.USUARIO_TIPO.USTI_ID = Convert.ToInt32(data.SqlLectorDatos["USTI_ID"]);
                    if (data.SqlLectorDatos["USTI_NOMBRE"] != DBNull.Value)
                        u.USUARIO_TIPO.USTI_NOMBRE = Convert.ToString(data.SqlLectorDatos["USTI_NOMBRE"]);
                    if (data.SqlLectorDatos["USTI_DESC"] != DBNull.Value)
                        u.USUARIO_TIPO.USTI_DESC = Convert.ToString(data.SqlLectorDatos["USTI_DESC"]);
                    if (data.SqlLectorDatos["USTI_NIVEL_PERMISOS"] != DBNull.Value)
                        u.USUARIO_TIPO.USTI_NIVEL_PERMISOS = Convert.ToInt32(data.SqlLectorDatos["USTI_NIVEL_PERMISOS"]);
                    if (data.SqlLectorDatos["OPERACION"] != DBNull.Value)
                        u.OPER_ID = Convert.ToString(data.SqlLectorDatos["OPERACION"]);
                }
                return u;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal bool Usuario_Guardar(UsuarioBC u)
        {
            try
            {
                data.CargarSqlComando("[dbo].[GUARDAR_USUARIO]");
                if (u.USUA_ID != 0)
                    data.AgregarSqlParametro("@USUA_ID", u.USUA_ID);
                else
                    data.AgregarSqlParametro("@USUA_RUT", u.USUA_RUT);
                data.AgregarSqlParametro("@USTI_ID", u.USUARIO_TIPO.USTI_ID);
                data.AgregarSqlParametro("@USUA_NOMBRE", u.USUA_NOMBRE);
                data.AgregarSqlParametro("@USUA_APELLIDO", u.USUA_APELLIDO);
                data.AgregarSqlParametro("@USUA_CORREO", u.USUA_CORREO);    
                data.AgregarSqlParametro("@USUA_USERNAME", u.USUA_USERNAME.ToLower());
                data.AgregarSqlParametro("@USUA_OBSERVACION", u.USUA_OBSERVACION);
                data.AgregarSqlParametro("@USUA_PASSWORD", new FuncionesGenerales().Encriptar(u.USUA_PASSWORD,u.USUA_USERNAME.ToLower()));
                data.AgregarSqlParametro("@OPER_ID", u.OPER_ID);
                data.EjecutarSqlEscritura();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal bool Usuario_Eliminar(int usua_id)
        {
            try
            {
                data.CargarSqlComando("[dbo].[ELIMINAR_USUARIO]");
                data.AgregarSqlParametro("@USUA_ID", usua_id);
                data.EjecutarSqlEscritura();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal bool Usuario_Activar(int usua_id)
        {
            try
            {
                data.CargarSqlComando("[dbo].[ACTIVAR_USUARIO]");
                data.AgregarSqlParametro("@USUA_ID", usua_id);
                data.EjecutarSqlEscritura();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        #endregion
        #region UsuarioTipo
        internal DataTable UsuarioTipo_ObtenerTodo(string usti_nombre)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_USUARIO_TIPO]");
                if (!string.IsNullOrEmpty(usti_nombre))
                    data.AgregarSqlParametro("@USTI_NOMBRE", usti_nombre);
                return data.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal UsuarioTipoBC UsuarioTipo_ObtenerXId(int usti_id)
        {
            try
            {
                UsuarioTipoBC u = new UsuarioTipoBC();
                data.CargarSqlComando("[dbo].[LISTAR_USUARIO_TIPO]");
                data.AgregarSqlParametro("@USTI_ID", usti_id);
                data.EjecutarSqlLector();
                while (data.SqlLectorDatos.Read())
                {
                    u.USTI_ID = Convert.ToInt32(data.SqlLectorDatos["USTI_ID"]);
                    u.USTI_NOMBRE = Convert.ToString(data.SqlLectorDatos["USTI_NOMBRE"]);
                    u.USTI_DESC = Convert.ToString(data.SqlLectorDatos["USTI_DESC"]);
                    u.USTI_NIVEL_PERMISOS = Convert.ToInt32(data.SqlLectorDatos["USTI_NIVEL_PERMISOS"]);
                    u.MENU_ID = Convert.ToString(data.SqlLectorDatos["MENU_ID"]);
                }
                return u;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal bool UsuarioTipo_Guardar(UsuarioTipoBC ut)
        {
            try
            {
                data.CargarSqlComando("[dbo].[GUARDAR_USUARIO_TIPO]");
                if (ut.USTI_ID != 0)
                    data.AgregarSqlParametro("@USTI_ID", ut.USTI_ID);
                data.AgregarSqlParametro("@MENU_ID", ut.MENU_ID);
                data.AgregarSqlParametro("@USTI_NOMBRE", ut.USTI_NOMBRE);
                data.AgregarSqlParametro("@USTI_DESC", ut.USTI_DESC);
                data.AgregarSqlParametro("@USTI_NIVEL_PERMISOS", ut.USTI_NIVEL_PERMISOS);
                data.EjecutarSqlEscritura();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        internal bool UsuarioTipo_Eliminar(int usti_id)
        {
            try
            {
                data.CargarSqlComando("[dbo].[ELIMINAR_USUARIO_TIPO]");
                data.AgregarSqlParametro("@USTI_ID", usti_id);
                data.EjecutarSqlEscritura();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
        #endregion

        #region envio

        internal DataTable envio_detalle(int id)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_envio_detalle]");
                //if (desde != DateTime.MinValue && hasta != DateTime.MinValue)
                //{
                //    data.AgregarSqlParametro("@fh_desde", desde);
                //    data.AgregarSqlParametro("@fh_hasta", hasta);
                //}
                //if (usua_id != 0)
                //    data.AgregarSqlParametro("@id_usuario", usua_id);
                //if (regi_id != 0)
                //    data.AgregarSqlParametro("@regi_id", regi_id);
                //if (ciud_id != 0)
                //    data.AgregarSqlParametro("@ciud_id", ciud_id);
                //if (comu_id != 0)
                //    data.AgregarSqlParametro("@comu_id", comu_id);
                //if (hora_id != 0)
                //    data.AgregarSqlParametro("@hora_id", hora_id);
                //if (!string.IsNullOrEmpty(peru_numero))
                data.AgregarSqlParametro("@enru_id", id);
                return data.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }

        internal bool envio_Eliminar(long env_id)
        {
            try
            {
                data.CargarSqlComando("[dbo].[ELIMINAR_ENVIO]");
                data.AgregarSqlParametro("@env_id", env_id);
                data.EjecutarSqlEscritura();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }

        internal DataTable Envio_ObtenerTodo() //(DateTime desde, DateTime hasta, int hora_id, int regi_id, int ciud_id, int comu_id, int usua_id, string peru_numero)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_ENVIOS]");
                //if (desde != DateTime.MinValue && hasta != DateTime.MinValue)
                //{
                //    data.AgregarSqlParametro("@fh_desde", desde);
                //    data.AgregarSqlParametro("@fh_hasta", hasta);
                //}
                //if (usua_id != 0)
                //    data.AgregarSqlParametro("@id_usuario", usua_id);
                //if (regi_id != 0)
                //    data.AgregarSqlParametro("@regi_id", regi_id);
                //if (ciud_id != 0)
                //    data.AgregarSqlParametro("@ciud_id", ciud_id);
                //if (comu_id != 0)
                //    data.AgregarSqlParametro("@comu_id", comu_id);
                //if (hora_id != 0)
                //    data.AgregarSqlParametro("@hora_id", hora_id);
                //if (!string.IsNullOrEmpty(peru_numero))
                //    data.AgregarSqlParametro("@peru_numero", peru_numero);
                return data.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                data.LimpiarSqlParametros();
                data.CerrarSqlConeccion();
            }
        }
   

        #endregion 

            
    }
}