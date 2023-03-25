using System.Reflection;

namespace ValidationFactory.Validators;
public record DateValidator<T>() : Validator, IValidator<T>
{
    public List<(bool, Exception)> Validate(T value, int? minYear, int? param2, string source, PropertyInfo pi, object model)
    {
        var errorList = new List<(bool, Exception)>();
        if (!DateTime.TryParse(value.ToString(), out _))
            throw new ArgumentException("T must be proper System.DateTime.");

        string stringValue = value.ToString();

        if (!IsGetMethod() && !IsEncryted(stringValue))
        {
            if (DateTime.Parse(stringValue).Year < minYear)
            {
                Console.WriteLine("Time is too old. Wrong Date!");
                errorList.Add((false, new Exception($"Time is too old. Wrong Date! Year Must Bigger Than > {minYear}") { Source = source }));
            }
        }

        if (errorList.Count == 0) Console.WriteLine("All tests succesful");
        return errorList;
    }
}