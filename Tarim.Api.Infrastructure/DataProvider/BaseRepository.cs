using System;
using System.Data;
using System.Threading.Tasks;
using Tarim.Api.Infrastructure.Common;
using Tarim.Api.Infrastructure.Interface;

using MySql.Data.MySqlClient;

namespace Tarim.Api.Infrastructure.DataProvider
{
    public abstract class BaseRepository
    {
        private readonly IConnection _connection;

        protected BaseRepository(IConnection connection) 
        {
            _connection = connection;
        }

 

        protected async Task GetResultAsync<T>(string query, Func<IDataReader, Results<T>> customRead,
            params IDbDataParameter[] parameters) where T : class
        {
            await _connection.GetResultAsync(query, customRead, parameters);
        }
        protected async Task GetResultAsync<T>(string query, Func<IDataReader, Result<T>> customRead,
            params IDbDataParameter[] parameters) where T : class
        {
            await _connection.GetResultAsync(query, customRead, parameters);
        }

        protected async Task GetResultAsync<T>(string query, Func<IDataReader, Results<T>> customRead) where T : class
        {
            await _connection.GetResultAsync(query, customRead);
        }

        protected async Task GetResultAsync<T>(string query, Func<IDataReader, Result<T>> customRead) where T : class
        {
            await _connection.GetResultAsync(query, customRead);
        }

        protected async Task<int> ExecuteNonQueryAsync(string query, params IDbDataParameter[] parameters)
        {
            return await _connection.ExecuteNonQueryAsync(query, parameters);
        }

        public IDbDataParameter GetParameter(string paramName, object paramValue, MySqlDbType type)
        {
            return new MySqlParameter(paramName, type) { Value = paramValue };
        }

        public IDbDataParameter GetParameter(string paramName, MySqlDbType type, int size)
        {
            return new MySqlParameter(paramName, type, size) { Direction = ParameterDirection.Output };
        }
    }
}
