using Onlooker.Common;

namespace Onlooker.IntermediateConfiguration.GUI.Processing.Numeric;

public abstract class NumericValue
{
    public int NumericConstant { get; }
    
    protected NumericValue(int numericConstant)
    {
        NumericConstant = numericConstant;
    }
    
    public abstract int Calculate(ScreenOrigin origin);
}