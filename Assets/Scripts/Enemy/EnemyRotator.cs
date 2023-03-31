using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyRotator : MonoBehaviour
    {
        [SerializeField] private float _maxDegreesDelta;
        
        private Transform _target;
        private Vector3 _positionToLookAt;
        private Enemy _self;

        private void Awake()
        {
            _self = GetComponent<Enemy>();
        }

        private void Start()
        {
            _target = _self.Target.transform;
        }

        private void Update()
        {
            if(_target == null) return;
            
            RotateTowardsTarget();
        }

        private void RotateTowardsTarget()
        {
            UpdatePositionToLookAt();
            
            var lookRotation = Quaternion.LookRotation(_positionToLookAt);
            transform.rotation = Quaternion.RotateTowards( transform.rotation, lookRotation, _maxDegreesDelta);
        }

        private void UpdatePositionToLookAt()
        {
            var deltaPosition = _target.position - transform.position;
            _positionToLookAt = new Vector3(deltaPosition.x, transform.position.y, deltaPosition.z);
        }
    }
}