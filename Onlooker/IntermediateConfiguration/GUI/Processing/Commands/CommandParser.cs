using System.Text;
using Jammo.ParserTools;
using Jammo.ParserTools.Lexing;
using Jammo.ParserTools.Tokenization;
using Jammo.ParserTools.Tools;
using Onlooker.Common.MethodOutput;
using Onlooker.Common.StringResources.Gui;

namespace Onlooker.IntermediateConfiguration.GUI.Processing.Commands;

public class CommandParser
{
    public OutputResult<OperationOutput, Action> Parse(string value)
    {
        var tokens = new Lexer(value, new LexerOptions { IncludeUnderscoreAsAlphabetic = true }).ToArray();

        if (tokens.Any(t =>
                !t.Is(LexerTokenId.Alphabetic) &&
                !t.Is(LexerTokenId.AlphaNumeric) &&
                !t.Is(LexerTokenId.Period)))
        {
            return new OutputResult<OperationOutput, Action>(
                new OperationOutput(
                    OperationOutputType.Failure, CommandParseOutput.InvalidCommandFormat),
                () => _ = 0);
        }

        var path = string.Join<LexerToken>("", tokens);

        if (path.Split().Any(string.IsNullOrWhiteSpace))
        {
            return new OutputResult<OperationOutput, Action>(
                new OperationOutput(
                    OperationOutputType.Failure, CommandParseOutput.InvalidCommandFormat),
                () => _ = 0);
        }

        Action? action = null;

        switch (path)
        {
            case "GameInternal.Play":
                action = GameInternalCommands.Play;
                break;
        }

        if (action != null)
        {
            return new OutputResult<OperationOutput, Action>(
                new OperationOutput(
                    OperationOutputType.Success,
                    CommandParseOutput.ValidCommandFormat),
                action);
        }
        
        return new OutputResult<OperationOutput, Action>(
            new OperationOutput(
                OperationOutputType.Failure, CommandParseOutput.InvalidCommandFormat),
            () => _ = 0);
    }
}