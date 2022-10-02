using Data;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Features.UI.Menu.Views
{
    public class MenuSlidersController : MonoBehaviour
    {
        [SerializeField] private Slider _mouseSensitivity;
        [SerializeField] private Slider _soundVolume;
        
        private void Start()
        {
            _mouseSensitivity
                .OnValueChangedAsObservable()
                .Subscribe(value =>
                {
                    PlayerPrefs.SetFloat(GlobalConst.MouseSensitivityPref, value);
                })
                .AddTo(this);

            _soundVolume
                .OnValueChangedAsObservable()
                .Subscribe(value =>
                {
                    PlayerPrefs.SetFloat(GlobalConst.AudioVolumePref, value);
                })
                .AddTo(this);
        }
    }
}