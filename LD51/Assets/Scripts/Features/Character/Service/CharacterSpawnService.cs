﻿using Features.Character.Configs;
using Features.Character.Controllers;
using Features.Character.Data;
using Features.Character.Factories;
using Features.Character.Views;
using UnityEngine;

namespace Features.Character.Service
{
    public class CharacterSpawnService
    {
        private readonly CharacterMovementController _characterMovementController;
        private readonly CharacterModelFactory _characterModelFactory;
        private readonly CharacterViewFactory _characterViewFactory;
        private readonly CharacterConfig _characterConfig;

        private CharacterView _currentCharacter;

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
            _currentCharacter = _characterViewFactory.Create(characterModel);
            _currentCharacter.transform.position = position;
            
            _characterMovementController.SetCharacter(characterModel);
        }

        public void TeleportCurrentTo(Vector3 position)
        {   
            _currentCharacter.Rigidbody.velocity = Vector3.zero;
            _currentCharacter.Rigidbody.angularVelocity = Vector3.zero;

            _currentCharacter.transform.position = position;

        }
    }
}