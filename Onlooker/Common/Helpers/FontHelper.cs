using System.Reflection;
using Microsoft.Xna.Framework.Graphics;
using Onlooker.Common.Extensions;
using Onlooker.Common.MethodOutput;
using Onlooker.Common.MethodOutput.OutputTypes;
using Onlooker.Common.StringResources;
using Onlooker.Monogame;
using SpriteFontPlus;

namespace Onlooker.Common.Helpers;

public static class FontHelper
{
    private static Dictionary<string, SpriteFont> FontCache { get; }

    static FontHelper()
    {
        FontCache = new Dictionary<string, SpriteFont>();
    }

    public static OutputResult<FileProcessingOutput, SpriteFont?> GetDefaultFont(string name)
    {
        if (!FontCache.TryGetValue(name, out var cacheFont))
        {
            return new OutputResult<FileProcessingOutput, SpriteFont?>(
                FileProcessingOutput.Success(ResourceLoadOutput.LoadedResourceFromCache), cacheFont);
        }
        
        var assembly = Assembly.GetExecutingAssembly();

        using var stream = assembly.GetManifestResourceStream("Defaults.Fonts." + name + ".ttf");

        if (stream == null)
            return new OutputResult<FileProcessingOutput, SpriteFont?>(
                FileProcessingOutput.Failure(ResourceLoadOutput.DoesNotExist), null);

        var bake = TtfFontBaker.Bake(
            stream.ReadAllBytes().ToArray(),
            25,
            1024,
            1024,
            new[]
            {
                CharacterRange.BasicLatin,
                CharacterRange.Latin1Supplement,
                CharacterRange.LatinExtendedA,
                CharacterRange.Cyrillic
            }
        );

        var font = bake.CreateSpriteFont(GameManager.Current.GraphicsDevice);

        FontCache[name] = font;

        return new OutputResult<FileProcessingOutput, SpriteFont?>(
            FileProcessingOutput.Success(ResourceLoadOutput.LoadedResource), font);
    }
}