using UnityEngine;
using Weapon.Ammunition;

namespace Data.Weapon
{
    [CreateAssetMenu(menuName = "Weapon/Data/FireArm", order = 61)]
    public class FireArmData : WeaponData
    {
        [field: SerializeField] public float FiringRate { get; private set; }
        [field: SerializeField] public uint MagazineSize { get; private set; }
        [field: SerializeField] public float ReloadTime { get; private set; }
        [field: SerializeField] public AmmunitionType AmmunitionType { get; private set; }
    }
}
