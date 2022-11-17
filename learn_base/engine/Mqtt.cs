using System.Text;
using MQTTnet;
using MQTTnet.Client;

namespace learn_base.engine;

public class Mqtt
{
    private Dictionary<string, IMqttClient> _clients = new();

    public void Init(string sn, string server, int port)
    {
        try
        {
            var options = new MqttClientOptionsBuilder().WithClientId(sn)
                .WithTcpServer(server, port)
                .WithCredentials("","")
                .Build();
            var mqttFactory = new MqttFactory();
            var client = mqttFactory.CreateMqttClient();

            client.ConnectedAsync += _client_ConnectedAsync;
            client.DisconnectedAsync += _client_DisconnectedAsync;
            client.ApplicationMessageReceivedAsync += _client_ApplicationMessageReceivedAsync;

            client.ConnectAsync(options, CancellationToken.None).GetAwaiter().GetResult();
            Console.WriteLine("The MQTT client is connected.");
            _clients.Add(sn, client);
            var mqttClient = _clients[sn];
            mqttClient.SubscribeAsync("com/data");
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

    private Task _client_ConnectedAsync(MqttClientConnectedEventArgs arg)
    {
        Console.WriteLine("客户端已连接：{0}", arg.ConnectResult.ResultCode);
        return Task.CompletedTask;
    }

    private Task _client_DisconnectedAsync(MqttClientDisconnectedEventArgs arg)
    {
        Console.WriteLine("客户端断开连接：{0}", arg.ConnectResult.ResultCode);
        return Task.CompletedTask;
    }

    private Task _client_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
    {
        Console.WriteLine("收到消息：{0}  {1}  {2}", arg.ClientId,arg.ApplicationMessage.Topic,
            Encoding.UTF8.GetString(arg.ApplicationMessage.Payload)
            );
        return Task.CompletedTask;
    }
}