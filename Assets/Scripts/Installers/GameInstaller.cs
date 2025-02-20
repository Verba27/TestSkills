using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private SquaresView squaresViewPrefab;

    [SerializeField] private CharacterView characterViewPrefab;

    [SerializeField] private GameInputSystem input;

    public override void InstallBindings()
    {
        Container.Bind<Camera>().FromInstance(Camera.main);
        Container.Bind<GameInputSystem>().FromNewComponentOnNewPrefab(input).AsSingle();
        Container.BindInterfacesAndSelfTo<ScreenPositionProvider>().AsSingle();
        Container.Bind<GameUIManager>().AsSingle();
        
        Container.BindFactory<SquaresView, SquaresView.Factory>()
            .FromComponentInNewPrefab(squaresViewPrefab)
            .UnderTransformGroup("Squares");
        Container.Bind<ISquareView>().To<SquaresView>().AsTransient();
        Container.BindInterfacesAndSelfTo<SquaresSpawner>().AsSingle();
        Container.BindInterfacesAndSelfTo<SquaresRegistry>().AsSingle();

        Container.BindFactory<CharacterView, CharacterView.Factory>()
            .FromComponentInNewPrefab(characterViewPrefab);
        Container.BindInterfacesAndSelfTo<CharacterCircleController>().AsSingle();
    }
}