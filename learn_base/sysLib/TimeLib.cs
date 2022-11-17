namespace learn_base.sysLib;

public class TimeLib
{
    /// <summary>
    /// 获取系统当前时间(yyyy-MM-ddTHH:mm:ss.fff) eg：2022-10-28T13:38:23.527
    /// </summary>
    public string Now()
    {
        return DateTime.Now.ToString("yyy-MM-ddTHH:mm:ss.fff");
    }
}