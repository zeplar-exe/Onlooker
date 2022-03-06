using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Onlooker.Common;
using Onlooker.Common.Extensions;
using Onlooker.Common.Helpers;
using Onlooker.Common.MethodOutput;
using Onlooker.IntermediateConfiguration.GUI.Processing.Commands;
using Onlooker.IntermediateConfiguration.Modules;
using Onlooker.IntermediateConfiguration.Modules.Entities;
using Onlooker.Monogame;
using Onlooker.Monogame.Graphics;
using Onlooker.ObjectProperties;
using Vector2 = Microsoft.Xna.Framework.Vector2;

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

        Font = ModuleRoot.Current.GetPersistentModule<CommonFontsModule>().Information;
        Background = TextureHelper.CreateSolidColor(Color.BlueViolet);
    }
    
    public override void LoadFromXml(XElement element)
    {
        base.LoadFromXml(element);
        
        ScaleToText.Value = element.Attribute("rect_scales")?.Value.SafeParseBool() ?? ScaleToText.Value;
        ScaleToRect.Value = element.Attribute("text_scales")?.Value.SafeParseBool() ?? ScaleToRect.Value;
        Text.Value = element.Attribute("text")?.Value;
        FontSize.Value = element.Attribute("font_size")?.Value.SafeParseInt() ?? FontSize.Value;
        Padding.Value = Onlooker.Common.Padding.FromXml(element);

        var onClickCommand = element.Attribute("on_click")?.Value;

        if (onClickCommand != null)
        {
            var parser = new CommandParser();
            var output = parser.Parse(onClickCommand);

            if (output.Output.Type == OperationOutputType.Success)
                OnClick += (_, _) => output.Value.Invoke();
        }
    }
    
    private bool ContinuedPress { get; set; }

    public override void Update(GameTime time)
    {
        if (ContinuedPress)
        {
            if (!MouseHelper.IsLeftButtonHeldOverRect(Rect))
            {
                if (MouseHelper.IsLeftButtonReleasedOverRect(Rect))
                {
                    OnClick?.Invoke(this,EventArgs.Empty);
                }
                else
                {
                    ContinuedPress = false;
                }
            }
        }
        else
        {
            if (MouseHelper.IsLeftButtonPressedOverRect(Rect))
                ContinuedPress = true;
        }

        base.Update(time);
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        canvas.Draw(ZIndex, new TextureGraphic(Background, Rect));
        canvas.Draw(ZIndex, new StringGraphic(Text.ToBuilder(), Font, Rect));
    }

    public override bool IsLocked()
    {
        return false;
    }
}