using System.Reflection;
using learn_base.util;

namespace learn_base.test;

public class FileTest


{
    public static void LoadPlugin()
    {
        var ass = Assembly.LoadFile(@"plugins\HJ212Plugin.dll");
        var asse = Assembly.LoadFrom("");
    }

    public static void FileInfoTest()
    {
        var fileInfo = new FileInfo(@"plugins\HJ212Plugin.dll");
        Console.WriteLine(fileInfo.Exists);
        Console.WriteLine(fileInfo.Name);
        Console.WriteLine(Path.GetFileNameWithoutExtension(@"plugins\HJ212Plugin.dll"));
    }

    public static void PathTest()
    {
        Console.WriteLine(Path.Combine("D:", @"\abd\main.txt"));
        Console.WriteLine(Path.Combine(@"D:\", @"main.txt"));
    }

    public static void InfoTest()
    {
        var info = new DirectoryInfo("D:");
        var files = info.GetFiles();
        var directories = info.GetDirectories();
        var systemInfos = info.GetFileSystemInfos();
    }

    public static void ReadFileTest()
    {
        FileUtil.ReadFile("D:", "_20221216_091939.xlsx");
    }

    public static void ReadPdfTest()
    {
        //FileUtil.ReadFile("D:\\Data\\JC", "李轩轩.pdf");
        FileUtil.ReadPdf("D:\\Data\\JC\\李轩轩.pdf");
    }

    public static void FileWatcher()
    {
        AccessDbTest.Init();
        var path = @"D:\Data\di_bei\";
        FileUtil.FileWatcher(path, "*.mdb");
    }
}