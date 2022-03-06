using Onlooker.Monogame;

namespace Onlooker.IntermediateConfiguration.Modules;

public class ModuleRoot
{ // TODO: For termination error messages, add ReportError or something
    private Dictionary<int, IModule> PersistentModules { get; }

    public DirectoryInfo Directory { get; }

    public static ModuleRoot Current => GameManager.Current.ModuleRoot;

    public ModuleRoot(string directory)
    {
        Directory = new DirectoryInfo(directory);
        PersistentModules = new Dictionary<int, IModule>();
    }
    
    public TModule GetModule<TModule>() where TModule : IModule, new()
    {
        var module = new TModule();
        module.Init(this);

        return module;
    }

    public TModule GetPersistentModule<TModule>() where TModule : IModule, new()
    {
        var hash = typeof(TModule).GetHashCode();
        
        if (!PersistentModules.TryGetValue(hash, out var module))
        {
            module = new TModule();
            module.Init(this);
            
            PersistentModules[hash] = module;
        }

        return (TModule)module;
    }

    public void UnloadPersistentModule<TModule>() where TModule : IModule
    {
        PersistentModules.Remove(typeof(TModule).GetHashCode());
    }
}