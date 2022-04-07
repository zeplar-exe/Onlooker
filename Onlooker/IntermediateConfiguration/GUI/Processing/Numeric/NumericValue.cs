using Onlooker.Common;
using Onlooker.Common._2D;
using Onlooker.IntermediateConfiguration.GUI.Elements;
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

    public NumericValue Copy()
    {
        return new NumericValue(Property.Value, Type);
    }

    public void CopyTo(NumericValue value)
    {
        value.Property.Value = Property.Value;
        value.Type = Type;
    }

    public NumericValue ToPixels(GuiElement element, ScreenOrigin origin)
    {
        switch (Type)
        {
            case NumericType.Pixels:
                return this;
            case NumericType.ParentPercentage:
                switch (origin)
                {
                    case ScreenOrigin.XPosition:
                        return new NumericValue(
                            Math2.FloorToInt(element.X.Property.Value / 100d * Property.Value), 
                            NumericType.Pixels);
                    case ScreenOrigin.YPosition:
                        return new NumericValue(
                            Math2.FloorToInt(element.Y.Property.Value / 100d * Property.Value), 
                            NumericType.Pixels);
                    case ScreenOrigin.Width:
                        return new NumericValue(
                            Math2.FloorToInt(element.Width.Property.Value / 100d * Property.Value), 
                            NumericType.Pixels);
                    case ScreenOrigin.Height:
                        return new NumericValue(
                            Math2.FloorToInt(element.Height.Property.Value / 100d * Property.Value), 
                            NumericType.Pixels);
                }
                break;
            case NumericType.ScreenPercentage:
                switch (origin)
                {
                    case ScreenOrigin.XPosition:
                        return new NumericValue(
                            Math2.FloorToInt(CommonValues.ScreenWidth.X / 100d * Property.Value), 
                            NumericType.Pixels);
                    case ScreenOrigin.YPosition:
                        return new NumericValue(
                            Math2.FloorToInt(CommonValues.ScreenHeight.X / 100d * Property.Value), 
                            NumericType.Pixels);
                    case ScreenOrigin.Width:
                        return new NumericValue(
                            Math2.FloorToInt(CommonValues.ScreenWidth.X / 100d * Property.Value), 
                            NumericType.Pixels);
                    case ScreenOrigin.Height:
                        return new NumericValue(
                            Math2.FloorToInt(CommonValues.ScreenHeight.X / 100d * Property.Value), 
                            NumericType.Pixels);
                }
                break;
            default:
                return this;
        }

        return this;
    }
}