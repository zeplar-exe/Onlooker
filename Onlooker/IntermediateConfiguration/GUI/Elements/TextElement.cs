using Microsoft.Xna.Framework.Graphics;
using Onlooker.ObjectProperties;

namespace Onlooker.IntermediateConfiguration.GUI.Elements;

public abstract class TextElement : GuiElement
{
    public StringProperty Text { get; }
    public SpriteFont? Font { get; set; }
    public IntegerProperty FontSize { get; }

    public TextElement()
    {
        Text = new StringProperty("");
        FontSize = new IntegerProperty(14);
    }
}