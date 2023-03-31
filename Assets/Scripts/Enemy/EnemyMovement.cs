using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent), typeof(Enemy))]
    public class EnemyMovement : MonoBehaviour
    {
        private const int MinimalDistance = 3;
        
        private Transform _target;
        private Enemy _self;

        public NavMeshAgent Agent { get; private set; }
        public bool IsMoving => Agent.isStopped == false;
        
        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            _self = GetComponent<Enemy>();
        }

        private void Start()
        {
            _target = _self.Target.transform;
        }

        private void Update()
        {
            if(_target == null) return;
            if (Agent.enabled == false) return;
            
            if (IsTargetReached())
            {
                Agent.isStopped = true;
                Agent.destination = transform.position;
                return;
            }
            
            if(Agent.isStopped)
                Agent.isStopped = false;

            Agent.destination = _target.position;
        }
        
        private bool IsTargetReached()
        {
            float distance = Vector3.SqrMagnitude(Agent.transform.position - _target.transform.position);
            return distance < MinimalDistance;
        }
    }
}