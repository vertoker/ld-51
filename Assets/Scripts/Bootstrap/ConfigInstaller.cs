
using Features.Core.Config;
﻿using Configs;
using Features.CameraControl.Configs;
using Features.Character.Configs;

using UnityEngine;
using Zenject;

namespace Bootstrap
{
    [CreateAssetMenu(menuName = "Installers/ConfigInstaller", 
        fileName = "ConfigInstaller")]
    public class ConfigInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private CoreConfig _coreConfig;
        [SerializeField] private CameraBehaviourConfig _cameraBehaviourConfig;
        [SerializeField] private CharacterConfig _characterConfig;


        [SerializeField] private LevelListConfig _levelListConfig;

        [SerializeField] private SoundConfig _soundConfig;
        [SerializeField] private InputConfig _inputConfig;


        public override void InstallBindings()
        {
            Container.BindInstance(_coreConfig);

            Container.BindInstance(_cameraBehaviourConfig);
            Container.BindInstance(_characterConfig);


            Container.BindInstance(_levelListConfig);

            Container.BindInstance(_soundConfig);
            Container.BindInstance(_inputConfig);

        }
    }
}