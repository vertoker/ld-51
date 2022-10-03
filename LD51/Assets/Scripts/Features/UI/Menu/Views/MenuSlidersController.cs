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

        [SerializeField] private float _standardSensitivity;
        [SerializeField] private float _standardVolume;
        
        private void Start()
        {
            _mouseSensitivity.value = PlayerPrefs.HasKey(GlobalConst.MouseSensitivityPref)
                ? PlayerPrefs.GetFloat(GlobalConst.MouseSensitivityPref)
                : _standardSensitivity;
            
            _soundVolume.value = PlayerPrefs.HasKey(GlobalConst.AudioVolumePref)
                ? PlayerPrefs.GetFloat(GlobalConst.AudioVolumePref)
                : _standardVolume;

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