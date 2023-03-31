using System.Collections;
using UnityEngine;

namespace UI
{
    public class LoadingCurtain : MonoBehaviour, IUIElement
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Hide() => StartCoroutine(DoFadeOut());

        public void Show()
        {
            _canvasGroup.alpha = 1;
            gameObject.SetActive(true);
        }
        
        private IEnumerator DoFadeOut()
        {
            var seconds = new WaitForSeconds(0.2f);
            
            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= 0.04f;
                yield return seconds;
            }
            
            gameObject.SetActive(false);
        }
    }
}