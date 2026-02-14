using ConsoleAppFramework;
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
        var conf = ConnectionConfig.Load(System.Environment.GetEnvironmentVariable("connection_config")!);
        await using var ssh = new SshTunnel(conf);
        await ssh.ConnectAsync();
        var db = new PgCatalog(conf);
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
    public async Task ShowTablesCommand(string schemaName, string? tableNameLike = null)
    {
        var conf = ConnectionConfig.Load(System.Environment.GetEnvironmentVariable("connection_config")!);
        await using var ssh = new SshTunnel(conf);
        await ssh.ConnectAsync();
        var db = new PgCatalog(conf);
        await foreach (var table in db.ListTablesAsync(schemaName, tableNameLike))
        {
            Console.WriteLine(table.Name);
            await foreach (var column in table.ListColumnsAsync())
            {
                Console.WriteLine($"\t{column.ColumnName}");
            }
        }
    }
}
