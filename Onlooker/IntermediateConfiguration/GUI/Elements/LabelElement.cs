using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Onlooker.Common.Extensions;
using Onlooker.Common.Helpers;
using Onlooker.IntermediateConfiguration.Modules;
using Onlooker.IntermediateConfiguration.Modules.Common;
using Onlooker.Monogame.Graphics;
using Onlooker.ObjectProperties;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Onlooker.IntermediateConfiguration.GUI.Elements;

public class LabelElement : GuiElement
{
    public StringProperty Text { get; }
    public SpriteFont Font { get; set; }
    public IntegerProperty FontSize { get; }
    public Texture2D Background { get; set; }
    public BooleanProperty ScaleToText { get; }
    public BooleanProperty ScaleToRect { get; }
    public PaddingProperty Padding { get; }
    
    public LabelElement()
    {
        Text = new StringProperty("");
        FontSize = new IntegerProperty(14);
        ScaleToText = new BooleanProperty(false);
        ScaleToRect = new BooleanProperty(false);
        Padding = new PaddingProperty(Common.Padding.Empty);
        
        Font = ModuleRoot.Current.GetPersistentModule<CommonFontsModule>().Information;
        Background = TextureHelper.CreateSolidColor(Color.BlueViolet);
        
        RectChanged += (_, e) =>
        {
            if (ScaleToRect)
            {
                var size = Font.MeasureString(Text);
                var (xSize, ySize) = Rect.Size;

                if (size.X < xSize || size.Y < ySize)
                {
                    // Something something fontsize
                }
            }
        };

        Text.ValueChanged += (_, e) =>
        {
            if (ScaleToText)
            {
                var size = Font.MeasureString(Text);
                var rectSize = new Vector2(Rect.Width, Rect.Height);
                
                if (size.X > rectSize.X || size.Y > rectSize.Y)
                {
                    Width.Property.Value = (int)size.X;
                    Height.Property.Value = (int)size.X;
                }
            }
        };
    }
    
    public override void LoadFromXml(XElement element)
    {
        base.LoadFromXml(element);
        
        ScaleToText.Value = element.Attribute("rect_scales")?.Value.SafeParseBool() ?? ScaleToText.Value;
        ScaleToRect.Value = element.Attribute("text_scales")?.Value.SafeParseBool() ?? ScaleToRect.Value;
        Text.Value = element.Attribute("text")?.Value;
        FontSize.Value = element.Attribute("font_size")?.Value.SafeParseInt() ?? FontSize.Value;
        Padding.Value = Common.Padding.FromXml(element);
        // TODO: Font, Background, Padding
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        canvas.Draw(ZIndex, new TextureGraphic(Background, Rect));
        canvas.Draw(ZIndex, new StringGraphic(Text.ToBuilder(), Font, Rect));
        
        base.Draw(canvas, time);
    }

    public override bool IsLocked()
    {
        return false;
    }
}