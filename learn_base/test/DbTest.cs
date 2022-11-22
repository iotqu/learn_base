using learn_base.mapper;
using learn_base.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace learn_base.test;

[TestClass]
public class DbTest
{
    [TestMethod]
    public void Update()
    {
        var inEnd = new InEnd
        {
            Id = 1,
            Name = "屈背背2",
            DataModels = "aaaa"
        };
        var result = InEndMapper.UpdateOutEnd(inEnd);
        Assert.AreEqual(true, result);
    }
}