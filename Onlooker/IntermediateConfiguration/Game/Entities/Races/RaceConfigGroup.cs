namespace Onlooker.IntermediateConfiguration.Game.Entities.Races;

public class RaceConfigGroup : ConfigGroup
{
    [ConfigLocation("game/entities/races")]
    public List<RaceConfig> RaceConfigs { get; }

    public RaceConfigGroup()
    {
        RaceConfigs = new List<RaceConfig>();
    }

    public override void WriteToDirectory(DirectoryInfo root, IProgress<ConfigWriteStatus> progress)
    {
        foreach (var config in RaceConfigs)
        {
            using var stream = File.OpenWrite(Path.Join(root.FullName, config.Id + ConfigFile.Extension));
            var result = config.WriteToStream(stream);
            
            progress.Report(result);
        }
    }
}