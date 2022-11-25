using learn_base.model;

namespace learn_base.mapper
{
    public class InEndMapper : BaseMapper<InEnd>
    {
        public InEndMapper() : base()
        {
        }

        public static InEndMapper Instance()
        {
            return new InEndMapper();
        }

        public static List<InEnd> QueryInEndList(InEnd param)
        {
            var queryable = DbInstance.Queryable<InEnd>();
            if (param.Id != 0)
            {
                queryable.Where(item => item.Id == param.Id);
            }

            if (!string.IsNullOrEmpty(param.Type))
            {
                queryable.Where(item => item.Type == param.Type);
            }

            return queryable.ToList();
        }

        /// <summary>
        /// 对指定列进行更新
        /// </summary>
        public static bool UpdateOutEnd(InEnd param)
        {
            return DbInstance.Updateable(param).IgnoreColumns(ignoreAllNullColumns: true).IgnoreColumns(it => new
            {
                it.Uuid,
                it.Type,
                it.Name
            }).ExecuteCommandHasChange();
        }
    }
}