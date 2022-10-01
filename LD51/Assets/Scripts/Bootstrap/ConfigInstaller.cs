using Features.CameraControl.Configs;
using UnityEngine;
using Zenject;

namespace Bootstrap
{
    [CreateAssetMenu(menuName = "Installers/ConfigInstaller", fileName = "ConfigInstaller")]
    public class ConfigInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private CameraBehaviourConfig cameraBehaviourConfig;
        public override void InstallBindings()
        {
            Container.BindInstance(cameraBehaviourConfig);
        }
    }
}