using Jammo.ParserTools;
using Jammo.ParserTools.Lexing;
using Onlooker.Common.MethodOutput;
using Onlooker.Common.StringResources.Gui;

namespace Onlooker.IntermediateConfiguration.Gui.Processing.Numeric;

public class NumericValueParser
{
    public OutputResult<OperationOutput, NumericValue> Parse(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return new OutputResult<OperationOutput, NumericValue>(
                new OperationOutput(
                    OperationOutputType.Failure,
                    string.Format(NumericValueOutput.InvalidNumericFormat, text)),
                new NumericValue(0, NumericType.Pixels));
        }
        
        var navigator = new Lexer(text.Trim()).ToNavigator();
        navigator.TryMoveNext(out var first);
        
        if (!first.Is(LexerTokenId.Numeric))
        {
            return new OutputResult<OperationOutput, NumericValue>(
                OperationOutput.Failure(
                    string.Format(NumericValueOutput.InvalidNumericFormat, text)),
                new NumericValue(0, NumericType.Pixels));
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
                        new NumericValue(number, NumericType.ScreenPercentage));
                default:
                    return new OutputResult<OperationOutput, NumericValue>(
                        new OperationOutput(
                            OperationOutputType.Failure,
                            string.Format(NumericValueOutput.InvalidNumericFormat, text)),
                        new NumericValue(0, NumericType.ScreenPercentage));
            }
        }
        
        return new OutputResult<OperationOutput, NumericValue>(
            new OperationOutput(
                OperationOutputType.Success,
                string.Format(NumericValueOutput.ValidNumericFormat, text)),
            new NumericValue(number, NumericType.Pixels));
    }
}