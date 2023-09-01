using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace learn_base.test;

/// <summary>
/// 正则表达式相关的demo
/// </summary>
public class RegexTest
{
    /// <summary>
    /// 变量替换-正则表达式 eg: ${app:name}
    /// </summary>
    public static void VarReplaceTest()
    {
        var str = @"com/iot/${iot:sn}/${app:name}/data";
        var pattern = @"\${[^/]*:[^/]*}";
        var regex = new Regex(pattern);
        var flag = regex.IsMatch(str);
        Console.WriteLine(flag);
        foreach (Match match in regex.Matches(str))
        {
            Console.WriteLine("匹配到的: " + match.Value);
            str = str.Replace(match.Value, "140");
        }

        Console.WriteLine("替换匹配后的：" + str);
    }

    public static void customTest2()
    {
        var str = "010-12345678";
        var pattern = @"^0\d{2,3}-\d{8}$";
        var regex = new Regex(pattern);
        var flag = regex.IsMatch(str);
        Console.WriteLine(flag);
    }

    // 正则匹配 x:[1,4]
    public static void customTest3()
    {
        var str = "A2:[1,2];B:[4,5]";
        var pattern = @"[a-zA-Z0-9]+:\[[0-9]+,[0-9]+]";
        var regex = new Regex(pattern);
        var flag = regex.IsMatch(str);
        foreach (Match match in regex.Matches(str))
        {
            Console.WriteLine(match.Value);
        }
    }

    // 正则匹配 以字母开头的 长度为1~32 字母数字下划线组合
    public static void customTest4()
    {
        var str = "ss__";
        var pattern = "^[a-zA-Z]\\w{0,31}$";
        var regex = new Regex(pattern);
        var flag = regex.IsMatch(str);
        Console.WriteLine(flag);
        foreach (Match match in regex.Matches(str))
        {
            Console.WriteLine(match.Value);
        }
    }

    public static readonly ConcurrentDictionary<string, string> AttrRelCache = new();

    public static void main()
    {
        var list = new List<string> { "6" };
        test(list);
        Console.Write(list.Count);
    }

    public static void test(List<string> list)
    {
        list.Add("1");
        list.Add("2");
    }
}