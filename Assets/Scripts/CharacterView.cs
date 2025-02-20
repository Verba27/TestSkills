using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class CharacterView : MonoBehaviour, ICharacterView
{
    [Inject] private IInGameEventHandler _inGameEventHandler;
    [SerializeField] private float speed = 5f;
    [SerializeField] private int minSpeed = 2;
    private List<Vector3> _path;
    private bool _isMoving = false;
    private float _totalDistanceTraveled = 0f;

    public class Factory : PlaceholderFactory<CharacterView>
    {
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
        return Mathf.Max(minSpeed, speed * speedMultiplier);
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