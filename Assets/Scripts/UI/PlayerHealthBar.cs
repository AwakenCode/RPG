using System.Collections;
using Character;
using Common;
using Service;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class PlayerHealthBar : MonoBehaviour
    {
        private const float MaxDelta = 0.005f;
        
        [SerializeField] private Image _imageCurrentHp;

        private IHealth _target;
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

        private async void Init()
        {
            var player = await _taskProvider.GetTask<Player>();
            _target = player.Health;
            _target.Changed += OnValueChanged;
        }

        private void OnValueChanged()
        {
            StartCoroutine(ChangeBarValue(_target.Value, _target.MaxValue));
        }

        private IEnumerator ChangeBarValue(float currentValue, float maxValue)
        {
            float targetValue = currentValue / maxValue;
            
            while (_imageCurrentHp.fillAmount != targetValue)
            {
                _imageCurrentHp.fillAmount = Mathf.MoveTowards(_imageCurrentHp.fillAmount, targetValue, MaxDelta);
                yield return null;
            }
        }
    }
}