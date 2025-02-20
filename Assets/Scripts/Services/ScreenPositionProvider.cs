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

        float x = Random.Range(screenMin.x * 0.9f, screenMax.x * 0.9f);
        float y = Random.Range(screenMin.y * 0.9f, screenMax.y * 0.9f);
        position = new Vector3(x, y, 0);
    }
}