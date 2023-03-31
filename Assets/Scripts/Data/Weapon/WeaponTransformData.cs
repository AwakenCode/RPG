using UnityEngine;
using Weapon;

namespace Data.Weapon
{
    [CreateAssetMenu(menuName = "Weapon/TransformData", order = 61)]
    public class WeaponTransformData : ScriptableObject
    {
        [field: SerializeField] public WeaponType Type { get; private set; }
        [field: SerializeField] public Vector3 Position { get; private set; }
        [field: SerializeField] public Quaternion Rotation { get; private set; }
    }
}