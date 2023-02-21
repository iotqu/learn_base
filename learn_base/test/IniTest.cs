using System.Runtime.InteropServices;
using System.Text;

namespace learn_base.test;

public static class IniTest
{
    [DllImport("kernel32")]
    private static extern long GetPrivateProfileString(string section, string key, string defaultValue,
        byte[] retVal, int size, string filePath);

    [DllImport("kernel32")]
    private static extern long GetPrivateProfileString(string section, string key, string defaultValue,
        StringBuilder retVal, int size, string filePath);

    [DllImport("kernel32")]
    private static extern int GetPrivateProfileInt(string section, string key, int noText, string filePath);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    private static extern uint GetPrivateProfileSection(string lpAppName, IntPtr lpReturnedString, uint nSize,
        string lpFileName);

    private const string Path = @"G:\workspace\c#\rulex\conf\rulex.ini";

    public static void GetTest()
    {
        var sn = "RULE:140114";
        var path = Get("app", "lua_log_path");
        var s = path + sn.Split(':')[1];
        //Console.Write(s);
    }

    public static void Test()
    {
    }

    /// <summary>
    /// 读取所有的配置参数
    /// </summary>
    public static Dictionary<string, Dictionary<string, object>> ReadAll()
    {
        Dictionary<string, Dictionary<string, object>> result = new();
        foreach (var section in ReadSection(null))
        {
            result[section] = ReadItem(section);
        }

        return result;
    }

    /// <summary>
    /// 获取指定section下的key名称，如果section为null，则获取所有的section名称
    /// </summary>
    public static List<string> ReadSection(string section)
    {
        var buf = new byte[65536];
        var length = GetPrivateProfileString(section, null, null, buf, buf.Length, Path);
        return new List<string>(Encoding.Default.GetString(buf.Take((int)length).ToArray())
            .Split(new[] { '\0' }, StringSplitOptions.RemoveEmptyEntries));
    }

    /// <summary>
    /// 获取指定section下的item(key=value)
    /// </summary>
    public static Dictionary<string, object> ReadItem(string section)
    {
        const uint maxBuffer = 32767;
        var ptr = Marshal.AllocCoTaskMem((int)maxBuffer * sizeof(char));
        var bytesReturned = GetPrivateProfileSection(section, ptr, maxBuffer, Path);
        var str = Marshal.PtrToStringAuto(ptr, (int)bytesReturned);
        Marshal.FreeCoTaskMem(ptr);
        var items = str.Split(new[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
        return (from item in items where !item.StartsWith("#") select item.Split(new[] { '=' }, 2))
            .ToDictionary<string[], string, object>(data => data[0], data => data[1]);
    }


    /// <summary>
    /// 获取配置文件内容
    /// </summary>
    public static int GetInt(string section, string key)
    {
        return GetPrivateProfileInt(section, key, 10, Path);
    }

    /// <summary>
    /// 根据section与key获取指定的value
    /// </summary>
    public static string Get(string section, string key)
    {
        var buf = new byte[255];
        GetPrivateProfileString(section, key, null, buf, buf.Length, Path);
        return Encoding.Default.GetString(buf);
    }

    public static string GetAppName()
    {
        return Get("app", "name");
    }

    public static bool GetBool(string section, string key)
    {
        return bool.Parse(Get(section, key));
    }
}