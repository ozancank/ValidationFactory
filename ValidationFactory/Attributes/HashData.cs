namespace ValidationFactory.Attributes;

[AttributeUsage(AttributeTargets.All)]
public class HashData : Attribute
{
    public HashData()
    {
    }

    public int Min { get; set; }
}