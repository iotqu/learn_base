namespace learn_base.sysLib;

public class StringLib
{
    public string[] Split(string data, string reg)
    {
        return data.Split(char.Parse(reg));
    }

    public int Len(string[] data)
    {
        return data.Length;
    }
}