namespace Onlooker.IntermediateConfiguration.Game.Entities.Personality;

public class PersonalityConfigGroup : ConfigGroup
{
    [RelativeConfigLocation("personalities")]
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
            
            progress.Report(new ConfigUpdateStatus($"Loaded 'game/entities/personalities/{file.Name}'", UpdateStatusType.Success));
        }
    }

    public override void WriteToDirectory(DirectoryInfo root, IProgress<ConfigWriteStatus> progress)
    {
        
    }
}