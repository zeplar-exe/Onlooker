using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Onlooker.Common.Args;
using Onlooker.Common.Helpers;
using Onlooker.IntermediateConfiguration;
using Onlooker.Monogame.Controllers;
using Onlooker.Monogame.Graphics;
using Onlooker.Monogame.Logging;

namespace Onlooker.Monogame;

public class GameManager : Game
{
    private GraphicsDeviceManager Graphics { get; }
    private SpriteBatch SpriteBatch { get; set; }
    
    private List<GameController> Controllers { get; }
    
    public static GameManager Current;
    
    public MainController MainController { get; }
    public InputFrameworkController Input { get; }
    public ConfigurationRoot Configuration { get; }
    public AppLogger Logger { get; }
    
    public bool EnableControllersOnHook { get; set; }

    public static GameController? FindControllerById(Guid id) => Current.Controllers.Find(c => c.Id == id);

    public GameManager(string name)
    {
        if (Current != null)
            throw new InvalidOperationException("A game manager has already been created.");
        
        Current = this;

        IsMouseVisible = true;
        Window.AllowUserResizing = false; // TODO: Handle window resizing
        Window.AllowAltF4 = true;
        
        Graphics = new GraphicsDeviceManager(this);
        Configuration = new ConfigurationRoot();
        Controllers = new List<GameController>();

        var logDirectory = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), name, "logs");

        Directory.CreateDirectory(logDirectory);
        
        Logger = new AppLogger(logDirectory);

        MainController = new MainController { Enabled = true };
        Input = new InputFrameworkController { Enabled = true };
        
        HookController(MainController);
        HookController(Input);
        
        Init();
    }

    public async void Init()
    {
        await AsyncHelper.OnInterval(Logger.FlushAsync, TimeSpan.FromSeconds(5), CancellationToken.None);
    }

    public void HookController(GameController controller)
    {
        if (!Controllers.Contains(controller))
        {
            if (EnableControllersOnHook)
                controller.Enabled = true;
            
            Controllers.Add(controller);
            controller.OnStart();
        }
    }
    
    public bool UnhookController(GameController controller) => Controllers.Remove(controller);

    protected override async void OnExiting(object sender, EventArgs args)
    {
        await Logger.DisposeAsync();
    }

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
            
            if (controller.IsLocked())
                continue;

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