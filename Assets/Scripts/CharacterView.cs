using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using UnityEngine;
using Zenject;

public class CharacterView : MonoBehaviour, ICharacterView
{
    [Inject]
    private readonly IGameEventHandler _gameEventHandler;

    public float speed = 2f;
    private CancellationTokenSource _moveCts;
    private List<Vector3> _path;
    private bool _isMoving = false;

    public class Factory : PlaceholderFactory<CharacterView> {}

    void Start()
    {
        _path = new List<Vector3>();
        StartMove();
    }

    private void StartMove()
    {
        _moveCts = new CancellationTokenSource();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main ? Camera.main.ScreenToWorldPoint(Input.mousePosition) : Vector3.zero;
            mousePosition.z = 0;
            
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
            _path.Add(mousePosition);

            if (hitCollider != null && hitCollider.gameObject == gameObject)
            {
                _moveCts.Cancel();
                _moveCts.Dispose();
                _moveCts = new CancellationTokenSource();
                _isMoving = false;
                _path.Clear();
                return;
            }

            if (!_isMoving)
            {
                MoveAlongPath(_moveCts.Token).Forget();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Square"))
        {
            _gameEventHandler.SquareGathered(other.gameObject);
        }
    }

    private async UniTask MoveAlongPath(CancellationToken token)
    {
        _isMoving = true;

        while (_path.Count > 0)
        {
            if (token.IsCancellationRequested)
            {
                _isMoving = false;
                return;
            }

            Vector3 target = _path[0];
            _path.RemoveAt(0);        
            
            while (Vector3.Distance(transform.position, target) > 0.1f)
            {
                if (token.IsCancellationRequested)
                {
                    _isMoving = false;
                    return;
                }

                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                await UniTask.Yield();
            }
        }

        _isMoving = false;
    }

    public void Dispose()
    {
        _moveCts?.Cancel();
        _moveCts?.Dispose();
    }
}