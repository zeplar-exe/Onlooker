using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Onlooker.Common;
using Onlooker.Common._2D;
using Onlooker.Common.Extensions;
using Onlooker.Common.MethodOutput;
using Onlooker.IntermediateConfiguration.GUI.Processing.Numeric;
using Onlooker.Monogame.Controllers;
using Onlooker.Monogame.Graphics;
using Onlooker.Monogame.Logging;
using Onlooker.ObjectProperties;
using Onlooker.ObjectProperties.Binding;

namespace Onlooker.IntermediateConfiguration.GUI.Elements;

public abstract class GuiElement : GameController
{
    public NumericValue X { get; }
    public NumericValue Y { get; }
    
    public NumericValue Width { get; }
    public NumericValue Height { get; }
    
    public event EventHandler<ObjectPropertyValueChangedArgs<Rectangle>>? RectChanged;

    public Rectangle Rect => new(
        CreatePixelValue(X, ScreenOrigin.XPosition).Property, 
        CreatePixelValue(Y, ScreenOrigin.YPosition).Property,
        CreatePixelValue(Width, ScreenOrigin.Width).Property, 
        CreatePixelValue(Height, ScreenOrigin.Height).Property);

    public List<GuiElement> Children { get; }
    public int ZIndex { get; set; }

    protected GuiElement()
    {
        X = new NumericValue(0, NumericType.Pixels);
        Y = new NumericValue(0, NumericType.Pixels);
        
        Width = new NumericValue(50, NumericType.Pixels);
        Height = new NumericValue(50, NumericType.Pixels);
        
        Children = new List<GuiElement>();

        X.Property.ValueChanged += (sender, args) =>
        {
            RectChanged?.Invoke(sender, new ObjectPropertyValueChangedArgs<Rectangle>(
                new Rectangle(args.OldValue, Rect.Y, Rect.Width, Rect.Height), Rect));
        };
        
        Y.Property.ValueChanged += (sender, args) =>
        {
            RectChanged?.Invoke(sender, new ObjectPropertyValueChangedArgs<Rectangle>(
                new Rectangle(Rect.X, args.OldValue, Rect.Width, Rect.Height), Rect));
        };
        
        Width.Property.ValueChanged += (sender, args) =>
        {
            RectChanged?.Invoke(sender, new ObjectPropertyValueChangedArgs<Rectangle>(
                new Rectangle(Rect.X, Rect.Y, args.OldValue, Rect.Height), Rect));
        };
        
        Height.Property.ValueChanged += (sender, args) =>
        {
            RectChanged?.Invoke(sender, new ObjectPropertyValueChangedArgs<Rectangle>(
                new Rectangle(Rect.X, Rect.Y, Rect.Width, args.OldValue), Rect));
        };
    }

    public override void Update(GameTime time)
    {
        foreach (var child in Children)
            child.Update(time);
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        foreach (var child in Children)
            child.Draw(canvas, time);
    }

    public virtual void LoadFromXml(XElement element)
    {
        var numericParser = new NumericValueParser();

        var xOutput = numericParser.Parse(element.Attribute("x_pos")?.Value ?? "0");
        var yOutput = numericParser.Parse(element.Attribute("y_pos")?.Value ?? "0");
        var widthOutput = numericParser.Parse(element.Attribute("width")?.Value ?? "80");
        var heightOutput = numericParser.Parse(element.Attribute("height")?.Value ?? "80");

        // TODO: Handle output messages, clean this entire process
        
        xOutput.Value.CopyTo(X);
        yOutput.Value.CopyTo(Y);
        widthOutput.Value.CopyTo(Width);
        heightOutput.Value.CopyTo(Height);
        
        ZIndex = element.Attribute("z")?.Value.SafeParseInt() ?? ZIndex;
    }

    private void ParseAttributeToProperty()
    {
        
    }

    public NumericValue CreatePixelValue(NumericValue value, ScreenOrigin origin)
    {
        switch (value.Type)
        {
            case NumericType.Pixels:
                return value;
            case NumericType.ParentPercentage:
                switch (origin)
                {
                    case ScreenOrigin.XPosition:
                        return new NumericValue(
                            Math2.FloorToInt(X.Property.Value / 100d * value.Property.Value), 
                            NumericType.Pixels);
                    case ScreenOrigin.YPosition:
                        return new NumericValue(
                            Math2.FloorToInt(Y.Property.Value / 100d * value.Property.Value), 
                            NumericType.Pixels);
                    case ScreenOrigin.Width:
                        return new NumericValue(
                            Math2.FloorToInt(Width.Property.Value / 100d * value.Property.Value), 
                            NumericType.Pixels);
                    case ScreenOrigin.Height:
                        return new NumericValue(
                            Math2.FloorToInt(Height.Property.Value / 100d * value.Property.Value), 
                            NumericType.Pixels);
                }
                break;
            case NumericType.ScreenPercentage:
                switch (origin)
                {
                    case ScreenOrigin.XPosition:
                        return new NumericValue(
                            Math2.FloorToInt(CommonValues.ScreenWidth.X / 100d * value.Property), 
                            NumericType.Pixels);
                    case ScreenOrigin.YPosition:
                        return new NumericValue(
                            Math2.FloorToInt(CommonValues.ScreenHeight.X / 100d * value.Property), 
                            NumericType.Pixels);
                    case ScreenOrigin.Width:
                        return new NumericValue(
                            Math2.FloorToInt(CommonValues.ScreenWidth.X / 100d * value.Property), 
                            NumericType.Pixels);
                    case ScreenOrigin.Height:
                        return new NumericValue(
                            Math2.FloorToInt(CommonValues.ScreenHeight.X / 100d * value.Property), 
                            NumericType.Pixels);
                }
                break;
            default:
                return value;
        }

        return value;
    }
}