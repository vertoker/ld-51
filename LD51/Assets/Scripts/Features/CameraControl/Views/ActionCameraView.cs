using System.Collections;
using System.Threading.Tasks;
using Features.CameraControl.Configs;
using Features.CameraControl.Messages;
using Features.UI.Menu.Messages;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.CameraControl.Views
{
    public class ActionCameraView : MonoBehaviour
    {
        [SerializeField] private Transform settingsMenuPresenter;
        [SerializeField] private Transform mainMenuPresenter;
        [SerializeField] private Transform infoMenuPresenter;
        [SerializeField] private Transform creditsMenuPresenter;
        
        private CameraBehaviourConfig _behaviourConfig;
        private SignalBus _signalBus;
        
        private bool _seekTarget;
        private Transform _cameraTarget;
        private EntityViewOption _viewOption;
        private Vector3 _objectOffset;
        private Vector2 _lookRotation;
        
        [Inject]
        public void Construct(CameraBehaviourConfig behaviourConfig,
            SignalBus signalBus)
        {
            _signalBus = signalBus;
            _behaviourConfig = behaviourConfig;
            
            _signalBus
                .GetStream<CameraControlSignals.CameraToMenu>()
                .Subscribe(button => CatchMenuTranslation(button.TranslateOption));
            
            _signalBus
                .GetStream<CameraControlSignals.CameraToObject>()
                .Subscribe(target => CatchObjectTranslation(target.ViewOption, target.Transform, target.Offset, target.PresenterMode));
        }

        private  void CatchMenuTranslation(MenuAction translation)
        {
            _seekTarget = false;
            var targetTranslation = translation switch
            {
                MenuAction.MainMenu => mainMenuPresenter,
                MenuAction.Settings => settingsMenuPresenter,
                MenuAction.Info => infoMenuPresenter,
                MenuAction.Credits => creditsMenuPresenter,
                _ => transform
            };
            _cameraTarget = targetTranslation;
            StartCoroutine( TranslateToMenuOption(targetTranslation));
        }
        
        private  void CatchObjectTranslation(EntityViewOption viewOption, Transform target, Vector3 objectOffset, bool presenterMode)
        {
            transform.SetParent(target);
            _seekTarget = false;
            _cameraTarget = target;
            _viewOption = viewOption;
            _objectOffset = objectOffset;
            if (presenterMode) 
                StartCoroutine( TranslateToObject(target, 5.0f));
        }

        //private async Task TranslateToObject(Transform objectTransform, float presentationTime)
        //{
        //    var estimatedTime = 0f;
            
        //    while (transform.position != objectTransform.position - _objectOffset && objectTransform == _cameraTarget)
        //    {
        //        transform.position = Vector3.Lerp(transform.position, objectTransform.position + _objectOffset, _behaviourConfig.CameraTranslateSpeed * Time.deltaTime);
        //        transform.LookAt(objectTransform);
        //        await Task.Yield();
        //    }
            
        //    while (estimatedTime < presentationTime)
        //    {
        //        estimatedTime += Time.deltaTime;
        //        transform.RotateAround(objectTransform.position, Vector3.up, 10f * Time.deltaTime);
        //        await Task.Yield();
        //    }

        //    _seekTarget = true;
        //}

        private IEnumerator TranslateToObject
            (Transform objectTransform, float presentationTime)
        {
            var estimatedTime = 0f;

            while (transform.position != objectTransform.position - _objectOffset && objectTransform == _cameraTarget)
            {
                transform.position = Vector3.Lerp(transform.position, objectTransform.position + _objectOffset, _behaviourConfig.CameraTranslateSpeed * Time.deltaTime);
                transform.LookAt(objectTransform);
                yield return null;
            }

            while (estimatedTime < presentationTime)
            {
                estimatedTime += Time.deltaTime;
                transform.RotateAround(objectTransform.position, Vector3.up, 10f * Time.deltaTime);
                yield return null;
            }

            _seekTarget = true;
        }

        private IEnumerator TranslateToMenuOption(Transform presenter)
        {
            var accuracy = 0.999999f;
            while (transform && transform.position != presenter.position && 
                   Mathf.Abs(Quaternion.Dot(transform.rotation,presenter.rotation)) < accuracy &&
                   presenter == _cameraTarget && transform)
            {
                transform.position = Vector3.Lerp(transform.position, presenter.position, _behaviourConfig.CameraTranslateSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, presenter.rotation, _behaviourConfig.CameraRotateSpeed * Time.deltaTime);
                yield return null;
            }
        }

        private void Update()
        {
            if (!_seekTarget) return;
            switch (_viewOption)
            { 
                //TODO: apply rotation first, then grab rotation from camera view to controllable rotation. Need to split views on head, body, camera view
                case EntityViewOption.First:
                {
                    //Camera behaviour in first person view
                    break;
                }
                case EntityViewOption.Third:
                {
                    //Camera behaviour in third person view
                    break;
                }
                case EntityViewOption.ActorFocused:
                {
                    transform.position = Vector3.Lerp(transform.position, _cameraTarget.position, _behaviourConfig.CameraTranslateSpeed * Time.deltaTime);
                    transform.LookAt(_cameraTarget);
                    break;
                }
            }

        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}