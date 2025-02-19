using UniRx;
using Zenject;

public class GameRootController : IInitializable
{
    private readonly IGameStateModel _gameStateModel;
    private CompositeDisposable _disposable;
    private CharacterView _characterView;

    public GameRootController(IGameStateModel gameStateModel)
    {
        _gameStateModel = gameStateModel;
    }

    public void Initialize()
    {
        _gameStateModel.CurrentGameState.Value = GameState.GameMenu;
        GameMenuStart();
    }

    private void GameMenuStart()
    {
        _gameStateModel.CurrentGameState.Value = GameState.GamePlay;
    }
}