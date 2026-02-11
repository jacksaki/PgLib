using ConsoleAppFramework;
using PgLib;
using PgLib.Connection;
using PgLib.Objects;

namespace PgLibCmd;

public class DbObjectCommand
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="schemaName">-s, schema name</param>
    /// <param name="tableName">-t, tablename</param>
    /// <returns></returns>
    [Command("table")]
    public async Task ShowTableDDLCommand(string schemaName, string tableName)
    {
        var db = new PgCatalog(ConnectionConfig.Load(System.Environment.GetEnvironmentVariable("connection_config")!));
        var table = await db.GetTableAsync(schemaName, tableName);
        Console.WriteLine(await table!.GenerateDDLAsync(new DDLOptions() { AddConstraints = true, AddIndexes = true, AddSchema = false }));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="schemaName">-s, schema name</param>
    /// <param name="tableNameLike">-t, tablename</param>
    /// <returns></returns>
    [Command("table-list")]
    public async Task ShowTablesCommand(string schemaName, string? tableNameLike=null)
    {
        var db = new PgCatalog(ConnectionConfig.Load(System.Environment.GetEnvironmentVariable("connection_config")!));
        await foreach(var table in db.ListTablesAsync(schemaName, tableNameLike))
        {
            Console.WriteLine(table.Name);
        }
    }
}
