using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class SquaresView : MonoBehaviour, IPoolable<IMemoryPool>, ISquareView
    {
        [Inject] private SquaresRegistry _squaresRegistry;
        private IMemoryPool _pool;
        
        [SerializeField] private SpriteRenderer spriteRenderer = default;

        public class Factory : PlaceholderFactory<SquaresView> {}

        public void OnDespawned()
        {
            _squaresRegistry.RemoveSquare(this);
        }

        public void OnSpawned(IMemoryPool pool)
        {
            spriteRenderer.color = Color.green;

            _pool = pool;
            _squaresRegistry.AddSquare(this);
        }

        public void Dispose()
        {
            _pool.Despawn(this);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }

    public interface ISquareView
    {
        void SetPosition(Vector3 position);
    }
}