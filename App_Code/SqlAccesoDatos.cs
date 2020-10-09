using System;
using System.Data;
using System.Data.SqlClient;

namespace Qanalytics.Data.Access.SqlClient
{

    internal sealed class SqlAccesoDatos
    {
        private SqlConnection sqlConeccion;
        private SqlCommand sqlComando;
        private SqlDataReader sqlLectorDatos;

        private Int32 totalFilasAfectadas;
        private int id;

        public SqlAccesoDatos(String nombreConeccion)
        {
            try
            {
                this.sqlConeccion = new SqlConnection(Configuracion.CadenaConeccion(nombreConeccion));

                this.sqlComando = new SqlCommand();
                this.totalFilasAfectadas = 0;

            }
            catch (SqlException excepcion)
            {
                throw (excepcion);
            }
        }

        public void CargarSqlComando(String nombreProcedimiento)
        {
            try
            {
                this.sqlComando.Connection = this.sqlConeccion;

                this.sqlComando.CommandText = nombreProcedimiento;
                this.sqlComando.CommandType = System.Data.CommandType.StoredProcedure;
                this.LimpiarSqlParametros();
            }
            catch (SqlException excepcion)
            {
                throw (excepcion);
            }
        }

        public DataSet dsCargarSqlQuery(String Query)
        {
            SqlDataAdapter _adap = default(SqlDataAdapter);
            DataSet ds_ = new DataSet();
            _adap = new SqlDataAdapter(Query, sqlConeccion);

            try
            {
                _adap.SelectCommand.CommandTimeout = 1300;
                _adap.Fill(ds_, "Datos");
            }
            catch (SqlException excepcion)
            {
                throw (excepcion);
            }

            return ds_;
        }

        public SqlParameter AgregarSqlParametro(String nombreParametro, Object valorParametro)
        {
            try
            {
                SqlParameter sqlParametro = this.sqlComando.CreateParameter();
                sqlParametro.ParameterName = nombreParametro;

                if (valorParametro == null)
                {
                    sqlParametro.Value = DBNull.Value;
                }
                else sqlParametro.Value = valorParametro;
                this.sqlComando.Parameters.Add(sqlParametro);
                return sqlParametro;
            }
            catch (SqlException excepcion)
            {
                throw (excepcion);
            }

        }

        public void AgregarSqlParametro(String nombreParametro, SqlDbType tipo)
        {
            try
            {
                SqlParameter sqlParametro = this.sqlComando.CreateParameter();
                sqlParametro.ParameterName = nombreParametro;
                sqlParametro.SqlDbType = tipo;
                this.sqlComando.Parameters.Add(sqlParametro).Direction = ParameterDirection.Output;
            }
            catch (SqlException excepcion)
            {
                throw (excepcion);
            }
        }

        public void LimpiarSqlParametros()
        {
            this.sqlComando.Parameters.Clear();
        }

        public void EjecutarSqlEscritura()
        {
            try
            {

                this.sqlComando.Connection.Open();
                this.sqlComando.CommandTimeout = 120;
                //this.sqlComando.ExecuteScalar();
                this.totalFilasAfectadas = this.sqlComando.ExecuteNonQuery();
            }
            catch (SqlException excepcion)
            {
                throw (excepcion);
            }
            finally
            {
                this.sqlComando.Connection.Close();
            }
        }


        public DataTable EjecutarSqlquery2()
        {
            DataTable resultado = new DataTable();
            try
            {

                this.sqlComando.Connection.Open();
                this.sqlComando.CommandTimeout = 120;
                //this.sqlComando.ExecuteScalar();
                resultado.Load(this.sqlComando.ExecuteReader());
            }
            catch (SqlException excepcion)
            {
                throw (excepcion);
            }
            finally
            {
                this.sqlComando.Connection.Close();
            }

            return resultado;
        }


        public DataSet EjecutarSqlquery3()
        {
            SqlDataAdapter customerDA = new SqlDataAdapter();
            customerDA.SelectCommand = sqlComando;
            DataSet resultado = new DataSet();

            try
            {
                this.sqlComando.Connection.Open();
                this.sqlComando.CommandTimeout = 120;
                customerDA.Fill(resultado);
            }
            catch (SqlException excepcion)
            {
                throw (excepcion);
            }
            finally
            {
                this.sqlComando.Connection.Close();
            }

            return resultado;
        }
        public DataSet RetornaDS()
        {

            SqlDataAdapter _adap = default(SqlDataAdapter);
            DataSet ds_ = new DataSet();
            _adap = new SqlDataAdapter();
            _adap.SelectCommand = sqlComando;
            try
            {
                _adap.SelectCommand.CommandTimeout = 1300;
                _adap.Fill(ds_);
            }
            catch (SqlException excepcion)
            {
                throw (excepcion);
            }

            return ds_;
        }

        public void EjecutaSqlInsertIdentity()
        {
            try
            {

                this.sqlComando.Connection.Open();
                this.sqlComando.CommandTimeout = 120;
                //this.sqlComando.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                //this.sqlComando.ExecuteScalar();
                //this.id = (int)this.sqlComando.Parameters["@ID"].Value;
                this.id = Convert.ToInt32(this.sqlComando.ExecuteScalar());
                //this.totalFilasAfectadas = this.sqlComando.ExecuteNonQuery();
            }
            catch (SqlException excepcion)
            {
                throw (excepcion);
            }
            finally
            {
                this.sqlComando.Connection.Close();
            }
        }

        public string EjecutaSqlScalar()
        {
            try
            {

                this.sqlComando.Connection.Open();
                this.sqlComando.CommandTimeout = 120;
                //this.sqlComando.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                //this.sqlComando.ExecuteScalar();
                //this.id = (int)this.sqlComando.Parameters["@ID"].Value;
                return this.sqlComando.ExecuteScalar().ToString();
                //this.totalFilasAfectadas = this.sqlComando.ExecuteNonQuery();
            }
            catch (SqlException excepcion)
            {
                throw (excepcion);
            }
            finally
            {
                this.sqlComando.Connection.Close();
            }
        }

        public void EjecutarSqlLector()
        {
            try
            {
                this.sqlComando.Connection.Open();
                this.sqlComando.CommandTimeout = 120;
                this.sqlLectorDatos = this.sqlComando.ExecuteReader();
            }
            catch (SqlException excepcion)
            {
                throw (excepcion);
            }
        }

        public void CerrarSqlConeccion()
        {
            this.sqlComando.Connection.Close();
        }

        public int ID
        {
            get { return this.id; }
        }

        public SqlDataReader SqlLectorDatos
        {
            get { return sqlLectorDatos; }
            set { sqlLectorDatos = value; }
        }

        public object obtenerValorParametro(String nombreParametro)
        {
            return this.sqlComando.Parameters[nombreParametro].Value;
        }

        public SqlParameter AgregarSqlParametroOUT(String nombreParametro, Object valorParametro)
        {
            try
            {
                SqlParameter sqlParametro = this.sqlComando.CreateParameter();
                sqlParametro.ParameterName = nombreParametro;
                sqlParametro.Value = valorParametro;
                sqlParametro.Direction = ParameterDirection.Output;
                sqlParametro.Size = 9999;
                this.sqlComando.Parameters.Add(sqlParametro);
                return sqlParametro;
            }
            catch (SqlException excepcion)
            {
                throw (excepcion);
            }
        }

    }
}
