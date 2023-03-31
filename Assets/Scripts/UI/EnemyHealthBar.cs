using System.Collections;
using Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EnemyHealthBar : MonoBehaviour
    {
        private const float MaxDelta = 0.005f;
        
        [SerializeField] private Image _imageCurrentHp;

        private IHealth _target;

        private void Awake()
        {
            _target = GetComponentInParent<IHealth>();
        }

        private void OnEnable()
        {
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