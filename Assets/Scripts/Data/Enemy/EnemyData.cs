using Enemy;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Data.Enemy
{
    [CreateAssetMenu(menuName = "Enemy/Data", order = 61)]
    public class EnemyData : ScriptableObject
    {
        [field: SerializeField] public EnemyType EnemyType { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject Template { get; private set; }
        [field: SerializeField] public uint Hp { get; private set; }
        [field: SerializeField] public uint Reward { get; private set; }
        [SerializeField] private uint _cooldown;

        public WaitForSeconds CoolDownWaiter { get; private set; }
        
        private void Awake()
        {
            CoolDownWaiter = new WaitForSeconds(_cooldown);
        }
    }
}