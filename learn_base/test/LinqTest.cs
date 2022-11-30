namespace learn_base.test;

public class LinqTest
{
    public static void SelectTest()
    {
        int[] data = { 1, 2, 3, 4 };
        var res = from it in data where it % 2 == 0 select it;
        Console.WriteLine(res.Count());

        var ints = data.Where(it => it % 2 == 0).ToList();
        Console.WriteLine(ints.Count);
    }

}