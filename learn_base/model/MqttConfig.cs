using System.Reflection;
using learn_base.util;
using log4net;

namespace learn_base.model;

public class MqttConfig
{
    private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

    public string ClientId { get; set; }
    public string Server { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public MqttConfig? Convert(string config)
    {
        if (string.IsNullOrEmpty(config))
        {
            log.WarnFormat("mqtt config is empty: {0}", config);
            return null;
        }

        try
        {
            return Json.Parse<MqttConfig>(config);
        }
        catch (Exception e)
        {
            log.WarnFormat("mqtt config error, config-> {0}", config);
            return null;
            //throw new Exception("mqtt config error, config-> " + config);
        }
    }
}