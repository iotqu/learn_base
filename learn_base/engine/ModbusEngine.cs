using System.IO.Ports;
using EasyModbus;
using xktComm;

namespace learn_base.engine;

public class ModbusEngine : IEngine
{
    private ModbusClient client;

    public void Init(string config)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 启动Modbus客户端 client携带串口参数则为Modbus Rtu,否则为Modbus Tcp
    /// </summary>
    public void Start()
    {
        var tcp = new ModbusTcp();
        var rtu = new ModbusRtu();
        var ppi = new SiemensPPI(); // 串口协议
        var s7 = new SiemensS7(); // 网口协议

        client = new ModbusClient("192.168.5.28", 502)
        {
            UnitIdentifier = 1,
            SerialPort = "COM2",
            Baudrate = 9600,
            Parity = Parity.Even,
            StopBits = StopBits.One
        };
        client.Connect();
    }

    public void Work(string data)
    {
    }

    public IEnumerable<int> ReadRegisters(ushort regStartAddr, ushort regEndAddr)
    {
        var length = (ushort)(regEndAddr - regStartAddr + 1);
        return client.ReadHoldingRegisters(regStartAddr, length);
    }

    public byte[]? WriteRegister(ushort regAddr, ushort value)
    {
        client.receiveData = null;
        client.WriteSingleRegister(regAddr, value);
        return client.receiveData;
    }
}