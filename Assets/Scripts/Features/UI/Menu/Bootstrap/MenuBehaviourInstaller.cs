using Features.UI.Menu.Messages;
using Features.UI.Menu.Models;
using UnityEngine;
using Zenject;

namespace Features.UI.Menu.Bootstrap
{
    public class MenuBehaviourInstaller : Installer
    {
        public override void InstallBindings()
        {
            Debug.Log($"[MenuBehaviourInstaller] Installing");
            
            InstallModels();
            InstallSignals();
        }

        private void InstallModels()
        {
            Container.BindInterfacesTo<MenuBehaviourCatchService>().AsSingle();
            Container.BindInterfacesTo<MenuManagerService>().AsSingle();
        }
        
        private void InstallSignals()
        {
            Container.DeclareSignal<MenuBehaviourSignals.ActionAdded>();
            Container.DeclareSignal<MenuBehaviourSignals.ButtonPressed>();
        }
    }
}