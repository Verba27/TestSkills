using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class SquaresSpawner : IInitializable, IDisposable
    {
        
        private readonly SquaresView.Factory _squaresFactory;
        private readonly IScreenPositionProvider _screenPositionProvider;
        private readonly IGameStateModel _gameStateModel;
        private readonly CompositeDisposable _disposable = new ();
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private float _lastSpawnTime = 0f;

        public SquaresSpawner(SquaresView.Factory squaresFactory, IScreenPositionProvider screenPositionProvider,
            IGameStateModel gameStateModel)
        {
            _squaresFactory = squaresFactory;
            _screenPositionProvider = screenPositionProvider;
            _gameStateModel = gameStateModel;
        }

        void SpawnObject()
        {
            _gameStateModel.CurrentGameState
                .Where(x => x == GameState.GamePlay)
                .Subscribe(_ => GameStateChanged())
                .AddTo(_disposable);
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
                _screenPositionProvider.GetRandomScreenPosition(out var position);
                var square = _squaresFactory.Create();
                square.SetPosition(position);
                _lastSpawnTime = Time.realtimeSinceStartup;
                await UniTask.Delay(2000, cancellationToken: token);
            }
        }

        public void Initialize()
        {
            SpawnObject();
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
            _disposable?.Dispose();
        }
    }
}