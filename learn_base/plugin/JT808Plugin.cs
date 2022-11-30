using rulex.core.plugin;

namespace learn_base.plugin;

public class Jt808Plugin : IPlugin
{
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public void Init(string sn, string config, string type)
    {
        throw new NotImplementedException();
    }

    public bool Start()
    {
        throw new NotImplementedException();
    }

    public void InBound()
    {
        throw new NotImplementedException();
    }

    public void Load()
    {
        throw new NotImplementedException();
    }

    public PluginInfo PluginInfo()
    {
        return new PluginInfo
        {
            Name = "JT808 Plugin",
            Version = "v1.0.0",
            Author = "Mr.Qu",
            LastTime = DateTime.Now
        };
    }
}