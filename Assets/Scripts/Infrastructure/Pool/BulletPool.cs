using UnityEngine;
using Weapon.Ammunition;

namespace Infrastructure.Pool
{
    public class BulletPool : MonoBehaviour, IObjectPool<Bullet>
    {
        [SerializeField] private Transform _container;

        private ObjectPool<Bullet> _pool;
        
        private void Awake()
        {
            _pool = new ObjectPool<Bullet>(OnDestroyed, OnGot, OnReleased);
        }

        public int InactiveCount => _pool.CountInactive;

        public Bullet Get() => _pool.Get();

        public void Release(Bullet entity) => _pool.Release(entity);

        public void Dispose() => _pool.Dispose();

        private void OnReleased(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
            bullet.transform.SetParent(_container);
        }

        private void OnDestroyed(Bullet bullet) => Object.Destroy(bullet.gameObject);

        private void OnGot(Bullet bullet) => bullet.gameObject.SetActive(true);
    }
}
