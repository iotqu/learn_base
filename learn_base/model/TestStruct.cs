namespace learn_base.model;

public class TestStruct
{
    public struct BookStruct
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public static void Convert(BookStruct data)
    {
        data.id = 14014;
    }

    public TestStruct (int id,string name)
    {
        BookStruct bookStruct = default;
        bookStruct.id = id;
        bookStruct.name = name;
    }
}