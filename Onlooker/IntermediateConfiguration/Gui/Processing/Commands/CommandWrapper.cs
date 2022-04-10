using System.Reflection;
using System.Reflection.Emit;

namespace Onlooker.IntermediateConfiguration.Gui.Processing.Commands;

public class CommandWrapper
{
    public static CommandWrapper Empty => new(
        new DynamicMethod("", null, null),
        Array.Empty<object>());
    
    public MethodInfo Method { get; }
    public object?[] Parameters { get; }
    
    public CommandWrapper(MethodInfo method, object?[] parameters)
    {
        Method = method;
        Parameters = parameters;
    }

    public object? Invoke() => Invoke<object>();

    public T? Invoke<T>()
    {
        var result = Method.Invoke(null, Parameters);

        return result is T targetTyped ? targetTyped : default;
    }
}