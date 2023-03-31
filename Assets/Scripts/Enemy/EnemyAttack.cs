using System;
using Common;
using Data.Weapon;
using UnityEngine;

namespace Enemy
{
    public abstract class EnemyAttack : MonoBehaviour
    {
        [SerializeField] protected Weapon.Weapon Weapon;

        private WeaponTransformData _weaponTransformData;
        private Enemy _self;

        protected IHealth Target;
        
        private void OnValidate()
        {
            if (Weapon == null)
                throw new ArgumentNullException(nameof(Weapon), "weapon is unassigned");
        }
        
        protected virtual void Awake()
        {
            _self = GetComponent<Enemy>();
        }

        private void Start()
        {
            Target = _self.Target.Health;
            PrepareWeapon();
        }

        private void Update()
        {
            Attack();
        }

        public void DropWeapon()
        {
            Weapon.Rigidbody.isKinematic = false;
            Weapon.Rigidbody.useGravity = true;
        }
        
        protected abstract void Attack();
        
        private void PrepareWeapon()
        {
            _weaponTransformData = _self.DataProvider.GetWeaponTransformData(Weapon.Data.Type);
            Weapon.Collider.isTrigger = true;
            Weapon.Rigidbody.isKinematic = true;
            Weapon.Rigidbody.useGravity = false;
            Weapon.transform.localPosition = _weaponTransformData.Position;
            Weapon.transform.localRotation = _weaponTransformData.Rotation;
        }
    }
}