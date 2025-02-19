using System;
using UnityEngine;
using Zenject;

public class SquaresView : MonoBehaviour, ISquareView, IInitializable, IDisposable
{
    [Inject] private readonly IGameScoreHandler _gameScoreHandler;
    [Inject] private readonly ISquaresRegistry _squaresRegistry;

    [SerializeField] private SpriteRenderer spriteRenderer = default;

    public class Factory : PlaceholderFactory<SquaresView>
    {
    }

    public void Initialize()
    {
        spriteRenderer.color = Color.green;
        _squaresRegistry.Squares.Add(this);
    }

    public void Dispose()
    {
        _squaresRegistry.Squares.Remove(this);
        Destroy(gameObject);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Circle"))
        {
            _gameScoreHandler.SquareGathered(other.gameObject);
            Dispose();
        }
    }
}