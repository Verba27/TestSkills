public class DistanceTracker
{
    private readonly IInGameEventHandler _inGameEventHandler;
    private float _totalDistanceTraveled;

    public DistanceTracker(IInGameEventHandler inGameEventHandler)
    {
        _inGameEventHandler = inGameEventHandler;
        _totalDistanceTraveled = inGameEventHandler.GetSavedDistance();
    }

    public void UpdateDistance(float deltaDistance)
    {
        _totalDistanceTraveled += deltaDistance;
        _inGameEventHandler.DistancePassed((int)_totalDistanceTraveled);
    }
}