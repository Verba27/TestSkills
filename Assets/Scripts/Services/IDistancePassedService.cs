using UniRx;

public interface IDistancePassedService
{
    ReactiveProperty<int> Distance { get; }
}

public class DistancePassedService : IDistancePassedService
{
    public ReactiveProperty<int> Distance { get; set; } = new ReactiveProperty<int>(0);

    
}