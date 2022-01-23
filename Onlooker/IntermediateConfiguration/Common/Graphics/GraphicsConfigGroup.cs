using Microsoft.Xna.Framework.Graphics;

namespace Onlooker.IntermediateConfiguration.Common.Graphics;

public class GraphicsConfigGroup : ConfigGroup
{
    [ConfigLocation("configuration/common/graphics/loading_screen.png")]
    public Texture2D? LoadingScreen { get; set; }
}