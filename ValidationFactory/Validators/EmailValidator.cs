using System.Reflection;
using System.Text.RegularExpressions;
using ValidationFactory.Enums;

namespace ValidationFactory.Validators;
public record EmailValidator<T>() : Validator, IValidator<T>
{
    public List<(bool, Exception)> Validate(T value, int? validateType, int? param2, string source, PropertyInfo pi, object model)
    {
        var errorList = new List<(bool, Exception)>();
        if (!typeof(T).IsValueType && typeof(T) != typeof(string))
            throw new ArgumentException("T must be a value type or System.String.");

        string emailValue = value.ToString();

        if (!IsGetMethod() && !IsEncryted(emailValue))
        {
            if (validateType != null)
            {
                switch ((EmailValidateType)validateType)
                {
                    case EmailValidateType.Syntax:
                        {
                            var mailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                            if (!Regex.IsMatch(emailValue, mailRegex))
                            {
                                Console.WriteLine("Email is not correct");
                                errorList.Add((false, new Exception("Email is not correct") { Source = source }));
                            }
                            break;
                        }
                    case EmailValidateType.Gmail:
                        {
                            var mailRegex = @"^([\w\.\-]+)@(gmail)\.(com)$";
                            if (!Regex.IsMatch(emailValue, mailRegex))
                            {
                                Console.WriteLine("Email is not gmail");
                                errorList.Add((false, new Exception("Email is not gmail!") { Source = source }));
                            }
                            break;
                        }
                    case EmailValidateType.Government:
                        {
                            var mailRegex = @"^([\w\.\-]+)@(gov)\.(tr)$";
                            if (!Regex.IsMatch(emailValue, mailRegex))
                            {
                                Console.WriteLine("Email is not government email");
                                errorList.Add((false, new Exception("Email is not government email!") { Source = source }));
                            }
                            break;
                        }
                    case EmailValidateType.Education:
                        {
                            var mailRegex = @"^([\w\.\-]+)@(edu)\.(tr)$";
                            if (!Regex.IsMatch(emailValue, mailRegex))
                            {
                                Console.WriteLine("Email is not educational email");
                                errorList.Add((false, new Exception("Email is not educatinal email!") { Source = source }));
                            }
                            break;
                        }
                }
            }
        }

        if (errorList.Count == 0) Console.WriteLine("All tests succesful");
        return errorList;
    }
}