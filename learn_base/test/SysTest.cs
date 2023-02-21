using System.Net;
using System.Net.NetworkInformation;

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

}