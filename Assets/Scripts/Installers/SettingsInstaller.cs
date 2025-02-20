using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SettingsInstaller", menuName = "Installers/SettingsInstaller")]
public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
{
    public GameSettings gameSettings;
    public override void InstallBindings()
    {
        Container.BindInstance(gameSettings).AsSingle();
    }
}