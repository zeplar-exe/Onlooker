using Onlooker.Common;

namespace Onlooker.IntermediateConfiguration.GUI.Processing.Numeric;

public class PixelValue : NumericValue
{
    public PixelValue(int numericConstant) : base(numericConstant)
    {
        
    }
    
    public override int Calculate(ScreenOrigin origin)
    {
        return NumericConstant;
    }
}