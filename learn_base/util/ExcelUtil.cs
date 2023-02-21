using System.Data;
using System.Data.OleDb;
using System.Text;
using Spire.Xls;

namespace learn_base.util;

public class ExcelUtil
{

    private const string path = @"D:\_20221216_091939.xlsx";

    public static void SpireTest()
    {
        var book = new Workbook();
        book.LoadFromFile(@"D:\_20221216_091939.xlsx");
        var sheet = book.Worksheets[0];
        foreach (var row in sheet.Rows)
        {
            var data = row.Columns.Where(column => column.Value != "")
                .Aggregate<CellRange, string?>(null, (current, column) => current + column.Value + ",");

            if (data == null) continue;
            Console.WriteLine("data-> " + data);
        }
    }

    public static void MicroTest()
    {
        FileStream fileStream = new FileStream(@"D:\_20221216_091939.xlsx", FileMode.Open, FileAccess.Read);
        byte[] bytes = new byte[fileStream.Length];
        fileStream.Read(bytes, 0, bytes.Length);
        string c = Encoding.UTF8.GetString(bytes);
        Console.WriteLine(c);
        fileStream.Close();
    }

    /// <summary>
    /// OldDb 不支持.Net Framework 4.0
    /// </summary>
    public static void OldDbTest()
    {
        try
        {
            //连接字符串
            string connstring = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={path};Extended Properties='Excel 8.0;HDR=NO;IMEX=1';"; // Office 07及以上版本 不能出现多余的空格 而且分号注意
            //string connstring = Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1';"; //Office 07以下版本
            using (OleDbConnection conn = new OleDbConnection(connstring))
            {
                conn.Open();
                DataTable sheetsName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" }); //得到所有sheet的名字
                var firstSheetName = sheetsName.Rows[0][2].ToString(); //得到第一个sheet的名字
                var sql = $"SELECT * FROM [{firstSheetName}]"; //查询字符串          //string sql = string.Format("SELECT * FROM [{0}] WHERE [日期] is not null", firstSheetName); //查询字符串
                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connstring);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                System.Data.DataTable table = new System.Data.DataTable();
                table= dataSet.Tables[0];
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Console.WriteLine("行号："+i+table.Rows[i][0] + table.Rows[i][1] + table.Rows[i][2]);
                }
                adapter.Dispose();
            }
        }
        catch (Exception)
        {
        }
    }
}