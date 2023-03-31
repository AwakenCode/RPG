using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Weapon.Ammunition
{
    [CreateAssetMenu(menuName = "Ammunition/BulletData", order = 61)]
    public class BulletData : ScriptableObject
    {
        [field: SerializeField] public AmmunitionType Type { get; private set; }
        [field: SerializeField] public uint Speed { get; private set; }
        [field: SerializeField] public float LifeTime { get; private set; }
        [field: SerializeField] public uint Damage { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject Template { get; private set; }
    }
}