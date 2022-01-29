using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Onlooker.Common.Extensions;
using Onlooker.Common.Helpers;
using Onlooker.Monogame;
using Onlooker.ObjectProperties;

namespace Onlooker.IntermediateConfiguration.GUI.Elements;

public class ButtonElement : GuiElement
{
    public StringProperty Text { get; }
    public SpriteFont Font { get; set; }
    public IntegerProperty FontSize { get; }
    public Texture2D Background { get; set; }
    public BooleanProperty ScaleToText { get; }
    public BooleanProperty ScaleToRect { get; }
    public PaddingProperty Padding { get; }

    public event EventHandler? OnClick;
    
    public ButtonElement() : base(new RectangleProperty(new Rectangle(0, 0, 200, 200)))
    {
        Text = new StringProperty("");
        FontSize = new IntegerProperty(14);
        ScaleToText = new BooleanProperty(false);
        ScaleToRect = new BooleanProperty(false);
        Padding = new PaddingProperty(Onlooker.Common.Padding.Empty);

        Rect.ValueChanged += (_, e) =>
        {
            if (ScaleToRect)
            {
                var size = Font!.MeasureString(Text);
                var rectSize = new Vector2(Rect.Value.Width, Rect.Value.Height);

                if (size.X < rectSize.X || size.Y < rectSize.Y)
                {
                    // Something something fontsize
                }
            }
        };

        Text.ValueChanged += (_, e) =>
        {
            if (ScaleToText)
            {
                var size = Font!.MeasureString(Text);
                var rectSize = new Vector2(Rect.Value.Width, Rect.Value.Height);
                
                if (size.X > rectSize.X || size.Y > rectSize.Y)
                {
                    Rect.Value = new Rectangle(Rect.Value.Location, new Point((int)size.X, (int)size.Y));
                }
            }
        };
        
        Font = GameManager.Current.Configuration.CommonConfig.Fonts.Information;
        Background = TextureHelper.CreateSolidColor(Color.BlueViolet);
    }
    
    public override void LoadFromXml(XElement element)
    {
        base.LoadFromXml(element);
        
        ScaleToText.Value = element.Attribute("rect_scales")?.Value.SafeParseBool() ?? ScaleToText.Value;
        ScaleToRect.Value = element.Attribute("text_scales")?.Value.SafeParseBool() ?? ScaleToRect.Value;
        Rect.Value = new Rectangle(0, 0, 50, 50);
        Text.Value = element.Attribute("text")?.Value;
        FontSize.Value = element.Attribute("font_size")?.Value.SafeParseInt() ?? FontSize.Value;
        Padding.Value = Onlooker.Common.Padding.FromXml(element);
    }

    public override void Update(GameTime time)
    {
        if (MouseHelper.IsLeftButtonPressedOverRect(Rect))
        {
            OnClick?.Invoke(this,EventArgs.Empty);
        }
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