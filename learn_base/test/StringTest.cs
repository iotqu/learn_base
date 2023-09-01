namespace learn_base.test;

public class StringTest
{
    public static void splitTest()
    {
        const string str = "1-2=3-4=5";
        var strings = str.Split(new[] { '-', '=' }, 2);
        Console.WriteLine(strings);
    }

    public static void splitTest2()
    {
        const string str = "";
        var strings = str.Split(new[] { '-', '=' }, 2);
        Console.WriteLine(strings);
    }

    public static void test3()
    {
        object a = false;
        if (a is bool)
        {
            Console.WriteLine("这是真的：" + a);
            return;
        }

        Console.WriteLine("这不是Bool：" + a);
    }
}