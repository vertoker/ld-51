using Features.CameraControl.Configs;
using Features.Character.Configs;
using UnityEngine;
using Zenject;

namespace Bootstrap
{
    [CreateAssetMenu(menuName = "Installers/ConfigInstaller", fileName = "ConfigInstaller")]
    public class ConfigInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private CameraBehaviourConfig _cameraBehaviourConfig;
        [SerializeField] private CharacterConfig _characterConfig;
        public override void InstallBindings()
        {
            Container.BindInstance(_cameraBehaviourConfig);
            Container.BindInstance(_characterConfig);
        }
    }
}