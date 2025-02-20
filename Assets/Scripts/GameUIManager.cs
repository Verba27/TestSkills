using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

public class GameUIManager : MonoBehaviour
{
    [Inject] private IScoreService _scoreService;
    [Inject] private IDistancePassedService _distancePassedService;

    [SerializeField] private TextMeshProUGUI scoreText = default;
    [SerializeField] private TextMeshProUGUI distanceText = default;

    private readonly CompositeDisposable _disposable = new CompositeDisposable();

    private void Start()
    {
        _scoreService.Score.Subscribe(score => scoreText.text = "Score: " + score).AddTo(_disposable);
        _distancePassedService.Distance.Subscribe(distance => distanceText.text = "Distance: " + distance)
            .AddTo(_disposable);
    }

    private void OnDestroy()
    {
        _disposable.Dispose();
    }
}