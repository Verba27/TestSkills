using UnityEngine;

public class ScreenPositionProvider : IScreenPositionProvider
{
    private readonly Camera _camera;

    public ScreenPositionProvider(Camera camera)
    {
        _camera = camera;
    }

    public void GetRandomScreenPosition(out Vector3 position)
    {
        Vector3 screenMin = _camera.ViewportToWorldPoint(new Vector3(0, 0, 9));
        Vector3 screenMax = _camera.ViewportToWorldPoint(new Vector3(1, 1, 9));

        float x = Random.Range(screenMin.x, screenMax.x);
        float y = Random.Range(screenMin.y, screenMax.y);
        position = new Vector3(x * 0.9f, y * 0.9f, 0);
    }
}