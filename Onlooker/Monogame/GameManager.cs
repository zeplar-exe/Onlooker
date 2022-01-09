using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Onlooker.Monogame;

public class GameManager : Game
{
    private GraphicsDeviceManager Graphics { get; }
    private SpriteBatch SpriteBatch { get; set; }
    private List<GameHook> Hooks { get; }

    public SceneCamera Camera { get; }
    
    public GameManager()
    {
        Graphics = new GraphicsDeviceManager(this);
        Hooks = new List<GameHook>();
        Camera = new SceneCamera();
    }

    public void Hook(GameHook hook)
    {
        hook.SetManager(this);
        Hooks.Add(hook);
    }

    public bool Unhook(GameHook hook) => Hooks.Remove(hook);

    protected override void LoadContent()
    {
        SpriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        foreach (var hook in Hooks)
        {
            hook.OnUpdate(gameTime);
        }
        
        base.Update(gameTime);
    }
    
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        
        foreach (var hook in Hooks)
        {
            hook.OnDraw(gameTime);
        }
        
        base.Draw(gameTime);
    }
}