using Npgsql;
using PgLib.Connection;
using PgLib.Query;
using System.Data;
namespace PgLib;

public partial class PgQuery : IDisposable
{
    #region ExecuteReaderAsync
    public static async IAsyncEnumerable<NpgsqlDataReader> ExecuteReaderAsync(ConnectionConfig config, SQLSet sqlSet, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        await foreach (var reader in PgQuery.ExecuteReaderHelperAsync(config, sqlSet, commandType, commandBehavior))
        {
            yield return reader;
        }
    }
    public static async IAsyncEnumerable<NpgsqlDataReader> ExecuteReaderAsync(ConnectionConfig config, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        await foreach (var reader in PgQuery.ExecuteReaderHelperAsync(config, query, parameter, commandType, commandBehavior))
        {
            yield return reader;
        }
    }
    public static async IAsyncEnumerable<NpgsqlDataReader> ExecuteReaderAsync(NpgsqlConnection connection, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        await foreach (var reader in PgQuery.ExecuteReaderHelperAsync(connection, query, parameter, commandType, commandBehavior))
        {
            yield return reader;
        }
    }
    public static async IAsyncEnumerable<NpgsqlDataReader> ExecuteReaderAsync(NpgsqlConnection connection, SQLSet sqlSet, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        await foreach (var reader in PgQuery.ExecuteReaderHelperAsync(connection, sqlSet, commandType, commandBehavior))
        {
            yield return reader;
        }
    }
    public static async IAsyncEnumerable<NpgsqlDataReader> ExecuteReaderAsync(string connectionString, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        await foreach (var reader in PgQuery.ExecuteReaderHelperAsync(connectionString, query, parameter, commandType, commandBehavior))
        {
            yield return reader;
        }
    }
    public static async IAsyncEnumerable<NpgsqlDataReader> ExecuteReaderAsync(string connectionString, SQLSet sqlSet, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        await foreach (var reader in PgQuery.ExecuteReaderHelperAsync(connectionString, sqlSet, commandType, commandBehavior))
        {
            yield return reader;
        }
    }
    #endregion

    #region ExecuteReader
    public static IEnumerable<NpgsqlDataReader> ExecuteReader(ConnectionConfig config, SQLSet sqlSet, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return ExecuteReaderHelper(config, sqlSet, commandType, commandBehavior);
    }
    public static IEnumerable<NpgsqlDataReader> ExecuteReader(ConnectionConfig config, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return ExecuteReaderHelper(config, query, parameter, commandType, commandBehavior);
    }
    public static IEnumerable<NpgsqlDataReader> ExecuteReader(NpgsqlConnection connection, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return ExecuteReaderHelper(connection, query, parameter, commandType, commandBehavior);
    }
    public static IEnumerable<NpgsqlDataReader> ExecuteReader(NpgsqlConnection connection, SQLSet sqlSet, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return ExecuteReaderHelper(connection, sqlSet, commandType, commandBehavior);
    }
    public static IEnumerable<NpgsqlDataReader> ExecuteReader(string connectionString, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return ExecuteReaderHelper(new NpgsqlConnection(connectionString), query, parameter, commandType, commandBehavior);
    }
    public static IEnumerable<NpgsqlDataReader> ExecuteReader(string connectionString, SQLSet sqlSet, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return ExecuteReaderHelper(new NpgsqlConnection(connectionString), sqlSet, commandType, commandBehavior);
    }
    #endregion

    #region ExecuteNonQuery
    public static int ExecuteNonQuery(ConnectionConfig config, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text)
    {
        using (var exec = new PgQuery(config))
        {
            return exec.ExecuteNonQuery(query, parameter, commandType);
        }
    }
    public static int ExecuteNonQuery(string connectionString, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text)
    {
        using (var exec = new PgQuery(connectionString))
        {
            return exec.ExecuteNonQuery(query, parameter, commandType);
        }
    }
    public static int ExecuteNonQuery(NpgsqlConnection connection, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text)
    {
        using (var exec = new PgQuery(connection))
        {
            return exec.ExecuteNonQuery(query, parameter, commandType);
        }
    }
    #endregion

