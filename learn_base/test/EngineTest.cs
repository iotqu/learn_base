using learn_base.engine;

namespace learn_base.test;

public static class EngineTest
{
    public static void SshTest()
    {
        const string config =
            "{\"Sn\":\"140114\",\"Host\":\"192.168.5.211\",\"Port\":22,\"Username\":\"root\",\"Password\":\"Abc12345\"}";
        IEngine engine = new SshEngine();
        engine.Init(config);
        engine.Start();
        engine.Work("cd /root/logs/;ls -l");
    }
}