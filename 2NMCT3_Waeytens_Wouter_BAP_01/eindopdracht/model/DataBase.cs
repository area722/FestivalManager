using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindopdracht.model
{
    class DataBase
    {
        //system configuration referencen
        private static ConnectionStringSettings ConnectionStringSetting
        {
            get { return ConfigurationManager.ConnectionStrings["ConnectionString"]; }
        }

        private static DbConnection Getconnection()
        {
            DbConnection con = DbProviderFactories.GetFactory(ConnectionStringSetting.ProviderName).CreateConnection();
            con.ConnectionString = ConnectionStringSetting.ConnectionString;
            con.Open();
            return con;
        }

        public static void releaseConnection(DbConnection con)
        {
            if (con != null)
            {
                con.Close();
                con = null;
            }
        }

        private static DbCommand BuildCommand(string sql, params DbParameter[] parameters)
        {
            DbCommand command = Getconnection().CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = sql;
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            return command;
        }

        public static DbDataReader GetData(string sql, params DbParameter[] parameters)
        {
            DbCommand command = null;
            DbDataReader reader = null;
            try
            {
                command = BuildCommand(sql, parameters);
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                return reader;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (reader != null)
                {
                    reader.Close();
                    if (command != null)
                    {
                        releaseConnection(command.Connection);
                    }
                }
                throw;
            }
        }

        public static int modifyData(string sql, params DbParameter[] parameters)
        {
            DbCommand command = null;
            try
            {
                command = BuildCommand(sql, parameters);
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (command != null)
                {
                    releaseConnection(command.Connection);
                    return 0;
                }
                throw;
            }
        }

        public static DbParameter addparameter(string name, object value)
        {
            DbParameter par = DbProviderFactories.GetFactory(ConnectionStringSetting.ProviderName).CreateParameter();
            par.ParameterName = name;
            par.Value = value;
            return par;
        }

        public static DbTransaction BeginTransaction()
        {
            DbConnection con = null;
            try
            {
                con = Getconnection();
                return con.BeginTransaction();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                releaseConnection(con);
                throw;
            }
        }

        #region transaction

        private static DbCommand BuildCommand(DbTransaction trans, string sql, params DbParameter[] parameters)
        {
            DbCommand command = trans.Connection.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = sql;
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            return command;
        }

        public static DbDataReader GetData(DbTransaction trans, string sql, params DbParameter[] parameters)
        {
            DbCommand command = null;
            DbDataReader reader = null;
            try
            {
                command = BuildCommand(trans, sql, parameters);
                command.Transaction = trans;
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                return reader;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (reader != null)
                {
                    reader.Close();
                    if (command != null)
                    {
                        releaseConnection(command.Connection);
                    }
                }
                throw;
            }
        }

        public static int modifyData(DbTransaction trans, string sql, params DbParameter[] parameters)
        {
            DbCommand command = null;
            try
            {
                command = BuildCommand(trans, sql, parameters);
                command.Transaction = trans;
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (command != null)
                {
                    releaseConnection(command.Connection);
                    return 0;
                }
                throw;
            }
        }

        #endregion
    }
}
