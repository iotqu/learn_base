using MQTTnet.Client;

namespace learn_base.engine.mqttEngine;

public struct MqttEngine : IEngine
{
    public string Sn;
    public MqttClient Client;
    public int Status;

    public void Convert()
    {
        Sn += "001";
    }

    public void Init()
    {
        Console.WriteLine("Mqtt init -> " + Sn);
    }

    public void Start()
    {
        Console.WriteLine("Mqtt start -> " + Sn);
    }
}