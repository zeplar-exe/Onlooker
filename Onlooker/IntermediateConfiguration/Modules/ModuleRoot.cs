namespace Onlooker.IntermediateConfiguration.Modules;

public class ModuleRoot
{
    public DirectoryInfo Directory { get; }

    public ModuleRoot(DirectoryInfo directory)
    {
        Directory = directory;
    }
    
    public TModule GetModule<TModule>() where TModule : IModule, new()
    {
        var module = new TModule();

        module.Init(this);

        return module;
    }
}