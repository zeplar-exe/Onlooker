using Microsoft.Xna.Framework.Graphics;
using Onlooker.Common.Helpers;
using Onlooker.Common.StringResources.Configuration;
using Onlooker.IntermediateConfiguration.Game;
using Onlooker.Monogame;
using SettingsConfig;
using SettingsConfig.Serialization;
using SettingsConfig.Settings;

namespace Onlooker.IntermediateConfiguration;

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
        var document = SettingsDocument.FromStream(stream);
        var iconSetting = document["icon"];

        if (iconSetting?.Value is TextSetting text)
        {
            var fullIconPath = text.Value.StartsWith("rel::") ? Path.Join(Source.FullName, IconPath) : IconPath;

            if (string.IsNullOrWhiteSpace(fullIconPath))
            {
                IconTexture = TextureHelper.MissingTexture;

                yield return new ConfigUpdateStatus(
                    string.Format(ConfigurationProgress.FileReceivedInvalidFile, Source.FullName, fullIconPath),
                    UpdateStatusType.Invalid);
            }

            if (!File.Exists(fullIconPath))
            {
                IconTexture = TextureHelper.MissingTexture;

                yield return new ConfigUpdateStatus(
                    string.Format(ConfigurationProgress.FileReceivedInvalidFile, Source.FullName, fullIconPath),
                    UpdateStatusType.Invalid);
            }

            Exception? exception = null;

            try
            {
                IconTexture = Texture2D.FromFile(GameManager.Current.GraphicsDevice, fullIconPath);
            }
            catch (InvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                yield return new ConfigUpdateStatus(
                    ConfigurationProgress.FileReceivedIncorrectIconFormat,
                    UpdateStatusType.Invalid);
            }
        }

        SettingsDeserializer.DeserializeTo(document, this);
        
        yield return new ConfigUpdateStatus(
            string.Format(ConfigurationProgress.FileLoaded, Source),
            UpdateStatusType.Success);
    }

    public override void Dispose()
    {
        IconTexture?.Dispose();
        
        base.Dispose();
    }
}