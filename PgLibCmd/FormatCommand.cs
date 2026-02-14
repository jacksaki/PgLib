using ConsoleAppFramework;
using SQLFormatter;

namespace PgLibCmd;

public class FormatCommand
{
    /// <summary>
    /// Format SQL file
    /// </summary>
    /// <param name="filePath">-f, SQL file</param>
    /// <param name="configPath">-c, config path</param>
    /// <returns></returns>
    [Command("format-file")]
    public async Task FormatAsync(string filePath, string? configPath = null)
    {
        var installer = LibraryInstaller.Create();
        if (!installer.IsInstalled)
        {
            Console.Error.WriteLine($"sqlfluff is not installed. install? [Y/n]");
            var key = Console.ReadLine();
            if (!"Y".Equals(key, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            installer.Logged += (sender, e) => Console.WriteLine(e.FormattedMessage);
            await installer.InstallAsync();
        }
        var formatter = configPath == null ? SQLFluffFormatter.Create() : SQLFluffFormatter.Create(configPath);
        formatter.Logged += (sender, e) => Console.WriteLine(e.Message);

        await formatter.FixSQLFileAsync(System.IO.Path.GetFullPath(filePath));
    }

    /// <summary>
    /// Format SQL
    /// </summary>
    /// <param name="sql">-s, SQL text</param>
    /// <param name="configPath">-c, config path</param>
    /// <returns></returns>
    [Command("format-sql")]
    public async Task FormatSQLAsync(string sql, string? configPath = null)
    {
        var installer = LibraryInstaller.Create();
        if (!installer.IsInstalled)
        {
            Console.Error.WriteLine($"sqlfluff is not installed. install? [Y/n]");
            var key = Console.ReadLine();
            if (!"Y".Equals(key, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            installer.Logged += (sender, e) => Console.WriteLine(e.FormattedMessage);
            await installer.InstallAsync();
        }
        var formatter = configPath == null ? SQLFluffFormatter.Create() : SQLFluffFormatter.Create(configPath);
        formatter.Logged += (sender, e) => Console.WriteLine(e.Message);
        await formatter.ExecuteAsync(sql);
    }
}
