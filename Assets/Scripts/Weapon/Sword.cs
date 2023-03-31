using System.Collections;
using Common;
using Data.Weapon;
using Service.Data;
using UnityEngine;
using Zenject;

namespace Weapon
{
    [RequireComponent(typeof(Animator))]
    public class Sword : Weapon, IDamage
    {
        private readonly int _swinging = Animator.StringToHash("Swinging");
        private readonly int _isSwinging = Animator.StringToHash("IsSwinging");
        
        private Animator _animator;
        private MeleeWeaponData _data;
        private string _apple;
        private IHealth _lastVictim;

        [Inject]
        private void Construct(IDataProvider dataProvider)
        {
            _data = (MeleeWeaponData) dataProvider.GetWeaponData(WeaponType.Sword);
            Init(_data);
        }
        
        private bool IsSwinging => _animator.GetBool(_isSwinging);
        
        protected override void Awake()
        {
            base.Awake();
            _animator = GetComponent<Animator>();
        }
        
        private void OnTriggerStay(Collider other)
        {
            if (IsSwinging == false) return;
            if (other.TryGetComponent(out IHealth health) == false) return;
            if(health == _lastVictim) return;
            
            DealDamage(health);
        }

        public override void Fire()
        {
            if(IsSwinging) return;

            _animator.SetTrigger(_swinging);
            _animator.SetBool(_isSwinging, true);
            StartCoroutine(WaitSwinging());
        }
        
        public void DealDamage(IHealth health)
        {
            health.TakeDamage(_data.Damage);
            _lastVictim = health;
            StartCoroutine(ClearLastVictim());
        }

        private IEnumerator WaitSwinging()
        {
            yield return new WaitForSeconds(_data.Cooldown);
            _animator.SetBool(_isSwinging, false);
        }

        private IEnumerator ClearLastVictim()
        {
            yield return new WaitForSeconds(_data.Cooldown);
            _lastVictim = null;
        }
    }
}
