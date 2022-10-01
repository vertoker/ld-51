using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Features.UI.Menu.Views
{
    public class MenuSettingsBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Fade settings")]
        [SerializeField] private float fadeOutSpeed;
        [SerializeField] private float fadeInSpeed;
        [SerializeField] private CanvasGroup targetPadding;
        private bool _fadeOption;
        private SignalBus _signalBus;
        
        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _fadeOption = true;
            StartCoroutine(FadeOut());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _fadeOption = false;
            StartCoroutine(FadeIn());
        }

        
        private IEnumerator FadeIn()
        {
            while (targetPadding.alpha > 0 && !_fadeOption)
            {
                targetPadding.alpha = Mathf.MoveTowards(targetPadding.alpha, 0, fadeInSpeed * Time.deltaTime);
                yield return null;
            }
        }
        
        private IEnumerator FadeOut()
        {
            while (targetPadding.alpha < 1 && _fadeOption)
            {
                targetPadding.alpha = Mathf.MoveTowards(targetPadding.alpha, 1, fadeOutSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
