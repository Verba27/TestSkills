using System;
using UniRx;
using UnityEngine;
using Zenject;

public class GameInputSystem : MonoBehaviour
{
    [Inject] private readonly Camera _camera;
    private readonly Subject<Vector3> _pathSubject = new Subject<Vector3>();

    public IObservable<Vector3> Path => _pathSubject.AsObservable();

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            _pathSubject.OnNext(mousePosition);
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = _camera.ScreenToWorldPoint(touch.position);

            _pathSubject.OnNext(touchPosition);
        }
    }
}