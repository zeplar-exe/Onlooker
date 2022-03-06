namespace Onlooker.Common.Helpers;

public static class FileSystemHelper
{
    public static string FromWorkingDirectory(string relative)
    {
        return Path.Join(Directory.GetCurrentDirectory(), relative);
    }
}