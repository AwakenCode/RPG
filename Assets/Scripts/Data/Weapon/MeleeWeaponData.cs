using UnityEngine;

namespace Data.Weapon
{
    [CreateAssetMenu(menuName = "Weapon/Data/Melee", order = 61)]
    public class MeleeWeaponData : WeaponData
    {
        [field: SerializeField] public float Cooldown { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
    }
}