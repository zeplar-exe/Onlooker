using Microsoft.Xna.Framework;

namespace Onlooker.Common;

public static class Time
{
    public static GameTime LastUpdate { get; set; } = new(TimeSpan.Zero, TimeSpan.Zero);
}