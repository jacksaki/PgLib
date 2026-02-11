using PgLib.Objects.Query;

namespace PgLib.Objects;

public sealed class PgDatabase
{
    internal PgDatabase(PgCatalog catalog)
    {
        _catalog = catalog;
    }

    private readonly PgCatalog _catalog;
    public static Task<PgDatabase?> GetAsync(PgCatalog catalog, string name, CancellationToken ct = default)
    {
        return PgDatabaseQuery.GetAsync(catalog, name, ct);
    }

    public IAsyncEnumerable<PgSchema> ListSchemaAsync(CancellationToken ct = default)
    {
        return this._catalog.ListSchemasAsync(ct);
    }

    public static IAsyncEnumerable<PgDatabase> ListAsync(PgCatalog catalog, string? nameLike, CancellationToken ct = default)
    {
        return PgDatabaseQuery.ListAsync(catalog, nameLike, ct);
    }
}
