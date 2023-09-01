using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using rulex.common.util;

namespace learn_base.common.validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class JsonFormatAttribute : ValidationAttribute
{
    private string NameSpace { get; }
    private string OtherProperty { get; }
    private string Suffix { get; }

    public JsonFormatAttribute(string otherProperty)
    {
        NameSpace = "learn_base.common.";
        OtherProperty = otherProperty;
        Suffix = "Config";
    }

    public JsonFormatAttribute(string nameSpace, string otherProperty, string suffix)
    {
        NameSpace = nameSpace;
        OtherProperty = otherProperty;
        Suffix = suffix;
    }

    public override bool IsValid(object? value)
    {
        if (value == null || value.Equals("")) return false;
        try
        {
            JObject.Parse(value.ToString() ?? string.Empty);
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected override ValidationResult IsValid(object value, ValidationContext context)
    {
        if (!IsValid(value)) return new ValidationResult(ErrorMessage);
        var o = context.ObjectInstance.GetType().GetProperty(OtherProperty)?.GetValue(context.ObjectInstance, null) ??
                  context.ObjectInstance.GetType().GetField(OtherProperty)?.GetValue(context.ObjectInstance);
        if (o == null) return new ValidationResult($"The value of the [{OtherProperty}] is null");

        var type = Type.GetType(NameSpace + o + Suffix);
        if (type == null) return new ValidationResult($"The value of the [{OtherProperty}] is invalid-> {o}");

        var instance = JsonConvert.DeserializeObject(value.ToString(), type);
        if (instance == null) return new ValidationResult($"The value of the [{OtherProperty}] is invalid-> {0}");

        string msg = null;
        foreach (var field in instance.GetType().GetFields())
        {
            if (!field.IsDefined(typeof(ValidationAttribute), true)) continue;
            var proVal = field.GetValue(instance);
            string temp = null;
            foreach (ValidationAttribute attr in field.GetCustomAttributes(typeof(ValidationAttribute), true))
            {
                var res = attr.GetValidationResult(proVal, new ValidationContext(instance, null, null));
                if (res == null)
                {
                    if (attr.IsValid(proVal)) continue;
                    temp += attr.ErrorMessage + ";";
                }
                else
                {
                    temp += res.ErrorMessage + ";";
                }
            }

            if (temp != null) msg += temp + "\n";
        }

        msg = msg?.TrimEnd('\n');
        return msg == null ? ValidationResult.Success : new ValidationResult(msg);
    }

    private static string Validate(object instance, object value)
    {
        string result = null;
        var data = JObject.Parse(value.ToString());
        foreach (var info in instance.GetType().GetFields())
        {
            if (!info.IsDefined(typeof(ValidationAttribute), true)) continue;
            result = info.GetCustomAttributes(typeof(ValidationAttribute), true).Cast<ValidationAttribute>()
                .Where(attr => !attr.IsValid(data[StrUtil.FirstToLower(info.Name)]))
                .Aggregate(result, (current, attr) => current + (attr.ErrorMessage + ","));

            //if (result != null && result.EndsWith(",")) result = result.TrimEnd(',') + "\n";
        }

        return result;
    }
}