namespace Onlooker.IntermediateConfiguration.Entities.Moods;

public class MoodConfigGroup : ConfigGroup
{
    public List<MoodConfig> MoodConfigs { get; }
    
    public MoodConfigGroup()
    {
        MoodConfigs = new List<MoodConfig>();
    }

    public override void UpdateFromDirectory(DirectoryInfo root, IProgress<ConfigUpdateStatus> progress)
    {
        MoodConfigs.Clear();

        foreach (var file in root.EnumerateFiles("*.txt", SearchOption.TopDirectoryOnly))
        {
            var config = new MoodConfig(file);
            config.UpdateFromStream(file.OpenRead());
            
            MoodConfigs.Add(config);
        }
    }

    public override void WriteToDirectory(DirectoryInfo root, IProgress<ConfigWriteStatus> progress)
    {
        throw new NotImplementedException();
    }
}