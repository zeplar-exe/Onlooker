using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Onlooker.Common.Extensions;
using Onlooker.Common.Helpers;
using Onlooker.Monogame;
using Onlooker.ObjectProperties;

namespace Onlooker.IntermediateConfiguration.GUI.Elements;

public class LabelElement : GuiElement
{
    public RectangleProperty Rect { get; }
    public StringProperty Text { get; }
    public SpriteFont Font { get; set; }
    public IntegerProperty FontSize { get; }
    public Texture2D Background { get; set; }
    public BooleanProperty ScaleToText { get; }
    public BooleanProperty ScaleToRect { get; }
    public PaddingProperty Padding { get; }
    
    public LabelElement()
    {
        Rect = new RectangleProperty(Rectangle.Empty);
        Text = new StringProperty("");
        FontSize = new IntegerProperty(14);
        ScaleToText = new BooleanProperty(false);
        ScaleToRect = new BooleanProperty(false);
        Padding = new PaddingProperty(Onlooker.Common.Padding.Empty);
        
        Font = GameManager.Current.Configuration.CommonConfig.Fonts.Information!;
        Background = TextureHelper.CreateSolidColor(Color.DarkGray);
    }
    
    public override void LoadFromXml(XElement element)
    {
        Rect.Value = new Rectangle();
        Text.Value = element.Attribute("text")?.Value;
        FontSize.Value = element.Attribute("font_size")?.Value.SafeParseInt() ?? FontSize.Value;
        ScaleToText.Value = element.Attribute("text_scales")?.Value.SafeParseBool() ?? ScaleToText.Value;
        ScaleToRect.Value = element.Attribute("rect_scales")?.Value.SafeParseBool() ?? ScaleToRect.Value;
        Padding.Value = Onlooker.Common.Padding.FromXml(element);
        // TODO: Font, Background, Padding
    }

    public override void Update(GameTime time)
    {
        
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        canvas.Draw(ZIndex, new TextureItem(Background, Rect));
        canvas.Draw(ZIndex, new StringItem(Text.ToBuilder(), Font, Rect));
    }

    public override bool IsLocked()
    {
        return false;
    }
}