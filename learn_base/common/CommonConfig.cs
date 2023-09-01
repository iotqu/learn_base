using System.ComponentModel.DataAnnotations;
using learn_base.common.validation;
using Newtonsoft.Json.Linq;
using rulex.common.util;

namespace learn_base.common;

/// <summary>
/// 通用的含有主机:端口:用户名:密码的配置
/// </summary>
public struct TcpConfig
{
    public string Sn;

    [CustomValidation(typeof(ValidationExtension), "IpValidate")]
    public string Host;

    public int Port;
    public string Username;
    public string Password;
}

public struct MqttConfig
{
    public string ClientId;
    public string Server;
    public int Port;
    public string Username;
    public string Password;
}

public struct TextConfig
{
    public string path;
    public string name;
}

public struct ModbusConfig
{
    /// 工作模式 RTU、TCP
    [Required(ErrorMessage = "[工作模式] 不能为空")]
    public string Mode;

    /// 采集频率,单位：秒，默认5秒
    [Range(3, 30, ErrorMessage = "[采集频率] 无效")]
    public int Frequency;

    /// <summary>
    /// 寄存器地址
    /// </summary>
    //public int Address;

    /// <summary>
    /// 读取的数量
    /// </summary>
    //public int Quantity;

    [JsonFormat("Mode", ErrorMessage = "[Modbus配置]非法的JSON字符串")]
    public JObject Config;

    public override string ToString()
    {
        return JsonUtil.Serialize(this);
    }
}