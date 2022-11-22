using MQTTnet.Client;

namespace learn_base.engine.mqttEngine;

public class HttpEngine : IEngine
{
    public string sn;
    public MqttClient client;
    public int status;

    public void Init(string config)
    {
        Console.WriteLine("Http init -> " + sn);
    }

    public void Start()
    {
        Console.WriteLine("Http start -> " + sn);
    }
}