using System.Reflection;
using rulex.core.plugin;

namespace learn_base.test;

/// <summary>
/// 热插拔相关测试案例, .NetCore 无法加载.Net Framework的dll
/// </summary>
public class HotSwapTest
{
    public static void LoadPlugin()
    {
        var path = Path.Combine(Environment.CurrentDirectory, "plugins");
        var info = new DirectoryInfo(path);
        foreach (var file in info.GetFiles())
        {
            var assembly = Assembly.LoadFile(file.FullName);

            var scopeName = assembly.ManifestModule.ScopeName;
            var libraryName =
                scopeName.Remove(scopeName.LastIndexOf("."), scopeName.Length - scopeName.LastIndexOf("."));
            var type = assembly.GetType(libraryName + "." + libraryName, true, true);
            if (!typeof(IPlugin).IsAssignableFrom(type))
            {
                Console.WriteLine("无效的插件: 未继承插件接口!");
                continue;
            }

            var plugin = (IPlugin)Activator.CreateInstance(type);
            var pluginInfo = plugin.PluginInfo();
            Console.WriteLine(pluginInfo.ToString());
        }
    }
}