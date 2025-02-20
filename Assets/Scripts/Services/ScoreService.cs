using UniRx;

public class ScoreService : IScoreService
{
    public ReactiveProperty<int> Score { get; set; } = new ReactiveProperty<int>(0);
}