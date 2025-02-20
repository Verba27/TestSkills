using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using Zenject;

public class SquaresSpawner : IInitializable, IDisposable
{
    private readonly GameSettings _gameSettings;
    private readonly SquaresView.Factory _squaresFactory;
    private readonly ISquaresRegistry _squaresRegistry;
    private readonly IScreenPositionProvider _screenPositionProvider;
    private readonly IGameStateModel _gameStateModel;
    private readonly CompositeDisposable _disposable = new();
    private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    public SquaresSpawner(SquaresView.Factory squaresFactory,
        IScreenPositionProvider screenPositionProvider,
        IGameStateModel gameStateModel,
        ISquaresRegistry squaresRegistry,
        GameSettings gameSettings)
    {
        _squaresFactory = squaresFactory;
        _screenPositionProvider = screenPositionProvider;
        _gameStateModel = gameStateModel;
        _squaresRegistry = squaresRegistry;
        _gameSettings = gameSettings;
    }

    public void Initialize()
    {
        _gameStateModel.CurrentGameState
            .Where(x => x == GameState.GamePlay)
            .Subscribe(_ => GameStateChanged())
            .AddTo(_disposable);
        _squaresRegistry.Squares.ObserveCountChanged()
            .Subscribe().AddTo(_disposable);
    }

    public void Dispose()
    {
        _cancellationTokenSource?.Dispose();
        _disposable?.Dispose();
    }

    private void GameStateChanged()
    {
        var token = _cancellationTokenSource.Token;
        AsyncSpawn(token).Forget();
    }

    private async UniTask AsyncSpawn(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            if (_squaresRegistry.Squares.Count >= _gameSettings.maxSquareCount)
            {
                await UniTask.Delay(500, cancellationToken: token);
                continue;
            }

            ISquareView square = _squaresFactory.Create();
            _screenPositionProvider.GetRandomScreenPosition(out var position);
            square.SetPosition(position);
            square.Initialize();
            await UniTask.Delay(_gameSettings.squareSpawnInterval * 1000, cancellationToken: token);
        }
    }
}