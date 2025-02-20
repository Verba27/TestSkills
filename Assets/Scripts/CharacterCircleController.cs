using System;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;
using Zenject;

public class CharacterCircleController : IInitializable, IDisposable
{
    private readonly GameInputSystem _gameInputSystem;
    private readonly CharacterView.Factory _characterFactory;
    private readonly CompositeDisposable _disposable = new();
    
    private CharacterView _characterView;
    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
    private List<Vector3> _path = new List<Vector3>();
    private ICharacterView _character;


    public CharacterCircleController(GameInputSystem gameInputSystem, CharacterView.Factory characterFactory)
    {
        _gameInputSystem = gameInputSystem;
        _characterFactory = characterFactory;
    }

    public void Initialize()
    {
        _character = _characterFactory.Create();

        var token = _cancellationTokenSource.Token;
        _gameInputSystem.Path.ThrottleFirst(TimeSpan.FromSeconds(0.1))
            .Subscribe(path =>
            {
                if (IsClickOnCircle(path))
                {
                    CancelMovement();
                    return;
                }

                Move(path, token);
            }).AddTo(_disposable);
    }

    public void Dispose()
    {
        _cancellationTokenSource?.Dispose();
        _disposable?.Dispose();
    }

    private void Move(Vector3 path, CancellationToken token)
    {
        _path.Add(path);
        _character.SetPath(_path, token);
    }

    private bool IsClickOnCircle(Vector3 clickPosition)
    {
        Collider2D hitCollider = Physics2D.OverlapPoint(clickPosition);
        return hitCollider != null && hitCollider.CompareTag("Circle");
    }

    private void CancelMovement()
    {
        _path.Clear();
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
        _cancellationTokenSource = new CancellationTokenSource();
    }
}