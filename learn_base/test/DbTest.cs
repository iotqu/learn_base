using learn_base.mapper;
using learn_base.model;
using learn_base.util;

namespace learn_base.test;

public class DbTest
{

    public static void Query()
    {
        var queryable = DbUtil.db.Queryable<InEnd>();
        queryable.Where(item => item.Type == "HTTP");
        queryable.Where(item => item.Id == 2);
        var data = queryable.ToList();
    }

    public static void QueryPage()
    {
        var total = 0; //总数据
        var data = DbUtil.db.Queryable<InEnd>().ToPageList(1, 1, ref total);
        Console.WriteLine(data[0].Config);
    }

    public static void Update()
    {
        var inEnd = new InEnd
        {
            Id = 1,
            Name = "屈背背2",
            DataModels = "aaaa"
        };
        var result = InEndMapper.UpdateOutEnd(inEnd);
    }
}