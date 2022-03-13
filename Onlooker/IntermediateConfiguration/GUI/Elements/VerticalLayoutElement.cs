using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Onlooker.ObjectProperties;

namespace Onlooker.IntermediateConfiguration.GUI.Elements;

public class VerticalLayoutElement : GuiElement
{
    public PaddingProperty Padding { get; }
    public BooleanProperty KeepWidthOnChildren { get; }

    public VerticalLayoutElement()
    {
        Padding = new PaddingProperty(Common._2D.Padding.Empty);
        KeepWidthOnChildren = new BooleanProperty(false);
    }
    
    public override void LoadFromXml(XElement element)
    {
        base.LoadFromXml(element);
        
        Padding.Value = Common._2D.Padding.FromXml(element);

        bool.TryParse(element.Value, out var keepWidth); // keepWidth is null if failed anyway
        KeepWidthOnChildren.Value = keepWidth;
        
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
        var y = 0;
        
        foreach (var child in Children)
        {
            child.Y.Property.Value = y;

            if (KeepWidthOnChildren)
            {
                child.Width.Property.Value = Width.Property.Value;
                child.Width.Type = Width.Type;
            }

            y += child.Height.Property.Value;
        }
        
        base.Update(time);
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