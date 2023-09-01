using System.Reflection;
using System.Text;
using log4net;
using NLua;

namespace learn_base.util;

public class Lua
{
    private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);
    private static NLua.Lua lua = new();

    public static NLua.Lua InitLua()
    {
        lua.State.Encoding = Encoding.Default;
        lua.DoString(@"json = require('script/json')");
        lua.DoString(@"log = require('script/log')");
        lua["log.outfile"] = "logs/lua";
        return lua;
    }

    /// <summary>
    /// 将制定类中所有的公共静态方法添加到Lua中
    /// </summary>
    /// <param name="target"></param>
    public static void AddFunc(object target)
    {
        //var methods = target.GetType().GetMethods(); // 此种方式将所有公共方法(包含父类) 添加到Lua虚拟机中
        var methods = target.GetType().GetMethods(BindingFlags.Public | BindingFlags.Static);
        foreach (var method in methods)
        {
            AddFunc(method.Name, target, method);
        }
    }

    /// <summary>
    /// 将C#函数添加到Lua虚拟机中作为LuaFunction（此方式会向global中添加很多变量）
    /// </summary>
    /// <param name="funcName">Lua虚拟机中目标函数的别名</param>
    /// <param name="target">目标函数对应的类</param>
    /// <param name="func">目标函数</param>
    public static void AddFunc(string funcName, object? target, MethodBase? func)
    {
        //lua.RegisterFunction(funcName, func); //此种方式只能添加“静态函数”
        lua.RegisterFunction(funcName, target, func); // 此种方式可添加“静态和非静态函数”
    }

    /// <summary>
    /// 将整个类中的公共非静态方法作为LuaFunction，添加Lua中的Table
    /// </summary>
    /// <param name="k">Lua中table的名称</param>
    /// <param name="func">table中的函数</param>
    public static void AddLib(string k, object func)
    {
        lua[k] = func; //此种方式与RegisterFunction()一样最终都会调用SetObjectToPath 方法
    }

    /// <summary>
    /// 调用Lua虚拟机中的函数
    /// </summary>
    /// <returns></returns>
    public static object[] InvokeFunc(string funcName, params object[] param)
    {
        var chunk = funcName + "(";
        foreach (var par in param)
        {
            if (par is string)
            {
                chunk += "\"" + par + "\",";
            }
            else
            {
                chunk += par + ",";
            }
        }

        chunk = chunk.TrimEnd(',') + ")";
        //const string format = "{0}(\"{1}\",\"{2}\")";
        //var chunk = string.Format(format, funcName, id, data);
        lua.GetFunction("DataToHttp").Call("sn:101", "biz data");
        try
        {
            return lua.DoString(chunk);
        }
        catch (Exception e)
        {
            Log.Error(e);
            return new object[] { false };
        }
    }

    public static void ValidateLua()
    {
        try
        {
            lua.DoString("lua");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        //  判断Lua虚拟机中是否含有指定函数，可以在添加创建规则的时候判断Lua脚本是否合法
        var actions = lua["Actions"];
        if (actions.GetType() != typeof(LuaFunction))
        {
            Console.WriteLine("lua中未找到Actions函数");
        }
        else
        {
            var fun = actions as LuaFunction;
            var call = fun.Call("sn:10014", "topic:com/data");
        }
    }
}