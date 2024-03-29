﻿using learn_base.common.validation;
using SqlSugar;

namespace learn_base.model;

[SugarTable("m_in_ends")]
public class InEnd
{
    [SugarColumn(IsPrimaryKey = true)]
    public int Id { get; set; }
    public string Uuid { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    [JsonFormat("Type", ErrorMessage = "[Modbus配置]非法的JSON字符串")]
    public string Config { get; set; }

    [SugarColumn(ColumnName = "x_data_models")]
    public string DataModels { get; set; }

    public void Convert()
    {
        Id = 1401141036;
        Name = "屈背背";
    }
}