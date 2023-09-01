using System.Data.OleDb;

namespace learn_base.test;

/// <summary>
/// Microsoft Access 数据库练习(只支持Windows平台)
/// </summary>
public class AccessDbTest
{
    //private const string dbPath = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + @"C:\Users\admin\Desktop\Test.accdb";
    private const string DbPath = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @"D:\Data\di_bei\TestData.mdb";

    public static OleDbConnection Init()
    {
        var conn = new OleDbConnection(DbPath);
        conn.Open();
        return conn;
    }

    public static void SelectTest()
    {
        var conn = Init();
        Select("SELECT * FROM t_device", conn);
        conn.Close();
    }

    public static void SelectTop1()
    {
        var conn = Init();
        Select("SELECT TOP 1 * FROM Table_Data ORDER BY 编号 DESC", conn);
        conn.Close();
    }

    public static void InsertTest()
    {
        var conn = Init();
        Insert("INSERT INTO t_device(deviceName) VALUES ('DT_13')", conn);
        conn.Close();
    }

    private static void Select(string sql, OleDbConnection conn)
    {
        var cmd = new OleDbCommand(sql, conn);
        var reader = cmd.ExecuteReader();
        var count = reader.FieldCount;
        while (reader.Read())
        {
            string data = null;
            for (var i = 0; i < count; i++)
            {
                data = data + reader.GetValue(i) + ",";
            }

            Console.WriteLine(data);
        }
    }

    private static void Insert(string sql, OleDbConnection conn)
    {
        var cmd = new OleDbCommand(sql, conn);
        var count = cmd.ExecuteNonQuery();
    }
}