using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Onlooker.Common.Helpers;
using Onlooker.Monogame.Graphics;

namespace Onlooker.IntermediateConfiguration.GUI.Elements;

public class ButtonElement : TextElement
{
    public event EventHandler? OnClick;
    
    public ButtonElement()
    {
        RectFill.Value = Color.BlueViolet;
    }
    
    public override void LoadFromXml(XElement element)
    {
        base.LoadFromXml(element);

        var onClickCommand = element.Attribute("on_click")?.Value;

        if (onClickCommand != null)
        {
            OnClick += (_, _) => ParseCommand(element.Attribute("on_click")).Invoke();
        }
    }
    
    private bool ContinuedPress { get; set; }

    public override void Update(GameTime time)
    {
        base.Update(time);
        
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
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        base.Draw(canvas, time);
        
        canvas.Draw(ZIndex, new StringGraphic(Text.ToBuilder(), Font, Rect));
    }

    public override bool IsLocked()
    {
        return false;
    }
}