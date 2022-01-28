using Onlooker.IntermediateConfiguration.Common.Fonts;
using Onlooker.IntermediateConfiguration.Common.Graphics;
using Onlooker.IntermediateConfiguration.Game;

namespace Onlooker.IntermediateConfiguration.Common;

public class CommonConfig : ConfigGroup
{
    [RelativeConfigLocation("graphics")] public GraphicsConfigGroup Graphics { get; set; }
    [RelativeConfigLocation("fonts")] public FontsConfigGroup Fonts { get; set; }
}