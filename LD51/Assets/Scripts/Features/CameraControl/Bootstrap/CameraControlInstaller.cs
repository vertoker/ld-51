using Features.CameraControl.Messages;
using Zenject;

namespace Features.CameraControl.Bootstrap
{
    public class CameraControlInstaller : Installer
    {
        public override void InstallBindings()
        {
            InstallSignals();
        }

        private void InstallSignals()
        {
            Container.DeclareSignal<CameraControlSignals.CameraToMenu>();
            Container.DeclareSignal<CameraControlSignals.CameraToObject>();
        }
    }
}