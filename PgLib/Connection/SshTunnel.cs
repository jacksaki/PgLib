using PgLib.Connection;
using Renci.SshNet;
using System.Collections.Concurrent;
using static Org.BouncyCastle.Math.EC.ECCurve;

internal sealed class SshTunnel : IDisposable
{
    private readonly ConnectionConfig _config;
    private readonly object _lock = new();
    private SshClient? _client;
    private ForwardedPortLocal? _port;
    private CancellationTokenSource _cts = new();
    private Task? _monitorTask;
    private bool _disposed;

    public uint? LocalPort { get; private set; }

    public SshTunnel(ConnectionConfig config)
    {
        _config = config;
    }

    private AuthenticationMethod GetAuthenticationMethod(SshConfig conf)
    {
        if (string.IsNullOrEmpty(conf.SshPrivateKey))
        {
            return new PasswordAuthenticationMethod(conf.SshUserName, conf.SshPassword);
        }
        else
        {
            return new PrivateKeyAuthenticationMethod(conf.SshUserName, new PrivateKeyFile(conf.SshPrivateKey, string.IsNullOrEmpty(conf.SshPassword) ? null : conf.SshPassword));
        }
    }

    public void Start()
    {
        lock (_lock)
        {
            Cleanup();

            LocalPort = _config.SshConfig!.LocalPort ?? LocalPortAllocator.Allocate();
            var ssh = _config.SshConfig!;
            var connectionInfo = new ConnectionInfo(
                ssh.SshHostName,
                ssh.SshPort,
                ssh.SshUserName,
                GetAuthenticationMethod(ssh)
            );

            _client = new SshClient(connectionInfo);
            _client.Connect();

            _port = new ForwardedPortLocal(
                "127.0.0.1",
                LocalPort.Value,
                "localhost",
                _config.DbPort
            );

            _client.AddForwardedPort(_port);
            _port.Start();
            StartMonitor();
        }
    }

    private void StartMonitor()
    {
        _monitorTask = Task.Run(async () =>
        {
            while (!_cts.Token.IsCancellationRequested)
            {
                await Task.Delay(5000, _cts.Token);

                if (_client == null || !_client.IsConnected)
                {
                    try
                    {
                        Start(); // 再接続
                    }
                    catch
                    {
                        // 必要ならログ出す
                    }
                }
            }
        });
    }

    private void Cleanup()
    {
        try { _port?.Stop(); } catch { }
        try { _client?.Disconnect(); } catch { }

        _port?.Dispose();
        _client?.Dispose();

        _port = null;
        _client = null;
    }

    public void Dispose()
    {
        if (_disposed) return;

        _cts.Cancel();
        _monitorTask?.Wait();

        Cleanup();

        _cts.Dispose();
        _disposed = true;
    }
}
