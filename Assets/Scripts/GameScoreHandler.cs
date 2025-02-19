using UnityEngine;

public class GameScoreHandler : IGameScoreHandler
{
    private readonly IScoreService _gameStateModel;

    public GameScoreHandler(IScoreService gameStateModel)
    {
        _gameStateModel = gameStateModel;
    }

    public void DistancePassed(int value)
    {
        _gameStateModel.Distance.Value += value;
    }

    public void SquareGathered(GameObject otherGameObject)
    {
        _gameStateModel.Score.Value++;
        Debug.Log("Score: " + _gameStateModel.Score.Value);
    }
}