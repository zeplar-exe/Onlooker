using System.Reflection;
using Microsoft.Xna.Framework.Graphics;
using Onlooker.IntermediateConfiguration.Game;
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
            var locationAttribute = property.GetCustomAttribute<ConfigLocationAttribute>();
            
            if (locationAttribute == null)
                continue;
            
            if (typeof(ConfigFile).IsAssignableFrom(property.PropertyType))
            {
                if (property.GetValue(this) is not ConfigFile config)
                    continue;

                var file = new FileInfo(locationAttribute.RelativeLocation);
                
                if (!file.Exists)
                    continue;
                
                config.UpdateFromStream(file.OpenRead());
            
                progress.Report(
                    CreateFileLoadedStatus(Path.Join(locationAttribute.RelativeLocation, file.Name)));
            }
            else if (property.GetValue(this) is ICollection<ConfigFile> collection)
            {
                var enumerableType = collection.GetType().GetGenericArguments()[0];
                
                collection.Clear();

                foreach (var file in root.EnumerateFiles("*.txt", SearchOption.TopDirectoryOnly))
                {
                    if (Activator.CreateInstance(enumerableType, file) is not ConfigFile config)
                        continue;

                    config.UpdateFromStream(file.OpenRead());
                    collection.Add(config);

                    progress.Report(
                        CreateFileLoadedStatus(Path.Join(locationAttribute.RelativeLocation, file.Name)));
                }
            }
            else if (typeof(ConfigGroup).IsAssignableFrom(property.PropertyType))
            {
                if (property.GetValue(this) is not ConfigGroup config)
                    continue;
                
                config.UpdateFromDirectory(
                    new DirectoryInfo(Path.Join(Directory.GetCurrentDirectory(), locationAttribute.RelativeLocation)),
                    progress);
            }
            else if (property.PropertyType == typeof(Texture2D))
            {
                if (!WarnWriteAccess(property))
                    continue;
                
                var texture = Texture2D.FromFile(
                    GameManager.Current.GraphicsDevice, 
                    Path.Join(Directory.GetCurrentDirectory(), locationAttribute.RelativeLocation));
                
                property.SetValue(this, texture);
            }
            else if (property.PropertyType == typeof(SpriteFont))
            {
                if (!WarnWriteAccess(property))
                    continue;
                
                var bake = TtfFontBaker.Bake(File.ReadAllBytes(
                        Path.Join(Directory.GetCurrentDirectory(), locationAttribute.RelativeLocation)),
                    25,
                    1024,
                    1024,
                    new[]
                    {
                        CharacterRange.BasicLatin,
                        CharacterRange.Latin1Supplement,
                        CharacterRange.LatinExtendedA,
                        CharacterRange.Cyrillic
                    } // Remove: These hardcoded values
                );
                
                property.SetValue(this, bake.CreateSpriteFont(GameManager.Current.GraphicsDevice));
        
                progress.Report(CreateFileLoadedStatus(locationAttribute.RelativeLocation));
            }
            else
            {
                continue;
            }
            
            progress.Report(CreateFileLoadedStatus(locationAttribute.RelativeLocation));
        }
    }
    
    private bool WarnWriteAccess(PropertyInfo p)
    {
        if (!p.CanWrite)
        {
            Console.WriteLine(
                $"WARNING: The property '{p.Name}' has a ConfigLocation attribute, but is readonly.");
        }

        return p.CanWrite;
    }

    public virtual void WriteToDirectory(DirectoryInfo root, IProgress<ConfigWriteStatus> progress)
    {
        foreach (var property in GetType().GetProperties(
                     BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
            var locationAttribute = property.GetCustomAttribute<ConfigLocationAttribute>();

            if (locationAttribute == null)
                continue;
            
            if (typeof(ConfigFile).IsAssignableFrom(property.PropertyType))
            {
                if (property.GetValue(this) is not ConfigFile config)
                    continue;
                
                var result = config.WriteToStream(config.Source.OpenWrite());
            
                progress.Report(result);
            }
            else if (property.GetValue(this) is IEnumerable<ConfigFile> enumerable)
            {
                foreach (var config in enumerable)
                {
                    var result = config.WriteToStream(config.Source.OpenWrite());
            
                    progress.Report(result);
                }
            }
            else if (typeof(ConfigGroup).IsAssignableFrom(property.PropertyType))
            {
                if (property.GetValue(this) is not ConfigGroup config)
                    continue;
                
                config.WriteToDirectory(
                    new DirectoryInfo(Path.Join(Directory.GetCurrentDirectory(), locationAttribute.RelativeLocation)),
                    progress);
            }
            else if (property.PropertyType == typeof(Texture2D))
            {
                using var stream = File.OpenRead(Path.Join(Directory.GetCurrentDirectory(),
                    "configuration/common/graphics/loading_screen.png"));

                if (property.GetValue(this) is not Texture2D loadingScreen)
                    continue;
        
                loadingScreen.SaveAsPng(stream, loadingScreen.Width, loadingScreen.Height);
                
                progress.Report(CreateFileWrittenStatus(locationAttribute.RelativeLocation));
            }
            else if (property.PropertyType == typeof(SpriteFont))
            {
                throw new NotSupportedException();
            }
        }
    }

    protected ConfigUpdateStatus CreateFileLoadedStatus(string relativePath)
    {
        return new ConfigUpdateStatus($"Loaded '{relativePath}'", UpdateStatusType.Success);
    }
    
    protected ConfigWriteStatus CreateFileWrittenStatus(string relativePath)
    {
        return new ConfigWriteStatus($"Wrote '{relativePath}'", WriteStatusType.Success);
    }
}