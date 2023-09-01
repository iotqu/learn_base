using System.Net.NetworkInformation;
using System.Text;

namespace learn_base.util;

public static class NetUtil
{
    /// <summary>
    /// 检测IP或域名是否可用
    /// </summary>
    public static bool Ping(string host)
    {
        var ping = new Ping();
        var options = new PingOptions
        {
            DontFragment = true
        };
        var buffer = Encoding.UTF8.GetBytes("");
        try
        {
            var pong = ping.Send(host, 3000, buffer, options);
            var result = pong is { Status: IPStatus.Success or IPStatus.TimedOut };
            if (!result)
            {
                Console.WriteLine(pong.Status);
            }

            return result;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// 检查本机网络情况
    /// </summary>
    /// <returns>0-连接到陀曼云 1-连接到外网 2-未联网</returns>
    public static int CheckNetwork()
    {
        if (Ping("prod.zjtmcloud.com"))
        {
            return 0;
        }

        Console.WriteLine("connect to toman failed!");
        return Ping("www.baidu.com") ? 1 : 2;
    }
}