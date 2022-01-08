namespace Onlooker.IntermediateConfiguration;

public abstract class ConfigGroup
{
    public abstract void UpdateFromDirectory(DirectoryInfo root, IProgress<ConfigUpdateStatus> progress);
    public abstract void WriteToDirectory(DirectoryInfo root, IProgress<ConfigWriteStatus> progress);
}