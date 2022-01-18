using Microsoft.Xna.Framework.Graphics;

namespace Onlooker.IntermediateConfiguration.Common.Graphics;

public class GraphicsConfigGroup : ConfigGroup
{
    [ConfigLocation("configuration/common/graphics/loading_screen.png")]
    public Texture2D? LoadingScreen { get; set; }
    
    [ConfigLocation("configuration/common/graphics/black.png")]
    public Texture2D? Black { get; set; }
}