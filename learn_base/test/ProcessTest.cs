using System.Diagnostics;

namespace learn_base.test;

public class ProcessTest
{
    public static void processTest()
    {
        foreach (var p in Process.GetProcessesByName("rulex"))
        {
            //var components = p.Container.Components;
            var title = p.MainWindowTitle;
            var module = p.MainModule;
        }
    }
}