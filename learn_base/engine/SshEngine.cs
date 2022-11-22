using System.Reflection;
using learn_base.common;
using learn_base.util;
using log4net;
using Renci.SshNet;

namespace learn_base.engine;

public class SshEngine : IEngine
{
    private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

    private string _sn;
    private SshClient _client;
    private HostConfig _config;

    public void Init(string config)
    {
        _config = Json.Parse<HostConfig>(config);
        _sn = _config.Sn;
    }

    public void Start()
    {
        _client = new SshClient(_config.Host, _config.Port, _config.Username, _config.Password);
        _client.Connect();
    }

    public void Work(string command)
    {
        var runCommand = _client.RunCommand(command);
        if (runCommand != null) Log.Info(runCommand.Result);
    }
}