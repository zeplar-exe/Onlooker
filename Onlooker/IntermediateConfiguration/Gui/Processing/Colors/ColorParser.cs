using System.Reflection;
using Jammo.ParserTools;
using Jammo.ParserTools.Lexing;
using Jammo.ParserTools.Tools;
using Microsoft.Xna.Framework;
using Onlooker.Common.MethodOutput;
using Onlooker.Common.StringResources.Gui;

namespace Onlooker.IntermediateConfiguration.Gui.Processing.Colors;

public class ColorParser
{
    public OutputResult<OperationOutput, Color> Parse(string? text)
    {
        var colorResult = Color.Transparent;
        
        if (string.IsNullOrWhiteSpace(text))
        {
            return new OutputResult<OperationOutput, Color>(
                OperationOutput.Failure(string.Format(ColorParseOutput.InvalidColorFormat, text)), colorResult);
        }

        var navigator = new Lexer(text.Trim()).ToNavigator();
        var state = new StateMachine<ParserState>(ParserState.ParsePossibleName);
        var colorState = new StateMachine<ColorSegmentState>(ColorSegmentState.ParseR);
        
        foreach (var token in navigator.EnumerateFromIndex())
        {
            switch (state.Current)
            {
                case ParserState.ParsePossibleName:
                {
                    if (token.Is(LexerTokenId.Alphabetic))
                    {
                        state.MoveTo(ParserState.Completed);

                        var colorProperty = typeof(Color).GetProperty(token.ToString(),
                            BindingFlags.Public | 
                            BindingFlags.Static |
                            BindingFlags.DeclaredOnly | 
                            BindingFlags.IgnoreCase);

                        if (colorProperty == null)
                        {
                            if (!navigator.AtEnd)
                                goto case ParserState.Completed;

                            return new OutputResult<OperationOutput, Color>(
                                OperationOutput.Failure(ColorParseOutput.InvalidColorName), colorResult);
                        }

                        colorResult = (Color)colorProperty.GetValue(null, null)!;

                        goto case ParserState.Completed;
                    }
                    
                    state.MoveTo(ParserState.ParseColorSegment);
                    goto case ParserState.ParseColorSegment;
                }
                case ParserState.ParseColorSegment:
                {
                    if (token.Is(LexerTokenId.Whitespace))
                        break;
                        
                    if (!token.Is(LexerTokenId.Numeric))
                    {
                        return new OutputResult<OperationOutput, Color>(
                            OperationOutput.Failure(string.Format(ColorParseOutput.InvalidColorFormat, text)), colorResult);
                    }
                    
                    switch (colorState.Current)
                    {
                        case ColorSegmentState.ParseR:
                        {
                            colorResult.R = byte.Parse(token.ToString());
                            colorState.MoveTo(ColorSegmentState.ParseG);
                            
                            break;
                        }
                        case ColorSegmentState.ParseG:
                        {
                            colorResult.G = byte.Parse(token.ToString());
                            colorState.MoveTo(ColorSegmentState.ParseB);
                            
                            break;
                        }
                        case ColorSegmentState.ParseB:
                        {
                            colorResult.B = byte.Parse(token.ToString());
                            colorState.MoveTo(ColorSegmentState.ParseA);
                            
                            break;
                        }
                        case ColorSegmentState.ParseA:
                        {
                            colorResult.A = byte.Parse(token.ToString());
                            state.MoveTo(ParserState.Completed);
                            
                            break;
                        }
                    }
                    
                    
                    if (state.Current == ParserState.Completed)
                        break;
                    
                    if (!navigator.TakeIf(t => t.Is(LexerTokenId.Comma), out var comma))
                    {
                        return new OutputResult<OperationOutput, Color>(
                            OperationOutput.Failure(string.Format(ColorParseOutput.InvalidColorFormat, text)), colorResult);
                    }
                    
                    // add Skipped/Taken property in navigator to denote when it happens and to function like Started
                    
                    break;
                }
                case ParserState.Completed:
                {
                    navigator.SkipWhile(t => t.Is(LexerTokenId.Whitespace) || t.Is(LexerTokenId.Newline));

                    if (!navigator.AtEnd)
                    {
                        return new OutputResult<OperationOutput, Color>(
                            OperationOutput.Failure(string.Format(ColorParseOutput.InvalidColorFormat, text)), colorResult);
                    }
                    
                    break;
                }
            }
        }

        return new OutputResult<OperationOutput, Color>(
            OperationOutput.Success(string.Format(ColorParseOutput.ValidColorFormat, text)), colorResult);
    }

    private enum ParserState
    {
        ParsePossibleName,
        ParseColorSegment,
        Completed
    }

    private enum ColorSegmentState
    {
        ParseR,
        ParseG,
        ParseB,
        ParseA,
    }
}