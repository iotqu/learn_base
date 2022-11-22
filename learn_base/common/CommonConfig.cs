namespace learn_base.common;

/// <summary>
/// 通用的含有主机:端口:用户名:密码的配置
/// </summary>
public struct HostConfig
{
    public string Host;
    public int Port;
    public string Username;
    public string Password;
}

public struct MqttConfig
{
    public string clientId;
    public string server;
    public int port;
    public string username;
    public string password;
}

public struct TextConfig
{
    public string path;
    public string name;
}