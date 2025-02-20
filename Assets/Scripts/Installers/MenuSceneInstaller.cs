using Zenject;

public class MenuSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<MenuController>().FromComponentInHierarchy().AsSingle();
    }
}