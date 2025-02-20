using UniRx;

public interface IScoreService
{
    ReactiveProperty<int> Score { get; set; }
}