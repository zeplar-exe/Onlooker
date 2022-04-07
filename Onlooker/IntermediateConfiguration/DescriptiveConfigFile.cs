using Microsoft.Xna.Framework.Graphics;
using Onlooker.Common.Helpers;
using Onlooker.Common.StringResources.Configuration;
using Onlooker.Monogame;
using YASF;
using YASF.Serialization;
using YASF.Settings;

namespace Onlooker.IntermediateConfiguration;

public abstract class DescriptiveConfigFile : ConfigFile
{
    private const string RelativePathPrefix = "rel::";
    
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
        
        IconTexture = TextureHelper.MissingTexture;

        if (iconSetting?.Value is TextSetting text)
        {
            yield return FillIconTexture(text.Value);
        }

        SettingsDeserializer.DeserializeTo(document, this);
        
        yield return new ConfigUpdateStatus(
            string.Format(ConfigurationProgress.FileLoaded, Source),
            UpdateStatusType.Success);
    }

    private ConfigUpdateStatus FillIconTexture(string iconPath)
    {
        var fullIconPath = iconPath.StartsWith(RelativePathPrefix) ?
            Path.Join(Source.DirectoryName, iconPath.Substring(RelativePathPrefix.Length)) :
            iconPath;

        if (string.IsNullOrWhiteSpace(fullIconPath))
        {
            return new ConfigUpdateStatus(
                string.Format(ConfigurationProgress.FileReceivedInvalidFile, Source.FullName, fullIconPath),
                UpdateStatusType.Invalid);
        }

        if (!File.Exists(fullIconPath))
        {
            return new ConfigUpdateStatus(
                string.Format(ConfigurationProgress.FileReceivedInvalidFile, Source.FullName, fullIconPath),
                UpdateStatusType.Invalid);
        }

        try
        {
            IconTexture = Texture2D.FromFile(GameManager.Current.GraphicsDevice, fullIconPath);
        }
        catch (InvalidOperationException)
        {
            return new ConfigUpdateStatus(
                ConfigurationProgress.FileReceivedIncorrectIconFormat,
                UpdateStatusType.Invalid);
        }

        return new ConfigUpdateStatus(
            string.Format(ConfigurationProgress.IconLoaded, fullIconPath),
            UpdateStatusType.Success);
    }

    public override void Dispose()
    {
        IconTexture?.Dispose();
        
        base.Dispose();
    }
}