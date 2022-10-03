using Configs;
using Data;
using Features.Character.Models;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.Character.Controllers
{
    public class CharacterSoundController : MonoBehaviour
    {
        [SerializeField] private AudioSource _rightFootSource;
        [SerializeField] private AudioSource _leftFootSource;
        [SerializeField] private AudioSource _jumpSource;
        [SerializeField] private AudioSource _dashSource;
        
        private AudioStateData _footsteps;
        private AudioStateData _jump;
        private AudioStateData _dash;
        
        private CharacterModel _characterModel;

        [Inject]
        public void Construct(SoundConfig soundConfig)
        {
            _footsteps = new AudioStateData(soundConfig.GetSoundsByType(SoundType.Footsteps));
            _jump = new AudioStateData(soundConfig.GetSoundsByType(SoundType.Jump));
            _dash = new AudioStateData(soundConfig.GetSoundsByType(SoundType.Dash));
        }
        
        public void SetCharacter(CharacterModel characterModel)
        {
            _characterModel = characterModel;
        }

        private void Start()
        {
            var freeSource = _rightFootSource;
            
            Observable
                .EveryUpdate()
                .Where(_ => _characterModel.IsMoving.Value &&
                            _characterModel.Grounded.Value)
                .Subscribe(_ =>
                {
                    if (_rightFootSource.isPlaying && !_leftFootSource.isPlaying)
                        freeSource = _leftFootSource;
                    else if (_leftFootSource.isPlaying && !_rightFootSource.isPlaying)
                        freeSource = _rightFootSource;
                    else if (_leftFootSource.isPlaying && _rightFootSource.isPlaying) return;

                    freeSource.volume = PlayerPrefs.GetFloat(GlobalConst.AudioVolumePref);
                    freeSource.clip = _footsteps.GetNext();
                    freeSource.pitch = _characterModel.Speed > 10f 
                        ? _characterModel.Speed / 10f
                        : 1f;
                    
                    freeSource.Play();
                })
                .AddTo(this);
        }

        public void PlayDash()
        {
            _dashSource.volume = PlayerPrefs.GetFloat(GlobalConst.AudioVolumePref);
            _dashSource.clip = _dash.GetRandom();
            _dashSource.Play();
        }

        public void PlayJump()
        {
            _jumpSource.volume = PlayerPrefs.GetFloat(GlobalConst.AudioVolumePref);
            _jumpSource.clip = _jump.GetRandom();
            _jumpSource.Play();
        }
    }
}