using System;
using Cysharp.Threading.Tasks;
using Features.Character.Configs;
using Features.Character.Models;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.Character.Controllers
{
    public class CharacterEffectsPresenter : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _footsteps;
        [SerializeField] private ParticleSystem _jump;
        
        private CharacterModel _characterModel;
        private CharacterConfig _characterConfig;

        [Inject]
        public void Construct(CharacterConfig characterConfig)
        {
            _characterConfig = characterConfig;
        }

        public void SetCharacter(CharacterModel characterModel)
        {
            _characterModel = characterModel;

            _characterModel.IsMoving
                .AsObservable()
                .Subscribe(async _ =>
                {
                    if (_characterModel.Grounded.Value && _characterModel.IsMoving.Value)
                        _footsteps.Play();
                    else
                        _footsteps.Stop();
                    await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
                })
                .AddTo(this);
        }

        public void PlayJumpEffect()
        {
            _jump.Play();
            /*var jump = Instantiate(_characterConfig.JumpParticles);
            jump.transform.position = _jumpHolder.position;
            
            await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
            Destroy(jump);*/
        }
    }
}