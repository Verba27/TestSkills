using System;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using Zenject;

public class SquareDestroyer : ISquareDestroyer
{
    private readonly SquaresRegistry _squaresRegistry;
    private readonly IGameEventHandler _gameEventHandler;

    public SquareDestroyer(SquaresRegistry squaresRegistry, IGameEventHandler gameEventHandler)
    {
        _squaresRegistry = squaresRegistry;
        _gameEventHandler = gameEventHandler;
    }

    void CheckForDestroy(GameObject otherGameObject)
    {
        _squaresRegistry.Squares.FirstOrDefault(x => x.gameObject == otherGameObject)?.Dispose();
    }

    public void Dispose()
    {
        _gameEventHandler.OnSquareGathered -= CheckForDestroy;

    }

    public void Initialize()
    {
        _gameEventHandler.OnSquareGathered += CheckForDestroy;

    }
}

public interface ISquareDestroyer : IDisposable, IInitializable
{
}