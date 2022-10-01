using System.Collections;
using Features.UI.Menu.Messages;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Features.UI.Menu.Views
{
    public class MenuButtonBehaviour : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Action")]
        [SerializeField] private string property;
        
        [Header("Fade settings")]
        [SerializeField] private float fadeOutSpeed;
        [SerializeField] private float fadeInSpeed;
        [SerializeField] private CanvasGroup targetPadding;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField]
        private bool _fadeOption;
        
        [Header("Faded material settings")] 
        [SerializeField] private string emissionProperty = "_EmissionColor";
        
        [Header("Click sound")]
        [SerializeField] private AudioSource audioSource;
        
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            audioSource.Play();
            StartCoroutine(ButtonFadeIn());
            _signalBus.TryFire(new MenuBehaviourSignals.ButtonPressed(property));
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

        private IEnumerator ButtonFadeIn()
        {
            var resultColor = Color.white;
            var loop = true;
            while (loop)
            {
                if (meshRenderer.material.GetColor(emissionProperty) == Color.white)
                {
                    yield return ButtonFadeOut();
                    loop = false;
                }
                else
                {
                    meshRenderer.material.SetColor(emissionProperty, 
                        Color.Lerp(meshRenderer.material.GetColor(emissionProperty), resultColor, 4f * Time.deltaTime));
                    yield return null;
                }
            }
        }

        private IEnumerator ButtonFadeOut()
        {
            var resultColor = Color.black;
            while (!meshRenderer.material.GetColor(emissionProperty).Equals(resultColor))
            {
                meshRenderer.material.SetColor(emissionProperty, 
                    Color.Lerp(meshRenderer.material.GetColor(emissionProperty), resultColor, 4f * Time.deltaTime));
                yield return null;
            }
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
