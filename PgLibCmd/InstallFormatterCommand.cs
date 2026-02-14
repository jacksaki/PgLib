using ConsoleAppFramework;
using SQLFormatter;

namespace PgLibCmd;

public class InstallFormatterCommand
{
    [Command("install-formatter")]
    public async Task InstallAsync()
    {
        var installer = LibraryInstaller.Create();
        installer.Logged += (sender, e) => Console.WriteLine(e.FormattedMessage);
        await installer.InstallAsync();
    }
}
