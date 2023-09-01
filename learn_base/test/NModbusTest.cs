using System.IO.Ports;
using Modbus.Device;

namespace learn_base.test;

public class NModbusTest
{
    public static void SerialPortTest()
    {
        var port = new SerialPort("COM2");
        port.BaudRate = 9600;
        port.DataBits = 8;
        port.Parity = Parity.Even;
        port.StopBits = StopBits.One;
        //port.Open();
        var client = ModbusSerialMaster.CreateRtu(port);
        while (true)
        {
            var data = client.ReadHoldingRegisters(1, 0, 1);
            foreach (var it in data)
            {
                Console.Write(it + " ");
            }

            Console.WriteLine("");
            Thread.Sleep(5000);
        }
    }
}