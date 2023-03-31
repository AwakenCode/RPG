using System;
using Common;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Enemy))]
    [RequireComponent(typeof(EnemyAttack))]
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [field: SerializeField] public float Value { get; private set; }

        private Enemy _self;

        public float MaxValue { get; private set; }
        public bool IsAlive => Value > 0;

        public event Action Changed;

        private void Awake()
        {
            _self = GetComponent<Enemy>();
            MaxValue = Value = _self.Data.Hp;
        }
        
        public void TakeDamage(float damage)
        {
            Value = Mathf.Clamp(Value - damage, 0, _self.Data.Hp);
            Changed?.Invoke();
        }
    }
}