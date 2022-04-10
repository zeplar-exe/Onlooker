using System.Text;

namespace Onlooker.Monogame.Logging;

public class LogMessageBuilder
{
    private bool IncludeTimeProperty { get; set; }
    private bool IncludeDateProperty { get; set; }
    
    private List<string> Messages { get; }

    public LogMessageBuilder()
    {
        Messages = new List<string>(100);
    }

    public void AppendText(string text)
    {
        Messages.Add(text);
    }

    public void IncludeDate(bool b = true)
    {
        IncludeDateProperty = b;
    }

    public void IncludeTime(bool b = true)
    {
        IncludeTimeProperty = b;
    }

    public static LogMessageBuilder TimedMessage(string message)
    {
        var builder = new LogMessageBuilder();

        builder.IncludeTime();
        builder.AppendText(message);
        
        return builder;
    }

    public static LogMessageBuilder DatedMessage(string message)
    {
        var builder = new LogMessageBuilder();

        builder.IncludeDate();
        builder.AppendText(message);

        return builder;
    }
    
    public static LogMessageBuilder TimestampedMessage(string message)
    {
        var builder = new LogMessageBuilder();

        builder.IncludeTime();
        builder.IncludeDate();
        builder.AppendText(message);
        
        return builder;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();

        if (IncludeDateProperty)
        {
            builder.Append(DateTime.Now.ToShortDateString());
            builder.Append(' ');
        }

        if (IncludeTimeProperty)
        {
            builder.Append(DateTime.Now.ToLongTimeString());
        }

        if (IncludeDateProperty || IncludeTimeProperty)
        {
            builder.Append(": ");
        }

        foreach (var message in Messages)
        {
            builder.Append(message);
        }

        return builder.ToString();
    }
}