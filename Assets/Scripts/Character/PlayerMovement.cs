using System.Threading.Tasks;
using Service.Input;
using UnityEngine;
using Zenject;

namespace Character
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSmoothTime;
        [SerializeField] private float _gravityMultiplier;

        private CharacterController _characterController;
        private Vector3 _direction;
        private Vector3 _motion;
        private float _velocity;
        private float _currentVelocity;
        private IInputService _input;

        [Inject]
        private void Construct(Task<PlayerInput> input)
        {
            Init(input);
        }
        
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if(_input == null) return;
            
            ApplyGravity();
            Move();
            Rotate();
        }

        private async void Init(Task<PlayerInput> input)
        {
            await input;
            _input = input.Result;
        }
        
        private void Move()
        {
            _direction = new Vector3(_input.Movement.x, _direction.y, _input.Movement.y);
            _motion = _speed * Time.deltaTime * _direction;
            _characterController.Move(_motion);
        }

        private void Rotate()
        {
            if (_direction.x == 0 && _direction.z == 0) return;

            float targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, _rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        private void ApplyGravity()
        {
            if (_characterController.isGrounded && _direction.y < 0)
                _velocity = -1f;
            else
                _velocity += Physics.gravity.y * _gravityMultiplier * Time.deltaTime;

            _direction.y = _velocity;
        }
    }
}