using Features.CameraControl.Bootstrap;
using Features.SceneSwitcher.Bootstrap;
using Features.UI.Menu.Bootstrap;
using UnityEngine;
using Zenject;

namespace Bootstrap
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private CanvasGroup screenFadeGroup;

        public override void Start()
        {
            screenFadeGroup.alpha = 0;
        }
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            
            InstallDependencies();
            Container.Install<CameraControlInstaller>();
            Container.Install<MenuBehaviourInstaller>();
            Container.Install<SceneSwitcherInstaller>();
        }
        
        private void InstallDependencies()
        {
            Container.BindInstance(screenFadeGroup).WithId("FadeCanvasGroup");
        }
    }
}