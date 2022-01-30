using System.Collections;
using System.Reflection;
using System.Xml.Linq;
using Microsoft.Xna.Framework.Graphics;
using Onlooker.Common.StringResources.Configuration;
using Onlooker.Common.Wrappers;
using Onlooker.IntermediateConfiguration.Game;
using Onlooker.IntermediateConfiguration.GUI.Processing;
using Onlooker.Monogame;
using SpriteFontPlus;

namespace Onlooker.IntermediateConfiguration;

public abstract class ConfigGroup
{
    public virtual void UpdateFromDirectory(DirectoryInfo root, IProgress<ConfigUpdateStatus> progress)
    {
        foreach (var property in GetType().GetProperties(
                     BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
            var locationAttribute = property.GetCustomAttribute<RelativeConfigLocationAttribute>();
            
            if (locationAttribute == null)
                continue;

            var value = property.GetValue(this);

            if (typeof(ConfigFile).IsAssignableFrom(property.PropertyType))
            {
                var file = new FileInfo(Path.Join(root.FullName, locationAttribute.Location));
                
                if (!file.Exists)
                {
                    progress.Report(CreateMissingFileStatus(file));
                    continue;
                }
                
                if (value is not ConfigFile config)
                {
                    if (!WarnWriteAccess(property))
                        continue;

                    config = (ConfigFile)Activator.CreateInstance(property.PropertyType, file)!;
                    property.SetValue(this, config);
                }
                
                progress.Report(config.UpdateFromStream(file.OpenRead()));
            }
            else if(value is IList list && value.GetType().IsGenericType)
            {
                var enumerableType = list.GetType().GetGenericArguments()[0];
                
                if (!typeof(ConfigFile).IsAssignableFrom(enumerableType))
                    continue;
                
                list.Clear();

                var directory = new DirectoryInfo(Path.Join(root.FullName, locationAttribute.Location));

                foreach (var file in directory.EnumerateFiles("*.txt", SearchOption.TopDirectoryOnly))
                {
                    if (Activator.CreateInstance(enumerableType, file) is not ConfigFile config)
                        continue;
                    
                    progress.Report(config.UpdateFromStream(file.OpenRead()));
                    list.Add(config);
                }
            }
            else if (typeof(ConfigGroup).IsAssignableFrom(property.PropertyType))
            {
                if (value is not ConfigGroup config)
                {
                    if (!WarnWriteAccess(property))
                        continue;

                    config = (ConfigGroup)Activator.CreateInstance(property.PropertyType)!;
                    property.SetValue(this, config);
                }
                
                config.UpdateFromDirectory(
                    new DirectoryInfo(Path.Join(root.FullName, locationAttribute.Location)),
                    progress);
            }
            else if (property.PropertyType == typeof(Texture2D))
            {
                if (!WarnWriteAccess(property))
                    continue;
                
                var file = new FileInfo(Path.Join(root.FullName, locationAttribute.Location));
                
                if (!file.Exists)
                {
                    progress.Report(CreateMissingFileStatus(file));
                    continue;
                }
                
                var texture = Texture2D.FromFile(GameManager.Current.GraphicsDevice, file.FullName);
                property.SetValue(this, texture);
                
                progress.Report(CreateFileLoadedStatus(file));
            }
            else if (property.PropertyType == typeof(SpriteFont))
            {
                if (!WarnWriteAccess(property))
                    continue;

                var file = new FileInfo(Path.Join(root.FullName, locationAttribute.Location));
                
                if (!file.Exists)
                {
                    progress.Report(CreateMissingFileStatus(file));
                    continue;
                }
                
                var bake = TtfFontBaker.Bake(
                    File.ReadAllBytes(file.FullName),
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
                
                property.SetValue(this, bake.CreateSpriteFont(GameManager.Current.GraphicsDevice));
                progress.Report(CreateFileLoadedStatus(file));
            }
            else if (property.PropertyType == typeof(GuiDocument))
            {
                if (!WarnWriteAccess(property))
                    continue;
                
                var file = new FileInfo(Path.Join(root.FullName, locationAttribute.Location));

                if (!file.Exists)
                {
                    progress.Report(CreateMissingFileStatus(file));
                    continue;
                }

                var processor = new GuiProcessor();
                var xmlResult = processor.ProcessXml(XDocumentWrapper.Create(file));
                
                property.SetValue(this, xmlResult.Value);
                
                progress.Report(CreateFileLoadedStatus(file));
            }
            else
            {
                continue;
            }
        }
    }
    
    private bool WarnWriteAccess(PropertyInfo p)
    {
        if (!p.CanWrite)
        {
            Console.WriteLine(ConfigurationProgress.ReadonlyConfigGroupProperty, p.Name);
        }

        return p.CanWrite;
    }

    public virtual void WriteToDirectory(DirectoryInfo root, IProgress<ConfigWriteStatus> progress)
    {
        foreach (var property in GetType().GetProperties(
                     BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
            var locationAttribute = property.GetCustomAttribute<RelativeConfigLocationAttribute>();

            if (locationAttribute == null)
                continue;

            var value = property.GetValue(this);
            
            if (typeof(ConfigFile).IsAssignableFrom(property.PropertyType))
            {
                if (value is not ConfigFile config)
                    continue;
                
                var result = config.WriteToStream(config.Source.OpenWrite());
            
                progress.Report(result);
            }
            else if (value is IEnumerable<ConfigFile> enumerable)
            {
                foreach (var config in enumerable)
                {
                    var result = config.WriteToStream(config.Source.OpenWrite());
            
                    progress.Report(result);
                }
            }
            else if (typeof(ConfigGroup).IsAssignableFrom(property.PropertyType))
            {
                if (value is not ConfigGroup config)
                    continue;
                
                config.WriteToDirectory(
                    new DirectoryInfo(Path.Join(root.FullName, locationAttribute.Location)),
                    progress);
            }
            else if (property.PropertyType == typeof(Texture2D))
            { // None of these are going to change in-game
                continue;
            }
            else if (property.PropertyType == typeof(SpriteFont))
            {
                continue;
            }
            else if (property.PropertyType == typeof(GuiDocument))
            {
                continue;
            }
            else
            {
                continue;
            }
        }
    }

    protected static ConfigUpdateStatus CreateFileLoadedStatus(FileInfo file)
    {
        return new ConfigUpdateStatus(
            string.Format(ConfigurationProgress.FileLoaded, file), 
            UpdateStatusType.Success);
    }

    protected static ConfigUpdateStatus CreateMissingFileStatus(FileInfo file)
    {
        return new ConfigUpdateStatus(
            string.Format(ConfigurationProgress.ConfigurationFileMissing, file.Name),
            UpdateStatusType.Invalid);
    }
}