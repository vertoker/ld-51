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
        }
        
        private void Start()
        {
            /*Observable
                .EveryUpdate()
                .Where(_ => _characterModel.IsMoving.Value &&
                            _characterModel.Grounded.Value)
                .Subscribe(async _ =>
                {
                    var footsteps = Instantiate(_characterConfig.FootstepParticles);
                    footsteps.transform.position = _footstepsHolder.position;

                    await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
                    Destroy(footsteps);
                })
                .AddTo(this);*/
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