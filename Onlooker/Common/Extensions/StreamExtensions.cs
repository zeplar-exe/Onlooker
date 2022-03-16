namespace Onlooker.Common.Extensions;

public static class StreamExtensions
{
    public static IEnumerable<byte> ReadAllBytes(this Stream stream)
    {
        while (stream.Position != stream.Length)
            yield return (byte)stream.ReadByte();
    }
}