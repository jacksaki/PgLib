using Npgsql;
using PgLib.Query;
using System.Runtime.CompilerServices;

namespace PgLib.Objects.Query;

public class PgDatabaseQuery
{
    internal static SQLSet GenerateSQLSet()
        => new SQLSet(SQL, new NpgsqlParameter[]
        {
            new NpgsqlParameter("database_name", NpgsqlTypes.NpgsqlDbType.Text),
        });

    private static readonly string SQL = @"SELECT
 d.oid
,d.datname
FROM
 pg_database d
WHERE
(@database_name IS NULL OR d.datname = @database_name::text)
ORDER BY
 d.datname";

    internal static async IAsyncEnumerable<PgDatabase> ListAsync(PgCatalog catalog, string? nameLike, [EnumeratorCancellation] CancellationToken ct)
    {
        var sqlSet = GenerateSQLSet();
        sqlSet["database_name"]!.Value = nameLike.Like(DBNull.Value);

        using var q = catalog.CreateQuery();
        await foreach (var db in q.SelectAsync<PgDatabase, PgCatalog>(catalog, sqlSet, ct))
        {
            yield return db;
        }
    }
    internal static async Task<PgDatabase?> GetAsync(PgCatalog catalog, string name, CancellationToken ct)
    {
        var sqlSet = GenerateSQLSet();
        sqlSet["database_name"]!.Value = name;

        using var q = catalog.CreateQuery();
        var result = await q.SelectAsync<PgDatabase, PgCatalog>(catalog, sqlSet, ct).ToTask();
        return result.FirstOrDefault();
    }
}
