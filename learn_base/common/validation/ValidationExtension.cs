using System.ComponentModel.DataAnnotations;
using System.Reflection;
using learn_base.util;

namespace learn_base.common.validation;

public static class ValidationExtension
{
    /// <summary>
    /// 验证IP(域名)不能为空以及是否可用
    /// </summary>
    public static ValidationResult IpValidate(string host)
    {
        if (string.IsNullOrWhiteSpace(host)) return new ValidationResult("IP(域名)不能为空");
        return SysUtil.Ping(host) ? ValidationResult.Success : new ValidationResult("IP(域名)不可用");
    }

    public static string Validate<T>(this T t)
    {
        var context = new ValidationContext(t, null, null);
        var results = new List<ValidationResult>();
        var flag = Validator.TryValidateObject(t, context, results, true);
        return results.Aggregate<ValidationResult, string>(null, (it, res) => it + res.ErrorMessage + "\n");
    }
}