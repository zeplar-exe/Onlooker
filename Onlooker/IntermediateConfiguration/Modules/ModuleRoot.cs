using Onlooker.Monogame;

namespace Onlooker.IntermediateConfiguration.Modules;

public class ModuleRoot
{
    private Dictionary<int, IModule> LoadedModules { get; }

    public DirectoryInfo Directory { get; }

    public static ModuleRoot Current => GameManager.Current.ModuleRoot;

    public ModuleRoot(string directory)
    {
        Directory = new DirectoryInfo(directory);
        LoadedModules = new Dictionary<int, IModule>();
    }

    public void UpdateAll()
    {
        foreach (var module in LoadedModules)
        {
            module.Value.Init(this);
        }
    }
    
    public TModule GetModule<TModule>() where TModule : IModule, new()
    {
        var hash = typeof(TModule).GetHashCode();
    
        if (!LoadedModules.TryGetValue(hash, out var module))
        {
            module = new TModule();
            module.Init(this);
        
            LoadedModules[hash] = module;
        }

        return (TModule)module;
    }

    public bool UnloadModule<TModule>() where TModule : IModule
    {
        return LoadedModules.Remove(typeof(TModule).GetHashCode());
    }
}