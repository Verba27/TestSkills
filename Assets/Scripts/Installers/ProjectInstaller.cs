using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameStateModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<ScoreService>().AsSingle();
        Container.BindInterfacesAndSelfTo<DistancePassedService>().AsSingle();
        Container.BindInterfacesAndSelfTo<InGameEventHandler>().AsSingle();
        Container.Bind<SaveService>().AsSingle();
        Container.Bind<GameData>().AsSingle();
    }
}