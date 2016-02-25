using System.Collections.Generic;
using System;

namespace AyrA.SQL
{
    public class SQLRow
    {
        private Dictionary<string, object> _cols;
        private List<string> _names;
        private List<string> _dt;

        public List<string> Names
        {
            get
            {
                return _names;
            }
        }
        public List<string> DataTypes
        {
            get
            {
                return _dt;
            }
        }
        public Dictionary<string, object> Values
        {
            get
            {
                return _cols;
            }
        }

        public object this[string ColumnName]
        {
            get
            {
                try
                {
                    return Values[ColumnName];
                }
                catch
                {
                    throw new Exception("Error getting key '" + ColumnName + "' from collection. Keys: " + string.Join(", ", this.Names.ToArray()));
                }
            }
        }

        public object this[int ColumnIndex]
        {
            get
            {
                return Values[Names[ColumnIndex]];
            }
        }

        public SQLRow()
        {
            _cols = new Dictionary<string, object>();
            _names = new List<string>();
            _dt = new List<string>();
        }

        ~SQLRow()
        {
            _cols = null;
            _names = null;
        }

        public void Add(string col, string typeName, object value)
        {
            _cols.Add(col, value);
            _dt.Add(typeName);
            _names.Add(col);
        }
    }
}
