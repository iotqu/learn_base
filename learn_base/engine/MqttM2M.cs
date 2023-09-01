using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Loader;
using System.Text;
using learn_base.util;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace learn_base.engine;

public class MqttM2M
{
    public void Init()
    {
        MqttClient client = new MqttClient(IPAddress.Parse("127.0.0.1"));
        client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

        string clientId = Guid.NewGuid().ToString();
        client.Connect(clientId);

        client.Subscribe(new string[] { "com/iot/plugin" },
            new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });

        //client.Publish("com/data", Encoding.Default.GetBytes("qwesdfg"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
    }

    public void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        var msg = Encoding.UTF8.GetString(e.Message);
        Console.WriteLine("======receive message > " + e.Topic + " : " + msg);
        var obj = Json.Parse<dynamic>(msg);
        var cmd = (string)obj.cmd;//插件操作
        if (cmd == "load")
        {
            
        }
        var name = (string)obj.name;//插件名称
    }
}