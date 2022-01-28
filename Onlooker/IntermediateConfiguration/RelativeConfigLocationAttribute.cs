namespace Onlooker.IntermediateConfiguration;

[AttributeUsage(AttributeTargets.Property)]
public class RelativeConfigLocationAttribute : Attribute
{
    public string Location { get; }

    public RelativeConfigLocationAttribute(string location)
    {
        Location = location;
    }
}