using learn_base.model;

namespace learn_base.mapper
{
    public class RuleMapper : BaseMapper<Rule>
    {
        public RuleMapper() : base()
        {
        }

        public static RuleMapper Instance()
        {
            return new RuleMapper();
        }

        public static List<Rule> QueryRuleList(Rule param)
        {
            var queryable = DbInstance.Queryable<Rule>();
            return queryable.ToList();
        }
    }
}