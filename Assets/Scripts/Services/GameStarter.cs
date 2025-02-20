using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameStarter : MonoBehaviour
{
    private const string MENU_SCENE_NAME = "MenuScene";
    
    private void Start()
    {
        SceneManager.LoadScene(MENU_SCENE_NAME);
    }
}