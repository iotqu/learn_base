using learn_base.mapper;
using learn_base.model;

namespace learn_base.test;

public class DbTest
{
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