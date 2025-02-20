using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class CharacterView : MonoBehaviour, ICharacterView
{
    [Inject] private IInGameEventHandler _inGameEventHandler;
    [Inject] private GameSettings _gameSettings;

    private CharacterMovement _characterMovement;
    private DistanceTracker _distanceTracker;
    private bool _isMoving = false;

    public class Factory : PlaceholderFactory<CharacterView>
    {
    }

    private void Start()
    {
        _characterMovement = new CharacterMovement(transform, _gameSettings.maxSpeed, _gameSettings.minSpeed);
        _distanceTracker = new DistanceTracker(_inGameEventHandler);
    }

    public void SetPath(List<Vector3> path, CancellationToken token)
    {
        if (!_isMoving)
        {
            Move(path, token).Forget();
        }
    }

    private async UniTask Move(List<Vector3> path, CancellationToken token)
    {
        _isMoving = true;

        await _characterMovement.Move(path, token);

        _isMoving = false;
    }

    private void Update()
    {
        if (_isMoving)
        {
            float deltaDistance = _characterMovement.CurrentSpeed * Time.deltaTime;
            _distanceTracker.UpdateDistance(deltaDistance);
        }
    }
}

