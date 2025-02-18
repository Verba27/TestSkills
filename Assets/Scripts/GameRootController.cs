using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class GameRootController: IInitializable
    {
        private readonly IGameStateModel _gameStateModel;
        private readonly CharacterView.Factory _characterFactory;
        private readonly IGameEventHandler _gameEventHandler;
        private CompositeDisposable _disposable;

        public GameRootController(IGameStateModel gameStateModel, CharacterView.Factory characterFactory, IGameEventHandler gameEventHandler)
        {
            _gameStateModel = gameStateModel;
            _characterFactory = characterFactory;
            _gameEventHandler = gameEventHandler;
        }

        private async void GameMenuStart()
        {
            await UniTask.Delay(100);
            _characterFactory.Create();
            _gameStateModel.CurrentGameState.Value = GameState.GamePlay;
            _gameStateModel.Score.Subscribe(score => ScoreSpam());

        }

        private void ScoreSpam()
        {
            Debug.Log("Score: " + _gameStateModel.Score.Value);
        }

        public void Initialize()
        {
            _gameStateModel.CurrentGameState.Value = GameState.GameMenu;
            GameMenuStart();
        }
    }

    public enum GameState
    {
        GameMenu,
        GamePlay,
        GameOver
    }
}