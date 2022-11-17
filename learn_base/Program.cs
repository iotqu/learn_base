using System.Net;
using System.Reflection;
using System.Text;
using learn_base;
using learn_base.common;
using learn_base.core;
using learn_base.engine;
using learn_base.engine.mqttEngine;
using learn_base.model;
using learn_base.sysLib;
using learn_base.test;
using learn_base.util;
using log4net;

var log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);
//  结构体功能
// var bookStruct = TestStruct.Init(100,"Mr.Qu");
// TestStruct.Convert(bookStruct);
// Console.WriteLine(bookStruct.id);

//  实体类功能
// var inend = new Inend();
// inend.Id = 100;
// inend.Name = "Mr.Qu";
// inend.Convert();
// //inend.Id = 10086;
// Console.WriteLine(inend.Id);
// Console.WriteLine(inend.Name);

//  接口体+接口实现使用
// string config = "{\"sn\":\"IN:1401141036\",\"clientId\":\"1401141036\",\"server\":\"0.0.0.0\",\"port\":8181,\"username\":\"admin\",\"password\":\"123456\"}";
// var engine = Json.Parse<MqttEngine>(config);
// engine.Sn = "mqtt";
// engine.Convert();
// engine.Init();
// engine.Start();

//  反射功能
//  方式一
/*var types = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IEngine))))
    .ToList();
foreach (var type in types)
{
    Console.WriteLine(type.Name);
    if (type.Name.Equals("MqttEngine"))
    {
        Console.WriteLine("这是一个MQTT服务，是我们要用的");
        var engine = (IEngine)Activator.CreateInstance(type)!;
        engine.Init();
    }
}*/

//  方式二
// var typeName = string.Format(Assembly.GetEntryAssembly()?.GetName().Name + ".engine.mqttEngine.{0}Engine", "Mqtt");
// var instance = (IEngine)Assembly.GetExecutingAssembly().CreateInstance(typeName)!;
// instance.Init();
//
// var engine = (IEngine)Activator.CreateInstance(Type.GetType("learn_base.engine.mqttEngine.MqttEngine")!)!;
// engine.Init();


//  数据库功能
// var queryable = DbUtil.db.Queryable<Rule>();
// // queryable.Where(item => item.Type == "HTTP");
// // queryable.Where(item => item.Id == 2);
// List<Rule> datas = queryable.ToList();
// int total = 0; //总数据
// List<Inend> inends = DbUtil.db.Queryable<Inend>().ToPageList(1, 1, ref total);
// Console.WriteLine(inends[0].Config);
/*
Console.WriteLine(Environment.CurrentDirectory + @"\rulex.db");
DbTest.Update();
*/

//  文件功能
//FileUtil.ReadFile(@"D:\data", "test.txt");
//FileUtil.FileWatcher(@"G:\workspace\c#\rulex\rulex\bin\Debug\logs");
//FileUtil.ReadFile(@"D:\data\b.xlsx");
//FileUtil.ReadFile(@"C:\Users\admin\Desktop\ToMan\R&D Center\IoT\上位机\厂家数据\精诚\轮廓仪\","50x95-褚宾-1.FPK");

// json功能
// string str = "{\"ClientId\":\"1401141036\",\"Server\":\"0.0.0.0\",\"Port\":8181,\"Username\":\"admin\",\"Password\":\"123456\"}";
// //string str = "{name}";
// var config = new MqttConfig().Convert(str);
// Console.WriteLine(config.Password);

// string conf = "{\"clientId\":\"1401141036\",\"server\":\"0.0.0.0\",\"port\":8181,\"username\":\"admin\",\"password\":\"123456\"}";
// var mqttCon = Json.Parse<MqttCon>(conf);
// Console.WriteLine(mqttCon.password);
// Json.Convert(mqttCon);
// Console.WriteLine(mqttCon.password);

//  lua功能
// Console.WriteLine(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff"));
// var luaSource = "";
// var lua = Lua.InitLua();
// lua["sysLib"] = new DataToTargetLib();
// lua.DoFile("data_to_mqtt.lua");
//lua.DoString(luaSource);
// //var doFile = lua.DoFile("data_to_mqtt.lua");
// //  静态方法一个一个注册，然后使用
// //lua["Action"] = "我是字符串，冒牌的函数";
//Lua.AddLib("DataToMqtt", typeof(DataToTargetLib).GetMethod("DataToMqtt"));
//Lua.AddLib("Splits", typeof(DataToTargetLib).GetMethod("Splits"));
//lua.DoFile("data_to_mqtt.lua");
// //Lua.AddLib("DataToHttp", typeof(DataToTargetLib).GetMethod("DataToHttp"));
// // Lua.InvokeFunc("DataToHttp", "1", "test data");

// Lua.ValidateLua();

//  直接将类注册进lua虚拟机中，然后可以在lua调用类中的非静态方法
// Console.WriteLine(lua.State.Encoding);
// lua["sysLib"] = new DataToTargetLib(); //将C#中的类注入到Lua虚拟机中
//lua.LoadCLRPackage();
//lua.DoString(@"import('learn_base.sysLib')");
//lua.DoString("print('内部lua打印')");
// var result = lua.DoString("print('内部lua打印') return sysLib:DataToHttp('100','qbb')");
// Console.WriteLine(result[0]);
// mqtt功能
// var mqttFactories = new MqttFactories();
// mqttFactories.Init("1401141036", "127.0.0.1", 1883);
// mqttFactories.Sub("1401141036", "com/data");
// mqttFactories.Pub("1401141036", "com/data", "send data");

// new Mqtt().Init("140114","127.0.0.1",1883);
// Console.ReadLine();

//new MqttM2M().Init();
// var ipAddress = IPAddress.Parse("127.0.1000.1");
// Console.WriteLine(ipAddress.ToString());
//
// //IPAddress ip;
// var flag = IPAddress.TryParse("127.0.1000.1",out _);
// if(flag) Console.WriteLine("合法的IP");

//  Queue功能
// DataQueue.Push("a");
// DataQueue.Push("b");
// var data = DataQueue.Pull();
// Console.WriteLine(data);

//  ConcurrentQueue功能
// DataConcurrentQueue.Push("a");
// DataConcurrentQueue.Push("b");
// DataConcurrentQueue.Pull();

// Stack功能
//DataStack.Push("a");
// DataStack.Push("b");
// Console.WriteLine(DataStack.GetSize());
// Console.WriteLine(DataStack.Pull());
// Console.WriteLine(DataStack.GetSize());

// Console.WriteLine("操作系统位数：" + Environment.Is64BitOperatingSystem);
// Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
// Console.WriteLine(Environment.CurrentDirectory);


// Modbus功能
var engine = new ModbusEngine();
engine.Start();
//engine.WriteRegister(0, 99);
while (true)
{
    var data = engine.ReadRegisters(0, 9);
    foreach (var it in data)
    {
        Console.Write(it + " ");
    }

    Console.WriteLine();
    Thread.Sleep(5000);
}