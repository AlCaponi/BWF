using System;
//using System.Data.Odbc;
using System.Data.OleDb;
using System.Collections.Generic;

namespace AyrA.SQL
{
    /// <summary>
    /// Provides an easy-to-use SQL Odbc Interface
    /// </summary>
    public class SQLInterface : IDisposable
    {
        public const bool THROW = true;

        /// <summary>
        /// The way, Date is formatted
        /// </summary>
        public const string D_FORMAT = "yyy-MM-dd";
        /// <summary>
        /// The way, Time is formatted
        /// </summary>
        public const string T_FORMAT = "HH:mm:ss";
        /// <summary>
        /// The way, Date and Time are formatted
        /// </summary>
        public const string DT_FORMAT = "yyyy-MM-dd HH:mm:ss";

        private string DSN;
        private OleDbConnection OC;

        /// <summary>
        /// Initializes the Interface and opens the SQL Connection
        /// </summary>
        /// <param name="OdbcString">ODBC compatible connection string</param>
        public SQLInterface(string OdbcString)
        {
            OC = new OleDbConnection(DSN = OdbcString);
            OC.Open();
        }

        ~SQLInterface()
        {
            DSN = null;
        }

        /// <summary>
        /// Executes an SQL query and returns the number of rows affected
        /// </summary>
        /// <param name="SQL">SQL query</param>
        /// <returns>number of affected rows</returns>
        public int Exec(string SQL)
        {
            return Exec(SQL, null);
        }

        /// <summary>
        /// Executes an SQL query and returns the number of rows affected
        /// </summary>
        /// <param name="SQL">SQL query</param>
        /// <param name="Args">Query arguments</param>
        /// <returns>number of affected rows</returns>
        public int Exec(string SQL, params object[] Args)
        {
            OleDbCommand CMD = OC.CreateCommand();
            int result = 0;

            CMD.CommandText = SQL;
            if (Args != null)
            {
                for (int i = 0; i < Args.Length; i++)
                {
                    if (Args[i] is DateTime)
                    {
                        CMD.Parameters.Add(new OleDbParameter("@arg" + i, ((DateTime)Args[i]).ToString(DT_FORMAT)));
                    }
                    else
                    {
                        CMD.Parameters.Add(new OleDbParameter("@arg" + i, Args[i] == null ? DBNull.Value : Args[i]));
                    }
                }
            }

            try
            {
                result = CMD.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                if (THROW)
                {
                    throw ex;
                }
                return -1;
            }
            finally
            {
                CMD.Dispose();
            }

            return result;
        }

        /// <summary>
        /// Executes a SELECT query
        /// </summary>
        /// <param name="SQL">SELECT query</param>
        /// <returns>SQL Rowset</returns>
        public SQLRow[] ExecReader(string SQL)
        {
            return ExecReader(SQL, null);
        }

        /// <summary>
        /// Executes a SELECT query
        /// </summary>
        /// <param name="SQL">SELECT query</param>
        /// <param name="Args">Query arguments</param>
        /// <returns>SQL Rowset</returns>
        public SQLRow[] ExecReader(string SQL, params object[] Args)
        {
            OleDbCommand CMD = OC.CreateCommand();
            OleDbDataReader reader = null;

            CMD.CommandText = SQL;
            if (Args != null)
            {
                for (int i = 0; i < Args.Length; i++)
                {
                    if (Args[i] is DateTime)
                    {
                        CMD.Parameters.Add(new OleDbParameter("@arg" + i, ((DateTime)Args[i]).ToString(DT_FORMAT)));
                    }
                    else
                    {
                        CMD.Parameters.Add(new OleDbParameter("@arg" + i, Args[i] == null ? DBNull.Value : Args[i]));
                    }
                }
            }

            try
            {
                reader = CMD.ExecuteReader();
            }
            catch(Exception ex)
            {
                CMD.Dispose();
                if (THROW)
                {
                    throw ex;
                }
            }

            List<SQLRow> SR = new List<SQLRow>();

            while (reader.Read())
            {
                SQLRow SRR = new SQLRow();
                for (int j = 0; j < reader.FieldCount; j++)
                {
                    SRR.Add(reader.GetName(j), reader.GetDataTypeName(j), reader.IsDBNull(j) ? null : reader[j]);
                }
                SR.Add(SRR);
            }

            reader.Close();
            reader.Dispose();

            CMD.Dispose();

            return SR.ToArray();
        }

        /// <summary>
        /// Closes the Connection and disposes all Objects
        /// </summary>
        public void Dispose()
        {
            if (OC != null)
            {
                OC.Close();
                OC.Dispose();
                OC = null;
            }
        }

        public override string ToString()
        {
            return string.Format("SQLInterface: {0}", DSN);
        }

        public static bool IsNull(object o)
        {
            return o == null || Convert.IsDBNull(o) || DBNull.Value.Equals(o);
        }
    }
}
