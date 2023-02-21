using System.Collections.Concurrent;
using System.Reflection;
using System.Text;
using learn_base.model;
using log4net;
using MiniExcelLibs;

namespace learn_base.util;

public class FileUtil
{
    private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

    public static ConcurrentDictionary<string, StreamReader> _TEXT_CLIENTS = new();

    public static void ReadFile(string filePath, string fileName)
    {
        var read = new StreamReader(@$"{filePath}\{fileName}",Encoding.UTF32);
        while (!read.EndOfStream)
        {
            var readToEnd = read.ReadToEnd();
            var data = read.ReadLine();
            log.Info(data);
            Console.WriteLine(data);
        }
    }

    public static void ReadFile(string path)
    {
        var devices = MiniExcel.Query<Device>(path);
        foreach (var device in devices)
        {
            log.Info(device.id + " " + device.name + "" + device.日期);
        }
    }

    //private FileSystemWatcher watcher;

    public static void FileWatcher(string filePath)
    {
        var watcher = new FileSystemWatcher();
        watcher.Path = filePath;
        watcher.Filter = "*.csv";
        watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size | NotifyFilters.DirectoryName;
        watcher.IncludeSubdirectories = true; //监控指定路径中的子目录
        watcher.Created += fileWatcher_created;
        watcher.Changed += fileWatcher_changed;
        watcher.EnableRaisingEvents = true;
        while (true)
        {
        }
    }

    private static void fileWatcher_created(object sender, FileSystemEventArgs e)
    {
        //var read = new StreamReader(e.FullPath, Encoding.UTF8);
        var fs = new FileStream(e.FullPath, FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
        _TEXT_CLIENTS[e.FullPath] = new StreamReader(fs, Encoding.UTF8);
        log.Info("新增：" + e.ChangeType + " " + e.FullPath + " " + e.Name);
        //Console.WriteLine("新增：" + e.ChangeType + " " + e.FullPath + " " + e.Name);
    }

    private static void fileWatcher_changed(object sender, FileSystemEventArgs e)
    {
        log.Info("修改：" + e.ChangeType + " " + e.FullPath + " " + e.Name);

        //var fs = new FileStream(e.FullPath, FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
        var reader = _TEXT_CLIENTS.GetOrAdd(e.FullPath, new StreamReader(new FileStream(e.FullPath, FileMode.Open,FileAccess.Read,FileShare.ReadWrite), Encoding.UTF8));
        while (!reader.EndOfStream)
        {
            var data = reader.ReadLine();
            //var readLineAsync = reader.ReadLineAsync();
            //var data = readLineAsync.Result;
            log.Info(e.Name + " ==>" + data +"\n");
        }
        //Console.WriteLine("修改：" + e.ChangeType + " " + e.FullPath + " " + e.Name);
    }

    /// <summary>
    /// 判断目标是文件夹还是目录(目录包括磁盘)
    /// </summary>
    /// <param name="filepath">路径</param>
    /// <returns>返回true为一个文件，返回false为一个文件夹</returns>
    public static bool isFile(string filepath)
    {
        var fi = new FileInfo(filepath);
        return (fi.Attributes & FileAttributes.Directory) == 0;
    }
}