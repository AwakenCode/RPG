using UnityEngine;
using Weapon;

namespace Enemy
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyRangedAttack : EnemyAttack
    {
        [SerializeField] private EnemyAggro _enemyAggro;
        
        protected override void Attack()
        {
            if(Target == null) return;
            if(_enemyAggro.HasTarget == false) return;
            
            if(Target.IsAlive == false) return;
            
            if(Weapon is FireArm {CurrentAmmoInMagazine: 0} fireArm) 
                fireArm.Reload();
            
            Weapon.Fire();
        }
    }
}