    #region ExecuteNonQueryAsync
    public static async Task<int> ExecuteNonQueryAsync(ConnectionConfig config, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text)
    {
        using (var exec = new PgQuery(config))
        {
            return await exec.ExecuteNonQueryAsync(query, parameter, commandType);
        }
    }
    public static async Task<int> ExecuteNonQueryAsync(string connectionString, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text)
    {
        using (var exec = new PgQuery(connectionString))
        {
            return await exec.ExecuteNonQueryAsync(query, parameter, commandType);
        }
    }
    public static async Task<int> ExecuteNonQueryAsync(NpgsqlConnection connection, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text)
    {
        using (var exec = new PgQuery(connection))
        {
            return await exec.ExecuteNonQueryAsync(query, parameter, commandType);
        }
    }
    #endregion

    #region ExecuteScalar
    public static T? ExecuteScalar<T>(ConnectionConfig config, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text)
    {
        using (var exec = new PgQuery(config))
        {
            return exec.ExecuteScalar<T>(query, parameter, commandType);
        }
    }
    public static T? ExecuteScalar<T>(string connectionString, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text)
    {
        using (var exec = new PgQuery(connectionString))
        {
            return exec.ExecuteScalar<T>(query, parameter, commandType);
        }
    }
    public static T? ExecuteScalar<T>(NpgsqlConnection connection, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text)
    {
        using (var exec = new PgQuery(connection))
        {
            return exec.ExecuteScalar<T>(query, parameter, commandType);
        }
    }
    #endregion

    #region ExecuteScalarAsync
    public static async Task<T?> ExecuteScalarAsync<T>(ConnectionConfig config, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text)
    {
        using (var exec = new PgQuery(config))
        {
            return await exec.ExecuteScalarAsync<T>(query, parameter, commandType);
        }
    }
    public static async Task<T?> ExecuteScalarAsync<T>(string connectionString, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text)
    {
        using (var exec = new PgQuery(connectionString))
        {
            return await exec.ExecuteScalarAsync<T>(query, parameter, commandType);
        }
    }
    public static async Task<T?> ExecuteScalarAsync<T>(NpgsqlConnection connection, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text)
    {
        using (var exec = new PgQuery(connection))
        {
            return await exec.ExecuteScalarAsync<T>(query, parameter, commandType);
        }
    }
    #endregion

