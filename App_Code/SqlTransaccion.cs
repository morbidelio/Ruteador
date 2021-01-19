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
        #region Conductor
        internal DataTable Conductor_ObtenerTodo(DateTime fecha, int hora_id, bool cond_activo, bool cond_bloqueado, string cond_rut, string cond_nombre)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_CONDUCTOR]");
                if (fecha != DateTime.MinValue)
                {
                    data.AgregarSqlParametro("@fecha", fecha);
                    data.AgregarSqlParametro("@hora_id", hora_id);
                }
                data.AgregarSqlParametro("@cond_bloqueado", cond_bloqueado);
                data.AgregarSqlParametro("@cond_activo", cond_activo);
                if (!string.IsNullOrEmpty(cond_rut))
                    data.AgregarSqlParametro("@cond_rut", cond_rut);
                if (!string.IsNullOrEmpty(cond_nombre))
                    data.AgregarSqlParametro("@cond_nombre", cond_nombre);
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
        internal DataTable Conductor_ObtenerTodo(DateTime fecha, int hora_id, string cond_rut, string cond_nombre)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_CONDUCTOR]");
                if (fecha != DateTime.MinValue)
                {
                    data.AgregarSqlParametro("@fecha", fecha);
                    data.AgregarSqlParametro("@hora_id", hora_id);
                }
                if (!string.IsNullOrEmpty(cond_rut))
                    data.AgregarSqlParametro("@cond_rut", cond_rut);
                if (!string.IsNullOrEmpty(cond_nombre))
                    data.AgregarSqlParametro("@cond_nombre", cond_nombre);
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
        internal ConductorBC Conductor_ObtenerXId(int cond_id)
        {
            try
            {
                ConductorBC c = new ConductorBC();
                data.CargarSqlComando("[dbo].[LISTAR_CONDUCTOR]");
                data.AgregarSqlParametro("@cond_id", cond_id);
                data.EjecutarSqlLector();
                while (data.SqlLectorDatos.Read())
                {
                    if (data.SqlLectorDatos["COND_ID"] != DBNull.Value)
                        c.COND_ID = Convert.ToInt32(data.SqlLectorDatos["COND_ID"]);
                    if (data.SqlLectorDatos["COND_RUT"] != DBNull.Value)
                        c.COND_RUT = Convert.ToString(data.SqlLectorDatos["COND_RUT"]);
                    if (data.SqlLectorDatos["COND_IMAGEN"] != DBNull.Value)
                        c.COND_IMAGEN = Convert.ToString(data.SqlLectorDatos["COND_IMAGEN"]);
                    if (data.SqlLectorDatos["COND_NOMBRE"] != DBNull.Value)
                        c.COND_NOMBRE = Convert.ToString(data.SqlLectorDatos["COND_NOMBRE"]);
                    if (data.SqlLectorDatos["COND_ACTIVO"] != DBNull.Value)
                        c.COND_ACTIVO = Convert.ToBoolean(data.SqlLectorDatos["COND_ACTIVO"]);
                    if (data.SqlLectorDatos["COND_BLOQUEADO"] != DBNull.Value)
                        c.COND_BLOQUEADO = Convert.ToBoolean(data.SqlLectorDatos["COND_BLOQUEADO"]);
                    if (data.SqlLectorDatos["COND_TELEFONO"] != DBNull.Value)
                        c.COND_TELEFONO = Convert.ToString(data.SqlLectorDatos["COND_TELEFONO"]);
                    if (data.SqlLectorDatos["COND_MOTIVO_BLOQUEO"] != DBNull.Value)
                        c.COND_MOTIVO_BLOQUEO = Convert.ToString(data.SqlLectorDatos["COND_MOTIVO_BLOQUEO"]);
                    if (data.SqlLectorDatos["COND_EXTRANJERO"] != DBNull.Value)
                        c.COND_EXTRANJERO = Convert.ToBoolean(data.SqlLectorDatos["COND_EXTRANJERO"]);
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
        internal ConductorBC Conductor_ObtenerXRut(string cond_rut)
        {
            try
            {
                ConductorBC u = new ConductorBC();
                data.CargarSqlComando("[dbo].[LISTAR_CONDUCTOR_X_RUT]");
                data.AgregarSqlParametro("@COND_RUT", cond_rut);
                data.EjecutarSqlLector();
                while (data.SqlLectorDatos.Read())
                {
                    if (data.SqlLectorDatos["COND_ID"] != DBNull.Value)
                        u.COND_ID = Convert.ToInt32(data.SqlLectorDatos["COND_ID"]);
                    if (data.SqlLectorDatos["COND_RUT"] != DBNull.Value)
                        u.COND_RUT = Convert.ToString(data.SqlLectorDatos["COND_RUT"]);
                    if (data.SqlLectorDatos["COND_IMAGEN"] != DBNull.Value)
                        u.COND_IMAGEN = Convert.ToString(data.SqlLectorDatos["COND_IMAGEN"]);
                    if (data.SqlLectorDatos["COND_NOMBRE"] != DBNull.Value)
                        u.COND_NOMBRE = Convert.ToString(data.SqlLectorDatos["COND_NOMBRE"]);
                    if (data.SqlLectorDatos["COND_ACTIVO"] != DBNull.Value)
                        u.COND_ACTIVO = Convert.ToBoolean(data.SqlLectorDatos["COND_ACTIVO"]);
                    if (data.SqlLectorDatos["COND_BLOQUEADO"] != DBNull.Value)
                        u.COND_BLOQUEADO = Convert.ToBoolean(data.SqlLectorDatos["COND_BLOQUEADO"]);
                    if (data.SqlLectorDatos["COND_TELEFONO"] != DBNull.Value)
                        u.COND_TELEFONO = Convert.ToString(data.SqlLectorDatos["COND_TELEFONO"]);
                    if (data.SqlLectorDatos["COND_MOTIVO_BLOQUEO"] != DBNull.Value)
                        u.COND_MOTIVO_BLOQUEO = Convert.ToString(data.SqlLectorDatos["COND_MOTIVO_BLOQUEO"]);
                    if (data.SqlLectorDatos["COND_EXTRANJERO"] != DBNull.Value)
                        u.COND_EXTRANJERO = Convert.ToBoolean(data.SqlLectorDatos["COND_EXTRANJERO"]);
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
        internal bool Conductor_Guardar(ConductorBC c)
        {
            try
            {
                data.CargarSqlComando("[dbo].[GUARDAR_CONDUCTOR]");
                if (c.COND_ID != 0)
                    data.AgregarSqlParametro("@COND_ID", c.COND_ID);
                else
                    data.AgregarSqlParametro("@COND_RUT", c.COND_RUT);
                data.AgregarSqlParametro("@COND_IMAGEN", c.COND_IMAGEN);
                data.AgregarSqlParametro("@COND_NOMBRE", c.COND_NOMBRE);
                data.AgregarSqlParametro("@COND_TELEFONO", c.COND_TELEFONO);
                data.AgregarSqlParametro("@COND_EXTRANJERO", c.COND_EXTRANJERO);
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
        internal bool Conductor_Eliminar(int cond_id)
        {
            try
            {
                data.CargarSqlComando("[dbo].[ELIMINAR_CONDUCTOR]");
                data.AgregarSqlParametro("@COND_ID", cond_id);
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
        internal bool Conductor_Activar(int cond_id)
        {
            try
            {
                data.CargarSqlComando("[dbo].[ACTIVAR_CONDUCTOR]");
                data.AgregarSqlParametro("@COND_ID", cond_id);
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
        internal bool Conductor_Bloquear(int cond_id, string cond_motivo_bloqueo)
        {
            try
            {
                data.CargarSqlComando("[dbo].[BLOQUEAR_CONDUCTOR]");
                data.AgregarSqlParametro("@COND_ID", cond_id);
                if (!string.IsNullOrEmpty(cond_motivo_bloqueo))
                    data.AgregarSqlParametro("@cond_motivo_bloqueo", cond_motivo_bloqueo);
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
        #region Icono
        internal DataTable Icono_ObtenerTodo()
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_ICONO]");
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
        internal IconoBC Icono_ObtenerXId(int icon_id)
        {
            try
            {
                IconoBC i = new IconoBC();
                data.CargarSqlComando("[dbo].[LISTAR_ICONO]");
                data.AgregarSqlParametro("@icon_id", icon_id);
                data.EjecutarSqlLector();
                while (data.SqlLectorDatos.Read()) 
                {
                    if (data.SqlLectorDatos["ICON_ID"] != DBNull.Value)
                        i.ICON_ID = Convert.ToInt32(data.SqlLectorDatos["ICON_ID"]);
                    if (data.SqlLectorDatos["ICON_URL"] != DBNull.Value)
                        i.ICON_URL = Convert.ToString(data.SqlLectorDatos["ICON_URL"]);
                }
                return i;
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
        internal List<OrigenBC> Origen_ObtenerArray(string orig_nombre, int regi_id, int ciud_id, int comu_id)
        {
            try
            {
                List<OrigenBC> listado = new List<OrigenBC>();
                data.CargarSqlComando("[dbo].[LISTAR_ORIGEN]");
                if (!string.IsNullOrEmpty(orig_nombre))
                    data.AgregarSqlParametro("@orig_nombre", orig_nombre);
                if (regi_id != 0)
                    data.AgregarSqlParametro("@regi_id", regi_id);
                if (ciud_id != 0)
                    data.AgregarSqlParametro("@ciud_id", ciud_id);
                if (comu_id != 0)
                    data.AgregarSqlParametro("@comu_id", comu_id);
                data.EjecutarSqlLector();
                while (data.SqlLectorDatos.Read())
                {
                    OrigenBC o = new OrigenBC();
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
                    if (data.SqlLectorDatos["ICON_ID"] != DBNull.Value)
                        o.ICONO.ICON_ID = Convert.ToInt32(data.SqlLectorDatos["ICON_ID"]);
                    if (data.SqlLectorDatos["ICON_URL"] != DBNull.Value)
                        o.ICONO.ICON_URL = Convert.ToString(data.SqlLectorDatos["ICON_URL"]);
                    listado.Add(o);
                }
                return listado;
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
                    if (data.SqlLectorDatos["ICON_ID"] != DBNull.Value)
                        o.ICONO.ICON_ID = Convert.ToInt32(data.SqlLectorDatos["ICON_ID"]);
                    if (data.SqlLectorDatos["ICON_URL"] != DBNull.Value)
                        o.ICONO.ICON_URL = Convert.ToString(data.SqlLectorDatos["ICON_URL"]);
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
                    if (data.SqlLectorDatos["ICON_ID"] != DBNull.Value)
                        o.ICONO.ICON_ID = Convert.ToInt32(data.SqlLectorDatos["ICON_ID"]);
                    if (data.SqlLectorDatos["ICON_URL"] != DBNull.Value)
                        o.ICONO.ICON_URL = Convert.ToString(data.SqlLectorDatos["ICON_URL"]);
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
                if (o.ICONO.ICON_ID != 0)
                    data.AgregarSqlParametro("@ICON_ID", o.ICONO.ICON_ID);
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
        internal DataTable Puntos_ObtenerXPreRuta(long id_preruta)
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
        internal bool Pedido_CrearArchivo_integracion_old(int cliente_id, string archivo, string contenido, string nombre_comm)
        {


            SqlAccesoDatos data2 = new SqlAccesoDatos("Integra");
            try
            {
                data2.CargarSqlComando("[DBO].[RUTEADOR_Crea_Archivo_RoadShow]");
                data2.AgregarSqlParametro("@ID_CLIENTE", cliente_id);
                data2.AgregarSqlParametro("@RUTA_ARCHIVO", archivo);
                data2.AgregarSqlParametro("@DETALLE_ARCHIVO", contenido);
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
        internal string[] Pedido_campos_cabecera(int cliente_id)
        {

       
            SqlAccesoDatos data2 = new SqlAccesoDatos("Integra");
            try
            {
                data2.CargarSqlComando("[DBO].[RUTEADOR_CAMPOS_CABECERA]");
                data2.AgregarSqlParametro("@ID_CLIENTE", cliente_id);

                return data2.EjecutaSqlScalar().ToString().Split(',');


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
        internal string[] Pedido_campos_detalle(int cliente_id)
        {


            SqlAccesoDatos data2 = new SqlAccesoDatos("Integra");
            try
            {
                data2.CargarSqlComando("[DBO].[RUTEADOR_CAMPOS_detalle]");
                data2.AgregarSqlParametro("@ID_CLIENTE", cliente_id);

                return data2.EjecutaSqlScalar().ToString().Split(',');


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
        internal DataTable Pedido_ObtenerTodo(DateTime desde, DateTime hasta, int hora_id, int regi_id, int ciud_id, string comu_id, int usua_id, string peru_numero, bool solo_sin_ruta, long id_ruta)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_PEDIDOSV2]");
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
                if (!string.IsNullOrEmpty(comu_id))
                    data.AgregarSqlParametro("@comu_id", comu_id);
                if (hora_id != 0)
                    data.AgregarSqlParametro("@hora_id", hora_id);
                if (!string.IsNullOrEmpty(peru_numero))
                    data.AgregarSqlParametro("@peru_numero", peru_numero);
                if (solo_sin_ruta == true)
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
        internal List<PedidoBC> Pedido_ObtenerArray(bool solo_sin_ruta, DateTime desde, DateTime hasta, int hora_id, int regi_id, int ciud_id, int comu_id, int usua_id, string peru_numero, long id_ruta)
        {
            try
            {
                List<PedidoBC> listado = new List<PedidoBC>();
                data.CargarSqlComando("[dbo].[LISTAR_PEDIDOSV2]");
                data.AgregarSqlParametro("@solo_sin_ruta", solo_sin_ruta);
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
                if (id_ruta != 0)
                    data.AgregarSqlParametro("@id_ruta", id_ruta);
                data.EjecutarSqlLector();
                while (data.SqlLectorDatos.Read())
                {
                    PedidoBC p = new PedidoBC();
                    if (data.SqlLectorDatos["PERU_ID"] != DBNull.Value)
                        p.PERU_ID = Convert.ToInt64(data.SqlLectorDatos["PERU_ID"]);
                    if (data.SqlLectorDatos["PERU_NOMBRE"] != DBNull.Value)
                        p.PERU_NOMBRE = Convert.ToString(data.SqlLectorDatos["PERU_NOMBRE"]);
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
                    if (data.SqlLectorDatos["RPPE_ID"] != DBNull.Value)
                        p.RUTA_PEDIDO.RPPE_ID = Convert.ToInt32(data.SqlLectorDatos["RPPE_ID"]);
                    if (data.SqlLectorDatos["FH_PLANIFICA"] != DBNull.Value)
                        p.RUTA_PEDIDO.FH_PLANIFICA = Convert.ToDateTime(data.SqlLectorDatos["FH_PLANIFICA"]);
                    if (data.SqlLectorDatos["FH_LLEGADA"] != DBNull.Value)
                        p.RUTA_PEDIDO.FH_LLEGADA = Convert.ToDateTime(data.SqlLectorDatos["FH_LLEGADA"]);
                    if (data.SqlLectorDatos["FH_SALIDA"] != DBNull.Value)
                        p.RUTA_PEDIDO.FH_SALIDA = Convert.ToDateTime(data.SqlLectorDatos["FH_SALIDA"]);
                    if (data.SqlLectorDatos["SECUENCIA"] != DBNull.Value)
                        p.RUTA_PEDIDO.SECUENCIA = Convert.ToInt32(data.SqlLectorDatos["SECUENCIA"]);
                    if (data.SqlLectorDatos["tiempo"] != DBNull.Value)
                        p.RUTA_PEDIDO.tiempo = Convert.ToInt32(data.SqlLectorDatos["tiempo"]);
                    listado.Add(p);
                }
                return listado;
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
        internal List<PedidoBC> Pedido_ObtenerArray(DateTime desde, DateTime hasta, int hora_id, int regi_id, int ciud_id, int comu_id, int usua_id, string peru_numero, long id_ruta)
        {
            try
            {
                List<PedidoBC> listado = new List<PedidoBC>();
                data.CargarSqlComando("[dbo].[LISTAR_PEDIDOSV2]");
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
                if (id_ruta != 0)
                    data.AgregarSqlParametro("@id_ruta", id_ruta);
                data.EjecutarSqlLector();
                while (data.SqlLectorDatos.Read())
                {
                    PedidoBC p = new PedidoBC();
                    if (data.SqlLectorDatos["PERU_ID"] != DBNull.Value)
                        p.PERU_ID = Convert.ToInt64(data.SqlLectorDatos["PERU_ID"]);
                    if (data.SqlLectorDatos["PERU_NOMBRE"] != DBNull.Value)
                        p.PERU_NOMBRE = Convert.ToString(data.SqlLectorDatos["PERU_NOMBRE"]);
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
                    if (data.SqlLectorDatos["RPPE_ID"] != DBNull.Value)
                        p.RUTA_PEDIDO.RPPE_ID = Convert.ToInt32(data.SqlLectorDatos["RPPE_ID"]);
                    if (data.SqlLectorDatos["FH_PLANIFICA"] != DBNull.Value)
                        p.RUTA_PEDIDO.FH_PLANIFICA = Convert.ToDateTime(data.SqlLectorDatos["FH_PLANIFICA"]);
                    if (data.SqlLectorDatos["FH_LLEGADA"] != DBNull.Value)
                        p.RUTA_PEDIDO.FH_LLEGADA = Convert.ToDateTime(data.SqlLectorDatos["FH_LLEGADA"]);
                    if (data.SqlLectorDatos["FH_SALIDA"] != DBNull.Value)
                        p.RUTA_PEDIDO.FH_SALIDA = Convert.ToDateTime(data.SqlLectorDatos["FH_SALIDA"]);
                    if (data.SqlLectorDatos["SECUENCIA"] != DBNull.Value)
                        p.RUTA_PEDIDO.SECUENCIA = Convert.ToInt32(data.SqlLectorDatos["SECUENCIA"]);
                    if (data.SqlLectorDatos["tiempo"] != DBNull.Value)
                        p.RUTA_PEDIDO.tiempo = Convert.ToInt32(data.SqlLectorDatos["tiempo"]);
                    listado.Add(p);
                }
                return listado;
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
        internal PedidoBC Pedido_ObtenerXId(long peru_id, bool carga_detalle)
        {
            try
            {
                PedidoBC p = new PedidoBC();
                data.CargarSqlComando("[dbo].[LISTAR_PEDIDOSv2]");
                data.AgregarSqlParametro("@peru_id", peru_id);

                DataSet ds = data.EjecutarSqlquery3();
                DataRow datarow = ds.Tables[0].Rows[0];
                if (datarow["PERU_ID"] != DBNull.Value)
                    p.PERU_ID = Convert.ToInt64(datarow["PERU_ID"]);
                if (datarow["PERU_NOMBRE"] != DBNull.Value)
                    p.PERU_NOMBRE = Convert.ToString(datarow["PERU_NOMBRE"]);
                if (datarow["PERU_NUMERO"] != DBNull.Value)
                    p.PERU_NUMERO = Convert.ToString(datarow["PERU_NUMERO"]);
                if (datarow["PERU_CODIGO"] != DBNull.Value)
                    p.PERU_CODIGO = Convert.ToString(datarow["PERU_CODIGO"]);
                if (datarow["PERU_FECHA"] != DBNull.Value)
                    p.PERU_FECHA = Convert.ToDateTime(datarow["PERU_FECHA"]);
                if (datarow["PERU_PESO"] != DBNull.Value)
                    p.PERU_PESO = Convert.ToString(datarow["PERU_PESO"]);
                if (datarow["PERU_TIEMPO"] != DBNull.Value)
                    p.PERU_TIEMPO = Convert.ToString(datarow["PERU_TIEMPO"]);
                if (datarow["PERU_DIRECCION"] != DBNull.Value)
                    p.PERU_DIRECCION = Convert.ToString(datarow["PERU_DIRECCION"]);
                if (datarow["PERU_LATITUD"] != DBNull.Value)
                    p.PERU_LATITUD = Convert.ToDecimal(datarow["PERU_LATITUD"]);
                if (datarow["PERU_LONGITUD"] != DBNull.Value)
                    p.PERU_LONGITUD = Convert.ToDecimal(datarow["PERU_LONGITUD"]);
                if (datarow["HORA_ID"] != DBNull.Value)
                    p.HORA_SALIDA.HORA_ID = Convert.ToInt32(datarow["HORA_ID"]);
                if (datarow["HORA_COD"] != DBNull.Value)
                    p.HORA_SALIDA.HORA_COD = Convert.ToString(datarow["HORA_COD"]);
                if (datarow["PERU_USUA_ID"] != DBNull.Value)
                    p.USUARIO_PEDIDO.USUA_ID = Convert.ToInt32(datarow["PERU_USUA_ID"]);
                if (datarow["PERU_ENVIADO_RUTEADOR"] != DBNull.Value)
                    p.PERU_ENVIADO_RUTEADOR = Convert.ToBoolean(datarow["PERU_ENVIADO_RUTEADOR"]);
                if (datarow["PERU_FH_ENVIO"] != DBNull.Value)
                    p.PERU_FH_ENVIO = Convert.ToDateTime(datarow["PERU_FH_ENVIO"]);
                if (datarow["USR_ENVIO"] != DBNull.Value)
                    p.USUARIO_ENVIO.USUA_ID = Convert.ToInt32(datarow["USR_ENVIO"]);
                if (datarow["PERU_FH_CREACION"] != DBNull.Value)
                    p.PERU_FH_CREACION = Convert.ToDateTime(datarow["PERU_FH_CREACION"]);
                if (datarow["COMU_ID"] != DBNull.Value)
                    p.COMUNA.COMU_ID = Convert.ToInt32(datarow["COMU_ID"]);
                if (datarow["COMU_NOMBRE"] != DBNull.Value)
                    p.COMUNA.COMU_NOMBRE = Convert.ToString(datarow["COMU_NOMBRE"]);
                if (datarow["CIUD_ID"] != DBNull.Value)
                    p.COMUNA.CIUDAD.CIUD_ID = Convert.ToInt32(datarow["CIUD_ID"]);
                if (datarow["CIUD_NOMBRE"] != DBNull.Value)
                    p.COMUNA.CIUDAD.CIUD_NOMBRE = Convert.ToString(datarow["CIUD_NOMBRE"]);
                if (datarow["REGI_ID"] != DBNull.Value)
                    p.COMUNA.CIUDAD.REGION.REGI_ID = Convert.ToInt32(datarow["REGI_ID"]);
                if (datarow["REGI_NOMBRE"] != DBNull.Value)
                    p.COMUNA.CIUDAD.REGION.REGI_NOMBRE = Convert.ToString(datarow["REGI_NOMBRE"]);
                if (carga_detalle)
                {
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        PedidoDetalleBC detalle = new PedidoDetalleBC();
                        if (dr["PEDE_ID"] != DBNull.Value)
                            detalle.PEDE_ID = Convert.ToInt32(dr["PEDE_ID"]);
                        if (dr["CODIGO_PRODUCTO"] != DBNull.Value)
                            detalle.CODIGO_PRODUCTO = Convert.ToString(dr["CODIGO_PRODUCTO"]);
                        if (dr["CODIGO_CLIENTE"] != DBNull.Value)
                            detalle.CODIGO_CLIENTE = Convert.ToString(dr["CODIGO_CLIENTE"]);
                        if (dr["DIRECCION_CLIENTE"] != DBNull.Value)
                            detalle.DIRECCION_CLIENTE = Convert.ToString(dr["DIRECCION_CLIENTE"]);
                        if (dr["NOMBRE_CLIENTE"] != DBNull.Value)
                            detalle.NOMBRE_CLIENTE = Convert.ToString(dr["NOMBRE_CLIENTE"]);
                        if (dr["NUMERO_GUIA"] != DBNull.Value)
                            detalle.NUMERO_GUIA = Convert.ToString(dr["NUMERO_GUIA"]);
                        if (dr["PESO_PEDIDO"] != DBNull.Value)
                            detalle.PESO_PEDIDO = Convert.ToDecimal(dr["PESO_PEDIDO"]);
                        if (dr["PERU_ID"] != DBNull.Value)
                            detalle.PEDIDO.PERU_ID = Convert.ToInt64(dr["PERU_ID"]);
                        if (dr["PERU_NUMERO"] != DBNull.Value)
                            detalle.PEDIDO.PERU_NUMERO = Convert.ToString(dr["PERU_NUMERO"]);
                        if (dr["PERU_NOMBRE"] != DBNull.Value)
                            detalle.PEDIDO.PERU_NOMBRE = Convert.ToString(dr["PERU_NOMBRE"]);
                        if (dr["COMUNA_CLIENTE"] != DBNull.Value)
                            detalle.COMUNA_CLIENTE.COMU_ID = Convert.ToInt32(dr["COMUNA_CLIENTE"]);
                        if (dr["ID_COMUNA_CLIENTE"] != DBNull.Value)
                            detalle.COMUNA_CLIENTE.COMU_NOMBRE = Convert.ToString(dr["ID_COMUNA_CLIENTE"]);
                        if (dr["CIUD_ID_CLIENTE"] != DBNull.Value)
                            detalle.COMUNA_CLIENTE.CIUDAD.CIUD_ID = Convert.ToInt32(dr["CIUD_ID_CLIENTE"]);
                        if (dr["CIUD_NOMBRE_CLIENTE"] != DBNull.Value)
                            detalle.COMUNA_CLIENTE.CIUDAD.CIUD_NOMBRE = Convert.ToString(dr["CIUD_NOMBRE_CLIENTE"]);
                        if (dr["REGI_ID_CLIENTE"] != DBNull.Value)
                            detalle.COMUNA_CLIENTE.CIUDAD.REGION.REGI_ID = Convert.ToInt32(dr["REGI_ID_CLIENTE"]);
                        if (dr["REGI_NOMBRE_CLIENTE"] != DBNull.Value)
                            detalle.COMUNA_CLIENTE.CIUDAD.REGION.REGI_NOMBRE = Convert.ToString(dr["REGI_NOMBRE_CLIENTE"]);
                        p.DETALLE.Add(detalle);
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
        internal bool Pedido_Guardar(PedidoBC p, DataTable dt)
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
                if (dt != null)
                    data.AgregarSqlParametro("@DETALLES", dt);
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
            if (horario > 0) data.AgregarSqlParametro("@horario", horario);
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
        #region PedidoDetalle
        internal DataTable PedidoDetalle_ObtenerTodo(long peru_id, int regi_id, int ciud_id, int comu_id, long ruta_id)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_PEDIDO_DETALLE]");
                if (peru_id != 0)
                    data.AgregarSqlParametro("@peru_id", peru_id);
                if (regi_id != 0)
                    data.AgregarSqlParametro("@regi_id", regi_id);
                if (ciud_id != 0)
                    data.AgregarSqlParametro("@ciud_id", ciud_id);
                if (comu_id != 0)
                    data.AgregarSqlParametro("@comu_id", comu_id);
                if (ruta_id != 0)
                    data.AgregarSqlParametro("@ruta_id", ruta_id);
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
        internal PedidoDetalleBC PedidoDetalle_ObtenerXId(long pede_id)
        {
            try
            {
                PedidoDetalleBC p = new PedidoDetalleBC();
                data.CargarSqlComando("[dbo].[LISTAR_PEDIDO_DETALLE]");
                data.AgregarSqlParametro("@pede_id", pede_id);
                data.EjecutarSqlLector();
                while (data.SqlLectorDatos.Read())
                {
                    if (data.SqlLectorDatos["PEDE_ID"] != DBNull.Value)
                        p.PEDE_ID = Convert.ToInt32(data.SqlLectorDatos["PEDE_ID"]);
                    if (data.SqlLectorDatos["CODIGO_PRODUCTO"] != DBNull.Value)
                        p.CODIGO_PRODUCTO = Convert.ToString(data.SqlLectorDatos["CODIGO_PRODUCTO"]);
                    if (data.SqlLectorDatos["CODIGO_CLIENTE"] != DBNull.Value)
                        p.CODIGO_CLIENTE = Convert.ToString(data.SqlLectorDatos["CODIGO_CLIENTE"]);
                    if (data.SqlLectorDatos["DIRECCION_CLIENTE"] != DBNull.Value)
                        p.DIRECCION_CLIENTE = Convert.ToString(data.SqlLectorDatos["DIRECCION_CLIENTE"]);
                    if (data.SqlLectorDatos["NOMBRE_CLIENTE"] != DBNull.Value)
                        p.NOMBRE_CLIENTE = Convert.ToString(data.SqlLectorDatos["NOMBRE_CLIENTE"]);
                    if (data.SqlLectorDatos["NUMERO_GUIA"] != DBNull.Value)
                        p.NUMERO_GUIA = Convert.ToString(data.SqlLectorDatos["NUMERO_GUIA"]);
                    if (data.SqlLectorDatos["PESO_PEDIDO"] != DBNull.Value)
                        p.PESO_PEDIDO = Convert.ToDecimal(data.SqlLectorDatos["PESO_PEDIDO"]);
                    if (data.SqlLectorDatos["PERU_ID"] != DBNull.Value)
                        p.PEDIDO.PERU_ID = Convert.ToInt64(data.SqlLectorDatos["PERU_ID"]);
                    if (data.SqlLectorDatos["PERU_NUMERO"] != DBNull.Value)
                        p.PEDIDO.PERU_NUMERO = Convert.ToString(data.SqlLectorDatos["PERU_NUMERO"]);
                    if (data.SqlLectorDatos["PERU_NOMBRE"] != DBNull.Value)
                        p.PEDIDO.PERU_NOMBRE = Convert.ToString(data.SqlLectorDatos["PERU_NOMBRE"]);
                    if (data.SqlLectorDatos["COMUNA_CLIENTE"] != DBNull.Value)
                        p.COMUNA_CLIENTE.COMU_ID = Convert.ToInt32(data.SqlLectorDatos["COMUNA_CLIENTE"]);
                    if (data.SqlLectorDatos["ID_COMUNA_CLIENTE"] != DBNull.Value)
                        p.COMUNA_CLIENTE.COMU_NOMBRE = Convert.ToString(data.SqlLectorDatos["ID_COMUNA_CLIENTE"]);
                    if (data.SqlLectorDatos["CIUD_ID_CLIENTE"] != DBNull.Value)
                        p.COMUNA_CLIENTE.CIUDAD.CIUD_ID = Convert.ToInt32(data.SqlLectorDatos["CIUD_ID_CLIENTE"]);
                    if (data.SqlLectorDatos["CIUD_NOMBRE_CLIENTE"] != DBNull.Value)
                        p.COMUNA_CLIENTE.CIUDAD.CIUD_NOMBRE = Convert.ToString(data.SqlLectorDatos["CIUD_NOMBRE_CLIENTE"]);
                    if (data.SqlLectorDatos["REGI_ID_CLIENTE"] != DBNull.Value)
                        p.COMUNA_CLIENTE.CIUDAD.REGION.REGI_ID = Convert.ToInt32(data.SqlLectorDatos["REGI_ID_CLIENTE"]);
                    if (data.SqlLectorDatos["REGI_NOMBRE_CLIENTE"] != DBNull.Value)
                        p.COMUNA_CLIENTE.CIUDAD.REGION.REGI_NOMBRE = Convert.ToString(data.SqlLectorDatos["REGI_NOMBRE_CLIENTE"]);
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
        internal bool PedidoDetalle_Guardar(PedidoDetalleBC dp)
        {
            try
            {
                data.CargarSqlComando("[dbo].[GUARDAR_PEDIDO_DETALLE]");
                if (dp.PEDE_ID != 0)
                    data.AgregarSqlParametro("@PEDE_ID", dp.PEDE_ID);
                data.AgregarSqlParametro("@PERU_ID", dp.PEDIDO.PERU_ID);
                data.AgregarSqlParametro("@CODIGO_PRODUCTO", dp.CODIGO_PRODUCTO);
                data.AgregarSqlParametro("@CODIGO_CLIENTE", dp.CODIGO_CLIENTE);
                data.AgregarSqlParametro("@DIRECCION_CLIENTE", dp.DIRECCION_CLIENTE);
                data.AgregarSqlParametro("@NOMBRE_CLIENTE", dp.NOMBRE_CLIENTE);
                data.AgregarSqlParametro("@COMU_ID", dp.COMUNA_CLIENTE.COMU_ID);
                data.AgregarSqlParametro("@NUMERO_GUIA", dp.NUMERO_GUIA);
                data.AgregarSqlParametro("@PESO_PEDIDO", dp.PESO_PEDIDO);
                data.AgregarSqlParametro("@PEDE_CANTIDAD", dp.PEDE_CANTIDAD);
                data.AgregarSqlParametro("@PEDE_DESC_PRODUCTO", dp.PEDE_DESC_PRODUCTO);

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
        internal bool PedidoDetalle_Eliminar(long pede_id)
        {
            try
            {
                data.CargarSqlComando("[dbo].[ELIMINAR_PEDIDO_DETALLE]");
                data.AgregarSqlParametro("@PEDE_ID", pede_id);
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
        #region Pre_Ruta
        internal DataTable PreRuta_CrearEnvio(string gd, int usua_id, bool archivar)
        {
            try
            {
                data.CargarSqlComando("[dbo].[CREAR_ENVIO_PRE_RUTAS]");
                data.AgregarSqlParametro("@pedidos", gd);
                data.AgregarSqlParametro("@id_usuario", usua_id);
                data.AgregarSqlParametro("@archivar", archivar);
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
        internal DataTable PreRuta_ObtenerTodo(DateTime desde, DateTime hasta, int hora_id, int regi_id, int ciud_id, int comu_id, int usua_id, string peru_numero, string envio)
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
     //   internal List<PreRutaBC> PreRuta_ObtenerArray(DateTime desde, DateTime hasta, int hora_id, int regi_id, int ciud_id, int comu_id, int usua_id, string peru_numero, string envio)
     //   {
     //       try
     //       {
     //           List<PreRutaBC> listado = new List<PreRutaBC>();
     //           data.CargarSqlComando("[dbo].[LISTAR_PRERUTA]");
     //           if (desde != DateTime.MinValue && hasta != DateTime.MinValue)
     //           {
     //               data.AgregarSqlParametro("@fh_desde", desde);
     //               data.AgregarSqlParametro("@fh_hasta", hasta);
     //           }

     //           if (usua_id != 0)
     //               data.AgregarSqlParametro("@id_usuario", usua_id);
     //           if (regi_id != 0)
     //               data.AgregarSqlParametro("@regi_id", regi_id);
     //           if (ciud_id != 0)
     //               data.AgregarSqlParametro("@ciud_id", ciud_id);
     //           if (comu_id != 0)
     //               data.AgregarSqlParametro("@comu_id", comu_id);
     //           if (hora_id != 0)
     //               data.AgregarSqlParametro("@hora_id", hora_id);
     //           if (!string.IsNullOrEmpty(peru_numero))
     //               data.AgregarSqlParametro("@numero", peru_numero);
     //           if (!string.IsNullOrEmpty(envio))
     //               data.AgregarSqlParametro("@envio", envio);
     //           data.EjecutarSqlLector();
     //           while (data.SqlLectorDatos.Read())
     //           {
					//listado.Add(cargaDatosPreruta());
     //           }
     //           return listado;
     //       }
     //       catch (Exception ex)
     //       {
     //           throw ex;
     //       }
     //       finally
     //       {
     //           data.LimpiarSqlParametros();
     //           data.CerrarSqlConeccion();
     //       }
     //   }
        internal List<PreRutaBC> PreRuta_ObtenerArray(DateTime desde, DateTime hasta, int hora_id, int regi_id, int ciud_id, int comu_id, int usua_id, string peru_numero, string envio, bool puntos_ruta)
        {
            try
            {
                List<PreRutaBC> listado = new List<PreRutaBC>();
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
                if (puntos_ruta)
                    data.AgregarSqlParametro("@carga_pedidos", puntos_ruta);
                DataSet ds = data.EjecutarSqlquery3();
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    PreRutaBC pr = new PreRutaBC();
                    if (dr["ID"] != DBNull.Value)
                        pr.ID = Convert.ToInt32(dr["ID"]);
                    if (dr["NUMERO"] != DBNull.Value)
                        pr.NUMERO = Convert.ToString(dr["NUMERO"]);
                    if (dr["FH_VIAJE"] != DBNull.Value)
                        pr.FH_VIAJE = Convert.ToDateTime(dr["FH_VIAJE"]);
                    if (dr["ID_MOVIL"] != DBNull.Value)
                        pr.ID_MOVIL = Convert.ToInt32(dr["ID_MOVIL"]);
                    if (dr["ID_ESTADO"] != DBNull.Value)
                        pr.ID_ESTADO = Convert.ToInt32(dr["ID_ESTADO"]);
                    if (dr["OBSERVACION"] != DBNull.Value)
                        pr.OBSERVACION = Convert.ToString(dr["OBSERVACION"]);
                    if (dr["ID_TIPOVIAJE"] != DBNull.Value)
                        pr.ID_TIPOVIAJE = Convert.ToInt32(dr["ID_TIPOVIAJE"]);
                    if (dr["RETORNO"] != DBNull.Value)
                        pr.RETORNO = Convert.ToString(dr["RETORNO"]);
                    if (dr["FH_CREACION"] != DBNull.Value)
                        pr.FH_CREACION = Convert.ToDateTime(dr["FH_CREACION"]);
                    if (dr["FH_UPDATE"] != DBNull.Value)
                        pr.FH_UPDATE = Convert.ToDateTime(dr["FH_UPDATE"]);
                    if (dr["FH_RETORNO"] != DBNull.Value)
                        pr.FH_RETORNO = Convert.ToDateTime(dr["FH_RETORNO"]);
                    if (dr["FH_SALIDA"] != DBNull.Value)
                        pr.FH_SALIDA = Convert.ToDateTime(dr["FH_SALIDA"]);
                    if (dr["TOTAL_KG"] != DBNull.Value)
                        pr.TOTAL_KG = Convert.ToDecimal(dr["TOTAL_KG"]);
                    if (dr["CORREO_GPS"] != DBNull.Value)
                        pr.CORREO_GPS = Convert.ToBoolean(dr["CORREO_GPS"]);
                    if (dr["ID_CLIENTE_GPS"] != DBNull.Value)
                        pr.ID_CLIENTE_GPS = Convert.ToInt32(dr["ID_CLIENTE_GPS"]);
                    if (dr["RUTA"] != DBNull.Value)
                        pr.RUTA = Convert.ToString(dr["RUTA"]);
                    if (dr["FECHA_PRESENTACION"] != DBNull.Value)
                        pr.FECHA_PRESENTACION = Convert.ToDateTime(dr["FECHA_PRESENTACION"]);
                    if (dr["FECHA_INICIOCARGA"] != DBNull.Value)
                        pr.FECHA_INICIOCARGA = Convert.ToDateTime(dr["FECHA_INICIOCARGA"]);
                    if (dr["FECHA_FINCARGA"] != DBNull.Value)
                        pr.FECHA_FINCARGA = Convert.ToDateTime(dr["FECHA_FINCARGA"]);
                    if (dr["FECHA_DESPACHOEXP"] != DBNull.Value)
                        pr.FECHA_DESPACHOEXP = Convert.ToDateTime(dr["FECHA_DESPACHOEXP"]);
                    if (dr["FECHA_INICIOEXP"] != DBNull.Value)
                        pr.FECHA_INICIOEXP = Convert.ToDateTime(dr["FECHA_INICIOEXP"]);
                    if (dr["FECHA_FINEXP"] != DBNull.Value)
                        pr.FECHA_FINEXP = Convert.ToDateTime(dr["FECHA_FINEXP"]);
                    if (dr["RUTA_COLOR"] != DBNull.Value)
                        pr.RUTA_COLOR = Convert.ToString(dr["RUTA_COLOR"]);
                    // Conductor
                    if (dr["ID_CONDUCTOR"] != DBNull.Value)
                        pr.CONDUCTOR.COND_ID = Convert.ToInt32(dr["ID_CONDUCTOR"]);
                    if (dr["COND_RUT"] != DBNull.Value)
                        pr.CONDUCTOR.COND_RUT = Convert.ToString(dr["COND_RUT"]);
                    if (dr["COND_NOMBRE"] != DBNull.Value)
                        pr.CONDUCTOR.COND_NOMBRE = Convert.ToString(dr["COND_NOMBRE"]);
                    // Origen
                    if (dr["ID_ORIGEN"] != DBNull.Value)
                        pr.ORIGEN.ID = Convert.ToInt32(dr["ID_ORIGEN"]);
                    if (dr["ORIGEN_NOMBRE"] != DBNull.Value)
                        pr.ORIGEN.NOMBRE_PE = Convert.ToString(dr["ORIGEN_NOMBRE"]);
                    if (dr["ORIGEN_DIRECCION"] != DBNull.Value)
                        pr.ORIGEN.DIRECCION_PE = Convert.ToString(dr["ORIGEN_DIRECCION"]);
                    if (dr["ORIGEN_LAT"] != DBNull.Value)
                        pr.ORIGEN.LAT_PE = Convert.ToDecimal(dr["ORIGEN_LAT"]);
                    if (dr["ORIGEN_LON"] != DBNull.Value)
                        pr.ORIGEN.LON_PE = Convert.ToDecimal(dr["ORIGEN_LON"]);
                    // Horario
                    if (dr["HORA_ID"] != DBNull.Value)
                        pr.HORARIO.HORA_ID = Convert.ToInt32(dr["HORA_ID"]);
                    if (dr["HORARIO"] != DBNull.Value)
                    {
                        pr.ORIGEN.PERU_LLEGADA = Convert.ToString(dr["HORARIO"]);
                        pr.HORARIO.HORA_COD = Convert.ToString(dr["HORARIO"]);
                    }
                    // Operacion
                    if (dr["ID_OPE"] != DBNull.Value)
                        pr.OPERACION.OPER_ID = Convert.ToInt32(dr["ID_OPE"]);
                    // Envio
                    if (dr["ID_ENVIO"] != DBNull.Value)
                        pr.ENVIO.Env_ID = Convert.ToInt32(dr["ID_ENVIO"]);
                    // Trailer
                    if (dr["TRAI_ID"] != DBNull.Value)
                        pr.TRAILER.TRAI_ID = Convert.ToInt32(dr["TRAI_ID"]);
                    if (dr["TRAI_PLACA"] != DBNull.Value)
                        pr.TRAILER.TRAI_PLACA = Convert.ToString(dr["TRAI_PLACA"]);
                    // Trailer Tipo
                    if (dr["TRTI_ID"] != DBNull.Value)
                        pr.TRAILER.TRAILER_TIPO.TRTI_ID = Convert.ToInt32(dr["TRTI_ID"]);
                    if (dr["TIPO_VEHICULO"] != DBNull.Value)
                        pr.TRAILER.TRAILER_TIPO.TRTI_DESC = Convert.ToString(dr["TIPO_VEHICULO"]);
                    // Tracto
                    if (dr["TRAC_ID"] != DBNull.Value)
                        pr.TRACTO.TRAC_ID = Convert.ToInt32(dr["TRAC_ID"]);
                    if (dr["TRAC_PLACA"] != DBNull.Value)
                        pr.TRACTO.TRAC_PLACA = Convert.ToString(dr["TRAC_PLACA"]);
                    if (puntos_ruta)
                    {
                        pr.PEDIDOS = new List<PedidoBC>();
                        DataView dw = new DataView(ds.Tables[1]);
                        dw.RowFilter = string.Format("ID_RUTA = {0}", pr.ID);

                        foreach (DataRow dr2 in dw.ToTable().Rows)
                        {
                            PedidoBC p = new PedidoBC();
                            if (dr2["PERU_ID"] != DBNull.Value)
                                p.PERU_ID = Convert.ToInt64(dr2["PERU_ID"]);
                            if (dr2["PERU_NUMERO"] != DBNull.Value)
                                p.PERU_NUMERO = Convert.ToString(dr2["PERU_NUMERO"]);
                            if (dr2["PERU_CODIGO"] != DBNull.Value)
                                p.PERU_CODIGO = Convert.ToString(dr2["PERU_CODIGO"]);
                            if (dr2["PERU_FECHA"] != DBNull.Value)
                                p.PERU_FECHA = Convert.ToDateTime(dr2["PERU_FECHA"]);
                            if (dr2["PERU_PESO"] != DBNull.Value)
                                p.PERU_PESO = Convert.ToString(dr2["PERU_PESO"]);
                            if (dr2["PERU_TIEMPO"] != DBNull.Value)
                                p.PERU_TIEMPO = Convert.ToString(dr2["PERU_TIEMPO"]);
                            if (dr2["PERU_DIRECCION"] != DBNull.Value)
                                p.PERU_DIRECCION = Convert.ToString(dr2["PERU_DIRECCION"]);
                            if (dr2["PERU_LATITUD"] != DBNull.Value)
                                p.PERU_LATITUD = Convert.ToDecimal(dr2["PERU_LATITUD"]);
                            if (dr2["PERU_LONGITUD"] != DBNull.Value)
                                p.PERU_LONGITUD = Convert.ToDecimal(dr2["PERU_LONGITUD"]);
                            if (dr2["HORA_ID"] != DBNull.Value)
                                p.HORA_SALIDA.HORA_ID = Convert.ToInt32(dr2["HORA_ID"]);
                            if (dr2["HORA_COD"] != DBNull.Value)
                                p.HORA_SALIDA.HORA_COD = Convert.ToString(dr2["HORA_COD"]);
                            if (dr2["PERU_USUA_ID"] != DBNull.Value)
                                p.USUARIO_PEDIDO.USUA_ID = Convert.ToInt32(dr2["PERU_USUA_ID"]);
                            if (dr2["PERU_ENVIADO_RUTEADOR"] != DBNull.Value)
                                p.PERU_ENVIADO_RUTEADOR = Convert.ToBoolean(dr2["PERU_ENVIADO_RUTEADOR"]);
                            if (dr2["PERU_FH_ENVIO"] != DBNull.Value)
                                p.PERU_FH_ENVIO = Convert.ToDateTime(dr2["PERU_FH_ENVIO"]);
                            if (dr2["USR_ENVIO"] != DBNull.Value)
                                p.USUARIO_ENVIO.USUA_ID = Convert.ToInt32(dr2["USR_ENVIO"]);
                            if (dr2["PERU_FH_CREACION"] != DBNull.Value)
                                p.PERU_FH_CREACION = Convert.ToDateTime(dr2["PERU_FH_CREACION"]);
                            if (dr2["COMU_ID"] != DBNull.Value)
                                p.COMUNA.COMU_ID = Convert.ToInt32(dr2["COMU_ID"]);
                            if (dr2["COMU_NOMBRE"] != DBNull.Value)
                                p.COMUNA.COMU_NOMBRE = Convert.ToString(dr2["COMU_NOMBRE"]);
                            if (dr2["CIUD_ID"] != DBNull.Value)
                                p.COMUNA.CIUDAD.CIUD_ID = Convert.ToInt32(dr2["CIUD_ID"]);
                            if (dr2["CIUD_NOMBRE"] != DBNull.Value)
                                p.COMUNA.CIUDAD.CIUD_NOMBRE = Convert.ToString(dr2["CIUD_NOMBRE"]);
                            if (dr2["REGI_ID"] != DBNull.Value)
                                p.COMUNA.CIUDAD.REGION.REGI_ID = Convert.ToInt32(dr2["REGI_ID"]);
                            if (dr2["REGI_NOMBRE"] != DBNull.Value)
                                p.COMUNA.CIUDAD.REGION.REGI_NOMBRE = Convert.ToString(dr2["REGI_NOMBRE"]);
                            if (dr2["RPPE_ID"] != DBNull.Value)
                                p.RUTA_PEDIDO.RPPE_ID = Convert.ToInt32(dr2["RPPE_ID"]);
                            if (dr2["FH_PLANIFICA"] != DBNull.Value)
                                p.RUTA_PEDIDO.FH_PLANIFICA = Convert.ToDateTime(dr2["FH_PLANIFICA"]);
                            if (dr2["FH_LLEGADA"] != DBNull.Value)
                                p.RUTA_PEDIDO.FH_LLEGADA = Convert.ToDateTime(dr2["FH_LLEGADA"]);
                            if (dr2["FH_SALIDA"] != DBNull.Value)
                                p.RUTA_PEDIDO.FH_SALIDA = Convert.ToDateTime(dr2["FH_SALIDA"]);
                            if (dr2["SECUENCIA"] != DBNull.Value)
                                p.RUTA_PEDIDO.SECUENCIA = Convert.ToInt32(dr2["SECUENCIA"]);
                            if (dr2["tiempo"] != DBNull.Value)
                                p.RUTA_PEDIDO.tiempo = Convert.ToInt32(dr2["tiempo"]);
                            pr.PEDIDOS.Add(p);
                        }

                        pr.RESPUESTA = new List<RespuestaBC>();
                        dw = new DataView(ds.Tables[2]);
                        dw.RowFilter = string.Format("RUTA_ID = {0}", pr.ID);
                        foreach (DataRow dr2 in dw.ToTable().Rows)
                        {
                            RespuestaBC r = new RespuestaBC();
                            if (dr2["RURE_ID"] != DBNull.Value)
                                r.RURE_ID = Convert.ToInt64(dr2["RURE_ID"]);
                            if (dr2["RURE_RESPUESTA"] != DBNull.Value)
                                r.RURE_RESPUESTA = Convert.ToString(dr2["RURE_RESPUESTA"]);
                            if (dr2["RURE_ORDEN"] != DBNull.Value)
                                r.RURE_ORDEN = Convert.ToInt32(dr2["RURE_ORDEN"]);
                            pr.RESPUESTA.Add(r);
                        }
                    }
                    listado.Add(pr);
                }
                return listado;
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
        internal PreRutaBC PreRuta_ObtenerXId(long id_preruta, bool puntos_ruta)
        {
            try
            {
                PreRutaBC pr = new PreRutaBC();
                data.CargarSqlComando("[dbo].[LISTAR_PRERUTA]");
                data.AgregarSqlParametro("@ID_PRERUTA", id_preruta);
                if (puntos_ruta)
                    data.AgregarSqlParametro("@carga_pedidos", puntos_ruta);
                DataSet ds = data.EjecutarSqlquery3();
                DataRow datarow = ds.Tables[0].Rows[0];
                if (datarow["ID"] != DBNull.Value)
                    pr.ID = Convert.ToInt32(datarow["ID"]);
                if (datarow["NUMERO"] != DBNull.Value)
                    pr.NUMERO = Convert.ToString(datarow["NUMERO"]);
                if (datarow["FH_VIAJE"] != DBNull.Value)
                    pr.FH_VIAJE = Convert.ToDateTime(datarow["FH_VIAJE"]);
                if (datarow["ID_MOVIL"] != DBNull.Value)
                    pr.ID_MOVIL = Convert.ToInt32(datarow["ID_MOVIL"]);
                if (datarow["ID_ESTADO"] != DBNull.Value)
                    pr.ID_ESTADO = Convert.ToInt32(datarow["ID_ESTADO"]);
                if (datarow["OBSERVACION"] != DBNull.Value)
                    pr.OBSERVACION = Convert.ToString(datarow["OBSERVACION"]);
                if (datarow["ID_TIPOVIAJE"] != DBNull.Value)
                    pr.ID_TIPOVIAJE = Convert.ToInt32(datarow["ID_TIPOVIAJE"]);
                if (datarow["RETORNO"] != DBNull.Value)
                    pr.RETORNO = Convert.ToString(datarow["RETORNO"]);
                if (datarow["FH_CREACION"] != DBNull.Value)
                    pr.FH_CREACION = Convert.ToDateTime(datarow["FH_CREACION"]);
                if (datarow["FH_UPDATE"] != DBNull.Value)
                    pr.FH_UPDATE = Convert.ToDateTime(datarow["FH_UPDATE"]);
                if (datarow["FH_RETORNO"] != DBNull.Value)
                    pr.FH_RETORNO = Convert.ToDateTime(datarow["FH_RETORNO"]);
                if (datarow["FH_SALIDA"] != DBNull.Value)
                    pr.FH_SALIDA = Convert.ToDateTime(datarow["FH_SALIDA"]);
                if (datarow["TOTAL_KG"] != DBNull.Value)
                    pr.TOTAL_KG = Convert.ToDecimal(datarow["TOTAL_KG"]);
                if (datarow["CORREO_GPS"] != DBNull.Value)
                    pr.CORREO_GPS = Convert.ToBoolean(datarow["CORREO_GPS"]);
                if (datarow["ID_CLIENTE_GPS"] != DBNull.Value)
                    pr.ID_CLIENTE_GPS = Convert.ToInt32(datarow["ID_CLIENTE_GPS"]);
                if (datarow["RUTA"] != DBNull.Value)
                    pr.RUTA = Convert.ToString(datarow["RUTA"]);
                if (datarow["FECHA_PRESENTACION"] != DBNull.Value)
                    pr.FECHA_PRESENTACION = Convert.ToDateTime(datarow["FECHA_PRESENTACION"]);
                if (datarow["FECHA_INICIOCARGA"] != DBNull.Value)
                    pr.FECHA_INICIOCARGA = Convert.ToDateTime(datarow["FECHA_INICIOCARGA"]);
                if (datarow["FECHA_FINCARGA"] != DBNull.Value)
                    pr.FECHA_FINCARGA = Convert.ToDateTime(datarow["FECHA_FINCARGA"]);
                if (datarow["FECHA_DESPACHOEXP"] != DBNull.Value)
                    pr.FECHA_DESPACHOEXP = Convert.ToDateTime(datarow["FECHA_DESPACHOEXP"]);
                if (datarow["FECHA_INICIOEXP"] != DBNull.Value)
                    pr.FECHA_INICIOEXP = Convert.ToDateTime(datarow["FECHA_INICIOEXP"]);
                if (datarow["FECHA_FINEXP"] != DBNull.Value)
                    pr.FECHA_FINEXP = Convert.ToDateTime(datarow["FECHA_FINEXP"]);
                if (datarow["RUTA_COLOR"] != DBNull.Value)
                    pr.RUTA_COLOR = Convert.ToString(datarow["RUTA_COLOR"]);
                if (datarow["TIEMPO_RETORNO"] != DBNull.Value)
                    pr.TIEMPO_RETORNO = Convert.ToInt32(datarow["TIEMPO_RETORNO"]);
                // Conductor
                if (datarow["ID_CONDUCTOR"] != DBNull.Value)
                    pr.CONDUCTOR.COND_ID = Convert.ToInt32(datarow["ID_CONDUCTOR"]);
                if (datarow["COND_RUT"] != DBNull.Value)
                    pr.CONDUCTOR.COND_RUT = Convert.ToString(datarow["COND_RUT"]);
                if (datarow["COND_NOMBRE"] != DBNull.Value)
                    pr.CONDUCTOR.COND_NOMBRE = Convert.ToString(datarow["COND_NOMBRE"]);
                // Origen
                if (datarow["ID_ORIGEN"] != DBNull.Value)
                    pr.ORIGEN.ID = Convert.ToInt32(datarow["ID_ORIGEN"]);
                if (datarow["ORIGEN_NOMBRE"] != DBNull.Value)
                    pr.ORIGEN.NOMBRE_PE = Convert.ToString(datarow["ORIGEN_NOMBRE"]);
                if (datarow["ORIGEN_DIRECCION"] != DBNull.Value)
                    pr.ORIGEN.DIRECCION_PE = Convert.ToString(datarow["ORIGEN_DIRECCION"]);
                if (datarow["ORIGEN_LAT"] != DBNull.Value)
                    pr.ORIGEN.LAT_PE = Convert.ToDecimal(datarow["ORIGEN_LAT"]);
                if (datarow["ORIGEN_LON"] != DBNull.Value)
                    pr.ORIGEN.LON_PE = Convert.ToDecimal(datarow["ORIGEN_LON"]);
                // Horario
                if (datarow["HORA_ID"] != DBNull.Value)
                    pr.HORARIO.HORA_ID = Convert.ToInt32(datarow["HORA_ID"]);
                if (datarow["HORARIO"] != DBNull.Value)
                {
                    pr.ORIGEN.PERU_LLEGADA = Convert.ToString(datarow["HORARIO"]);
                    pr.HORARIO.HORA_COD = Convert.ToString(datarow["HORARIO"]);
                }
                // Operacion
                if (datarow["ID_OPE"] != DBNull.Value)
                    pr.OPERACION.OPER_ID = Convert.ToInt32(datarow["ID_OPE"]);
                // Envio
                if (datarow["ID_ENVIO"] != DBNull.Value)
                    pr.ENVIO.Env_ID = Convert.ToInt32(datarow["ID_ENVIO"]);
                // Trailer
                if (datarow["TRAI_ID"] != DBNull.Value)
                    pr.TRAILER.TRAI_ID = Convert.ToInt32(datarow["TRAI_ID"]);
                if (datarow["TRAI_PLACA"] != DBNull.Value)
                    pr.TRAILER.TRAI_PLACA = Convert.ToString(datarow["TRAI_PLACA"]);
                // Trailer Tipo
                if (datarow["TRTI_ID"] != DBNull.Value)
                    pr.TRAILER.TRAILER_TIPO.TRTI_ID = Convert.ToInt32(datarow["TRTI_ID"]);
                if (datarow["TIPO_VEHICULO"] != DBNull.Value)
                    pr.TRAILER.TRAILER_TIPO.TRTI_DESC = Convert.ToString(datarow["TIPO_VEHICULO"]);
                // Tracto
                if (datarow["TRAC_ID"] != DBNull.Value)
                    pr.TRACTO.TRAC_ID = Convert.ToInt32(datarow["TRAC_ID"]);
                if (datarow["TRAC_PLACA"] != DBNull.Value)
                    pr.TRACTO.TRAC_PLACA = Convert.ToString(datarow["TRAC_PLACA"]);
                if (puntos_ruta)
                {
                    pr.PEDIDOS = new List<PedidoBC>();
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        PedidoBC p = new PedidoBC();
                        if (dr["PERU_ID"] != DBNull.Value)
                            p.PERU_ID = Convert.ToInt64(dr["PERU_ID"]);
                        if (dr["PERU_NUMERO"] != DBNull.Value)
                            p.PERU_NUMERO = Convert.ToString(dr["PERU_NUMERO"]);
                        if (dr["PERU_CODIGO"] != DBNull.Value)
                            p.PERU_CODIGO = Convert.ToString(dr["PERU_CODIGO"]);
                        if (dr["PERU_FECHA"] != DBNull.Value)
                            p.PERU_FECHA = Convert.ToDateTime(dr["PERU_FECHA"]);
                        if (dr["PERU_PESO"] != DBNull.Value)
                            p.PERU_PESO = Convert.ToString(dr["PERU_PESO"]);
                        if (dr["PERU_TIEMPO"] != DBNull.Value)
                            p.PERU_TIEMPO = Convert.ToString(dr["PERU_TIEMPO"]);
                        if (dr["PERU_DIRECCION"] != DBNull.Value)
                            p.PERU_DIRECCION = Convert.ToString(dr["PERU_DIRECCION"]);
                        if (dr["PERU_LATITUD"] != DBNull.Value)
                            p.PERU_LATITUD = Convert.ToDecimal(dr["PERU_LATITUD"]);
                        if (dr["PERU_LONGITUD"] != DBNull.Value)
                            p.PERU_LONGITUD = Convert.ToDecimal(dr["PERU_LONGITUD"]);
                        if (dr["HORA_ID"] != DBNull.Value)
                            p.HORA_SALIDA.HORA_ID = Convert.ToInt32(dr["HORA_ID"]);
                        if (dr["HORA_COD"] != DBNull.Value)
                            p.HORA_SALIDA.HORA_COD = Convert.ToString(dr["HORA_COD"]);
                        if (dr["PERU_USUA_ID"] != DBNull.Value)
                            p.USUARIO_PEDIDO.USUA_ID = Convert.ToInt32(dr["PERU_USUA_ID"]);
                        if (dr["PERU_ENVIADO_RUTEADOR"] != DBNull.Value)
                            p.PERU_ENVIADO_RUTEADOR = Convert.ToBoolean(dr["PERU_ENVIADO_RUTEADOR"]);
                        if (dr["PERU_FH_ENVIO"] != DBNull.Value)
                            p.PERU_FH_ENVIO = Convert.ToDateTime(dr["PERU_FH_ENVIO"]);
                        if (dr["USR_ENVIO"] != DBNull.Value)
                            p.USUARIO_ENVIO.USUA_ID = Convert.ToInt32(dr["USR_ENVIO"]);
                        if (dr["PERU_FH_CREACION"] != DBNull.Value)
                            p.PERU_FH_CREACION = Convert.ToDateTime(dr["PERU_FH_CREACION"]);
                        if (dr["COMU_ID"] != DBNull.Value)
                            p.COMUNA.COMU_ID = Convert.ToInt32(dr["COMU_ID"]);
                        if (dr["COMU_NOMBRE"] != DBNull.Value)
                            p.COMUNA.COMU_NOMBRE = Convert.ToString(dr["COMU_NOMBRE"]);
                        if (dr["CIUD_ID"] != DBNull.Value)
                            p.COMUNA.CIUDAD.CIUD_ID = Convert.ToInt32(dr["CIUD_ID"]);
                        if (dr["CIUD_NOMBRE"] != DBNull.Value)
                            p.COMUNA.CIUDAD.CIUD_NOMBRE = Convert.ToString(dr["CIUD_NOMBRE"]);
                        if (dr["REGI_ID"] != DBNull.Value)
                            p.COMUNA.CIUDAD.REGION.REGI_ID = Convert.ToInt32(dr["REGI_ID"]);
                        if (dr["REGI_NOMBRE"] != DBNull.Value)
                            p.COMUNA.CIUDAD.REGION.REGI_NOMBRE = Convert.ToString(dr["REGI_NOMBRE"]);
                        if (dr["RPPE_ID"] != DBNull.Value)
                            p.RUTA_PEDIDO.RPPE_ID = Convert.ToInt32(dr["RPPE_ID"]);
                        if (dr["FH_PLANIFICA"] != DBNull.Value)
                            p.RUTA_PEDIDO.FH_PLANIFICA = Convert.ToDateTime(dr["FH_PLANIFICA"]);
                        if (dr["FH_LLEGADA"] != DBNull.Value)
                            p.RUTA_PEDIDO.FH_LLEGADA = Convert.ToDateTime(dr["FH_LLEGADA"]);
                        if (dr["FH_SALIDA"] != DBNull.Value)
                            p.RUTA_PEDIDO.FH_SALIDA = Convert.ToDateTime(dr["FH_SALIDA"]);
                        if (dr["SECUENCIA"] != DBNull.Value)
                            p.RUTA_PEDIDO.SECUENCIA = Convert.ToInt32(dr["SECUENCIA"]);
                        if (dr["tiempo"] != DBNull.Value)
                            p.RUTA_PEDIDO.tiempo = Convert.ToInt32(dr["tiempo"]);
                        pr.PEDIDOS.Add(p);
                    }
                    pr.RESPUESTA = new List<RespuestaBC>();
                    foreach (DataRow dr2 in ds.Tables[2].Rows)
                    {
                        RespuestaBC r = new RespuestaBC();
                        if (dr2["RURE_ID"] != DBNull.Value)
                            r.RURE_ID = Convert.ToInt64(dr2["RURE_ID"]);
                        if (dr2["RURE_RESPUESTA"] != DBNull.Value)
                            r.RURE_RESPUESTA = Convert.ToString(dr2["RURE_RESPUESTA"]);
                        if (dr2["RURE_ORDEN"] != DBNull.Value)
                            r.RURE_ORDEN = Convert.ToInt32(dr2["RURE_ORDEN"]);
                        pr.RESPUESTA.Add(r);
                    }
                }
                return pr;
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
        internal string PreRuta_ObtenerUltimosProcesos()
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
        internal bool PreRuta_GuardarPuntos(PreRutaBC p, string id_destinos, string tiempos, string hora_salida)
        {
            try
            {
                data.CargarSqlComando("[dbo].[GUARDAR_PUNTOS_PRE_RUTA]");
                if (p.ID != 0) 
                    data.AgregarSqlParametro("@ID_RUTA", p.ID);
                if (p.TRAILER.TRAI_ID != 0)
                    data.AgregarSqlParametro("@TRAI_ID", p.TRAILER.TRAI_ID);
                if (p.TRACTO.TRAC_ID != 0)
                    data.AgregarSqlParametro("@TRAC_ID", p.TRACTO.TRAC_ID);
                if (p.CONDUCTOR.COND_ID != 0)
                    data.AgregarSqlParametro("@COND_ID", p.CONDUCTOR.COND_ID);
                if (!string.IsNullOrEmpty(p.RETORNO))
                    data.AgregarSqlParametro("@RETORNO", p.RETORNO);
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
        internal bool PreRuta_GuardarPuntos(PreRutaBC pre_ruta)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID_DESTINO", typeof(int));
                dt.Columns.Add("SECUENCIA", typeof(int));
                dt.Columns.Add("FH_LLEGADA", typeof(DateTime));
                dt.Columns.Add("FH_SALIDA", typeof(DateTime));
                dt.Columns.Add("TIEMPO", typeof(int));

                DateTime fechaRelativa = pre_ruta.FECHA_DESPACHOEXP;
                string[] temp = pre_ruta.PEDIDOS[0].HORA_SALIDA.HORA_COD.Split(":".ToCharArray());

                fechaRelativa = fechaRelativa.Date.AddHours(Convert.ToInt32(temp[0])).AddMinutes(Convert.ToInt32(temp[1]));

                // parche EG, secuencia de 1 a infinito
                int contador = 1;
                foreach (PedidoBC p in pre_ruta.PEDIDOS)
                {
                    p.RUTA_PEDIDO.tiempo = Convert.ToInt32(p.RUTA_PEDIDO.FH_LLEGADA.Subtract(fechaRelativa).TotalMinutes);
                    fechaRelativa = p.RUTA_PEDIDO.FH_SALIDA;
                    DataRow dr = dt.NewRow();
                    dr["ID_DESTINO"] = p.PERU_ID;
                    dr["SECUENCIA"] = contador;// p.RUTA_PEDIDO.SECUENCIA;
                    dr["FH_LLEGADA"] = p.RUTA_PEDIDO.FH_LLEGADA;
                    dr["FH_SALIDA"] = p.RUTA_PEDIDO.FH_SALIDA;
                    dr["TIEMPO"] = p.RUTA_PEDIDO.tiempo;
                    contador = contador + 1;
                    dt.Rows.Add(dr);
                }

                data.CargarSqlComando("[dbo].[GUARDAR_PUNTOS_PRE_RUTA_V2]");
                data.AgregarSqlParametro("@ID_RUTA", pre_ruta.ID);
                data.AgregarSqlParametro("@PEDIDOS_RUTA", dt);
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
        internal bool PreRuta_Guardar(PreRutaBC p)
        {
            try
            {
                data.CargarSqlComando("[dbo].[GUARDAR_PRE_RUTA_V2]");
                if (p.ID != 0)
                    data.AgregarSqlParametro("@ID_PRERUTA", p.ID);
                if (p.HORARIO.HORA_ID != 0)
                    data.AgregarSqlParametro("@HORA_ID", p.HORARIO.HORA_ID);
                if (!string.IsNullOrEmpty(p.NUMERO))
                    data.AgregarSqlParametro("@NUMERO", p.NUMERO);
                if (p.FECHA_DESPACHOEXP != DateTime.MinValue)
                    data.AgregarSqlParametro("@FECHA", p.FECHA_DESPACHOEXP);
                if (p.TRAILER.TRAI_ID != 0)
                    data.AgregarSqlParametro("@TRAI_ID", p.TRAILER.TRAI_ID);
                if (p.TRACTO.TRAC_ID != 0)
                    data.AgregarSqlParametro("@TRAC_ID", p.TRACTO.TRAC_ID);
                if (p.CONDUCTOR.COND_ID != 0)
                    data.AgregarSqlParametro("@COND_ID", p.CONDUCTOR.COND_ID);
                if (!string.IsNullOrEmpty(p.RUTA_COLOR))
                    data.AgregarSqlParametro("@RUTA_COLOR", p.RUTA_COLOR);
                if (!string.IsNullOrEmpty(p.RETORNO))
                {
                    data.AgregarSqlParametro("@RETORNO", p.RETORNO);
                    data.AgregarSqlParametro("@TIEMPO_RETORNO", p.TIEMPO_RETORNO);
                }

                if (!string.IsNullOrEmpty(p.ORIGEN.ID.ToString()))
                    data.AgregarSqlParametro("@id_origen", p.ORIGEN.ID.ToString());
                data.EjecutaSqlInsertIdentity();
                p.ID = data.ID;
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
        internal bool PreRuta_Eliminar(long ruta_id)
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
        internal bool PreRuta_EliminarMultiple(string ruta_id)
        {
            try
            {
                data.CargarSqlComando("[dbo].[ELIMINAR_PRE_RUTA_MULTIPLE]");
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
        internal DataTable PreRuta_IngresarExcel(DataTable dt, int usua_id)
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
        internal DataTable PreRuta_ProcesarExcel(int usua_id)
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
                    if (data.SqlLectorDatos["PARA_ID"] != DBNull.Value)
                        p.PARA_ID = Convert.ToInt32(data.SqlLectorDatos["PARA_ID"]);
                    if (data.SqlLectorDatos["PARA_NOMBRE"] != DBNull.Value)
                        p.PARA_NOMBRE = Convert.ToString(data.SqlLectorDatos["PARA_NOMBRE"]);
                    if (data.SqlLectorDatos["PARA_OBS"] != DBNull.Value)
                        p.PARA_OBS = Convert.ToString(data.SqlLectorDatos["PARA_OBS"]);
                    if (data.SqlLectorDatos["PARA_VALOR"] != DBNull.Value)
                        p.PARA_VALOR = Convert.ToString(data.SqlLectorDatos["PARA_VALOR"]);
                    if (data.SqlLectorDatos["USUA_ID_CREACION"] != DBNull.Value)
                        p.USUA_ID_CREACION = Convert.ToInt32(data.SqlLectorDatos["USUA_ID_CREACION"]);
                    if (data.SqlLectorDatos["PARA_FH_CREACION"] != DBNull.Value)
                        p.PARA_FH_CREACION = Convert.ToDateTime(data.SqlLectorDatos["PARA_FH_CREACION"]);
                    if (data.SqlLectorDatos["USUA_ID_MODIFICACION"] != DBNull.Value)
                        p.USUA_ID_MODIFICACION = Convert.ToInt32(data.SqlLectorDatos["USUA_ID_MODIFICACION"]);
                    if (data.SqlLectorDatos["PARA_FH_MODIFICACION"] != DBNull.Value)
                        p.PARA_FH_MODIFICACION = Convert.ToDateTime(data.SqlLectorDatos["PARA_FH_MODIFICACION"]);
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
        #region Respuesta
        internal bool Respuesta_Guardar(DataTable dt, long id_ruta)
        {
            try
            {
                data.CargarSqlComando("[dbo].[GUARDAR_RUTA_RESPUESTAS]");
                data.AgregarSqlParametro("@ID_RUTA", id_ruta);
                data.AgregarSqlParametro("@RURE_RESPUESTA", dt);
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
        internal bool Respuesta_Guardar(string respuesta, long id_ruta)
        {
            try
            {
                data.CargarSqlComando("[dbo].[GUARDAR_RUTA_RESPUESTA]");
                data.AgregarSqlParametro("@RUTA_ID", id_ruta);
                data.AgregarSqlParametro("@RURE_RESPUESTA", respuesta);
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
        internal bool Respuesta_Eliminar(long rure_id, long ruta_id)
        {
            try
            {
                data.CargarSqlComando("[dbo].[ELIMINAR_RUTA_RESPUESTAS]");
                if (rure_id != 0)
                    data.AgregarSqlParametro("@RURE_ID", rure_id);
                if (ruta_id != 0)
                    data.AgregarSqlParametro("@RUTA_ID", ruta_id);
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
        #endregion
        #region reporte
        public DataTable obrenerReporteDespachoViaje(string ids)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_PRERUTA_DETALLE]");
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
                if (ids != "")
                    data.AgregarSqlParametro("@ID_PRERUTA", ids);
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
        #region Tracto
        internal DataTable Tracto_ObtenerTodo(DateTime fecha, int hora_id, string trac_placa)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_TRACTO]");
                if (fecha != DateTime.MinValue)
                {
                    data.AgregarSqlParametro("@fecha", fecha);
                    data.AgregarSqlParametro("@hora_id", hora_id);
                }
                if (!string.IsNullOrEmpty(trac_placa))
                    data.AgregarSqlParametro("@trac_placa", trac_placa);
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
        internal TractoBC Tracto_ObtenerXId(int trac_id)
        {
            try
            {
                TractoBC t = new TractoBC();
                data.CargarSqlComando("[dbo].[LISTAR_TRACTO]");
                data.AgregarSqlParametro("@trac_id", trac_id);
                data.EjecutarSqlLector();
                while (data.SqlLectorDatos.Read())
                {
                    if (data.SqlLectorDatos["TRAC_ID"] != DBNull.Value)
                        t.TRAC_ID = Convert.ToInt32(data.SqlLectorDatos["TRAC_ID"]);
                    if (data.SqlLectorDatos["TRAC_PLACA"] != DBNull.Value)
                        t.TRAC_PLACA = Convert.ToString(data.SqlLectorDatos["TRAC_PLACA"]);
                    if (data.SqlLectorDatos["TRAC_FH_CREACION"] != DBNull.Value)
                        t.TRAC_FH_CREACION = Convert.ToDateTime(data.SqlLectorDatos["TRAC_FH_CREACION"]);
                }
                return t;
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
        internal TractoBC Tracto_ObtenerXPlaca(string trac_placa)
        {
            try
            {
                TractoBC t = new TractoBC();
                data.CargarSqlComando("[dbo].[LISTAR_TRACTO_X_PLACA]");
                data.AgregarSqlParametro("@trac_placa", trac_placa);
                data.EjecutarSqlLector();
                while (data.SqlLectorDatos.Read())
                {
                    if (data.SqlLectorDatos["TRAC_ID"] != DBNull.Value)
                        t.TRAC_ID = Convert.ToInt32(data.SqlLectorDatos["TRAC_ID"]);
                    if (data.SqlLectorDatos["TRAC_PLACA"] != DBNull.Value)
                        t.TRAC_PLACA = Convert.ToString(data.SqlLectorDatos["TRAC_PLACA"]);
                    if (data.SqlLectorDatos["TRAC_FH_CREACION"] != DBNull.Value)
                        t.TRAC_FH_CREACION = Convert.ToDateTime(data.SqlLectorDatos["TRAC_FH_CREACION"]);
                }
                return t;
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
        internal bool Tracto_Guardar(TractoBC t)
        {
            try
            {
                data.CargarSqlComando("[dbo].[GUARDAR_TRACTO]");
                if (t.TRAC_ID != 0)
                    data.AgregarSqlParametro("@TRAC_ID", t.TRAC_ID);
                data.AgregarSqlParametro("@TRAC_PLACA", t.TRAC_PLACA);
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
        internal bool Tracto_Eliminar(int trac_id)
        {
            try
            {
                data.CargarSqlComando("[dbo].[ELIMINAR_TRACTO]");
                data.AgregarSqlParametro("@TRAC_ID", trac_id);
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
        #region Trailer
        internal DataTable Trailer_ObtenerTodo(DateTime fecha, int hora_id, string trai_numero, string trai_placa, int trti_id)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_TRAILER]");
                if (fecha != DateTime.MinValue)
                    data.AgregarSqlParametro("@fecha", fecha);
                if (hora_id != 0)
                    data.AgregarSqlParametro("@hora_id", hora_id);
                if (!string.IsNullOrEmpty(trai_placa))
                    data.AgregarSqlParametro("@trai_placa", trai_placa);
                if (!string.IsNullOrEmpty(trai_numero))
                    data.AgregarSqlParametro("@trai_numero", trai_numero);
                if (trti_id != 0)
                    data.AgregarSqlParametro("@trti_id", trti_id);
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
        internal TrailerBC Trailer_ObtenerXId(int trai_id)
        {
            try
            {
                TrailerBC t = new TrailerBC();
                data.CargarSqlComando("[dbo].[LISTAR_TRAILER]");
                data.AgregarSqlParametro("@trai_id", trai_id);
                data.EjecutarSqlLector();
                while (data.SqlLectorDatos.Read())
                {
                    if (data.SqlLectorDatos["TRAI_ID"] != DBNull.Value)
                        t.TRAI_ID = Convert.ToInt32(data.SqlLectorDatos["TRAI_ID"]);
                    if (data.SqlLectorDatos["TRAI_COD"] != DBNull.Value)
                        t.TRAI_COD = Convert.ToString(data.SqlLectorDatos["TRAI_COD"]);
                    if (data.SqlLectorDatos["TRAI_NUMERO"] != DBNull.Value)
                        t.TRAI_NUMERO = Convert.ToString(data.SqlLectorDatos["TRAI_NUMERO"]);
                    if (data.SqlLectorDatos["TRAI_PLACA"] != DBNull.Value)
                        t.TRAI_PLACA = Convert.ToString(data.SqlLectorDatos["TRAI_PLACA"]);
                    if (data.SqlLectorDatos["TRTI_ID"] != DBNull.Value)
                        t.TRAILER_TIPO.TRTI_ID = Convert.ToInt32(data.SqlLectorDatos["TRTI_ID"]);
                    if (data.SqlLectorDatos["TRTI_DESC"] != DBNull.Value)
                        t.TRAILER_TIPO.TRTI_DESC = Convert.ToString(data.SqlLectorDatos["TRTI_DESC"]);
                    if (data.SqlLectorDatos["TRTI_COLOR"] != DBNull.Value)
                        t.TRAILER_TIPO.TRTI_COLOR = Convert.ToString(data.SqlLectorDatos["TRTI_COLOR"]);
                }
                return t;
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
        internal TrailerBC Trailer_ObtenerXPlaca(string trai_numero, string trai_placa)
        {
            try
            {
                TrailerBC t = new TrailerBC();
                data.CargarSqlComando("[dbo].[LISTAR_TRAILER_X_PLACA]");
                if (!string.IsNullOrEmpty(trai_placa))
                    data.AgregarSqlParametro("@trai_placa", trai_placa);
                if (!string.IsNullOrEmpty(trai_numero))
                    data.AgregarSqlParametro("@trai_numero", trai_numero);
                data.EjecutarSqlLector();
                while (data.SqlLectorDatos.Read())
                {
                    if (data.SqlLectorDatos["TRAI_ID"] != DBNull.Value)
                        t.TRAI_ID = Convert.ToInt32(data.SqlLectorDatos["TRAI_ID"]);
                    if (data.SqlLectorDatos["TRAI_COD"] != DBNull.Value)
                        t.TRAI_COD = Convert.ToString(data.SqlLectorDatos["TRAI_COD"]);
                    if (data.SqlLectorDatos["TRAI_NUMERO"] != DBNull.Value)
                        t.TRAI_NUMERO = Convert.ToString(data.SqlLectorDatos["TRAI_NUMERO"]);
                    if (data.SqlLectorDatos["TRAI_PLACA"] != DBNull.Value)
                        t.TRAI_PLACA = Convert.ToString(data.SqlLectorDatos["TRAI_PLACA"]);
                    if (data.SqlLectorDatos["TRTI_ID"] != DBNull.Value)
                        t.TRAILER_TIPO.TRTI_ID = Convert.ToInt32(data.SqlLectorDatos["TRTI_ID"]);
                    if (data.SqlLectorDatos["TRTI_DESC"] != DBNull.Value)
                        t.TRAILER_TIPO.TRTI_DESC = Convert.ToString(data.SqlLectorDatos["TRTI_DESC"]);
                    if (data.SqlLectorDatos["TRTI_COLOR"] != DBNull.Value)
                        t.TRAILER_TIPO.TRTI_COLOR = Convert.ToString(data.SqlLectorDatos["TRTI_COLOR"]);
                }
                return t;
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
        internal bool Trailer_Guardar(TrailerBC t)
        {
            try
            {
                data.CargarSqlComando("[dbo].[GUARDAR_TRAILER]");
                if (t.TRAI_ID != 0)
                    data.AgregarSqlParametro("@TRAI_ID", t.TRAI_ID);
                data.AgregarSqlParametro("@TRAI_COD", t.TRAI_COD);
                data.AgregarSqlParametro("@TRAI_PLACA", t.TRAI_PLACA);
                data.AgregarSqlParametro("@TRAI_NUMERO", t.TRAI_NUMERO);
                data.AgregarSqlParametro("@TRTI_ID", t.TRAILER_TIPO.TRTI_ID);

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
        internal bool Trailer_Eliminar(int trai_id)
        {
            try
            {
                data.CargarSqlComando("[dbo].[ELIMINAR_TRAILER]");
                data.AgregarSqlParametro("@trai_id", trai_id);
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
        #region TrailerTipo
        internal DataTable TrailerTipo_ObtenerTodo(string trti_desc)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_TRAILER_TIPO]");
                if (!string.IsNullOrEmpty(trti_desc))
                    data.AgregarSqlParametro("@trti_desc", trti_desc);
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
        internal TrailerTipoBC TrailerTipo_ObtenerXId(int trti_id)
        {
            try
            {
                TrailerTipoBC t = new TrailerTipoBC();
                data.CargarSqlComando("[dbo].[LISTAR_TRAILER_TIPO]");
                data.AgregarSqlParametro("@trti_id", trti_id);
                data.EjecutarSqlLector();
                while (data.SqlLectorDatos.Read())
                {
                    if (data.SqlLectorDatos["TRTI_ID"] != DBNull.Value)
                        t.TRTI_ID = Convert.ToInt32(data.SqlLectorDatos["TRTI_ID"]);
                    if (data.SqlLectorDatos["TRTI_DESC"] != DBNull.Value)
                        t.TRTI_DESC = Convert.ToString(data.SqlLectorDatos["TRTI_DESC"]);
                    if (data.SqlLectorDatos["TRTI_COLOR"] != DBNull.Value)
                        t.TRTI_COLOR = Convert.ToString(data.SqlLectorDatos["TRTI_COLOR"]);
                }
                return t;
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
        }  internal bool TrailerTipo_Guardar(TrailerTipoBC t)
        {
            try
            {
                data.CargarSqlComando("[dbo].[GUARDAR_TRAILER_TIPO]");
                if (t.TRTI_ID != 0)
                    data.AgregarSqlParametro("@TRTI_ID", t.TRTI_ID);
                data.AgregarSqlParametro("@TRTI_DESC", t.TRTI_DESC);
                data.AgregarSqlParametro("@TRTI_COLOR", t.TRTI_COLOR);

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
        internal bool TrailerTipo_Eliminar(int trti_id)
        {
            try
            {
                data.CargarSqlComando("[dbo].[ELIMINAR_TRAILER_TIPO]");
                data.AgregarSqlParametro("@TRTI_ID", trti_id);
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
                    if (data.SqlLectorDatos["USTI_ID"] != DBNull.Value)
                        u.USTI_ID = Convert.ToInt32(data.SqlLectorDatos["USTI_ID"]);
                    if (data.SqlLectorDatos["USTI_NOMBRE"] != DBNull.Value)
                        u.USTI_NOMBRE = Convert.ToString(data.SqlLectorDatos["USTI_NOMBRE"]);
                    if (data.SqlLectorDatos["USTI_DESC"] != DBNull.Value)
                        u.USTI_DESC = Convert.ToString(data.SqlLectorDatos["USTI_DESC"]);
                    if (data.SqlLectorDatos["USTI_NIVEL_PERMISOS"] != DBNull.Value)
                        u.USTI_NIVEL_PERMISOS = Convert.ToInt32(data.SqlLectorDatos["USTI_NIVEL_PERMISOS"]);
                    if (data.SqlLectorDatos["MENU_ID"] != DBNull.Value)
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

    }
}