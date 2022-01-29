using Jammo.ParserTools;
using Jammo.ParserTools.Lexing;
using Onlooker.Common.MethodOutput;
using Onlooker.Common.StringResources.Gui;

namespace Onlooker.IntermediateConfiguration.GUI.Processing.Numeric;

public class NumericValueParser
{
    public OutputResult<OperationOutput, NumericValue> Parse(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return new OutputResult<OperationOutput, NumericValue>(
                new OperationOutput(
                    OperationOutputType.Failure,
                    string.Format(NumericValueOutput.InvalidNumericFormat, value)),
                new PixelValue(0));
        }
        
        var navigator = new Lexer(value).ToNavigator();
        
        if (!navigator.TryMoveNext(out var first) || !first.Is(LexerTokenId.Numeric))
        {
            return new OutputResult<OperationOutput, NumericValue>(
                new OperationOutput(
                    OperationOutputType.Failure,
                    string.Format(NumericValueOutput.InvalidNumericFormat, value)),
                new PixelValue(0));
        }
        
        var number = int.Parse(first.ToString());

        if (navigator.TryMoveNext(out var next))
        {
            switch (next.ToString())
            {
                case "%":
                    if (!navigator.AtEnd)
                        goto default;
                    
                    return new OutputResult<OperationOutput, NumericValue>(
                        new OperationOutput(
                            OperationOutputType.Success,
                            NumericValueOutput.ValidNumericFormat),
                        new ScreenPercentageValue(number));
                default:
                    return new OutputResult<OperationOutput, NumericValue>(
                        new OperationOutput(
                            OperationOutputType.Failure,
                            string.Format(NumericValueOutput.InvalidNumericFormat, value)),
                        new PixelValue(0));
            }
        }
        
        return new OutputResult<OperationOutput, NumericValue>(
            new OperationOutput(
                OperationOutputType.Success,
                string.Format(NumericValueOutput.ValidNumericFormat, value)),
            new PixelValue(number));
    }
}