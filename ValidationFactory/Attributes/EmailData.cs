using ValidationFactory.Enums;

namespace ValidationFactory.Attributes;

[AttributeUsage(AttributeTargets.All)]
public class EmailData : Attribute
{
    public EmailData()
    {
    }

    public EmailValidateType Type { get; set; }
}