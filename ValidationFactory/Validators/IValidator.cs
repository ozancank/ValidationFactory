using System.Reflection;

namespace ValidationFactory.Validators;

public interface IValidator<T>
{
    List<(bool, Exception)> Validate(T value, int? param1, int? param2, string source, PropertyInfo pi, object model);
}