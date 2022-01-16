namespace Onlooker.IntermediateConfiguration.Game.World.Terrain;

public class TerrainTypeConfigGroup : ConfigGroup
{
    public List<TerrainTypeConfig> TypeConfigs { get; }

    public TerrainTypeConfigGroup()
    {
        TypeConfigs = new List<TerrainTypeConfig>();
    }

    public override void UpdateFromDirectory(DirectoryInfo root, IProgress<ConfigUpdateStatus> progress)
    {
        TypeConfigs.Clear();

        foreach (var file in root.EnumerateFiles("*.txt", SearchOption.TopDirectoryOnly))
        {
            var config = new TerrainTypeConfig(file);
            config.UpdateFromStream(file.OpenRead());
            
            TypeConfigs.Add(config);
        }
    }

    public override void WriteToDirectory(DirectoryInfo root, IProgress<ConfigWriteStatus> progress)
    {
        foreach (var config in TypeConfigs)
        {
            using var stream = File.OpenWrite(Path.Join(root.FullName, config.Id + ConfigFile.Extension));
            var result = config.WriteToStream(stream);
            
            progress.Report(result);
        }
    }
}