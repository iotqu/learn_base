namespace learn_base.test;

public class DirectoryTest
{
    public static void PathTest()
    {
        Console.WriteLine(Path.Combine(@"D:",@"main.txt"));
        Console.WriteLine(Path.Combine(@"D:\",@"main.txt"));
    }

    public static void InfoTest()
    {
        var info = new DirectoryInfo("D:");
        var files = info.GetFiles();
        var directories = info.GetDirectories();
        var systemInfos = info.GetFileSystemInfos();
    }
}