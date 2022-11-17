namespace learn_base.core;

/// Stack 栈中数据  先进后出
public class DataStack
{
    private static readonly Stack<string> dataStack = new();

    public static int GetSize()
    {
        return dataStack.Count;
    }

    /// 推送数据
    public static void Push(string data)
    {
        dataStack.Push(data);
    }

    public static string Pull()
    {
        return dataStack.Pop();
    }
}