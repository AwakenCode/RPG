using System;
using Common;
using Data.Player;
using Service.Data;
using UnityEngine;
using Zenject;

namespace Character
{
    public class PlayerHealth : MonoBehaviour, IHealth
    {
        [field: SerializeField] public float Value { get; private set; }

        private PlayerData _data;

        [Inject]
        private void Construct(IDataProvider dataProvider)
        {
            _data = dataProvider.GetPlayerData(); 
            MaxValue = Value = _data.Hp;
        }

        public bool IsAlive => Value > 0;
        public float MaxValue { get; private set; }

        public event Action Changed;
        
        public void TakeDamage(float damage)
        {
            Value = Mathf.Clamp(Value - damage, 0, _data.Hp);
            Changed?.Invoke();
        }
    }
}