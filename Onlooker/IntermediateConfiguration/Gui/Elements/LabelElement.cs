using System.Text;
using Microsoft.Xna.Framework;
using Onlooker.Common.Helpers;
using Onlooker.Common.MethodOutput;
using Onlooker.IntermediateConfiguration.Modules;
using Onlooker.IntermediateConfiguration.Modules.Common;
using Onlooker.Monogame.Graphics;
using Onlooker.Monogame.Logging;

namespace Onlooker.IntermediateConfiguration.GUI.Elements;

public class LabelElement : TextElement
{
    public LabelElement()
    {
        Font = IModule.Get<CommonFontsModule>().Information;
        RectFill.Value = Color.BlueViolet;
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        base.Draw(canvas, time);
        
        if (Font == null)
        {
            var ((outputType, outputMessage), errorFont) = FontHelper.GetDefaultFont("error");

            if (outputType is not ProcessingOutputType.Success)
            {
                AppLoggerCommon.ErrorLog(LogMessageBuilder.TimestampedMessage(outputMessage));
                
                return;
            }
            
            canvas.Draw(ZIndex,
                new StringGraphic(new StringBuilder("null font"), errorFont!, Rect));
            
            return;
        }
        
        canvas.Draw(ZIndex, new StringGraphic(Text.ToBuilder(), Font, Rect));
    }

    public override bool IsLocked()
    {
        return false;
    }
}