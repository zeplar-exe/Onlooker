using SettingsConfig;

namespace Onlooker.IntermediateConfiguration.Entities.Personality;

public class PersonalityConfigGroup : ConfigGroup
{
    public List<PersonalityConfig> PersonalityConfigs { get; }

    public PersonalityConfigGroup()
    {
        PersonalityConfigs = new List<PersonalityConfig>();
    }

    public override void UpdateFromDirectory(DirectoryInfo root, IProgress<ConfigUpdateStatus> progress)
    {
        PersonalityConfigs.Clear();

        foreach (var file in root.EnumerateFiles("*.txt", SearchOption.TopDirectoryOnly))
        {
            var config = new PersonalityConfig(file);
            config.UpdateFromStream(file.OpenRead());
            
            PersonalityConfigs.Add(config);
        }
    }

    public override void WriteToDirectory(DirectoryInfo root, IProgress<ConfigWriteStatus> progress)
    {
        throw new NotImplementedException();
    }
}