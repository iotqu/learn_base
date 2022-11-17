using System.ComponentModel;
using System.Reflection;

namespace learn_base.common;

public class Constant
{
    /// <summary>
    /// 驱动的完全限定名格式  eg:rulex.common.core.engine.MqttEngine
    /// </summary>
    public static readonly string EngineFullName = Assembly.GetEntryAssembly()?.GetName().Name + ".engine.mqttEngine.{0}Engine";

    /// <summary>
    /// 引擎类型
    /// </summary>
    public enum EngineEnum
    {
        Mqtt,
        Text,
        Excel
    }

    /// <summary>
    /// 资源类型，引擎是数据源还是目的地
    /// </summary>
    public enum ResourceEnum
    {
        [Description("数据源")] InEnd,
        [Description("目的地")] OutEnd
    }
}