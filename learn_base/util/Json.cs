using System.Runtime.Serialization.Json;
using System.Text;
using learn_base.common;

namespace learn_base.util;

public class Json
{
    public static T? Parse<T>(string jsonString)
    {
        using var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
        return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms)!;
    }

    public static string Stringify(object jsonObject)
    {
        using var ms = new MemoryStream();
        new DataContractJsonSerializer(jsonObject.GetType()).WriteObject(ms, jsonObject);
        return Encoding.UTF8.GetString(ms.ToArray());
    }

    public static void Convert(MqttConfig config)
    {
        config.password = "111111";
    }
}