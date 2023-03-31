using Enemy;
using UnityEngine;

namespace Marker
{
    public class EnemyMarker : Marker
    {
        [field: SerializeField] public EnemyType EnemyType { get; private set; }
    }
}