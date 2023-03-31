using UnityEngine;
using Weapon;

namespace Marker
{
    public class WeaponMarker : Marker
    {
        [field: SerializeField] public WeaponType WeaponType { get; private set; }
    }
}