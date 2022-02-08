using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Onlooker.Common.Helpers;
using Onlooker.Monogame;
using SettingsConfig.Serialization;

namespace Onlooker.IntermediateConfiguration.Game;

public abstract class DescriptiveConfigFile : ConfigFile
{
    public string Id { get; set; }
    
    [SettingsSerializer.SerializationName("display_name")]
    [SettingsDeserializer.SerializationName("display_name")]
    public string DisplayName { get; set; }
    
    [SettingsSerializer.SerializationName("icon")]
    [SettingsDeserializer.SerializationName("icon")]
    public string IconPath { get; set; }
    
    public string Description { get; set; }
    
    [SettingsSerializer.Ignore]
    [SettingsDeserializer.Ignore]
    public Texture2D IconTexture { get; private set; }

    protected DescriptiveConfigFile(FileInfo source) : base(source)
    {
        
    }

    public override IEnumerable<ConfigUpdateStatus> UpdateFromStream(Stream stream)
    {
        foreach (var status in base.UpdateFromStream(stream))
            yield return status;

        var fullIconPath = Source.FullName;

        if (IconPath.StartsWith("rel::"))
        {
            fullIconPath = Path.Join(fullIconPath, IconPath);
        }
        else
        {
            fullIconPath = IconPath;
        }

        if (string.IsNullOrWhiteSpace(fullIconPath))
        {
            IconTexture = TextureHelper.CreateSolidColor(Color.Pink);

            yield return new ConfigUpdateStatus(
                $"'{Source.FullName}' received an empty icon path.",
                UpdateStatusType.Invalid);
            yield break;
        }

        if (!File.Exists(fullIconPath))
        {
            IconTexture = TextureHelper.CreateSolidColor(Color.Pink);
            
            yield return new ConfigUpdateStatus(
                $"'{Source.FullName}' received a file that does not exist '{fullIconPath}'.",
                UpdateStatusType.Invalid);
            yield break;
        }
        
        IconTexture = Texture2D.FromFile(GameManager.Current.GraphicsDevice, fullIconPath);
    }

    public override void Dispose()
    {
        IconTexture?.Dispose();
        
        base.Dispose();
    }
}