namespace learn_base.test;

public class StringTest
{
    public static void splitTest()
    {
        const string str = "1-2=3-4=5";
        var strings = str.Split(new[] { '-', '=' }, 2);
        Console.WriteLine(strings);
    }
}