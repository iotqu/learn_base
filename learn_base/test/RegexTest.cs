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
}