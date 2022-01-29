using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Onlooker.IntermediateConfiguration.GUI.Processing.Numeric;
using Onlooker.Monogame;
using Onlooker.Monogame.Controllers;
using Onlooker.ObjectProperties;

namespace Onlooker.IntermediateConfiguration.GUI.Elements;

public abstract class GuiElement : GameController
{
    public RectangleProperty Rect { get; }
    public List<GuiElement> Children { get; }
    public int ZIndex { get; }

    protected GuiElement()
    {
        Rect = new RectangleProperty(new Rectangle(0, 0, 50, 50));
        Children = new List<GuiElement>();
    }

    protected GuiElement(Rectangle rectangle)
    {
        Rect = new RectangleProperty(rectangle);
        Children = new List<GuiElement>();
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
        var widthOutput = numericParser.Parse(element.Attribute("width")?.Value ?? "50");
        var heightOutput = numericParser.Parse(element.Attribute("height")?.Value ?? "50");

        Rect.Value = new Rectangle(
            xOutput.Value.Calculate(ScreenOrigin.XPosition), yOutput.Value.Calculate(ScreenOrigin.YPosition), 
            widthOutput.Value.Calculate(ScreenOrigin.Width), heightOutput.Value.Calculate(ScreenOrigin.Height));
    }
}