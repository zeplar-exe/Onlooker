namespace Onlooker.Common.Extensions;

public static class FileSystemExtensions
{
    public static FileInfo ToRelativeFile(this DirectoryInfo directory, string path)
    {
        return new FileInfo(Path.Join(directory.FullName, path));
    }
    
    public static DirectoryInfo ToRelativeDirectory(this DirectoryInfo directory, string path)
    {
        return new DirectoryInfo(Path.Join(directory.FullName, path));
    }
}