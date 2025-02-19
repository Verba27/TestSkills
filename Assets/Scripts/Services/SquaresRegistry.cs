using UniRx;

public class SquaresRegistry : ISquaresRegistry
{
    public ReactiveCollection<SquaresView> Squares { get; set; } = new ReactiveCollection<SquaresView>();
}