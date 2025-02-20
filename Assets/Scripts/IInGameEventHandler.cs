using UnityEngine;

public interface IInGameEventHandler
{
    void DistancePassed(int value);
    void SquareGathered(GameObject otherGameObject);
    int GetSavedDistance();
}