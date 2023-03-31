using UnityEngine;

namespace Data.Player
{
    [CreateAssetMenu(menuName = "Player/Data", order = 61)]
    public class PlayerData : ScriptableObject
    {
        [field: SerializeField] public float Hp { get; private set; }
    }
}