using UniRx;

public interface ISquaresRegistry
{
    ReactiveCollection<SquaresView> Squares { get; set; }
}