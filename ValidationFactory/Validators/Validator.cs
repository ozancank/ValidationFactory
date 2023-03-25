namespace ValidationFactory.Validators;

public record Validator
{
    public HttpContext HttpContext => new HttpContextAccessor().HttpContext;
    public bool IsEncryted(string value) => value.IndexOf("encß") == 0;

    public bool IsHashed(string value) => value.IndexOf("hasß") == 0;

    public bool IsGetMethod() => HttpContext.Request.Method == "GET";
}