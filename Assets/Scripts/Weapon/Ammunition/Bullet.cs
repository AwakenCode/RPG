using System;
using Common;
using UnityEngine;

namespace Weapon.Ammunition
{
    public class Bullet : MonoBehaviour, IDamage
    {
        private BulletData _data;
        private float _elapsedTime;

        public event Action<Bullet> Destroyed;
        
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out IHealth health))
                DealDamage(health);
        }

        private void FixedUpdate()
        {
            transform.Translate(Time.deltaTime * _data.Speed * Vector3.forward);
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= _data.LifeTime)
                Destroy();
        }

        public void Init(BulletData data) => _data = data;

        public void DealDamage(IHealth health)
        {
            health.TakeDamage(_data.Damage);
            Destroy();
        }

        private void Destroy()
        {
            _elapsedTime = 0;
            _data = null;
            Destroyed?.Invoke(this);
        }
    }
}
