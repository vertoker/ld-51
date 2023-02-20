using Features.Character.Controllers;
using Features.Character.Factories;
using Features.Character.Service;
using Zenject;

namespace Features.Character.Installers
{
    public class CharacterInstaller : Installer
    {
        public override void InstallBindings()
        {
            InstallControllers();
            InstallServices();
            InstallFactories();
        }

        private void InstallControllers()
        {
            Container.BindInterfacesAndSelfTo<CharacterMovementController>().AsSingle();
        }

        private void InstallServices()
        {
            Container.BindInterfacesAndSelfTo<CharacterSpawnService>().AsSingle();
        }
        
        private void InstallFactories()
        {
            Container.Bind<CharacterModelFactory>().AsSingle();
            Container.Bind<CharacterViewFactory>().AsSingle();
        }
    }
}