namespace Onlooker.IntermediateConfiguration.Game.Entities.Moods;

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
            
            progress.Report(new ConfigUpdateStatus($"Loaded 'game/entities/moods/{file.Name}'", UpdateStatusType.Success));
        }
    }

    public override void WriteToDirectory(DirectoryInfo root, IProgress<ConfigWriteStatus> progress)
    {
        foreach (var config in MoodConfigs)
        {
            using var stream = File.OpenWrite(Path.Join(root.FullName, config.Id + ConfigFile.Extension));
            var result = config.WriteToStream(stream);
            
            progress.Report(result);
        }
    }
}