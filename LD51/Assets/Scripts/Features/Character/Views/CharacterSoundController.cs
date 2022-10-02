using Configs;
using Data;
using Features.Character.Models;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.Character.Views
{
    public class CharacterSoundController : MonoBehaviour
    {
        [SerializeField] private AudioSource _footstepSource;
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
            Observable
                .EveryUpdate()
                .Where(_ => _characterModel.IsMoving.Value &&
                            _characterModel.Grounded.Value &&
                            !_footstepSource.isPlaying)
                .Subscribe(_ =>
                {
                    _footstepSource.clip = _footsteps.GetNext();
                    _footstepSource.Play();
                })
                .AddTo(this);

            MessageBroker
                .Default
                .Receive<CharacterModel.Jump>()
                .Where(_ => !_jumpSource.isPlaying)
                .Subscribe(_ =>
                {
                    _jumpSource.clip = _jump.GetNext();
                    _jumpSource.Play();
                })
                .AddTo(this);

            MessageBroker
                .Default
                .Receive<CharacterModel.Dash>()
                .Where(_ => !_dashSource.isPlaying)
                .Subscribe(_ =>
                {
                    _dashSource.clip = _dash.GetNext();
                    _dashSource.Play();
                })
                .AddTo(this);
        }
    }
}