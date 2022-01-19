using Microsoft.Xna.Framework;

namespace Onlooker.Common;

public static class Time
{
    public static GameTime Delta { get; set; } = new(TimeSpan.Zero, TimeSpan.Zero);
}