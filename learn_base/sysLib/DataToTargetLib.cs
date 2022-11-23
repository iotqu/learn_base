namespace learn_base.sysLib;

public class DataToTargetLib
{
    public string DataToMqtt(string id, string data)
    {
        Console.WriteLine("data to mqtt :" + data);
        return "DataToMqtt success";
    }

    public string DataToHttp(string id, string data)
    {
        Console.WriteLine("data to http :" + data);
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