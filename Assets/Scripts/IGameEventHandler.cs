using System;
using UnityEngine;

namespace DefaultNamespace
{
    public interface IGameEventHandler
    {
        event Action OnGameStart;
        event Action OnGameEnd;
        event Action OnGamePause;
        event Action OnGameResume;
        event Action<GameObject> OnSquareGathered;
        
        void SquareGathered(GameObject otherGameObject);
    }


    public class GameEventHandler : IGameEventHandler
    {
        public event Action OnGameStart;
        public event Action OnGameEnd;
        public event Action OnGamePause;
        public event Action OnGameResume;
        public event Action<GameObject> OnSquareGathered;
        
        private readonly IGameStateModel _gameStateModel;

        public GameEventHandler(IGameStateModel gameStateModel)
        {
            _gameStateModel = gameStateModel;
        }

        public void GameStart()
        {
            OnGameStart?.Invoke();
        }
        
        public void GameEnd()
        {
            OnGameEnd?.Invoke();
        }
        
        public void GamePause()
        {
            OnGamePause?.Invoke();
        }
        
        public void GameResume()
        {
            OnGameResume?.Invoke();
        }
        
        public void SquareGathered(GameObject otherGameObject)
        {
            OnSquareGathered?.Invoke(otherGameObject);
            _gameStateModel.Score.Value++;
        }
    }
}