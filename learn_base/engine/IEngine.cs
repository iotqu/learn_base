namespace learn_base.engine;

public interface IEngine
{
    /// <summary>
    /// 初始化驱动配置参数
    /// </summary>
    void Init(string config);

    /// <summary>
    /// 启动驱动
    /// </summary>
    void Start();
}