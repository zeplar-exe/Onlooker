namespace Onlooker.Common.Extensions;

public static class ProgressExtensions
{
    public static void ReportMany<T>(this IProgress<T> progress, IEnumerable<T> enumerable)
    {
        foreach (var item in enumerable)
            progress.Report(item);
    }
}