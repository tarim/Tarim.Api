using System;
using System.Collections.Generic;
using System.Data;

using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace Tarim.Api.Infrastructure.DataProvider
{
    internal class DataReaderWrapper : IDataReader
    {

        protected readonly IDataReader _reader;

        private Dictionary<string, int> _columns = null;

        public IDataReader Reader
        {
            get { return _reader; }
        }

        protected DataReaderWrapper( IDataReader reader)
        {
            _reader = reader;
  
        }

        public bool ContainsColumn(string name)
        {
            EnsureColumns();
            return _columns.ContainsKey(name);
        }

        private void EnsureColumns()
        {
            if (_columns == null)
            {
                _columns = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                CollectColumns(_columns, _reader);
            }
        }

        private static void CollectColumns(IDictionary<string, int> columns, IDataRecord reader)
        {
            for (var i = 0; i < reader.FieldCount; i++)
            {
                var fldName = reader.GetName(i);
                if (!columns.ContainsKey(fldName))
                    columns.Add(fldName, i);

            }
        }

        #region IDataReader implementation
        public int GetOrdinal(string name) // overriden by the wrapper
        {
            EnsureColumns();
            return _columns.ContainsKey(name) ? _columns[name] : -1;
        }

        public decimal GetDecimal(int i) // overriden by the wrapper
        {
            MySqlDataReader oReader = _reader as MySqlDataReader;
            if (oReader != null)
            {
                var oracleValue = oReader.GetMySqlDecimal(i);
                return Convert.ToDecimal(oracleValue.ToDouble()); // precision issue fix
            }

            return _reader.GetDecimal(i);
        }

        public object GetValue(int i) // overriden by the wrapper
        {
            try
            {
                return _reader.GetValue(i);
            }
            catch
            {
                var oReader = _reader as MySqlDataReader;
                if (oReader != null)
                {
                    object oracleValue = oReader.GetValue(i);
                    if (oracleValue is MySqlDecimal)
                        return Convert.ToDecimal(((MySqlDecimal)oracleValue).ToDouble());
                }
                throw;
            }
        }

        

        public void Close()
        {
            _reader.Close();
        }

        public int Depth
        {
            get { return _reader.Depth; }
        }

        public DataTable GetSchemaTable()
        {
            return _reader.GetSchemaTable();
        }

        public bool IsClosed
        {
            get { return _reader.IsClosed; }
        }

        public bool NextResult()
        {
            return _reader.NextResult();
        }

        public bool Read()
        {
            return _reader.Read();
        }

        public int RecordsAffected
        {
            get { return _reader.RecordsAffected; }
        }

        public void Dispose()
        {
            _reader.Dispose();
            _columns = null;
        }

        public int FieldCount
        {
            get { return _reader.FieldCount; }
        }

        public bool GetBoolean(int i)
        {
            return _reader.GetBoolean(i);
        }

        public byte GetByte(int i)
        {
            return _reader.GetByte(i);
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            return _reader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
        }

        public char GetChar(int i)
        {
            return _reader.GetChar(i);
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            return _reader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
        }

        public IDataReader GetData(int i)
        {
            return _reader.GetData(i);
        }

        public string GetDataTypeName(int i)
        {
            return _reader.GetDataTypeName(i);
        }

        public DateTime GetDateTime(int i)
        {
            return _reader.GetDateTime(i);
        }

        public double GetDouble(int i)
        {
            return _reader.GetDouble(i);
        }

        public Type GetFieldType(int i)
        {
            return _reader.GetFieldType(i);
        }

        public float GetFloat(int i)
        {
            return _reader.GetFloat(i);
        }

        public Guid GetGuid(int i)
        {
            return _reader.GetGuid(i);
        }

        public short GetInt16(int i)
        {
            return _reader.GetInt16(i);
        }

        public int GetInt32(int i)
        {
            return _reader.GetInt32(i);
        }

        public long GetInt64(int i)
        {
            return _reader.GetInt64(i);
        }

        public string GetName(int i)
        {
            return _reader.GetName(i);
        }

        public string GetString(int i)
        {
            return _reader.GetString(i);
        }

        public int GetValues(object[] values)
        {
            return _reader.GetValues(values);
        }

        public bool IsDBNull(int i)
        {
            return _reader.IsDBNull(i);
        }

        public object this[string name]
        {
            get { return _reader[name]; }
        }

        public object this[int i]
        {
            get { return _reader[i]; }
        }
        #endregion
    }
}
