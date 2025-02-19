using UnityEngine;

public interface IScreenPositionProvider
{
    void GetRandomScreenPosition(out Vector3 position);
}