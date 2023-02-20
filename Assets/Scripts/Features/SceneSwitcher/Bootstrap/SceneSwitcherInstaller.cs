using Features.SceneSwitcher.Messages;
using Features.SceneSwitcher.Models;
using Zenject;

namespace Features.SceneSwitcher.Bootstrap
{
    public class SceneSwitcherInstaller : Installer
    {
        public override void InstallBindings()
        {
            InstallModels();
            InstallSignals();
        }

        private void InstallModels()
        {
            Container.BindInterfacesTo<SceneSwitcherService>().AsSingle();
            Container.BindInterfacesTo<SceneLoaderService>().AsSingle();
        }
    
        private void InstallSignals()
        {
            Container.DeclareSignal<SceneSwitcherSignals.SwitchToLevel>();
        }
    }
}