using UniRx;

public class GameStateModel : IGameStateModel
{
    public ReactiveProperty<GameState> CurrentGameState { get; set; }

    GameStateModel()
    {
        CurrentGameState = new ReactiveProperty<GameState>();
    }
}

public enum GameState
{
    GameMenu,
    GamePlay,
    GameOver
}