using System.Reflection;
using System.Text;
using learn_base.sysLib;
using NLua;

namespace learn_base.util;

public class Lua
{
    private static NLua.Lua lua = new();


    public static NLua.Lua InitLua()
    {
        lua.State.Encoding = Encoding.Default;
        lua.DoString(@"json = require('script/json')");
        lua.DoString(@"log = require('script/log')");
        AddLib("log.outfile","lua_log");
        //lua["log.outfile"] = "lua_log";
        return lua;
    }

    public static void AddLib(string funcName, MethodBase? func)
    {
        lua.RegisterFunction(funcName, null, func);
    }

    public static void AddLib(string k, object func)
    {
        lua[k] = func;
    }

    public static object[] InvokeFunc(string funcName, string id, string data)
    {
        const string format = "{0}(\"{1}\",\"{2}\")";
        var chunk = string.Format(format, funcName, id, data);
        return lua.DoString(chunk);
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