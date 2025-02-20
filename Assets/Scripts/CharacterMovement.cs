using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CharacterMovement
{
    private readonly Transform _transform;
    private readonly float _maxSpeed;
    private readonly int _minSpeed;
    public float CurrentSpeed { get; set; }


    public CharacterMovement(Transform transform, float maxSpeed, int minSpeed)
    {
        _transform = transform;
        _maxSpeed = maxSpeed;
        _minSpeed = minSpeed;
    }

    public async UniTask Move(List<Vector3> path, CancellationToken token)
    {
        while (path.Count > 0)
        {
            if (token.IsCancellationRequested) return;
            
            Vector3 target = path[0];
            path.RemoveAt(0);

            await MoveToTarget(target, token);
        }
    }

    private async UniTask MoveToTarget(Vector3 target, CancellationToken token)
    {
        while (_transform != null && Vector3.Distance(_transform.position, target) > 0.1f)
        {
            if (token.IsCancellationRequested) return;
            
            CurrentSpeed = CalculateSpeed(target);
            _transform.position = Vector3.MoveTowards(_transform.position, target, CurrentSpeed * Time.deltaTime);
            await UniTask.Yield();
        }
    }

    private float CalculateSpeed(Vector3 target)
    {
        float distance = Vector3.Distance(_transform.position, target);
        float speedMultiplier = Mathf.Clamp01(distance / 3f);
        return Mathf.Max(_minSpeed, _maxSpeed * speedMultiplier);
    }
}