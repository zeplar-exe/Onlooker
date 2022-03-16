using Onlooker.ObjectProperties;

namespace Onlooker.IntermediateConfiguration.GUI.Processing.Numeric;

public class NumericValue
{
    public IntegerProperty Property { get; }
    public NumericType Type { get; set; }

    public NumericValue(int value, NumericType type)
    {
        Property = new IntegerProperty(value);
        Type = type;
    }

    public void CopyTo(NumericValue value)
    {
        value.Property.Value = Property.Value;
        value.Type = Type;
    }
}