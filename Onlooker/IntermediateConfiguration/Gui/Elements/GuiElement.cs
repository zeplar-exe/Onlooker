using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Onlooker.Common._2D;
using Onlooker.Common.Extensions;
using Onlooker.Common.Helpers;
using Onlooker.Common.MethodOutput;
using Onlooker.IntermediateConfiguration.Gui.Events;
using Onlooker.IntermediateConfiguration.Gui.Processing.Colors;
using Onlooker.IntermediateConfiguration.Gui.Processing.Commands;
using Onlooker.IntermediateConfiguration.Gui.Processing.Numeric;
using Onlooker.IntermediateConfiguration.Modules;
using Onlooker.IntermediateConfiguration.Modules.Settings;
using Onlooker.Monogame.Controllers;
using Onlooker.Monogame.Graphics;
using Onlooker.Monogame.Logging;
using Onlooker.ObjectProperties;
using Onlooker.ObjectProperties.Binding;

namespace Onlooker.IntermediateConfiguration.Gui.Elements;

public abstract class GuiElement
{
    public NumericValue X { get; }
    public NumericValue Y { get; }
    
    public NumericValue Width { get; }
    public NumericValue Height { get; }
    
    public ColorProperty RectFill { get; }
    public PaddingProperty Padding { get; }

    public event EventHandler<MouseEventArgs>? OnMouseEnter;
    public event EventHandler<MouseEventArgs>? OnMouseLeave;
    
    public event EventHandler<MouseEventArgs>? OnLeftMouseButtonPressed;
    public event EventHandler<MouseEventArgs>? OnLeftMouseButtonMove;
    public event EventHandler<MouseEventArgs>? OnLeftMouseButtonHeld;
    public event EventHandler<MouseEventArgs>? OnLeftMouseButtonReleased;
    
    public event EventHandler<MouseEventArgs>? OnRightMouseButtonPressed;
    public event EventHandler<MouseEventArgs>? OnRightMouseButtonMove;
    public event EventHandler<MouseEventArgs>? OnRightMouseButtonHeld;
    public event EventHandler<MouseEventArgs>? OnRightMouseButtonReleased;
    
    public event EventHandler<MouseEventArgs>? OnMiddleMouseButtonPressed;
    public event EventHandler<MouseEventArgs>? OnMiddleMouseButtonMove;
    public event EventHandler<MouseEventArgs>? OnMiddleMouseButtonHeld;
    public event EventHandler<MouseEventArgs>? OnMiddleMouseButtonReleased;
    
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
        Padding = new PaddingProperty(Common._2D.Padding.Empty);
        
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

    public virtual void Update(GameTime time)
    {
        foreach (var child in Children)
            child.Update(time);
    }

    public virtual void Draw(DrawCanvas canvas, GameTime time)
    {
        canvas.Draw(ZIndex, new RectangleGraphic(RectFill, Rect));
        
        foreach (var child in Children)
            child.Draw(canvas, time);
    }

    public virtual void LoadFromXml(XElement element)
    {
        if (element.Attribute("x_pos").IsNotNull(out var xPosAttribute))
            GuiHelper.ParseNumericValue(xPosAttribute).CopyTo(X);
        
        if (element.Attribute("y_pos").IsNotNull(out var yPosAttribute))
            GuiHelper.ParseNumericValue(yPosAttribute).CopyTo(X);
        
        if (element.Attribute("width").IsNotNull(out var widthAttribute))
            GuiHelper.ParseNumericValue(widthAttribute).CopyTo(X);
        
        if (element.Attribute("height").IsNotNull(out var heightAttribute))
            GuiHelper.ParseNumericValue(heightAttribute).CopyTo(X);
        
        if (element.Attribute("rect_fill").IsNotNull(out var rectFillAttribute))
            RectFill.Value = GuiHelper.ParseColor(rectFillAttribute);

        ZIndex = element.Attribute("z")?.Value.SafeParseInt() ?? ZIndex;
    }
}