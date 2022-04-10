using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Onlooker.ObjectProperties;

namespace Onlooker.IntermediateConfiguration.Gui.Elements;

public class VerticalLayoutElement : GuiElement
{
    public PaddingProperty Padding { get; }
    public BooleanProperty SyncWidth { get; }

    public VerticalLayoutElement()
    {
        Padding = new PaddingProperty(Common._2D.Padding.Empty);
        SyncWidth = new BooleanProperty(false);
    }
    
    public override void LoadFromXml(XElement element)
    {
        base.LoadFromXml(element);
        
        Padding.Value = Common._2D.Padding.FromXml(element);

        bool.TryParse(element.Attribute("sync_width")?.Value, out var keepWidth); // keepWidth is null if failed anyway
        SyncWidth.Value = keepWidth;
        
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
        base.Update(time);
        
        var y = Y.Copy();
        
        foreach (var child in Children)
        {
            X.CopyTo(child.X);
            y.CopyTo(child.Y);

            if (SyncWidth)
            {
                child.Width.Property.Value = Width.Property.Value;
                child.Width.Type = Width.Type;
            }

            y.Property.Value += child.Height.Property.Value;
        }
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