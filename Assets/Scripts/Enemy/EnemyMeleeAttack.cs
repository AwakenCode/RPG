using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyMovement), typeof(Enemy))]
    public class EnemyMeleeAttack : EnemyAttack
    {
        private EnemyMovement _enemyMovement;

        protected override void Awake()
        {
            base.Awake();
            _enemyMovement = GetComponent<EnemyMovement>();
        }

        protected override void Attack()
        {
            if(Target == null) return;
            if(_enemyMovement.IsMoving) return;
            
            if(Target.IsAlive)
                Weapon.Fire();     
        }
    }
}