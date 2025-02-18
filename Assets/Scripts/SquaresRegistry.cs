using System.Collections.Generic;

namespace DefaultNamespace
{
    public class SquaresRegistry
    {
        readonly List<SquaresView> _squares = new List<SquaresView>();
        
        public IEnumerable<SquaresView> Squares
        {
            get { return _squares; }
        }
        
        public void AddSquare(SquaresView square)
        {
            _squares.Add(square);
        }
        
        public void RemoveSquare(SquaresView square)
        {
            _squares.Remove(square);
        }
    }
}