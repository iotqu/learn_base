using System.Globalization;
using HslCommunication.ModBus;
using HslCommunication.Profinet.Siemens;

namespace learn_base.test;

/// <summary>
/// HslCommunication协议包相关的测试案例
/// </summary>
public class HslTest
{
    private const int Time = 1000 * 2;
    private const string Ip = "127.0.0.1";

    public static void ModbusTcpTest()
    {
        var client = new ModbusTcpNet(Ip);
        var result = client.ConnectServer();
        while (result.IsSuccess)
        {
            var read = client.Read("0", 10);
            var data = client.ReadInt16("0", 10);
            Console.Write(client.ReadInt16("0").Content + " ");
            Console.Write(client.ReadInt16("1").Content + " ");
            Console.Write(client.ReadInt16("2").Content + " ");
            Console.Write(client.ReadInt16("3").Content + " ");
            Console.WriteLine(client.ReadInt16("4").Content);
            Thread.Sleep(Time);
        }

        Console.WriteLine("ModbusTcp连接失败：" + result.Message);
    }

    public static void ModbusRtuTest()
    {
        var client = new ModbusRtu();
        // 默认 9600、 8 data bit、  1 stop bit、 N
        client.SerialPortInni("COM2-9600-8-E-1");
        var result = client.Open();
        while (result.IsSuccess)
        {
            Console.Write(client.ReadInt16("0").Content + " ");
            Console.WriteLine(client.ReadInt16("1").Content);
            Thread.Sleep(Time);
        }

        Console.WriteLine("串口打开失败：" + result.Message);
    }

    /// <summary>
    /// https://www.cnblogs.com/dathlin/p/8685855.html
    /// </summary>
    public static void SiemensS7Test()
    {
        var client = new SiemensS7Net(SiemensPLCS.S200);
        var result = client.ConnectServer();
        while (result.IsSuccess)
        {
            Console.Write(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " " + client.ReadInt16("M100").Content + " ");
            Console.WriteLine(client.ReadInt16("M101").Content);
            Thread.Sleep(Time);
        }

        Console.WriteLine("SiemensS7-200 连接失败：" + result.Message);
    }
}