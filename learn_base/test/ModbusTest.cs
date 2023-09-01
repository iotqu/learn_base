using learn_base.engine;

namespace learn_base.test;

public class ModbusTest
{
    /// <summary>
    /// 串口方式
    /// </summary>
    public static void ComTest()
    {
        var engine = new ModbusEngine();
        engine.Start();
        engine.WriteRegister(0, 66);
        while (true)
        {
            var data = engine.ReadRegisters(0, 9);
            foreach (var it in data)
            {
                //log.Error(it + " ");
                Console.Write(it + " ");
            }

            Console.WriteLine();
            Thread.Sleep(5000);
        }
    }
}