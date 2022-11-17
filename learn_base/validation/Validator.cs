using System.ComponentModel.DataAnnotations;

namespace learn_base.validation;

public class Validator
{
    [Compare("")]
    public string name { get; set; }
}