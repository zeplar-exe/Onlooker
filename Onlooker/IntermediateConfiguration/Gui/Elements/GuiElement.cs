using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Onlooker.Common._2D;
using Onlooker.Common.Extensions;
using Onlooker.Common.MethodOutput;
using Onlooker.Common.MethodOutput.OutputTypes;
using Onlooker.IntermediateConfiguration.GUI.Processing.Colors;
using Onlooker.IntermediateConfiguration.GUI.Processing.Numeric;
using Onlooker.IntermediateConfiguration.Modules;
using Onlooker.IntermediateConfiguration.Modules.Settings;
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
    
    public ColorProperty RectFill { get; }
    
    public event EventHandler<ObjectPropertyValueChangedArgs<Rectangle>>? RectChanged;

    public Rectangle Rect => new(
        X.ToPixels(this, ScreenOrigin.XPosition).Property, 
        Y.ToPixels(this, ScreenOrigin.YPosition).Property,
        Width.ToPixels(this, ScreenOrigin.Width).Property, 
        Height.ToPixels(this, ScreenOrigin.Height).Property);

    public List<GuiElement> Children { get; }
    public int ZIndex { get; set; }

    protected GuiElement()
    {
        X = new NumericValue(0, NumericType.Pixels);
        Y = new NumericValue(0, NumericType.Pixels);
        
        Width = new NumericValue(50, NumericType.Pixels);
        Height = new NumericValue(50, NumericType.Pixels);

        RectFill = new ColorProperty(Color.Transparent);
        
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
        canvas.Draw(ZIndex, new RectangleGraphic(RectFill, Rect));
        
        foreach (var child in Children)
            child.Draw(canvas, time);
    }

    public virtual void LoadFromXml(XElement element)
    {
        if (element.Attribute("x_pos").IsNotNull(out var xPosAttribute))
            ParseNumericValue(xPosAttribute).CopyTo(X);
        
        if (element.Attribute("y_pos").IsNotNull(out var yPosAttribute))
            ParseNumericValue(yPosAttribute).CopyTo(X);
        
        if (element.Attribute("width").IsNotNull(out var widthAttribute))
            ParseNumericValue(widthAttribute).CopyTo(X);
        
        if (element.Attribute("height").IsNotNull(out var heightAttribute))
            ParseNumericValue(heightAttribute).CopyTo(X);
        
        if (element.Attribute("rect_fill").IsNotNull(out var rectFillAttribute))
            RectFill.Value = ParseColor(rectFillAttribute);

        ZIndex = element.Attribute("z")?.Value.SafeParseInt() ?? ZIndex;
    }

    protected NumericValue ParseNumericValue(XAttribute? attribute, string attributeDefault = "")
    {
        var parser = new NumericValueParser();

        var (output, value) = parser.Parse(attribute?.Value ?? attributeDefault);
        
        LogGuiOutput(output);

        return value;
    }

    protected Color ParseColor(XAttribute? attribute, string attributeDefault = "")
    {
        var parser = new ColorParser();

        var (output, value) = parser.Parse(attribute?.Value ?? attributeDefault);

        LogGuiOutput(output);

        return value;
    }

    protected void LogGuiOutput(params OperationOutput[] outputs)
    {
        var guiSettings = IModule.Get<SettingsModule>().GuiSettings;
        
        foreach (var output in outputs)
        {
            if (guiSettings.LogLevel is not LogLevel.None)
                return;
            
            switch (output.Type)
            {
                case OperationOutputType.Success:
                    if (guiSettings.LogLevel is not LogLevel.SuccessAndFailure or LogLevel.SuccessOnly)
                        continue;
                    break;
                case OperationOutputType.Failure:
                    if (guiSettings.LogLevel is not LogLevel.SuccessAndFailure or LogLevel.FailureOnly)
                        continue;
                    break;
            }
            
            AppLoggerCommon.GuiErrorLog(LogMessageBuilder.TimestampedMessage(output.ToString()));
        }
    }
}