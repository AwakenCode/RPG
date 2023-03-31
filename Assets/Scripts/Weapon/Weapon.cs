using Data.Weapon;
using UnityEngine;

namespace Weapon
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public abstract class Weapon : MonoBehaviour,  IWeapon
    {
        public WeaponData Data { get; private set; }
        public Collider Collider { get; private set; }
        public Rigidbody Rigidbody { get; private set; }

        protected virtual void Awake()
        {
            Collider = GetComponent<Collider>();
            Rigidbody = GetComponent<Rigidbody>();
        }

        protected void Init(WeaponData data) => Data = data;

        public abstract void Fire();
    }
}