using System.Net;
using System.Text;
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

        client.Subscribe(new string[] { "com/data" },
            new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });

        client.Publish("com/data",
            Encoding.Default.GetBytes("qwesdfg") /*Message*/,
            MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE /*QoS*/,
            false);
    }

    public void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        Console.WriteLine("======receive message > " + e.Topic + " : " + Encoding.UTF8.GetString(e.Message));
    }
}