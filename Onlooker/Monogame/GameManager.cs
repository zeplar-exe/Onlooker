using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Onlooker.Common;
using Onlooker.Common.Args;
using Onlooker.Common.Helpers;
using Onlooker.IntermediateConfiguration.Modules;
using Onlooker.IntermediateConfiguration.Modules.Settings;
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
    public ModuleRoot ModuleRoot { get; }

    public int PixelsPerCoordinate { get; set; }
    public bool EnableControllersOnHook { get; set; }
    
    public MouseHandler MouseHandler { get; set; }

    public static GameController? FindControllerById(Guid id) => Current.Controllers.Find(c => c.Id == id);

    public GameManager(string title)
    {
        if (Current != null)
            throw new InvalidOperationException("A game manager has already been created.");
        
        Current = this;

        Window.Title = title;
        IsMouseVisible = true;
        Window.AllowUserResizing = false; // TODO: Handle window resizing
        Window.AllowAltF4 = true;

        PixelsPerCoordinate = 10;
        
        Graphics = new GraphicsDeviceManager(this);
        ModuleRoot = new ModuleRoot(FileSystemHelper.FromWorkingDirectory("configuration"));
        Controllers = new List<GameController>();

        MainController = new MainController { Enabled = true };
        Input = new InputFrameworkController { Enabled = true };
        
        HookController(MainController);
        HookController(Input);

        InitAsync();
    }

    private async void InitAsync()
    {
        var interval = ModuleRoot.GetModule<SettingsModule>().LogSettings.Interval;
        
        await AsyncHelper.OnInterval(AppLogger.FlushAll, TimeSpan.FromMilliseconds(interval), CancellationToken.None);
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

    protected override void OnExiting(object sender, EventArgs args)
    {
        AppLogger.DisposeAll();
        TextureHelper.Dispose();
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
        Time.LastUpdate = gameTime;
        
        UpdateControllers(gameTime);
        HandleMouse(gameTime);
        HandlePossibleResize(gameTime);
        
        base.Update(gameTime);
    }

    private void UpdateControllers(GameTime time)
    {
        foreach (var controller in GetEnabledControllers())
        {
            if (controller.Disposed)
            {
                var args = new CancellationEventArgs();
                
                controller.OnDisposing(args);

                if (!args.Cancel)
                {
                    UnhookController(controller);
                    continue;
                }
            }
            
            if (controller.IsLocked())
                continue;

            controller.Update(time);
        }
    }

    private Point? MouseLockPosition { get; set; }
    
    private void HandleMouse(GameTime time)
    {
        switch (MouseHandler)
        {
            case MouseHandler.Free:
                MouseLockPosition = null;
                break;
            case MouseHandler.Locked:
                MouseLockPosition ??= Mouse.GetState().Position;
                Mouse.SetPosition(MouseLockPosition.Value.X, MouseLockPosition.Value.Y);
                break;
            case MouseHandler.LockedCenter:
                MouseLockPosition = new Point(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
                Mouse.SetPosition(MouseLockPosition.Value.X, MouseLockPosition.Value.Y);
                break;
        }
    }
    
    private Point PreviousSize { get; set; }

    private void HandlePossibleResize(GameTime time)
    {
        if (Window.ClientBounds.Size == PreviousSize) 
            return;

        PreviousSize = Window.ClientBounds.Size;
    }
    
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        var canvas = new DrawCanvas();

        foreach (var controller in GetEnabledControllers())
        {
            controller.Draw(canvas, gameTime);
        }

        if (canvas.Items.Count > 0)
        {
            SpriteBatch.Begin();

            foreach (var pair in canvas.Items.OrderBy(c => c.Key))
            {
                foreach (var graphic in pair.Value)
                {
                    graphic.Draw(SpriteBatch);
                }
            }

            SpriteBatch.End();
        }
        
        base.Draw(gameTime);
    }

    private IEnumerable<GameController> GetEnabledControllers()
    {
        return Controllers.Where(c => c.Enabled).ToArray();
    }
}