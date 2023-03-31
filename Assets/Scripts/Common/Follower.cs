using Character;
using Service;
using UnityEngine;
using Zenject;

namespace Common
{
    public class Follower : MonoBehaviour
    {
        [SerializeField] private float _smoothTime;

        private Transform _target;
        private Vector3 _targetPosition;
        private Vector3 _offset;
        private Vector3 _currentVelocity;
        private TaskProvider _taskProvider;

        [Inject]
        private void Construct(TaskProvider taskProvider)
        {
            _taskProvider = taskProvider;
        }

        private void Start()
        {
            Init();
        }

        private void LateUpdate()
        {
            if(_target == null) return;
            
            _targetPosition = _target.position + _offset;
            transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _currentVelocity, _smoothTime);
        }

        private async void Init()
        {
            var player = await _taskProvider.GetTask<Player>();
            _target = player.transform;
            _offset = transform.position - _target.position;
        }
    }
}