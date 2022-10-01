using Features.Character.Configs;
using Features.Character.Controllers;
using Features.Character.Data;
using Features.Character.Factories;
using UnityEngine;
using Zenject;

namespace Features.Character.Service
{
    public class CharacterSpawnService : IInitializable
    {
        private readonly CharacterMovementController _characterMovementController;
        private readonly CharacterModelFactory _characterModelFactory;
        private readonly CharacterViewFactory _characterViewFactory;
        private readonly CharacterConfig _characterConfig;

        private CharacterSpawnService(CharacterModelFactory characterModelFactory,
            CharacterMovementController characterMovementController,
            CharacterViewFactory characterViewFactory,
            CharacterConfig characterConfig)
        {
            _characterMovementController = characterMovementController;
            _characterModelFactory = characterModelFactory;
            _characterViewFactory = characterViewFactory;
            _characterConfig = characterConfig;
        }
        
        public void SpawnCharacter(Vector3 position)
        {
            var data = new CharacterData
            {
                Speed = _characterConfig.Speed,
                JumpForce = _characterConfig.JumpForce,
                DashForce = _characterConfig.DashForce
            };

            var characterModel = _characterModelFactory.Create(data);
            var characterView = _characterViewFactory.Create(characterModel);
            characterView.transform.position = position;
            
            _characterMovementController.SetCharacter(characterModel);
        }

        public void Initialize()
        {
            //SpawnCharacter(Vector3.zero);
        }
    }
}