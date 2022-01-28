using System.Xml.Linq;
using Onlooker.Common.Extensions;

namespace Onlooker.Common;

public record struct Padding(int Up, int Down, int Left, int Right)
{
    public static Padding Empty => new(0, 0, 0, 0);

    public static Padding FromXml(XElement element)
    {
        var paddingUp = element.Attribute("padding_up")?.Value.SafeParseInt() ?? 0;
        var paddingDown = element.Attribute("padding_down")?.Value.SafeParseInt() ?? 0;
        var paddingLeft = element.Attribute("padding_left")?.Value.SafeParseInt() ?? 0;
        var paddingRight = element.Attribute("padding_right")?.Value.SafeParseInt() ?? 0;

        return new Padding(paddingUp, paddingDown, paddingLeft, paddingRight);
    }
}