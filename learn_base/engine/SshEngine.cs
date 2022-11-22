using learn_base.common;
using learn_base.util;
using Renci.SshNet;

namespace learn_base.engine;

public class SshEngine : IEngine
{
    private string sn;
    private SshClient client;
    private HostConfig mainconfig;

    public void Init(string config)
    {
        mainconfig = Json.Parse<HostConfig>(config);
    }

    public void Start()
    {
        throw new NotImplementedException();
    }
}