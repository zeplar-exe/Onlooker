namespace Onlooker.IntermediateConfiguration.Modules;

public interface IModule
{
    public void Init(ModuleRoot root);
    public void Write(ModuleRoot root);
}