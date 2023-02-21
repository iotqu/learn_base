using System.ComponentModel;
using learn_base.common;
using learn_base.sysLib;
using rulex.common.util;

namespace learn_base.test;

public class StructTest
{
    public struct RequestData
    {
        public string ver = "1.0.0";
        public object data;
        public string dt = new DataToTargetLib().Now();

        public RequestData(object data)
        {
            this.data = data;
        }


        public override string ToString()
        {
            return JsonUtil.Serialize(this);
        }
    }

    public static void UpdateTest()
    {
        var data = new RequestData(new Dictionary<string, string> { { "gatewayId", "sn" } });
        Console.WriteLine(data.ToString());
    }

    public static void JsonTest()
    {
        var str = "{\"server\":\"emqx.zjtmcloud.com\",\"password\":\"81d4deb8008f4aedbe44\",\"clientId\":\"GW_Q1401140000\",\"port\":1883,\"username\":\"Q1401140000\"}";
        var config = JsonUtil.DeSerializeToModel<MqttConfig>(str);
    }
}