    #region Select (ConnectionString)
    public static IEnumerable<T> Select<T>(string connectionString, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.Select<T>(new NpgsqlConnection(connectionString), query, parameter, commandType, commandBehavior);
    }
    public static IEnumerable<T> Select<T, T0>(string connectionString, T0 t0, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.Select<T, T0>(new NpgsqlConnection(connectionString), t0, query, parameter, commandType, commandBehavior);
    }
    public static IEnumerable<T> Select<T, T0, T1>(string connectionString, T0 t0, T1 t1, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.Select<T, T0, T1>(new NpgsqlConnection(connectionString), t0, t1, query, parameter, commandType, commandBehavior);
    }
    public static IEnumerable<T> Select<T, T0, T1, T2>(string connectionString, T0 t0, T1 t1, T2 t2, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.Select<T, T0, T1, T2>(new NpgsqlConnection(connectionString), t0, t1, t2, query, parameter, commandType, commandBehavior);
    }
    public static IEnumerable<T> Select<T, T0, T1, T2, T3>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.Select<T, T0, T1, T2, T3>(new NpgsqlConnection(connectionString), t0, t1, t2, t3, query, parameter, commandType, commandBehavior);
    }
    public static IEnumerable<T> Select<T, T0, T1, T2, T3, T4>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.Select<T, T0, T1, T2, T3, T4>(new NpgsqlConnection(connectionString), t0, t1, t2, t3, t4, query, parameter, commandType, commandBehavior);
    }
    public static IEnumerable<T> Select<T, T0, T1, T2, T3, T4, T5>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.Select<T, T0, T1, T2, T3, T4, T5>(new NpgsqlConnection(connectionString), t0, t1, t2, t3, t4, t5, query, parameter, commandType, commandBehavior);
    }
    public static IEnumerable<T> Select<T, T0, T1, T2, T3, T4, T5, T6>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.Select<T, T0, T1, T2, T3, T4, T5, T6>(new NpgsqlConnection(connectionString), t0, t1, t2, t3, t4, t5, t6, query, parameter, commandType, commandBehavior);
    }
    public static IEnumerable<T> Select<T, T0, T1, T2, T3, T4, T5, T6, T7>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.Select<T, T0, T1, T2, T3, T4, T5, T6, T7>(new NpgsqlConnection(connectionString), t0, t1, t2, t3, t4, t5, t6, t7, query, parameter, commandType, commandBehavior);
    }
    #endregion
    #region Select (NpgsqlConnection)
    public static IEnumerable<T> Select<T>(NpgsqlConnection connection, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior);
            foreach (var record in records)
            {
                yield return record.Create<T>();
            }
        }
    }
    public static IEnumerable<T> Select<T, T0>(NpgsqlConnection connection, T0 t0, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior);
            foreach (var record in records)
            {
                yield return record.Create<T, T0>(t0);
            }
        }
    }
    public static IEnumerable<T> Select<T, T0, T1>(NpgsqlConnection connection, T0 t0, T1 t1, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior);
            foreach (var record in records)
            {
                yield return record.Create<T, T0, T1>(t0, t1);
            }
        }
    }
    public static IEnumerable<T> Select<T, T0, T1, T2>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior);
            foreach (var record in records)
            {
                yield return record.Create<T, T0, T1, T2>(t0, t1, t2);
            }
        }
    }
    public static IEnumerable<T> Select<T, T0, T1, T2, T3>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior);
            foreach (var record in records)
            {
                yield return record.Create<T, T0, T1, T2, T3>(t0, t1, t2, t3);
            }
        }
    }
    public static IEnumerable<T> Select<T, T0, T1, T2, T3, T4>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior);
            foreach (var record in records)
            {
                yield return record.Create<T, T0, T1, T2, T3, T4>(t0, t1, t2, t3, t4);
            }
        }
    }
    public static IEnumerable<T> Select<T, T0, T1, T2, T3, T4, T5>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior);
            foreach (var record in records)
            {
                yield return record.Create<T, T0, T1, T2, T3, T4, T5>(t0, t1, t2, t3, t4, t5);
            }
        }
    }
    public static IEnumerable<T> Select<T, T0, T1, T2, T3, T4, T5, T6>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior);
            foreach (var record in records)
            {
                yield return record.Create<T, T0, T1, T2, T3, T4, T5, T6>(t0, t1, t2, t3, t4, t5, t6);
            }
        }
    }
    public static IEnumerable<T> Select<T, T0, T1, T2, T3, T4, T5, T6, T7>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior);
            foreach (var record in records)
            {
                yield return record.Create<T, T0, T1, T2, T3, T4, T5, T6, T7>(t0, t1, t2, t3, t4, t5, t6, t7);
            }
        }
    }
    #endregion
    #region Select (ConnectionConfig)
    public static IEnumerable<T> Select<T>(ConnectionConfig config, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.Select<T>(config, query, parameter, commandType, commandBehavior);
    }
    public static IEnumerable<T> Select<T, T0>(ConnectionConfig config, T0 t0, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.Select<T, T0>(config, t0, query, parameter, commandType, commandBehavior);
    }
    public static IEnumerable<T> Select<T, T0, T1>(ConnectionConfig config, T0 t0, T1 t1, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.Select<T, T0, T1>(config, t0, t1, query, parameter, commandType, commandBehavior);
    }
    public static IEnumerable<T> Select<T, T0, T1, T2>(ConnectionConfig config, T0 t0, T1 t1, T2 t2, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.Select<T, T0, T1, T2>(config, t0, t1, t2, query, parameter, commandType, commandBehavior);
    }
    public static IEnumerable<T> Select<T, T0, T1, T2, T3>(ConnectionConfig config, T0 t0, T1 t1, T2 t2, T3 t3, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.Select<T, T0, T1, T2, T3>(config, t0, t1, t2, t3, query, parameter, commandType, commandBehavior);
    }
    public static IEnumerable<T> Select<T, T0, T1, T2, T3, T4>(ConnectionConfig config, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.Select<T, T0, T1, T2, T3, T4>(config, t0, t1, t2, t3, t4, query, parameter, commandType, commandBehavior);
    }
    public static IEnumerable<T> Select<T, T0, T1, T2, T3, T4, T5>(ConnectionConfig config, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.Select<T, T0, T1, T2, T3, T4, T5>(config, t0, t1, t2, t3, t4, t5, query, parameter, commandType, commandBehavior);
    }
    public static IEnumerable<T> Select<T, T0, T1, T2, T3, T4, T5, T6>(ConnectionConfig config, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.Select<T, T0, T1, T2, T3, T4, T5, T6>(config, t0, t1, t2, t3, t4, t5, t6, query, parameter, commandType, commandBehavior);
    }
    public static IEnumerable<T> Select<T, T0, T1, T2, T3, T4, T5, T6, T7>(ConnectionConfig config, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.Select<T, T0, T1, T2, T3, T4, T5, T6, T7>(config, t0, t1, t2, t3, t4, t5, t6, t7, query, parameter, commandType, commandBehavior);
    }
    #endregion

    #region SelectFirst (NpgsqlConnection)
    public static T? SelectFirst<T>(NpgsqlConnection connection, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T>();
        }
    }
    public static T? SelectFirst<T, T0>(NpgsqlConnection connection, T0 t0, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0>(t0);
        }
    }
    public static T? SelectFirst<T, T0, T1>(NpgsqlConnection connection, T0 t0, T1 t1, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1>(t0, t1);
        }
    }
    public static T? SelectFirst<T, T0, T1, T2>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1, T2>(t0, t1, t2);
        }
    }
    public static T? SelectFirst<T, T0, T1, T2, T3>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1, T2, T3>(t0, t1, t2, t3);
        }
    }
    public static T? SelectFirst<T, T0, T1, T2, T3, T4>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1, T2, T3, T4>(t0, t1, t2, t3, t4);
        }
    }
    public static T? SelectFirst<T, T0, T1, T2, T3, T4, T5>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1, T2, T3, T4, T5>(t0, t1, t2, t3, t4, t5);
        }
    }
    public static T? SelectFirst<T, T0, T1, T2, T3, T4, T5, T6>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1, T2, T3, T4, T5, T6>(t0, t1, t2, t3, t4, t5, t6);
        }
    }
    public static T? SelectFirst<T, T0, T1, T2, T3, T4, T5, T6, T7>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1, T2, T3, T4, T5, T6, T7>(t0, t1, t2, t3, t4, t5, t6, t7);
        }
    }
    #endregion
    #region SelectFirst (ConnectionString)
    public static T? SelectFirst<T>(string connectionString, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connectionString))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T>();
        }
    }
    public static T? SelectFirst<T, T0>(string connectionString, T0 t0, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connectionString))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0>(t0);
        }
    }
    public static T? SelectFirst<T, T0, T1>(string connectionString, T0 t0, T1 t1, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connectionString))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1>(t0, t1);
        }
    }
    public static T? SelectFirst<T, T0, T1, T2>(string connectionString, T0 t0, T1 t1, T2 t2, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connectionString))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1, T2>(t0, t1, t2);
        }
    }
    public static T? SelectFirst<T, T0, T1, T2, T3>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connectionString))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1, T2, T3>(t0, t1, t2, t3);
        }
    }
    public static T? SelectFirst<T, T0, T1, T2, T3, T4>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connectionString))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1, T2, T3, T4>(t0, t1, t2, t3, t4);
        }
    }
    public static T? SelectFirst<T, T0, T1, T2, T3, T4, T5>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connectionString))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1, T2, T3, T4, T5>(t0, t1, t2, t3, t4, t5);
        }
    }
    public static T? SelectFirst<T, T0, T1, T2, T3, T4, T5, T6>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connectionString))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1, T2, T3, T4, T5, T6>(t0, t1, t2, t3, t4, t5, t6);
        }
    }
    public static T? SelectFirst<T, T0, T1, T2, T3, T4, T5, T6, T7>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connectionString))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1, T2, T3, T4, T5, T6, T7>(t0, t1, t2, t3, t4, t5, t6, t7);
        }
    }
    #endregion
    #region SelectFirst (ConnectionConfig)
    public static T? SelectFirst<T>(ConnectionConfig config, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(config))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T>();
        }
    }
    public static T? SelectFirst<T, T0>(ConnectionConfig config, T0 t0, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(config))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0>(t0);
        }
    }
    public static T? SelectFirst<T, T0, T1>(ConnectionConfig config, T0 t0, T1 t1, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(config))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1>(t0, t1);
        }
    }
    public static T? SelectFirst<T, T0, T1, T2>(ConnectionConfig config, T0 t0, T1 t1, T2 t2, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(config))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1, T2>(t0, t1, t2);
        }
    }
    public static T? SelectFirst<T, T0, T1, T2, T3>(ConnectionConfig config, T0 t0, T1 t1, T2 t2, T3 t3, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(config))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1, T2, T3>(t0, t1, t2, t3);
        }
    }
    public static T? SelectFirst<T, T0, T1, T2, T3, T4>(ConnectionConfig config, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(config))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1, T2, T3, T4>(t0, t1, t2, t3, t4);
        }
    }
    public static T? SelectFirst<T, T0, T1, T2, T3, T4, T5>(ConnectionConfig config, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(config))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1, T2, T3, T4, T5>(t0, t1, t2, t3, t4, t5);
        }
    }
    public static T? SelectFirst<T, T0, T1, T2, T3, T4, T5, T6>(ConnectionConfig config, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(config))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1, T2, T3, T4, T5, T6>(t0, t1, t2, t3, t4, t5, t6);
        }
    }
    public static T? SelectFirst<T, T0, T1, T2, T3, T4, T5, T6, T7>(ConnectionConfig config, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(config))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1, T2, T3, T4, T5, T6, T7>(t0, t1, t2, t3, t4, t5, t6, t7);
        }
    }
    #endregion


    #region SelectAsync (ConnectionString)
    public static async IAsyncEnumerable<T> SelectAsync<T>(string connectionString, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        await foreach (var item in PgQuery.SelectAsync<T>(new NpgsqlConnection(connectionString), query, parameter, commandType, commandBehavior))
        {
            yield return item;
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0>(string connectionString, T0 t0, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        await foreach (var item in PgQuery.SelectAsync<T, T0>(new NpgsqlConnection(connectionString), t0, query, parameter, commandType, commandBehavior))
        {
            yield return item;
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1>(string connectionString, T0 t0, T1 t1, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        await foreach (var item in PgQuery.SelectAsync<T, T0, T1>(new NpgsqlConnection(connectionString), t0, t1, query, parameter, commandType, commandBehavior))
        {
            yield return item;
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2>(string connectionString, T0 t0, T1 t1, T2 t2, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        await foreach (var item in PgQuery.SelectAsync<T, T0, T1, T2>(new NpgsqlConnection(connectionString), t0, t1, t2, query, parameter, commandType, commandBehavior))
        {
            yield return item;
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        await foreach (var item in PgQuery.SelectAsync<T, T0, T1, T2, T3>(new NpgsqlConnection(connectionString), t0, t1, t2, t3, query, parameter, commandType, commandBehavior))
        {
            yield return item;
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3, T4>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        await foreach (var item in PgQuery.SelectAsync<T, T0, T1, T2, T3, T4>(new NpgsqlConnection(connectionString), t0, t1, t2, t3, t4, query, parameter, commandType, commandBehavior))
        {
            yield return item;
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3, T4, T5>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        await foreach (var item in PgQuery.SelectAsync<T, T0, T1, T2, T3, T4, T5>(new NpgsqlConnection(connectionString), t0, t1, t2, t3, t4, t5, query, parameter, commandType, commandBehavior))
        {
            yield return item;
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3, T4, T5, T6>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        await foreach (var item in PgQuery.SelectAsync<T, T0, T1, T2, T3, T4, T5, T6>(new NpgsqlConnection(connectionString), t0, t1, t2, t3, t4, t5, t6, query, parameter, commandType, commandBehavior))
        {
            yield return item;
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3, T4, T5, T6, T7>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        await foreach (var item in PgQuery.SelectAsync<T, T0, T1, T2, T3, T4, T5, T6, T7>(new NpgsqlConnection(connectionString), t0, t1, t2, t3, t4, t5, t6, t7, query, parameter, commandType, commandBehavior))
        {
            yield return item;
        }
    }
    #endregion
    #region SelectAsync (NpgsqlConnection)
    public static async IAsyncEnumerable<T> SelectAsync<T>(NpgsqlConnection connection, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var record in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                yield return record.Create<T>();
            }
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0>(NpgsqlConnection connection, T0 t0, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var record in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                yield return record.Create<T, T0>(t0);
            }
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1>(NpgsqlConnection connection, T0 t0, T1 t1, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var record in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                yield return record.Create<T, T0, T1>(t0, t1);
            }
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var record in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                yield return record.Create<T, T0, T1, T2>(t0, t1, t2);
            }
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var record in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                yield return record.Create<T, T0, T1, T2, T3>(t0, t1, t2, t3);
            }
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3, T4>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var record in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                yield return record.Create<T, T0, T1, T2, T3, T4>(t0, t1, t2, t3, t4);
            }
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3, T4, T5>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var record in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                yield return record.Create<T, T0, T1, T2, T3, T4, T5>(t0, t1, t2, t3, t4, t5);
            }
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3, T4, T5, T6>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var record in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                yield return record.Create<T, T0, T1, T2, T3, T4, T5, T6>(t0, t1, t2, t3, t4, t5, t6);
            }
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3, T4, T5, T6, T7>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var record in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                yield return record.Create<T, T0, T1, T2, T3, T4, T5, T6, T7>(t0, t1, t2, t3, t4, t5, t6, t7);
            }
        }
    }
    #endregion
    #region SelectAsync (ConnectionConfig)
    public static async IAsyncEnumerable<T> SelectAsync<T>(ConnectionConfig config, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(config))
        {
            await foreach (var record in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                yield return record.Create<T>();
            }
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0>(ConnectionConfig config, T0 t0, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(config))
        {
            await foreach (var record in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                yield return record.Create<T, T0>(t0);
            }
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1>(ConnectionConfig config, T0 t0, T1 t1, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(config))
        {
            await foreach (var record in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                yield return record.Create<T, T0, T1>(t0, t1);
            }
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2>(ConnectionConfig config, T0 t0, T1 t1, T2 t2, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(config))
        {
            await foreach (var record in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                yield return record.Create<T, T0, T1, T2>(t0, t1, t2);
            }
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3>(ConnectionConfig config, T0 t0, T1 t1, T2 t2, T3 t3, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(config))
        {
            await foreach (var record in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                yield return record.Create<T, T0, T1, T2, T3>(t0, t1, t2, t3);
            }
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3, T4>(ConnectionConfig config, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(config))
        {
            await foreach (var record in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                yield return record.Create<T, T0, T1, T2, T3, T4>(t0, t1, t2, t3, t4);
            }
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3, T4, T5>(ConnectionConfig config, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(config))
        {
            await foreach (var record in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                yield return record.Create<T, T0, T1, T2, T3, T4, T5>(t0, t1, t2, t3, t4, t5);
            }
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3, T4, T5, T6>(ConnectionConfig config, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(config))
        {
            await foreach (var record in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                yield return record.Create<T, T0, T1, T2, T3, T4, T5, T6>(t0, t1, t2, t3, t4, t5, t6);
            }
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3, T4, T5, T6, T7>(ConnectionConfig config, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(config))
        {
            await foreach (var record in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                yield return record.Create<T, T0, T1, T2, T3, T4, T5, T6, T7>(t0, t1, t2, t3, t4, t5, t6, t7);
            }
        }
    }
    #endregion

    #region SelectFirstAsync (NpgsqlConnection)
    public static async Task<T?> SelectFirstAsync<T>(NpgsqlConnection connection, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T>();
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0>(NpgsqlConnection connection, T0 t0, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0>(t0);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1>(NpgsqlConnection connection, T0 t0, T1 t1, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1>(t0, t1);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1, T2>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1, T2>(t0, t1, t2);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1, T2, T3>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1, T2, T3>(t0, t1, t2, t3);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1, T2, T3, T4>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1, T2, T3, T4>(t0, t1, t2, t3, t4);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1, T2, T3, T4, T5>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1, T2, T3, T4, T5>(t0, t1, t2, t3, t4, t5);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1, T2, T3, T4, T5, T6>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1, T2, T3, T4, T5, T6>(t0, t1, t2, t3, t4, t5, t6);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1, T2, T3, T4, T5, T6, T7>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1, T2, T3, T4, T5, T6, T7>(t0, t1, t2, t3, t4, t5, t6, t7);
            }
            return default(T);
        }
    }
    #endregion
    #region SelectFirstAsync (ConnectionString)
    public static async Task<T?> SelectFirstAsync<T>(string connectionString, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connectionString))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T>();
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0>(string connectionString, T0 t0, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connectionString))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0>(t0);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1>(string connectionString, T0 t0, T1 t1, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connectionString))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1>(t0, t1);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1, T2>(string connectionString, T0 t0, T1 t1, T2 t2, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connectionString))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1, T2>(t0, t1, t2);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1, T2, T3>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connectionString))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1, T2, T3>(t0, t1, t2, t3);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1, T2, T3, T4>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connectionString))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1, T2, T3, T4>(t0, t1, t2, t3, t4);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1, T2, T3, T4, T5>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connectionString))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1, T2, T3, T4, T5>(t0, t1, t2, t3, t4, t5);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1, T2, T3, T4, T5, T6>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connectionString))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1, T2, T3, T4, T5, T6>(t0, t1, t2, t3, t4, t5, t6);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1, T2, T3, T4, T5, T6, T7>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connectionString))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1, T2, T3, T4, T5, T6, T7>(t0, t1, t2, t3, t4, t5, t6, t7);
            }
            return default(T);
        }
    }
    #endregion
    #region SelectFirstAsync (ConnectionConfig)
    public static async Task<T?> SelectFirstAsync<T>(ConnectionConfig config, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(config))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T>();
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0>(ConnectionConfig config, T0 t0, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(config))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0>(t0);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1>(ConnectionConfig config, T0 t0, T1 t1, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(config))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1>(t0, t1);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1, T2>(ConnectionConfig config, T0 t0, T1 t1, T2 t2, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(config))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1, T2>(t0, t1, t2);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1, T2, T3>(ConnectionConfig config, T0 t0, T1 t1, T2 t2, T3 t3, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(config))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1, T2, T3>(t0, t1, t2, t3);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1, T2, T3, T4>(ConnectionConfig config, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(config))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1, T2, T3, T4>(t0, t1, t2, t3, t4);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1, T2, T3, T4, T5>(ConnectionConfig config, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(config))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1, T2, T3, T4, T5>(t0, t1, t2, t3, t4, t5);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1, T2, T3, T4, T5, T6>(ConnectionConfig config, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(config))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1, T2, T3, T4, T5, T6>(t0, t1, t2, t3, t4, t5, t6);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1, T2, T3, T4, T5, T6, T7>(ConnectionConfig config, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(config))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1, T2, T3, T4, T5, T6, T7>(t0, t1, t2, t3, t4, t5, t6, t7);
            }
            return default(T);
        }
    }
    #endregion

}