using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Onlooker.Common.Args;
using Onlooker.IntermediateConfiguration;
using Onlooker.Monogame.Controllers;

namespace Onlooker.Monogame;

public class GameManager : Game
{
    private GraphicsDeviceManager Graphics { get; }
    private SpriteBatch SpriteBatch { get; set; }
    
    private List<GameController> Controllers { get; }
    
    public static GameManager Current;
    
    public MainController MainController { get; }
    public AbsoluteConfiguration Configuration { get; }

    public static GameController? FindControllerById(Guid id) => Current.Controllers.Find(c => c.Id == id);

    public GameManager()
    {
        if (Current != null)
            throw new InvalidOperationException("A game manager has already been created.");
        
        Current = this;
        
        Graphics = new GraphicsDeviceManager(this);
        Configuration = new AbsoluteConfiguration();
        Controllers = new List<GameController>();

        MainController = new MainController { Enabled = true };
        
        HookController(MainController);
    }

    public void HookController(GameController controller)
    {
        if (!Controllers.Contains(controller))
        {
            Controllers.Add(controller);
            controller.OnStart();
        }
    }
    
    public bool UnhookController(GameController controller) => Controllers.Remove(controller);

    protected override void LoadContent()
    {
        SpriteBatch = new SpriteBatch(GraphicsDevice);

        foreach (var controller in GetEnabledControllers())
        {
            controller.OnContentLoad();
        }
    }

    protected override void Update(GameTime gameTime)
    {
        foreach (var controller in GetEnabledControllers())
        {
            if (controller.Disposed)
            {
                var args = new CancellationEventArgs();
                
                controller.OnDisposing(args);
                
                if (!args.Cancel)
                    Controllers.Remove(controller);
            }

            controller.Update(gameTime);
        }
        
        base.Update(gameTime);
    }
    
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        var canvas = new DrawCanvas();

        foreach (var controller in GetEnabledControllers())
        {
            controller.Draw(canvas, gameTime);
        }
        
        SpriteBatch.Begin();

        foreach (var pair in canvas.Items.OrderBy(c => c.Key))
        {
            foreach (var item in pair.Value)
            {
                item.Draw(SpriteBatch);
            }
        }
        
        SpriteBatch.End();
        
        base.Draw(gameTime);
    }

    private IEnumerable<GameController> GetEnabledControllers()
    {
        return Controllers.Where(c => c.Enabled).ToArray();
    }
}