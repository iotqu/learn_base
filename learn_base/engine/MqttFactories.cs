using System.Text;
using MQTTnet;
using MQTTnet.Client;

namespace learn_base.engine;

public class MqttFactories
{
    private Dictionary<string, IMqttClient> _clients = new();

    public void Init(string sn, string server, int port)
    {
        try
        {
            var options = new MqttClientOptionsBuilder().WithClientId(sn).WithTcpServer(server, port).Build();
            var mqttFactory = new MqttFactory();
            var client = mqttFactory.CreateMqttClient();

            client.ConnectAsync(options, CancellationToken.None).GetAwaiter().GetResult();
            Console.WriteLine("The MQTT client is connected.");
            _clients.Add(sn, client);
            // var mqttClient = _clients[sn];
            // var message = new MqttApplicationMessageBuilder().WithTopic("com/data").WithPayload("data").Build();
            // var result = await mqttClient.PublishAsync(message, CancellationToken.None);
            // Console.WriteLine(result.ReasonCode + " " + result.ReasonString);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    public void Pub(string sn, string topic, string data)
    {
        try
        {
            var client = _clients[sn];
            if (!client.IsConnected)
            {
                Console.WriteLine("非法的SN[{0}]", sn);
                return;
            }

            var message = new MqttApplicationMessageBuilder().WithTopic(topic).WithPayload(data).Build();
            var result = client.PublishAsync(message, CancellationToken.None).GetAwaiter().GetResult();
            Console.WriteLine(result.ReasonCode + " " + result.ReasonString);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void Sub(string sn, string topic)
    {
        try
        {
            var client = _clients[sn];
            if (!client.IsConnected)
            {
                Console.WriteLine("非法的SN[{0}]", sn);
                return;
            }

            client.ApplicationMessageReceivedAsync += e =>
            {
                Console.WriteLine("received message ——》" + Encoding.UTF8.GetString(e.ApplicationMessage.Payload));
                return Task.CompletedTask;
            };

            //client.ApplicationMessageReceivedAsync

            var mqttFactory = new MqttFactory();
            var options = mqttFactory.CreateSubscribeOptionsBuilder().WithTopicFilter(f => f.WithTopic(topic)).Build();
            var result = client.SubscribeAsync(options, CancellationToken.None).GetAwaiter().GetResult();
            Console.WriteLine(result.ReasonString);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}