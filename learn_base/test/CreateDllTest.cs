using System.CodeDom.Compiler;
using Microsoft.CSharp;
using rulex.core.plugin;

namespace learn_base.test;

public class CreateDllTest
{
    public static void LoadCsTest()
    {
        CreateDll(@"G:\workspace\c#\learn_base\learn_base\plugin\HJ212Plugin.cs");
    }

    /// <summary>
    /// 将.cs源码生成.dll程序集。 此方法应该在.Net Framework环境下运行，.NetCore不支持
    /// </summary>
    /// <param name="path"></param>
    public static void CreateDll(string path)
    {
        var provOptions = new Dictionary<string, string> { { "CompilerVersion", "v4.0" } };
        CodeDomProvider codeDomProvider = new CSharpCodeProvider(provOptions);
        var param = new CompilerParameters
        {
            GenerateExecutable = false,
            GenerateInMemory = false,
            OutputAssembly = @"G:\workspace\c#\learn_base\lib\HJ212Plugin.dll"
        };
        param.ReferencedAssemblies.Add(@"G:\workspace\c#\learn_base\lib\rulex.dll");
        // param.ReferencedAssemblies.Add("mscorlib.dll");
        // param.ReferencedAssemblies.Add("System.dll");
        // param.ReferencedAssemblies.Add("System.Core.dll");

        var compilerResults = codeDomProvider.CompileAssemblyFromSource(param, File.ReadAllText(path));
        var assembly = compilerResults.CompiledAssembly;
        var type = assembly.GetTypes().FirstOrDefault();
        if (!typeof(IPlugin).IsAssignableFrom(type))
        {
            // throw new LogException("Plugin type [" + pluginType.FullName + "] does not implement the log4net.IPlugin interface");
            Console.WriteLine("无效的插件: 未继承插件接口!");
            return;
        }


        var plugin = (IPlugin)Activator.CreateInstance(type);
        var pluginInfo = plugin.PluginInfo();
        Console.WriteLine(pluginInfo);
    }
}