using System.Text;
using IniParser;

namespace learn_base.test;

public class IniParserTest
{
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
            foreach (var key in section.Keys) item.Add(key.KeyName, key.Value);
        }
    }
}