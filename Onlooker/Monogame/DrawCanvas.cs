namespace Onlooker.Monogame;

public class DrawCanvas
{
    internal Dictionary<int, List<IDrawGraphic>> Items { get; }

    public DrawCanvas()
    {
        Items = new Dictionary<int, List<IDrawGraphic>>();
    }

    public void Draw(int z, IDrawGraphic graphic)
    {
        if (!Items.TryGetValue(z, out var list))
        {
            list = new List<IDrawGraphic>();
            Items.Add(z, list);
        }

        list.Add(graphic);
    }
}