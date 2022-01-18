namespace Onlooker.IntermediateConfiguration;

[AttributeUsage(AttributeTargets.Property)]
public class ConfigLocationAttribute : Attribute
{
    public string RelativeLocation { get; }

    public ConfigLocationAttribute(string relativeLocation)
    {
        RelativeLocation = relativeLocation;
    }
}