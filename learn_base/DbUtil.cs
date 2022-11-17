using SqlSugar;

namespace learn_base;

public class DbUtil
{
    public static SqlSugarScope db = new SqlSugarScope(new ConnectionConfig()
    {
        ConnectionString = @"data source=G:\workspace\golang\i4de\rulex\rulex.db",
        DbType = DbType.Sqlite,
        IsAutoCloseConnection = true, //自动释放连接，如果存在事务，在事务结束后释放
        InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
    },
        db =>
        {
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql); //输出sql,查看执行sql 性能无影响
                Console.WriteLine(string.Join(",", pars?.Select(it => it.ParameterName + ":" + it.Value)));//参数
            };
        });

    /*public SqlSugarHelper(string url)
    {
        db = new SqlSugarScope(new ConnectionConfig()
            {
                ConnectionString = url,
                DbType = DbType.Sqlite,
                IsAutoCloseConnection = true, //自动释放连接，如果存在事务，在事务结束后释放
                InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
            }
        );
    }*/
}