using System.Xml.Linq;
using Microsoft.Xna.Framework.Graphics;
using Onlooker.Common._2D;
using Onlooker.Common.Extensions;
using Onlooker.Common.Helpers;
using Onlooker.IntermediateConfiguration.Modules;
using Onlooker.IntermediateConfiguration.Modules.Common;
using Onlooker.ObjectProperties;

namespace Onlooker.IntermediateConfiguration.Gui.Elements;

public abstract class TextElement : GuiElement
{
    public StringProperty Text { get; }
    public SpriteFont? Font { get; set; }
    public IntegerProperty FontSize { get; }
    public BooleanProperty ScaleToText { get; }
    public BooleanProperty ScaleToRect { get; }

    public TextElement()
    {
        ScaleToText = new BooleanProperty(false);
        ScaleToRect = new BooleanProperty(false);
        
        Text = new StringProperty("");
        Font = IModule.Get<CommonFontsModule>().Information;
        FontSize = new IntegerProperty(14);
        
        RectChanged += (_, _) =>
        {
            if (ScaleToRect)
            {
                ScaleToRectImpl();
            }
        };

        Text.ValueChanged += (_, _) =>
        {
            if (ScaleToText)
            {
                ScaleToTextImpl();
            }
        };
    }

    protected virtual void ScaleToRectImpl()
    {
        var size = Font.MeasureString(Text);
        var (xSize, ySize) = Rect.Size;

        if (size.X < xSize || size.Y < ySize)
        {
            // Something something fontsize
        }
    }
    
    protected virtual void ScaleToTextImpl()
    {
        var size = Font.MeasureString(Text);
        var rectSize = new Vector2(Rect.Width, Rect.Height);
                
        if (size.X > rectSize.X || size.Y > rectSize.Y)
        {
            Width.Property.Value = (int)size.X;
            Height.Property.Value = (int)size.X;
        }
    }

    public override void LoadFromXml(XElement element)
    {
        base.LoadFromXml(element);
        
        ScaleToText.Value = element.Attribute("rect_scales")?.Value.SafeParseBool() ?? ScaleToText.Value;
        ScaleToRect.Value = element.Attribute("text_scales")?.Value.SafeParseBool() ?? ScaleToRect.Value;
        
        FontSize.Value = element.Attribute("font_size")?.Value.SafeParseInt() ?? FontSize.Value;
        
        var text = element.Attribute("text")?.Value;

        if (text == null)
        {
            text = GuiHelper.ParseCommand(element.Attribute("locale_text")).Invoke<string>() ?? string.Empty;
        }

        Text.Value = text;

        // TODO: Get fonts
    }
}