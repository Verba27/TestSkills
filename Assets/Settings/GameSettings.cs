using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "SettingsSO/GameSettings")]
public class GameSettings : ScriptableObject
{
    public int maxSpeed;
    public int minSpeed;
    public int maxSquareCount;
    public int squareSpawnInterval;
}