using UnityEngine;
using Zenject;

namespace Bootstrap
{
    [CreateAssetMenu(menuName = "Installers/RegistryInstaller", fileName = "RegistryInstaller")]
    public class RegistryInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            
        }
    }
}