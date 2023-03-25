using System.Reflection;

namespace ValidationFactory.Validators;
public record StringValidator<T>() : Validator, IValidator<T>
{
    public List<(bool, Exception)> Validate(T value, int? max, int? min, string source, PropertyInfo pi, object model)
    {
        var errorList = new List<(bool, Exception)>();
        if (!typeof(T).IsValueType && typeof(T) != typeof(string))
            throw new ArgumentException("T must be a value type or System.String.");

        string stringValue = value.ToString();

        if (!IsGetMethod() && !IsEncryted(stringValue))
        {
            List<string> invalidChars = new() { "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-" };

            if (stringValue.Length > max)
            {
                Console.WriteLine("String too Long");
                errorList.Add((false, new Exception($"String too Long. Text Must Shorter Than < {max}") { Source = source }));
            }

            if (!!stringValue.Equals(stringValue.ToLower()))
            {
                Console.WriteLine("Requres at least one uppercase");
                errorList.Add((false, new Exception("Requres at least one uppercase") { Source = source }));
            }

            foreach (string s in invalidChars)
            {
                if (stringValue.Contains(s))
                {
                    Console.WriteLine("String contains invalid character: " + s);
                    Exception exception = new("String contains invalid character: " + s) { Source = source };
                    errorList.Add((false, exception));
                    break;
                }
            }
        }

        if (errorList.Count == 0) Console.WriteLine("All tests succesful");
        return errorList;
    }
}