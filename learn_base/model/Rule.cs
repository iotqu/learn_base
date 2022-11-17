using SqlSugar;

namespace learn_base.model
{
    [SugarTable("m_rules")]
    public class Rule
    {
        public int Id { get; set; }

        public string Uuid { get; set; }

        public string Name { get; set; }

        [SugarColumn(ColumnName = "from_source", IsJson = true)]
        public List<string> Sources { get; set; }

        [SugarColumn(ColumnName = "from_device", IsJson = true)]
        public List<string> Devices { get; set; }

        public string Actions { get; set; }
        public string Success { get; set; }
        public string Failed { get; set; }

        public string Description { get; set; }

        [SugarColumn(ColumnName = "created_at")]
        public DateTime CreatTime { get; set; }
    }
}