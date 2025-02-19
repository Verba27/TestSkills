using UniRx;

public interface IGameStateModel
{
    ReactiveProperty<GameState> CurrentGameState { get; set; }
}