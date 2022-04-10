using System.Reflection;
using Jammo.ParserTools;
using Jammo.ParserTools.Lexing;
using Jammo.ParserTools.Tools;
using Onlooker.Common.MethodOutput;
using Onlooker.Common.MethodOutput.OutputTypes;
using Onlooker.Common.StringResources.Gui;

namespace Onlooker.IntermediateConfiguration.Gui.Processing.Commands;

public class CommandParser
{
    public OutputResult<OperationOutput, CommandWrapper> Parse(string value)
    {
        var tokens = new Lexer(value, new LexerOptions
        {
            IncludeUnderscoreAsAlphabetic = true,
            IncludePeriodAsNumeric = true
        });
        
        var navigator = tokens.ToNavigator();
        var state = new StateMachine<ParserState>();

        var commandPath = new List<LexerToken>();
        var parameters = new List<string>();

        foreach (var token in navigator.EnumerateFromIndex())
        {
            switch (state.Current)
            {
                case ParserState.ParseNameOrParameters:
                {
                    switch (token.Id)
                    {
                        case LexerTokenId.Alphabetic:
                        case LexerTokenId.AlphaNumeric:
                            commandPath.Add(token);

                            state.MoveTo(navigator.AtEnd ? ParserState.Completed : ParserState.ParseSeparator);
                            break;
                        default:
                            return InvalidCommand(value);
                    }
                    
                    break;
                }
                case ParserState.ParseSeparator:
                {
                    if (navigator.AtEnd)
                        return InvalidCommand(value);
                    
                    switch (token.Id)
                    {
                        case LexerTokenId.Period:
                            state.MoveTo(ParserState.ParseNameOrParameters);
                            break;
                        case LexerTokenId.LeftParenthesis:
                            state.MoveTo(ParserState.ParseParameterList);
                            break;
                        default:
                            return InvalidCommand(value);
                    }
                    
                    break;
                }
                case ParserState.ParseParameterList:
                {
                    switch (token.Id)
                    {
                        case LexerTokenId.Alphabetic:
                        case LexerTokenId.AlphaNumeric:
                            parameters.Add(token.ToString());
                            state.MoveTo(ParserState.ParseParameterListSeparator);
                            break;
                        case LexerTokenId.Whitespace:
                            break;
                        case LexerTokenId.RightParenthesis:
                            state.MoveTo(ParserState.Completed);
                            break;
                        default:
                            return InvalidCommand(value);
                    }
                    
                    if (navigator.AtEnd && state.Current != ParserState.Completed)
                        return InvalidCommand(value);
                    
                    break;
                }
                case ParserState.ParseParameterListSeparator:
                    switch (token.Id)
                    {
                        case LexerTokenId.Whitespace:
                            break;
                        case LexerTokenId.Comma:
                            state.MoveTo(ParserState.ParseParameterList);
                            break;
                        case LexerTokenId.RightParenthesis:
                            state.MoveTo(ParserState.Completed);
                            break;
                        default:
                            return InvalidCommand(value);
                    }
                    
                    break;
                case ParserState.Completed:
                {
                    switch (token.Id)
                    {
                        case LexerTokenId.Whitespace:
                            break;
                        default:
                            return InvalidCommand(value);
                    }
                    
                    break;
                }
            }
        }

        if (commandPath.Count == 0)
            return InvalidCommand(value);
        
        var first = commandPath.First();
        
        Type commandContext;

        switch (first.ToString())
        {
            case "GameInternal":
                commandContext = typeof(GameInternalCommands);
                break;
            case "LocaleService":
                commandContext = typeof(LocaleServiceCommands);
                break;
            default:
                return InvalidCommand(value);
        }
        
        foreach (var part in commandPath.Skip(1))
        {
            if (part == commandPath.Last())
            {
                var method = commandContext.GetMethod(part.ToString(),
                    BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
                
                if (method == null)
                    return InvalidCommand(value);
                
                return new OutputResult<OperationOutput, CommandWrapper>(
                    new OperationOutput(OperationOutputType.Success, 
                        string.Format(CommandParseOutput.ValidCommandFormat, value)),
                    new CommandWrapper(method, parameters.Cast<object>().ToArray()));
            }
            
            var nextType = commandContext.GetNestedType(part.ToString(),
                BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
                
            if (nextType == null)
                return InvalidCommand(value);

            commandContext = nextType;
        }

        return InvalidCommand(value);
    }

    private OutputResult<OperationOutput, CommandWrapper> InvalidCommand(string value)
    {
        return new OutputResult<OperationOutput, CommandWrapper>(
            new OperationOutput(OperationOutputType.Failure, string.Format(CommandParseOutput.InvalidCommandFormat, value)),
            CommandWrapper.Empty);
    }

    private enum ParserState
    {
        ParseNameOrParameters,
        ParseSeparator,
        ParseParameterList,
        ParseParameterListSeparator,
        Completed
    }
}