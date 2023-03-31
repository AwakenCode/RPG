using UnityEngine;
using UnityEngine.AddressableAssets;
using Weapon;

namespace Data.Weapon
{
    public abstract class WeaponData : ScriptableObject
    {
        [field: SerializeField] public WeaponType Type { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject Template { get; private set; }
    }
}
