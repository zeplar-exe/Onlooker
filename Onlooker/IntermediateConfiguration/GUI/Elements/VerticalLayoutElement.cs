using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Onlooker.Monogame;
using Onlooker.ObjectProperties;

namespace Onlooker.IntermediateConfiguration.GUI.Elements;

public class VerticalLayoutElement : GuiElement
{
    public RectangleProperty Rect { get; }
    public PaddingProperty Padding { get; }

    public VerticalLayoutElement()
    {
        Rect = new RectangleProperty(new Rectangle(0, 0, 200, 200));
        Padding = new PaddingProperty(Onlooker.Common.Padding.Empty);
    }
    
    public override void LoadFromXml(XElement element)
    {
        Padding.Value = Onlooker.Common.Padding.FromXml(element);
        
        Enum.TryParse<PaddingPreset>(element.Attribute("padding_generator")?.Value, true, out var paddingPreset);

        switch (paddingPreset)
        {
            case PaddingPreset.Center:
                break;
            case PaddingPreset.Left:
                break;
            case PaddingPreset.Right:
                break;
            case PaddingPreset.Top:
                break;
            case PaddingPreset.Bottom:
                break;
        }
    }
    
    public override void Update(GameTime time)
    {
        
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        
    }

    public override bool IsLocked()
    {
        return false;
    }
}

public enum PaddingPreset
{
    None = 0,
    
    Center,
    Left,
    Right,
    Top,
    Bottom
}