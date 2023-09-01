using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using Microsoft.Win32;
using NPOI.SS.Formula.Functions;

namespace learn_base.util;

public class SysUtil
{
    /// <summary>
    /// 检测IP或域名是否可用
    /// </summary>
    public static bool Ping(string host)
    {
        System.Net.ServicePointManager.SecurityProtocol =
            SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        var ping = new Ping();
        try
        {
            var pong = ping.Send(host, 100);
            return pong is { Status: IPStatus.Success };
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static void RegTable()
    {
        var key = Registry.LocalMachine;
        var subK = key.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\VisualStudio\12.0\VC\Runtimes\x86");
        if (subK == null)
        {
            key.Close();
            // todo 未安装 Microsoft visual c++ 2013
        }

        var subV = (string)subK.GetValue("Version");
        Console.WriteLine("result: " + subV);
    }

    public static void main()
    {
        //ExecuteCmd(@"H:\环境\vcredist_x86_2013.exe");
        var result = ExecuteCmd("ipconfig");
        Console.WriteLine("result: " + result);
    }

    public static string ExecuteCmd(string cmd)
    {
        var process = new Process();
        var startInfo = new ProcessStartInfo
        {
            FileName = "cmd.exe", // 执行的命令，这里为 cmd.exe
            Arguments = "/c " + cmd, // 命令参数
            UseShellExecute = false, // 允许使用操作系统的shell
            RedirectStandardInput = true, // 重定向标准输入
            RedirectStandardOutput = true, // 重定向标准输出
            CreateNoWindow = true // 不创建新窗口
        };
        process.StartInfo = startInfo;
        process.Start();
        var output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        return output;
    }
}