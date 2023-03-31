using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyHealth),typeof(EnemyAttack))]
    [RequireComponent(typeof(EnemyMovement), typeof(EnemyRotator))]
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyAggro _enemyAggro;
        
        private EnemyHealth _enemyHealth;
        private EnemyAttack _enemyAttack;
        private EnemyMovement _enemyMovement;
        private EnemyRotator _enemyRotator;

        private void Awake()
        {
            _enemyHealth = GetComponent<EnemyHealth>();
            _enemyAttack = GetComponent<EnemyAttack>();
            _enemyMovement = GetComponent<EnemyMovement>();
            _enemyRotator = GetComponent<EnemyRotator>();
        }

        private void OnEnable()
        {
            _enemyHealth.Changed += OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            if(_enemyHealth.IsAlive) return;

            _enemyMovement.enabled = false;
            _enemyMovement.Agent.enabled = false;
            _enemyAttack.enabled = false;
            _enemyRotator.enabled = false;
            _enemyAggro.enabled = false;
            
            _enemyAttack.DropWeapon();
            Destroy(gameObject, 3);
        }
    }
}