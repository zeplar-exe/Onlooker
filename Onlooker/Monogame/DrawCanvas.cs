namespace Onlooker.Monogame;

public class DrawCanvas
{
    internal Dictionary<int, List<DrawItem>> Items { get; }

    public DrawCanvas()
    {
        Items = new Dictionary<int, List<DrawItem>>();
    }

    public void Draw(int z, DrawItem item)
    {
        if (!Items.TryGetValue(z, out var list))
        {
            list = new List<DrawItem>();
            Items.Add(z, list);
        }

        list.Add(item);
    }
}