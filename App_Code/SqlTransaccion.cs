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
        internal DataTable Conductor_ObtenerTodo(bool cond_activo, bool cond_bloqueado, string cond_rut, string cond_nombre)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_CONDUCTOR]");
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
        internal DataTable Conductor_ObtenerTodo(string cond_rut, string cond_nombre)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_CONDUCTOR]");
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
        internal DataTable PreRuta_ObtenerTodo(DateTime desde, DateTime hasta, int hora_id = 0, int regi_id = 0, int ciud_id = 0, int comu_id = 0, int usua_id = 0, string peru_numero = null, string envio = null)
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
        internal PreRutaBC PreRuta_ObtenerXId(int id_preruta)
        {
            try
            {
                PreRutaBC pr = new PreRutaBC();
                data.CargarSqlComando("[dbo].[LISTAR_PRERUTA]");
                data.AgregarSqlParametro("@ID_PRERUTA", id_preruta);
                data.EjecutarSqlLector();
                while (data.SqlLectorDatos.Read())
                {
                    if (data.SqlLectorDatos["NUMERO"] != DBNull.Value)
                        pr.NUMERO = Convert.ToString(data.SqlLectorDatos["NUMERO"]);
                    if (data.SqlLectorDatos["FH_VIAJE"] != DBNull.Value)
                        pr.FH_VIAJE = Convert.ToDateTime(data.SqlLectorDatos["FH_VIAJE"]);
                    if (data.SqlLectorDatos["ID_MOVIL"] != DBNull.Value)
                        pr.ID_MOVIL = Convert.ToInt32(data.SqlLectorDatos["ID_MOVIL"]);
                    if (data.SqlLectorDatos["ID_ESTADO"] != DBNull.Value)
                        pr.ID_ESTADO = Convert.ToInt32(data.SqlLectorDatos["ID_ESTADO"]);
                    if (data.SqlLectorDatos["OBSERVACION"] != DBNull.Value)
                        pr.OBSERVACION = Convert.ToString(data.SqlLectorDatos["OBSERVACION"]);
                    if (data.SqlLectorDatos["ID_TIPOVIAJE"] != DBNull.Value)
                        pr.ID_TIPOVIAJE = Convert.ToInt32(data.SqlLectorDatos["ID_TIPOVIAJE"]);
                    if (data.SqlLectorDatos["RETORNO"] != DBNull.Value)
                        pr.RETORNO = Convert.ToString(data.SqlLectorDatos["RETORNO"]);
                    if (data.SqlLectorDatos["FH_CREACION"] != DBNull.Value)
                        pr.FH_CREACION = Convert.ToDateTime(data.SqlLectorDatos["FH_CREACION"]);
                    if (data.SqlLectorDatos["FH_UPDATE"] != DBNull.Value)
                        pr.FH_UPDATE = Convert.ToDateTime(data.SqlLectorDatos["FH_UPDATE"]);
                    if (data.SqlLectorDatos["FH_RETORNO"] != DBNull.Value)
                        pr.FH_RETORNO = Convert.ToDateTime(data.SqlLectorDatos["FH_RETORNO"]);
                    if (data.SqlLectorDatos["FH_SALIDA"] != DBNull.Value)
                        pr.FH_SALIDA = Convert.ToDateTime(data.SqlLectorDatos["FH_SALIDA"]);
                    if (data.SqlLectorDatos["TOTAL_KG"] != DBNull.Value)
                        pr.TOTAL_KG = Convert.ToDecimal(data.SqlLectorDatos["TOTAL_KG"]);
                    if (data.SqlLectorDatos["CORREO_GPS"] != DBNull.Value)
                        pr.CORREO_GPS = Convert.ToBoolean(data.SqlLectorDatos["CORREO_GPS"]);
                    if (data.SqlLectorDatos["ID_CLIENTE_GPS"] != DBNull.Value)
                        pr.ID_CLIENTE_GPS = Convert.ToInt32(data.SqlLectorDatos["ID_CLIENTE_GPS"]);
                    if (data.SqlLectorDatos["RUTA"] != DBNull.Value)
                        pr.RUTA = Convert.ToString(data.SqlLectorDatos["RUTA"]);
                    if (data.SqlLectorDatos["FECHA_PRESENTACION"] != DBNull.Value)
                        pr.FECHA_PRESENTACION = Convert.ToDateTime(data.SqlLectorDatos["FECHA_PRESENTACION"]);
                    if (data.SqlLectorDatos["FECHA_INICIOCARGA"] != DBNull.Value)
                        pr.FECHA_INICIOCARGA = Convert.ToDateTime(data.SqlLectorDatos["FECHA_INICIOCARGA"]);
                    if (data.SqlLectorDatos["FECHA_FINCARGA"] != DBNull.Value)
                        pr.FECHA_FINCARGA = Convert.ToDateTime(data.SqlLectorDatos["FECHA_FINCARGA"]);
                    if (data.SqlLectorDatos["FECHA_DESPACHOEXP"] != DBNull.Value)
                        pr.FECHA_DESPACHOEXP = Convert.ToDateTime(data.SqlLectorDatos["FECHA_DESPACHOEXP"]);
                    if (data.SqlLectorDatos["FECHA_INICIOEXP"] != DBNull.Value)
                        pr.FECHA_INICIOEXP = Convert.ToDateTime(data.SqlLectorDatos["FECHA_INICIOEXP"]);
                    if (data.SqlLectorDatos["FECHA_FINEXP"] != DBNull.Value)
                        pr.FECHA_FINEXP = Convert.ToDateTime(data.SqlLectorDatos["FECHA_FINEXP"]);
                    // Conductor
                    if (data.SqlLectorDatos["ID_CONDUCTOR"] != DBNull.Value)
                        pr.CONDUCTOR.COND_ID = Convert.ToInt32(data.SqlLectorDatos["ID_CONDUCTOR"]);
                    if (data.SqlLectorDatos["COND_RUT"] != DBNull.Value)
                        pr.CONDUCTOR.COND_RUT = Convert.ToString(data.SqlLectorDatos["COND_RUT"]);
                    if (data.SqlLectorDatos["COND_NOMBRE"] != DBNull.Value)
                        pr.CONDUCTOR.COND_NOMBRE = Convert.ToString(data.SqlLectorDatos["COND_NOMBRE"]);
                    // Origen
                    if (data.SqlLectorDatos["ID_ORIGEN"] != DBNull.Value)
                        pr.ORIGEN.ID = Convert.ToInt32(data.SqlLectorDatos["ID_ORIGEN"]);
                    if (data.SqlLectorDatos["ORIGEN_NOMBRE"] != DBNull.Value)
                        pr.ORIGEN.NOMBRE_PE = Convert.ToString(data.SqlLectorDatos["ORIGEN_NOMBRE"]);
                    if (data.SqlLectorDatos["ORIGEN_DIRECCION"] != DBNull.Value)
                        pr.ORIGEN.DIRECCION_PE = Convert.ToString(data.SqlLectorDatos["ORIGEN_DIRECCION"]);
                    if (data.SqlLectorDatos["ORIGEN_LAT"] != DBNull.Value)
                        pr.ORIGEN.LAT_PE = Convert.ToDecimal(data.SqlLectorDatos["ORIGEN_LAT"]);
                    if (data.SqlLectorDatos["ORIGEN_LON"] != DBNull.Value)
                        pr.ORIGEN.LON_PE = Convert.ToDecimal(data.SqlLectorDatos["ORIGEN_LON"]);
                    // Operacion
                    if (data.SqlLectorDatos["ID_OPE"] != DBNull.Value)
                        pr.OPERACION.OPER_ID = Convert.ToInt32(data.SqlLectorDatos["ID_OPE"]);
                    // Envio
                    if (data.SqlLectorDatos["ID_ENVIO"] != DBNull.Value)
                        pr.ENVIO.Env_ID = Convert.ToInt32(data.SqlLectorDatos["ID_ENVIO"]);
                    // Trailer
                    if (data.SqlLectorDatos["TRAI_ID"] != DBNull.Value)
                        pr.TRAILER.TRAI_ID = Convert.ToInt32(data.SqlLectorDatos["TRAI_ID"]);
                    if (data.SqlLectorDatos["TRAI_PLACA"] != DBNull.Value)
                        pr.TRAILER.TRAI_PLACA = Convert.ToString(data.SqlLectorDatos["TRAI_PLACA"]);
                    // Tracto
                    if (data.SqlLectorDatos["TRAC_ID"] != DBNull.Value)
                        pr.TRACTO.TRAC_ID = Convert.ToInt32(data.SqlLectorDatos["TRAC_ID"]);
                    if (data.SqlLectorDatos["TRAC_PLACA"] != DBNull.Value)
                        pr.TRACTO.TRAC_PLACA = Convert.ToString(data.SqlLectorDatos["TRAC_PLACA"]);
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
        internal bool PreRuta_GuardarPuntos(int id_preruta, string id_destinos, string tiempos, string hora_salida)
        {
            try
            {
                data.CargarSqlComando("[dbo].[GUARDAR_PUNTOS_PRE_RUTA]");
                if (id_preruta != 0) 
                    data.AgregarSqlParametro("@ID_RUTA", id_preruta);
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
        internal bool PreRuta_GuardarDetalle(PreRutaBC p)
        {
            try
            {
                data.CargarSqlComando("[dbo].[GUARDAR_PRE_RUTA]");
                data.AgregarSqlParametro("@ID_PRERUTA", p.ID);
                if (p.TRAILER.TRAI_ID != 0) 
                    data.AgregarSqlParametro("@TRAI_ID", p.TRAILER.TRAI_ID);
                if (p.TRACTO.TRAC_ID != 0) 
                    data.AgregarSqlParametro("@TRAC_ID", p.TRACTO.TRAC_ID);
                if (p.CONDUCTOR.COND_ID != 0) 
                    data.AgregarSqlParametro("@COND_ID", p.CONDUCTOR.COND_ID);
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
        #region Tracto
        internal DataTable Tracto_ObtenerTodo(string trac_placa)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_TRACTO]");
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
        internal DataTable Trailer_ObtenerTodo(string trai_numero, string trai_placa, int trti_id)
        {
            try
            {
                data.CargarSqlComando("[dbo].[LISTAR_TRAILER]");
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

    }
}