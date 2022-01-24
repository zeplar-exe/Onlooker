using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Onlooker.Common;
using Onlooker.ObjectProperties;

namespace Onlooker.Monogame.Controllers.UI;

public class Label : GameController
{
    public RectangleProperty Rect { get; }
    public Texture2D Background { get; set; }
    public StringProperty Text { get; }
    public SpriteFont Font { get; set; }
    public BooleanProperty ScaleToText { get; }
    public int ZIndex { get; }

    public Label(int zIndex)
    {
        Rect = new RectangleProperty(new Rectangle(0, 0, 120, 30));
        Background = TextureHelper.CreateSolidColor(Color.DarkGray);
        Text = new StringProperty("");
        ScaleToText = new BooleanProperty(false);
        Font = GameManager.Current.Configuration.CommonConfig.Fonts.Information!;
        ZIndex = zIndex;
        
        Text.ValueChanged += (_, e) =>
        {
            Console.WriteLine(e.NewValue);
            if (ScaleToText)
            {
                var (x, y) = Font.MeasureString(e.NewValue);

                Rect.Value = new Rectangle(Rect.Value.Location, new Point((int)x, (int)y));
            }
        };
    }

    public override void Update(GameTime time)
    {
        
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        canvas.Draw(ZIndex, new TextureItem(Background, Rect, Color.White));
        canvas.Draw(ZIndex, new StringItem(Text.ToBuilder(), Font, Rect, Color.White));
    }

    public override bool IsLocked()
    {
        return false;
    }
}