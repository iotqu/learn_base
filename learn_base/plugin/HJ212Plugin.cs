using rulex.core.plugin;

namespace learn_base.plugin;

public class Hj212Plugin : IPlugin
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
            Name = "HJ212 Plugin",
            Version = "v1.0.0",
            Author = "Mr.Qu",
            LastTime = DateTime.Now
        };
    }
}