using UniRx;

public class ScoreService : IScoreService
{
    public ReactiveProperty<int> Score { get; set; }
    public ReactiveProperty<float> Distance { get; }

    private ScoreService()
    {
        Score = new ReactiveProperty<int>();
        Distance = new ReactiveProperty<float>();
    }
}