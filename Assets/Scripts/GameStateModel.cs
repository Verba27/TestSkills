using System;
using UniRx;

namespace DefaultNamespace
{
    public class GameStateModel: IGameStateModel
    {
        GameStateModel()
        {
            CurrentGameState = new ReactiveProperty<GameState>();
            Score = new ReactiveProperty<int>();
            Distance = new ReactiveProperty<float>();
        }
        public ReactiveProperty<GameState> CurrentGameState  { get; set; }
        public ReactiveProperty<int> Score { get; set; }
        public ReactiveProperty<float> Distance { get; }
        
        
    }


    public interface IGameStateModel
    {
        ReactiveProperty<GameState> CurrentGameState { get;  set; }
        ReactiveProperty<int> Score { get; set; }
        ReactiveProperty<float> Distance { get; }
    }
}