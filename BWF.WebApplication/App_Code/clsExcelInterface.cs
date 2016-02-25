using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;

namespace AyrA.SQL
{
    /// <summary>
    /// Provides an easy-to-use SQL Odbc Interface
    /// </summary>
    public class ExcelInterface : IDisposable
    {
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

        public string[] Tables
        {
            get
            {
                return _tbl;
            }
            private set
            {
                _tbl = value;
            }
        }

        private string[] _tbl;
        private string DSN;
        private OleDbConnection OC;

        /// <summary>
        /// Initializes the Interface and opens the SQL Connection
        /// </summary>
        /// <param name="OdbcString">ODBC compatible connection string</param>
        public ExcelInterface(string FileName)
        {
            OC = new OleDbConnection(DSN = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0}; Extended Properties=\"Excel 8.0;HDR=Yes\";", FileName));
            OC.Open();

            using (DataTable DT = OC.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" }))
            {
                _tbl = new string[DT.Rows.Count];
                for (int i = 0; i < _tbl.Length; i++)
                {
                    _tbl[i] = DT.Rows[i]["TABLE_NAME"].ToString();
                }
            }
        }

        ~ExcelInterface()
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
                int j = 0;
                for (int i = 0; i < Args.Length; i++)
                {
                    ++j;
                    if (Args[i] is DateTime)
                    {
                        CMD.Parameters.Add(new OleDbParameter("@arg" + j, ((DateTime)Args[i]).ToString(DT_FORMAT)));
                    }
                    else if (Args[i].GetType().IsArray)
                    {
                        foreach (object o in (object[])Args[i])
                        {
                            CMD.Parameters.Add(new OleDbParameter("@arg" + j, o == null ? DBNull.Value : o));
                            ++j;
                        }
                    }
                    else
                    {
                        CMD.Parameters.Add(new OleDbParameter("@arg" + j, Args[i] == null ? DBNull.Value : Args[i]));
                    }
                }
            }

            try
            {
                result = CMD.ExecuteNonQuery();
            }
            catch
            {
                //NOOP
            }
            CMD.Dispose();

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
            catch
            {
                CMD.Dispose();

                return new SQLRow[0];
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

        public string[] GetColumns(string table)
        {
            List<string> Rows = new List<string>();
            DataTable tableColumns = OC.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, table, null });
            foreach (DataRow row in tableColumns.Rows)
            {
                Rows.Add((string)row["COLUMN_NAME"]);
                //var dateTypeColumn = row["DATA_TYPE"];
                //var ordinalPositionColumn = row["ORDINAL_POSITION"];
            }
            return Rows.ToArray();
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
            return string.Format("ExcelInterface: {0}", DSN);
        }
    }
}
