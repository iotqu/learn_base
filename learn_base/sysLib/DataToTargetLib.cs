using System.Reflection;
using log4net;

namespace learn_base.sysLib;

public class DataToTargetLib
{
    private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

    public string DataToMqtt(string id, string data)
    {
        Log.Info($"data to mqtt [{id}]: {data}");
        return "DataToMqtt success";
    }

    public static string DataToHttp(string id, string data)
    {
        Log.Info($"data to http [{id}]: {data}");
        return "DataToHttp success";
    }

    public string[] Split(string data, string reg)
    {
        return data.Split(char.Parse(reg));
    }

    public int Len(string[] data)
    {
        return data.Length;
    }

    /// <summary>
    /// 获取系统当前时间(yyyy-MM-ddTHH:mm:ss.fff) eg：2022-10-28T13:38:23.527
    /// </summary>
    public string Now()
    {
        return DateTime.Now.ToString("yyy-MM-ddTHH:mm:ss.fff");
    }
}