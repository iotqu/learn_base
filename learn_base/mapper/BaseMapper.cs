using System.Linq.Expressions;
using SqlSugar;

namespace learn_base.mapper
{
    public class BaseMapper<T> where T : class, new()
    {
        private static readonly string DbPath = Environment.CurrentDirectory + @"\rulex.db";

        public static readonly SqlSugarClient DbInstance = new(new ConnectionConfig()
        {
            //ConnectionString = @"data source=C:\rulex.db",
            ConnectionString = @"data source=" + DbPath,
            DbType = DbType.Sqlite,
            IsAutoCloseConnection = true,
            InitKeyType = InitKeyType.Attribute,
        });

        #region 使用原生SQL

        /// <summary>
        /// 使用SQL语句返回List
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected List<T> GetListModel(string sql)
        {
            return DbInstance.Ado.SqlQuery<T>(sql);
        }

        /// <summary>
        /// 使用SQL语句返回List
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected List<T2> GetListModel<T2>(string sql)
        {
            return DbInstance.Ado.SqlQuery<T2>(sql);
        }

        /// <summary>
        /// 使用SQL语句返回Model
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected T GetModel(string sql)
        {
            return DbInstance.Ado.SqlQuery<T>(sql).SingleOrDefault();
        }

        /// <summary>
        /// 使用SQL语句获取首行首列
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected object GetScalar(string sql)
        {
            return DbInstance.Ado.GetScalar(sql);
        }

        /// <summary>
        /// 用于执行增删查改的SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected int ExecuteCommand(string sql)
        {
            return DbInstance.Ado.ExecuteCommand(sql);
        }

        #endregion 使用原生SQL

