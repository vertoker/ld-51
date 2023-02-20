using Data;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Features.UI.Menu.Views
{
    public class MenuSlidersController : MonoBehaviour
    {
        [SerializeField] private Slider _mouseSensitivity;
        [SerializeField] private Slider _soundVolume;

        [Inject]
        private GlobalState globalState;

        //[SerializeField] private float _standardSensitivity;
        //[SerializeField] private float _standardVolume;
        
        private void Start()
        {
            InitRange();
            InitValues();

            SubscribeToChanges();
        }

        private void InitRange() 
        {
            _mouseSensitivity.minValue = 1;
            _mouseSensitivity.maxValue = GlobalConst.MaxMouseSensitivity;

            _soundVolume.minValue = 0;
            _soundVolume.maxValue = 1;
        }

        private void InitValues() 
        {
            _mouseSensitivity.value = PlayerPrefs.HasKey(GlobalConst.MouseSensitivityPref)
               ? PlayerPrefs.GetFloat(GlobalConst.MouseSensitivityPref)
               : GlobalConst.StandardSensitivity;

            _soundVolume.value = AudioListener.volume;
        }

        private void SubscribeToChanges() 
        {
            _mouseSensitivity
                .OnValueChangedAsObservable()
                .Subscribe(value =>
                {
                    PlayerPrefs.SetFloat(GlobalConst.MouseSensitivityPref, value);
                    globalState.mouseSense = value;
                })
                .AddTo(this);

            _soundVolume
                .OnValueChangedAsObservable()
                .Subscribe(value =>
                {
                    PlayerPrefs.SetFloat(GlobalConst.AudioVolumePref, value);
                    AudioListener.volume = value;
                })
                .AddTo(this);
        }
    }
}