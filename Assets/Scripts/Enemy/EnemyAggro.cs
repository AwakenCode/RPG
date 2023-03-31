using System.Collections;
using Character;
using UnityEngine;

namespace Enemy
{
    public class EnemyAggro : MonoBehaviour
    {
        [SerializeField] private Enemy _self;
        [SerializeField] private EnemyMovement _enemyMovement;
        [SerializeField] private EnemyRotator _enemyRotator;
        
        private IEnumerator _aggroCooldownTimer;
        
        public bool HasTarget { get; private set; }

        private void Awake()
        {
            SwitchFollowOff();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player _))
                StartFollow();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player _))
                StartTimerToSwitchFollowOff();
        }

        private void StartFollow()
        {
            if(HasTarget) return;
            
            StopFollowCoroutine();
            SwitchFollowOn();
        }

        private void StartTimerToSwitchFollowOff()
        {
            _aggroCooldownTimer = SwitchFollowOffAfterCooldown();
            StartCoroutine(_aggroCooldownTimer);
        }

        private void StopFollowCoroutine()
        {
            if(_aggroCooldownTimer == null) return;

            StopCoroutine(_aggroCooldownTimer);
            _aggroCooldownTimer = null;
        }
        
        private IEnumerator SwitchFollowOffAfterCooldown()
        {
            yield return _self.Data.CoolDownWaiter;
            SwitchFollowOff();
        }

        private void SwitchFollowOn()
        {
            HasTarget = true;
            _enemyMovement.enabled = true;
            _enemyRotator.enabled = true;
        }

        private void SwitchFollowOff()
        {
            HasTarget = false;
            _enemyMovement.enabled = false;
            _enemyRotator.enabled = false;
        }
    }
}