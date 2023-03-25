using ValidationFactory.Attributes;
using ValidationFactory.Enums;

namespace ValidationFactory.Models;

public class User
{
    public int IdUser { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }

    [StringData(Max = 10)]
    public string UserName { get; set; }

    [HashData]
    public string Password { get; set; }

    [DateData(MinYear = 1990)]
    public DateTime BirthDate { get; set; }

    [EmailData(Type = EmailValidateType.Syntax)]
    [EncryptData]
    public string Email { get; set; }

    [EmailData(Type = EmailValidateType.Gmail)]
    public string Email2 { get; set; }

    [EncryptData]
    public string Gsm { get; set; }

    public bool IsDeleted { get; set; } = false;
}