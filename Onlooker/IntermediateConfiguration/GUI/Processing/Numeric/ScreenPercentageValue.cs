using Onlooker.Common;

namespace Onlooker.IntermediateConfiguration.GUI.Processing.Numeric;

public class ScreenPercentageValue : NumericValue
{
    public ScreenPercentageValue(int numericConstant) : base(numericConstant)
    {
        
    }

    public override int Calculate(ScreenOrigin origin)
    {
        return origin switch
        {
            ScreenOrigin.XPosition => (int)Math.Round(CommonValues.ScreenWidth.X / 100, 0) * NumericConstant,
            ScreenOrigin.YPosition => (int)Math.Round(CommonValues.ScreenHeight.Y / 100, 0) * NumericConstant,
            ScreenOrigin.Width => (int)Math.Round(CommonValues.ScreenWidth.X / 100, 0) * NumericConstant,
            ScreenOrigin.Height => (int)Math.Round(CommonValues.ScreenWidth.Y / 100, 0) * NumericConstant,
            _ => 0
        };

    }
}