using System;
using System.Data;
using System.Threading.Tasks;
using Tarim.Api.Infrastructure.Common;
using Tarim.Api.Infrastructure.Interface;

using MySql.Data.MySqlClient;

namespace Tarim.Api.Infrastructure.DataProvider
{
    public class Connection : IConnection
    {
        private readonly string _connection;
        public Connection(string connection)
        {
            _connection = connection;
        }

        private static IDbCommand CreateCommand(string query)
        {
            return new MySqlCommand { CommandText = query, CommandType = CommandType.StoredProcedure };
        }
        private IDbConnection GetConn()
        {
            return new MySqlConnection(_connection);
        }
        public IDbDataParameter GetParameter(string paramName, object paramValue, MySqlDbType type)
        {
            return new MySqlParameter(paramName, type) { Value = paramValue };
        }

        public IDbDataParameter GetParameter(string paramName, MySqlDbType type, int size)
        {
            return new MySqlParameter(paramName, type, size) { Direction = ParameterDirection.Output };
        }

        private static void OpenConnection(IDbConnection conn)
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
        }

        private void Open(Action<IDbConnection> action)
        {
            using var conn = GetConn();
            OpenConnection(conn);
            action(conn);
        }

        private void DoWithDataReader(IDbCommand command, Action<IDataReader> action)
        {
            Open(conn =>
            {
                command.Connection = conn;
                using IDataReader dataReader = new MySqlReaderWrapper(command.ExecuteReader());
                action(dataReader);
            });
        }

        private Results<T> ReadEntity<T>(IDbCommand cmd, Func<IDataReader, Results<T>> customRead) where T : class
        {
            var result = new Results<T>();
            DoWithDataReader(cmd, reader =>
            {
                result = customRead(reader);
            });

            return result;
        }

        private Result<T> ReadEntity<T>(IDbCommand cmd, Func<IDataReader, Result<T>> customRead) where T : class
        {
            var result = new Result<T>();
            DoWithDataReader(cmd, reader =>
            {
                result = customRead(reader);
            });

            return result;
        }

        private static void DoWithCommand(string query, Action<IDbCommand> action, params IDbDataParameter[] parameters)
        {
            using var cmd = CreateCommand(query);
            foreach (var dbParameter in parameters)
                cmd.Parameters.Add(dbParameter);

            action(cmd);
        }

        private static void DoWithCommand<T>(string query, Func<IDbCommand, Results<T>> action, params IDbDataParameter[] parameters)
        {
            // var result = new Results<T>();

            DoWithCommand(query, command =>
            {
                action(command);
            }, parameters);
        }
        private static T DoWithCommand<T>(string query, Func<IDbCommand, T> action, params IDbDataParameter[] parameters)
        {
            var res = default(T);

            DoWithCommand(query, cmd =>
            {
                res = action(cmd);
            }, parameters);

            return res;
        }
        private int ExecuteNonQuery(IDbCommand command)
        {
            var res = -1;
            Open(conn =>
            {
                command.Connection = conn;
                res = command.ExecuteNonQuery();
            });
            return res;
        }
        protected int ExecuteNonQuery(string query, params IDbDataParameter[] parameters)
        {
            return DoWithCommand(query, cmd => ExecuteNonQuery(cmd), parameters);
        }

        //   public DbSet<Client> Clients { get; set; }
        //    public DbSet<RefreshToken> RefreshTokens { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="customRead"></param>
        /// <param name="parameters"></param>
        protected void GetResult<T>(string query, Func<IDataReader, Results<T>> customRead, params IDbDataParameter[] parameters) where T : class
        {
            DoWithCommand(query, cmd => ReadEntity(cmd, customRead), parameters);
        }





        #region async process
        public async Task GetResultAsync<T>(string query, Func<IDataReader, Results<T>> customRead, params IDbDataParameter[] parameters) where T : class
        {
            await DoWithCommandAsync(query, cmd => ReadEntityAsync(cmd, customRead), parameters);
        }

        public async Task GetResultAsync<T>(string query, Func<IDataReader, Result<T>> customRead, params IDbDataParameter[] parameters) where T : class
        {
            await DoWithCommandAsync(query, cmd => ReadEntityAsync(cmd, customRead), parameters);
        }

        public async Task<int> ExecuteNonQueryAsync(string query, params IDbDataParameter[] parameters)
        {
            return await DoWithCommandAsync(query, ExecuteNonQueryAsync, parameters);
        }
        private async Task<int> ExecuteNonQueryAsync(IDbCommand command)
        {
            var res = -1;
            try
            {
                await OpenAsync(conn =>
                {
                    command.Connection = conn;
                    res = command.ExecuteNonQuery();
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return res;
        }
        private static async Task<T> DoWithCommandAsync<T>(string query, Func<IDbCommand, Task<T>> actionAsync, params IDbDataParameter[] parameters)
        {
            var res = default(T);

            await DoWithCommandAsync(query, async cmd =>
            {
                res = await actionAsync(cmd);
            }, parameters);

            return res;
        }
        private async Task<Results<T>> ReadEntityAsync<T>(IDbCommand cmd, Func<IDataReader, Results<T>> customRead) where T : class
        {
            var result = new Results<T>();
            await DoWithDataReaderAsync(cmd, reader =>
            {
                result = customRead(reader);
            });

            return result;
        }
        private async Task<Result<T>> ReadEntityAsync<T>(IDbCommand cmd, Func<IDataReader, Result<T>> customRead) where T : class
        {
            var result = new Result<T>();
            await DoWithDataReaderAsync(cmd, reader =>
            {
                result = customRead(reader);
            });

            return result;
        }
        private async Task DoWithDataReaderAsync(IDbCommand command, Action<IDataReader> actionAsync)
        {
            await OpenAsync(conn =>
            {
                command.Connection = conn;
                using IDataReader dataReader = new MySqlReaderWrapper(command.ExecuteReader());
                actionAsync(dataReader);
            });
        }
        private async Task OpenAsync(Action<IDbConnection> actionAsync)
        {
            using var conn = GetConn();
            await OpenConnectionAsync(conn);
            actionAsync(conn);
        }
        private static async Task DoWithCommandAsync(string query, Func<IDbCommand, Task> actionAsync, params IDbDataParameter[] parameters)
        {
            using var cmd = CreateCommand(query);
            foreach (var dbParameter in parameters)
                cmd.Parameters.Add(dbParameter);

            await actionAsync(cmd);
        }
        private static async Task OpenConnectionAsync(IDbConnection conn)
        {
            if (conn.State != ConnectionState.Open)
                await ((MySqlConnection)conn).OpenAsync();
        }

        public async Task<IDataReader> ExecuteReaderAsync(IDbCommand command)
        {
            return await ((MySqlCommand)command).ExecuteReaderAsync();
        }

        public async Task<bool> ReadAsync(IDataReader reader)
        {
            return await ((MySqlDataReader)reader).ReadAsync();
        }

        public async Task<int> ExecuteNonQueryCommandAsync(IDbCommand command)
        {
            return await ((MySqlCommand)command).ExecuteNonQueryAsync();
        }

        public async Task<object> ExecuteScalarCommandAsync(IDbCommand command)
        {
            return await ((MySqlCommand)command).ExecuteScalarAsync();
        }
        #endregion
    }
}
