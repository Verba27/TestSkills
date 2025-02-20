using System;
using UniRx;
using UnityEngine;

public class InGameEventHandler : IInGameEventHandler
{
    private readonly IScoreService _scoreService;
    private readonly IDistancePassedService _distancePassedService;
    private readonly SaveService _saveService;
    private readonly CompositeDisposable _disposable = new CompositeDisposable();

    public InGameEventHandler(IScoreService scoreService,
        IDistancePassedService distancePassedService,
        SaveService saveService)
    {
        _scoreService = scoreService;
        _distancePassedService = distancePassedService;
        _saveService = saveService;
    }

    public void DistancePassed(int value)
    {
        _distancePassedService.Distance.Value = value;
    }

    public void SquareGathered(GameObject otherGameObject)
    {
        _scoreService.Score.Value++;
        _saveService.SaveGameData();
    }

    public int GetSavedDistance()
    {
        return _distancePassedService.Distance.Value;
    }
}