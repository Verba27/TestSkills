using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class CharacterView : MonoBehaviour, ICharacterView
{
    [Inject] private IInGameEventHandler _inGameEventHandler;
    [Inject] private GameSettings _gameSettings;
    private float _maxSpeed = 7f;
    private int _minSpeed = 2;
    private List<Vector3> _path;
    private bool _isMoving = false;
    private float _totalDistanceTraveled = 0f;

    public class Factory : PlaceholderFactory<CharacterView>
    {
    }

    private void Start()
    {
        _maxSpeed = _gameSettings.maxSpeed;
        _minSpeed = _gameSettings.minSpeed;
        _totalDistanceTraveled = _inGameEventHandler.GetSavedDistance();
    }

    public async UniTask Move(List<Vector3> path, CancellationToken token)
    {
        _isMoving = true;

        while (path.Count > 0)
        {
            if (token.IsCancellationRequested)
            {
                _isMoving = false;
                return;
            }

            Vector3 target = path[0];
            path.RemoveAt(0);

            await MoveToTarget(target, token);
        }

        _isMoving = false;
    }

    private async UniTask MoveToTarget(Vector3 target, CancellationToken token)
    {
        while (this != null && Vector3.Distance(transform.position, target) > 0.1f)
        {
            if (token.IsCancellationRequested)
            {
                _isMoving = false;
                return;
            }

            float currentSpeed = CalculateSpeed(target);
            transform.position = Vector3.MoveTowards(transform.position, target, currentSpeed * Time.deltaTime);
            
            _totalDistanceTraveled += currentSpeed * Time.deltaTime;
            _inGameEventHandler.DistancePassed((int)_totalDistanceTraveled);

            await UniTask.Yield();
        }
    }

    private float CalculateSpeed(Vector3 target)
    {
        float distance = Vector3.Distance(transform.position, target);
        float speedMultiplier = Mathf.Clamp01(distance / 3f);
        return Mathf.Max(_minSpeed, _maxSpeed * speedMultiplier);
    }

    public void SetPath(List<Vector3> path, CancellationToken token)
    {
        _path = path;
        if (!_isMoving)
        {
            Move(_path, token).Forget();
        }
    }
}