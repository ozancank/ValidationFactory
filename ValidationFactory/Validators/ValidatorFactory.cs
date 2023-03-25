namespace ValidationFactory.Validators;

public static class ValidatorFactory<T>
{
    private static readonly Dictionary<string, IValidator<T>> _validatorList = new();

    public static IValidator<T> GetValidator(Type attribute)
    {
        if (_validatorList.Count == 0)
        {
            _validatorList.Add("DateData", new DateValidator<T>());
            _validatorList.Add("EmailData", new EmailValidator<T>());
            _validatorList.Add("EncryptData", new EncryptValidator<T>());
            _validatorList.Add("HashData", new HashValidator<T>());
            _validatorList.Add("StringData", new StringValidator<T>());
            _validatorList.Add("Default", new DefaultValidator<T>());
        }

        return _validatorList.TryGetValue(attribute.Name, out IValidator<T> value) ? value : _validatorList["Default"];
    }
}