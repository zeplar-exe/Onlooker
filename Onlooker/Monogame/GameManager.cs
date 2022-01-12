using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Onlooker.IntermediateConfiguration;
using Onlooker.Monogame.Controllers;

namespace Onlooker.Monogame;

public class GameManager : Game
{
    private GraphicsDeviceManager Graphics { get; }
    private SpriteBatch SpriteBatch { get; set; }
    
    private List<GameController> Controllers { get; }

    public GameConfig Configuration { get; }
    
    public GameManager()
    {
        Graphics = new GraphicsDeviceManager(this);
        Configuration = new GameConfig();
        Controllers = new List<GameController>();
    }

    public void HookController(GameController controller)
    {
        if (!Controllers.Contains(controller))
            Controllers.Add(controller);
    }
    
    public bool UnhookController(GameController controller) => Controllers.Remove(controller);

    protected override void LoadContent()
    {
        SpriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        foreach (var controller in Controllers)
        {
            controller.Update(gameTime);
        }
        
        base.Update(gameTime);
    }
    
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        var canvas = new DrawCanvas();

        foreach (var controller in Controllers)
        {
            controller.Draw(canvas, gameTime);
        }
        
        base.Draw(gameTime);
    }
}