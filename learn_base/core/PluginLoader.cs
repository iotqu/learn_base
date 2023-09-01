using System.Reflection;
using System.Runtime.Loader;

namespace learn_base.core;

/// <summary>
/// class PluginLoadContext : AssemblyLoadContext{}
/// </summary>
public class PluginLoader : AssemblyLoadContext
{
    private AssemblyDependencyResolver _resolver;

    public PluginLoader(string pluginPath)
    {
        _resolver = new AssemblyDependencyResolver(pluginPath);
    }

    protected override Assembly Load(AssemblyName assemblyName)
    {
        string assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
        if (assemblyPath != null)
        {
            //加载程序集
            return LoadFromStream(new FileStream(assemblyPath, FileMode.Open, FileAccess.Read));
            // LoadFromAssemblyPath该函数并不会加载依赖的程序集
            //return LoadFromAssemblyPath(assemblyPath);
        }

        //返回null,则直接加载主项目程序集
        return null;
    }

    protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
    {
        string libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
        if (libraryPath != null)
        {
            //加载native dll文件
            return LoadUnmanagedDllFromPath(libraryPath);
        }

        //返回IntPtr.Zero,即null指针.将会加载主项中runtimes文件夹下的dll
        return IntPtr.Zero;
    }
}