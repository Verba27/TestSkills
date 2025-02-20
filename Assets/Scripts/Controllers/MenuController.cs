using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class MenuController : MonoBehaviour
{
    private const string GAME_SCENE_NAME = "GameScene";
    [Inject] private readonly GameStateModel _gameStateModel;
    [Inject] private readonly SaveService _saveService;
    [Inject] private readonly IScoreService _scoreService;
    [Inject] private readonly IDistancePassedService _distancePassedService;
    [SerializeField] private Button startButton = default;

    private void Start()
    {
        _gameStateModel.CurrentGameState.Value = GameState.GameMenu;
        SetSavedData();

        if (startButton == null)
        {
            startButton = GameObject.Find("StartButton").GetComponent<Button>();
        }

        startButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        _gameStateModel.CurrentGameState.Value = GameState.GamePlay;

        SceneManager.LoadScene("GameScene");
    }

    private void SetSavedData()
    {
        var gameData = _saveService.TryGetGameData();
        _scoreService.Score.Value = gameData.Score;
        _distancePassedService.Distance.Value = gameData.Distance;
    }

    private void OnDestroy()
    {
        if (startButton != null)
        {
            startButton.onClick.RemoveListener(StartGame);
        }
    }
}