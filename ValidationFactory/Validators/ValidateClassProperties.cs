using System.Reflection;

namespace ValidationFactory.Validators;

public static class ValidateClassProperties
{
    public static List<(bool, Exception)> GetValidateResult(object model)
    {
        var errorList = new List<(bool, Exception)>();
        bool isError = false;
        PropertyInfo[] properties = model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo property in properties)
        {
            for (int i = 0; i < property.GetCustomAttributes(true).Length; i++)
            {
                Type type = property.GetCustomAttributes(true)[i].GetType();
                var validator = ValidatorFactory<string>.GetValidator(type);

                string propValue = property.GetValue(model).ToString();
                int? attributeValue = null;
                int? attributeValue2 = null;
                if (property.GetCustomAttributesData()[i].NamedArguments.Count > 0)
                {
                    attributeValue = (int)property.GetCustomAttributesData()[i].NamedArguments[0].TypedValue.Value;
                    if (property.GetCustomAttributesData()[i].NamedArguments.Count > 1)
                        attributeValue2 = (int)property.GetCustomAttributesData()[i].NamedArguments[1].TypedValue.Value;
                }
                foreach ((bool, Exception) err in validator.Validate(propValue, attributeValue, attributeValue2, property.Name, property, model))
                {
                    isError = true;
                    errorList.Add(err);
                }
                if (isError) break;
            }
        }
        return errorList;
    }
}