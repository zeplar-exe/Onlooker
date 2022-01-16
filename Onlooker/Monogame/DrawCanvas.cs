namespace Onlooker.Monogame;

public class DrawCanvas
{
    internal Dictionary<int, List<IDrawItem>> Items { get; }

    public DrawCanvas()
    {
        Items = new Dictionary<int, List<IDrawItem>>();
    }

    public void Draw(int z, IDrawItem item)
    {
        if (!Items.TryGetValue(z, out var list))
        {
            list = new List<IDrawItem>();
            Items.Add(z, list);
        }

        list.Add(item);
    }
}