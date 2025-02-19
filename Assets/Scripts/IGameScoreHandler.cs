using UnityEngine;

public interface IGameScoreHandler
{
    void DistancePassed(int value);
    void SquareGathered(GameObject otherGameObject);
}