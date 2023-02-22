using System.Text;
using System.Text.RegularExpressions;
using IniParser;

namespace learn_base.test;

public class IniParserTest
{
    private static string placeholderPrefix = "${";
    private static string placeholderSuffix = "}";
    private static string placeholderCaller = ":";
    private static string valueSeparator = ",";
    private const string Path = @"G:\workspace\c#\learn_base\learn_base\conf\learn.ini";
    private static readonly Dictionary<string, Dictionary<string, string>> _sysParam = new();

    public static void ReadTest()
    {
        var parser = new FileIniDataParser();
        parser.Parser.Configuration.CommentString = "#";
        var data = parser.ReadFile(Path, Encoding.Default);
        var sections = data.Sections;

        foreach (var section in sections)
        {
            Console.WriteLine(section.SectionName);
            foreach (var key in section.Keys)
            {
                Console.WriteLine(" " + key.KeyName + " = " + key.Value);
            }
        }
    }

    public static void ReadAllTest()
    {
        var parser = new FileIniDataParser();
        parser.Parser.Configuration.CommentString = "#";
        var data = parser.ReadFile(Path, Encoding.Default);

        foreach (var section in data.Sections)
        {
            var item = new Dictionary<string, string>();
            _sysParam.Add(section.SectionName, item);
            foreach (var key in section.Keys)
            {
                item.Add(key.KeyName, ReplacePlaceholders(key.Value, @"\${[^/]*:[^/]*}"));
            }
        }
    }

    public static string GetSysParam(string section, string key)
    {
        if (string.IsNullOrWhiteSpace(section) || string.IsNullOrWhiteSpace(key)) return null!;
        var flag = _sysParam.TryGetValue(section, out var item);
        return !flag || !item.TryGetValue(key, out var value) ? null : value;
    }

    /// <summary>
    /// 根据正则pattern解析value里面的占位符(eg: ${app:name})，并替换为相应的属性值
    /// </summary>
    private static string ReplacePlaceholders(string value, string pattern)
    {
        var regex = new Regex(pattern);
        foreach (Match match in regex.Matches(value))
        {
            // 根据正则匹配到的占位符, 例：${app:name}
            var placeholder = match.Value;

            var length = placeholder.Length - placeholderPrefix.Length - placeholderSuffix.Length;
            // 获取${}里的真正属性名称,例：app:name
            var property = placeholder.Substring(placeholderPrefix.Length, length);
            var split = property.Split(placeholderCaller, StringSplitOptions.RemoveEmptyEntries);

            // 获取属性键placeholder对应的属性值
            var propVal = GetSysParam(split[0], split[1]);
            value = value.Replace(placeholder, propVal);
        }

        return value;
    }
}