using Features.Character.Installers;
using Zenject;

namespace Bootstrap
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Install<CharacterInstaller>();
        }
    }
}