using Cysharp.Diagnostics;
using R3;
using System.Diagnostics;
using System.IO.Compression;
using System.Reflection;
using System.Text.Json;

namespace SQLFormatter;

public class LibraryInstaller
{
    public ReactiveProperty<string> Title { get; }

    public static LibraryInstaller Create()
    {
        var confPath = System.IO.Path.ChangeExtension(System.Reflection.Assembly.GetExecutingAssembly().Location, ".conf");
        return LibraryInstaller.Create(confPath);
    }

    public string BaseDir => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
    public string PythonDir => Path.Combine(this.BaseDir, this.Config.PythonDir);
    public string PythonExePath => Path.Combine(this.PythonDir, "python.exe");
    public string SQLFluffExePath => Path.Combine(this.PythonDir, "Scripts", "sqlfluff.exe");
    public string PipPath => Path.Combine(this.PythonDir, "Scripts", "pip.exe");
    internal FormatterConfig Config { get; private set; }

    public delegate void LoggedEventHandler(object sender, LogItem e);
    public event LoggedEventHandler Logged = delegate { };
    public bool IsInstalled => File.Exists(this.SQLFluffExePath);

    private LibraryInstaller(FormatterConfig config)
    {
        this.Config = config;
        this.Title = new ReactiveProperty<string>();
    }

    public static LibraryInstaller Create(string confPath)
    {
        if (!File.Exists(confPath))
        {
            throw new FileNotFoundException("SQLFormatter.conf not found", confPath);
        }
        var installer = new LibraryInstaller(JsonSerializer.Deserialize<FormatterConfig>(File.ReadAllText(confPath))!);
        return installer;
    }

    private async Task RunCommandAsync(string filePath, string args)
    {
        var stdOuts = new List<string>();

        var psi = new ProcessStartInfo
        {
            FileName = filePath,
            Arguments = args,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        var (_, stdOut, stdError) = ExtendedProcessX.GetDualAsyncEnumerableEx(psi);
        var consumeStdOut = Task.Run(async () =>
        {
            await foreach (var item in stdOut)
            {
                Logged(this, new LogItem(InstallLogItemType.Output, item));
            }
        });

        var errorBuffered = new List<string>();
        var consumeStdError = Task.Run(async () =>
        {
            await foreach (var item in stdError)
            {
                Logged(this, new LogItem(InstallLogItemType.Error, item));
                errorBuffered.Add(item);
            }
        });
        try
        {
            await Task.WhenAll(consumeStdOut, consumeStdError);
        }
        catch (ProcessErrorException ex)
        {
            throw new Exception(ex.ExitCode.ToString());
        }
    }

    public async Task InstallAsync()
    {
        this.Title.Value = "事前チェック";
        var stdOuts = new List<string>();
        var stdErrors = new List<string>();

        if (this.IsInstalled)
        {
            this.Title.Value = "完了";
            return;
        }

        Directory.CreateDirectory(this.PythonDir);

        if (!File.Exists(this.PythonExePath))
        {
            this.Title.Value = "Embedded python インストール";
            await InstallEmbeddedPythonAsync(this.Config.PythonUrl, this.PythonDir);
            EnableSite(this.PythonDir);
            this.Title.Value = "get-pip";
            await InstallPipAsync(this.PythonExePath, this.PythonDir);
        }


        this.Title.Value = "pip インストール";
        await RunCommandAsync(this.PipPath, "install --upgrade pip");

        this.Title.Value = "sqlfluff インストール";
        await RunCommandAsync(this.PipPath, "install sqlfluff");

        //
        var sqlFluffConfPath = System.IO.Path.Combine(
            System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!,
            ".sqlfluff");

        System.IO.File.Copy(sqlFluffConfPath, Path.Combine(this.PythonDir, "Scripts", ".sqlfluff"));

        this.Title.Value = "完了";
    }

    private static async Task InstallEmbeddedPythonAsync(string url, string targetDir)
    {
        var zipPath = Path.Combine(targetDir, "python_embed.zip");

        using var wc = new HttpClient();
        File.WriteAllBytes(zipPath, await wc.GetByteArrayAsync(url));

        ZipFile.ExtractToDirectory(zipPath, targetDir, overwriteFiles: true);
        File.Delete(zipPath);
    }

    private static void EnableSite(string pythonDir)
    {
        var pth = Directory.GetFiles(pythonDir, "python*._pth").Single();

        var lines = File.ReadAllLines(pth)
            .Select(l => l.Trim())
            .ToList();

        if (!lines.Any(l => l == "import site"))
        {
            lines.Add("import site");
            File.WriteAllLines(pth, lines);
        }
    }

    private async Task InstallPipAsync(string pythonExe, string pythonDir)
    {
        var getPipPath = Path.Combine(pythonDir, "get-pip.py");

        using var wc = new HttpClient();
        File.WriteAllBytes(
            getPipPath,
            await wc.GetByteArrayAsync("https://bootstrap.pypa.io/get-pip.py"));

        await RunCommandAsync(pythonExe, $"\"{getPipPath}\"");
    }
}