        #region 查询数据库

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public List<T> Query()
        {
            return DbInstance.Queryable<T>().ToList();
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public List<T2> Query<T2>()
        {
            return DbInstance.Queryable<T2>().ToList();
        }

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// 可进行如：
        /// var getByWhere = db.Queryable<Student>().Where(it => it.Id == 1 || it.Name == "a").ToList();
        /// var list2 = db.Queryable<Order>().Where(it =>it.Name.Contains("jack")).ToList();//模糊查询 name like '%'+@name+'%'
        /// var getByFuns = db.Queryable<Student>().Where(it => SqlFunc.Between(it.Id,1,2)).ToList();
        public List<T> Query(Expression<Func<T, bool>> expression)
        {
            return DbInstance.Queryable<T>().Where(expression).ToList();
        }

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public List<T2> Query<T2>(Expression<Func<T2, bool>> expression)
        {
            return DbInstance.Queryable<T2>().Where(expression).ToList();
        }

        /// <summary>
        /// 查询前几条
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<T> QueryTop(int top)
        {
            return DbInstance.Queryable<T>().Take(top).ToList();
        }

        /// <summary>
        /// 查询首条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public T QueryFirst(Expression<Func<T, bool>> expression)
        {
            return DbInstance.Queryable<T>().First(expression);
        }

        /// <summary>
        /// 查询首条
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public T2 QueryFirst<T2>(Expression<Func<T2, bool>> expression)
        {
            return DbInstance.Queryable<T2>().First(expression);
        }

        /// <summary>
        /// 查询总和
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public int QuerySum(Expression<Func<T, bool>> expression)
        {
            return Convert.ToInt32(DbInstance.Queryable<T>().Sum(expression));
        }

        /// <summary>
        /// 是否存在该值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool QueryAny(Expression<Func<T, bool>> expression)
        {
            return DbInstance.Queryable<T>().Any(expression);
        }

        /// <summary>
        /// 是否存在该值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool QueryAny<T2>(Expression<Func<T2, bool>> expression)
        {
            return DbInstance.Queryable<T2>().Any(expression);
        }

        /// <summary>
        /// 根据新表名查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tabName"></param>
        /// <returns></returns>
        public List<T> QueryAs(string tabName)
        {
            return DbInstance.Queryable<T>().AS(tabName).ToList();
        }

        /// <summary>
        /// 查询匹配行数
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public int QueryCount(Expression<Func<T, bool>> expression)
        {
            return DbInstance.Queryable<T>().Where(expression).Count();
        }

        /// <summary>
        /// 查询匹配行数
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public int QueryCount<T2>(Expression<Func<T2, bool>> expression)
        {
            return DbInstance.Queryable<T2>().Where(expression).Count();
        }

        /// <summary>
        /// 查询匹配行数
        /// </summary>
        /// <returns></returns>
        public int QueryCount()
        {
            return DbInstance.Queryable<T>().Count();
        }

        /// <summary>
        /// 查询匹配行数
        /// </summary>
        /// <returns></returns>
        public int QueryCount<T2>()
        {
            return DbInstance.Queryable<T2>().Count();
        }

        #endregion 查询数据库

        #region 增加数据

        /// <summary>
        /// T[]插入数据
        /// </summary>
        /// <param name="insertObj"></param>
        /// <returns></returns>
        public int Add(T[] insertObj)
        {
            return DbInstance.Insertable(insertObj).ExecuteCommand();
        }

        /// <summary>
        /// T[]插入数据并返回ID
        /// </summary>
        /// <param name="insertObj"></param>
        /// <returns></returns>
        public int AddAndReturn(T[] insertObj)
        {
            return DbInstance.Insertable(insertObj).ExecuteReturnIdentity();
        }

        /// <summary>
        /// T插入数据
        /// </summary>
        /// <param name="insertObj"></param>
        /// <returns></returns>
        public int Add(T insertObj)
        {
            return DbInstance.Insertable(insertObj).ExecuteCommand();
        }

        /// <summary>
        /// T插入数据并返回ID
        /// </summary>
        /// <param name="insertObj"></param>
        /// <returns></returns>
        public int AddAndReturn(T insertObj)
        {
            return DbInstance.Insertable(insertObj).ExecuteReturnIdentity();
        }

        /// <summary>
        /// dynamic插入数据
        /// </summary>
        /// <param name="insertObj"></param>
        /// <returns></returns>
        public int Add(dynamic insertObj)
        {
            return DbInstance.Insertable(insertObj).ExecuteCommand();
        }

        /// <summary>
        /// dynamic插入数据并返回ID
        /// </summary>
        /// <param name="insertObj"></param>
        /// <returns></returns>
        public int AddAndReturn(dynamic insertObj)
        {
            return DbInstance.Insertable(insertObj).ExecuteReturnIdentity();
        }

        /// <summary>
        /// List<Model>插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="insertObj"></param>
        /// <returns></returns>
        public int Add(List<T> insertObj)
        {
            return DbInstance.Insertable(insertObj).ExecuteCommand();
        }

        /// <summary>
        /// List<Model>插入数据并返回ID
        /// </summary>
        /// <param name="insertObj"></param>
        /// <returns></returns>
        public int AddAndReturn(List<T> insertObj)
        {
            return DbInstance.Insertable(insertObj).ExecuteReturnIdentity();
        }

        /// <summary>
        /// Dictionary插入数据
        /// </summary>
        /// <param name="insertObj"></param>
        /// <returns></returns>
        public int Add(Dictionary<string, object> insertObj)
        {
            return DbInstance.Insertable(insertObj).ExecuteCommand();
        }

        /// <summary>
        /// Dictionary插入数据并返回ID
        /// </summary>
        /// <param name="insertObj"></param>
        /// <returns></returns>
        public int AddAndReturn(Dictionary<string, object> insertObj)
        {
            return DbInstance.Insertable(insertObj).ExecuteReturnIdentity();
        }

        #endregion 增加数据

        #region 删除数据

        /// <summary>
        /// 根据Linq删除数据
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public int DeleteTable(Expression<Func<T, bool>> expression)
        {
            return DbInstance.Deleteable<T>().Where(expression).ExecuteCommand();
        }

        /// <summary>
        /// 根据主键删除数据（需要先设置主键）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteTable(int id)
        {
            return DbInstance.Deleteable<T>().In(id).ExecuteCommand();
        }

        /// <summary>
        /// 根据int数组删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteTable(int[] ids)
        {
            return DbInstance.Deleteable<T>().In(ids).ExecuteCommand();
        }

        #endregion 删除数据

        #region 修改数据

        /// <summary>
        /// 根据Model进行更新
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int UpdateTable(T t)
        {
            return DbInstance.Updateable(t).ExecuteCommand();
        }

        /// <summary>
        /// 根据List进行更新
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int UpdateTable(List<T> t)
        {
            return DbInstance.Updateable(t).ExecuteCommand();
        }

        /// <summary>
        /// 根据字典类型进行更新
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public int UpdateTable(Dictionary<string, object> keyValues)
        {
            return DbInstance.Updateable(keyValues).ExecuteCommand();
        }

        /// <summary>
        /// 根据条件对指定列和条件更新
        /// </summary>
        /// <param name="columns">需要更新的列</param>
        /// <param name="t">更新的值（更新条件）</param>
        /// <returns></returns>
        public int UpdateTable(Expression<Func<T, object>> columns, T t)
        {
            return DbInstance.Updateable(t).WhereColumns(columns).ExecuteCommand();
        }

        /// <summary>
        /// 根据条件对指定列更新
        /// </summary>
        /// <param name="t"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public int UpdateTable(T t, Expression<Func<T, object>> columns)
        {
            return DbInstance.Updateable(t).UpdateColumns(columns).ExecuteCommand();
        }

        /// <summary>
        /// 对指定列的多个字段进行更新
        /// </summary>
        /// <param name="exp1">需要更新的值</param>
        /// <param name="exp2">需要更新的列（更新条件）</param>
        /// <returns></returns>
        public int UpdateTable(Expression<Func<T, object>> exp1, Expression<Func<T, object>> exp2)
        {
            return DbInstance.Updateable<T>(exp1).WhereColumns(exp2).ExecuteCommand();
        }

        /// <summary>
        /// 对指定行更新
        /// </summary>
        /// <param name="t">参数</param>
        /// <param name="exp1">需要更新的值</param>
        /// <param name="exp2">需要更新的列（更新条件）</param>
        /// <returns></returns>
        public int UpdateColumns(T t, Expression<Func<T, object>> exp1, Expression<Func<T, object>> exp2)
        {
            return DbInstance.Updateable(t).UpdateColumns(exp1).WhereColumns(exp2).ExecuteCommand();
        }

        /// <summary>
        /// 对指定行更新
        /// </summary>
        /// <param name="t">参数</param>
        /// <param name="exp1">需要更新的值</param>
        /// <param name="exp2">需要更新的列（更新条件）</param>
        /// <returns></returns>
        public int UpdateColumns(List<T> t, Expression<Func<T, object>> exp1, Expression<Func<T, object>> exp2)
        {
            return DbInstance.Updateable(t).UpdateColumns(exp1).WhereColumns(exp2).ExecuteCommand();
        }

        /// <summary>
        /// 对指定行更新
        /// </summary>
        /// <param name="t">参数</param>
        /// <param name="exp1">需要更新的值</param>
        /// <returns></returns>
        public int UpdateColumns(T t, Expression<Func<T, object>> exp1)
        {
            return DbInstance.Updateable(t).UpdateColumns(exp1).ExecuteCommand();
        }

        /// <summary>
        /// 对指定行更新
        /// </summary>
        /// <param name="t">参数</param>
        /// <param name="exp1">需要更新的值</param>
        /// <returns></returns>
        public int UpdateColumns(List<T> t, Expression<Func<T, object>> exp1)
        {
            return DbInstance.Updateable(t).UpdateColumns(exp1).ExecuteCommand();
        }

        /// <summary>
        /// 根据某列更新
        /// </summary>
        /// <param name="t">参数</param>
        /// <param name="exp1">更新条件</param>
        /// <returns></returns>
        public int UpdateWhereColumns(T t, Expression<Func<T, object>> exp1)
        {
            return DbInstance.Updateable(t).WhereColumns(exp1).ExecuteCommand();
        }

        /// <summary>
        /// 对指定行不更新
        /// </summary>
        /// <param name="t">参数</param>
        /// <param name="exp1">需要更新的值</param>
        /// <param name="exp2">需要更新的列（更新条件）</param>
        /// <returns></returns>
        public int IgnoreColumns(T t, Expression<Func<T, object>> exp1, Expression<Func<T, object>> exp2)
        {
            return DbInstance.Updateable(t).IgnoreColumns(exp1).WhereColumns(exp2).ExecuteCommand();
        }

        /// <summary>
        /// 对指定行不更新
        /// </summary>
        /// <param name="t">参数</param>
        /// <param name="exp1">需要更新的值</param>
        /// <param name="exp2">需要更新的列（更新条件）</param>
        /// <returns></returns>
        public int IgnoreColumns(T t, Expression<Func<T, object>> exp1)
        {
            return DbInstance.Updateable(t).IgnoreColumns(exp1).ExecuteCommand();
        }

        /// <summary>
        /// 更新不为空的字段
        /// </summary>
        /// <param name="t">参数</param>
        /// <returns></returns>
        public int UpdateNotNullColumns(T t)
        {
            return DbInstance.Updateable(t).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommand();
        }

        #endregion 修改数据
    }
}