namespace Onlooker.IntermediateConfiguration.Modules;

public interface IModule
{
    public void Init(ModuleRoot root);
    public void Write(ModuleRoot root);

    public static T Get<T>() where T : IModule, new()
    {
        return ModuleRoot.Current.GetModule<T>();
    }
}