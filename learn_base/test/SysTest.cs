using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using Microsoft.Win32;

namespace learn_base.test;

public class SysTest
{
    public static void GetDeviceInfo()
    {
        string mac = string.Empty; //物理MAC地址
        string ip = string.Empty; //IPv6地址

        foreach (var netInterface in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
            {
                var address = netInterface.GetPhysicalAddress();
                mac = BitConverter.ToString(address.GetAddressBytes());

                IPAddress[] addresses = Dns.GetHostAddresses(Dns.GetHostName());
                if (addresses != null && addresses[0] != null)
                {
                    ip = addresses[0].ToString();
                    break;
                }
            }
        }

        Console.WriteLine(ip);
        Console.WriteLine(mac);
    }

    public static void GetSerialPort()
    {
        using (var search =
               new ManagementObjectSearcher(
                   "select * from Win32_PnPEntity where ClassGuid = '{4d36e965-e325-11ce-bfc1-08002be10318}'"))
        {
            foreach (var info in search.Get())
            {
                var name = Convert.ToString(info.Properties["Name"].Value);
                if (name == null || !(name.Contains("(COM") && name.EndsWith(")"))) continue;
                Console.WriteLine(name);
            }
        }
    }

    public static void GetSerialPort2()
    {
        try
        {
            var rootKey = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DEVICEMAP\SERIALCOMM");
            foreach (var name in rootKey.GetValueNames())
            {
                var result = (string)(rootKey.GetValue(name, "未找到通讯端口，请关闭程序，重新插拔数据线!"));
                Console.WriteLine(result);
            }
        }
        catch (Exception ex)
        {
        }
    }
}