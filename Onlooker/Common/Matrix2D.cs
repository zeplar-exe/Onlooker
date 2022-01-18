namespace Onlooker.Common;

public class Matrix2D<T>
{
    private T[,] Items { get; }

    public Vector2Int Size => new Vector2Int(Items.GetLength(0), Items.GetLength(1));
    public int Width => Size.X;
    public int Height => Size.Y;

    public T this[int x, int y]
    {
        get => Items[x, y];
        set => Items[x, y] = value;
    }

    public Matrix2D(Vector2Int size)
    {
        Items = new T[size.X, size.Y];
    }

    public Matrix2D(T[,] initial)
    {
        Items = initial;
    }

    public Matrix2D(Matrix2D<T> initial)
    {
        Items = initial.Items;
    }

    public Matrix2D(int columnSize, IEnumerable<T> initial)
    {
        if (columnSize == 0)
        {
            Items = new T[0, 0];
            
            return;
        }

        var initialArray = initial.ToArray();
        var height = (int)Math.Ceiling((double)initialArray.Length / columnSize);
        Items = new T[columnSize, height];

        var index = 0;
        var x = 0;
        var y = 0;
        
        while (index < initialArray.Length)
        {
            if (x == columnSize)
            {
                x = 0;
                y++;
            }
            
            if (y == height)
                break;

            Items[x, y] = initialArray[index];
            
            x++;
            index++;
        }
    }

    public Matrix2D(Vector2Int size, IEnumerable<T> initial)
    {
        Items = new T[size.X, size.Y];
        
        if (size.X == 0 || size.Y == 0)
            return;

        var x = 0;
        var y = 0;
        var index = 0;
        var initialArray = initial.ToArray();

        while (index < initialArray.Length)
        {
            if (x == size.X)
            {
                x = 0;
                y++;
            }
            
            if (y == size.Y)
                break;

            Items[x, y] = initialArray[index];
            
            x++;
            index++;
        }
    }

    public T[] Flatten()
    {
        var array = new T[Width * Height];
        var index = 0;
        
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                array[index++] = Items[x, y];
            }
        }

        return array;
    }
}