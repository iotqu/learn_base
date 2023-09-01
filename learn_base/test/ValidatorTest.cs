using System.ComponentModel.DataAnnotations;
using learn_base.common;
using learn_base.common.validation;
using learn_base.model;
using Newtonsoft.Json.Linq;

namespace learn_base.test;

public class ValidatorTest
{
    public static void CustomValidationTest()
    {
        var config = new TcpConfig();
        config.Host = "";

        var vc = new ValidationContext(config, null, null);
        ICollection<ValidationResult> results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(config, vc, results, true);
        foreach (var item in results)
        {
            Console.WriteLine($"Message:{item.ErrorMessage},Member:{string.Join(",", item.MemberNames)}");
        }
    }

    public static void JsonValidateTest()
    {
        var modbus = new ModbusConfig
        {
            Mode = "Tcp",
            Config = new JObject
            {
                ["ip"] = "192.168.5.777",
                ["port"] = 502
            }
        };

        var inEnd = new InEnd
        {
            Type = "Modbus",
            Config = modbus.ToString()
        };

        var result = inEnd.Validate();
        Console.WriteLine(result);
    }
}