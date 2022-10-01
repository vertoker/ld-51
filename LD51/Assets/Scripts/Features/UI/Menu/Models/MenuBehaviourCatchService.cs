using Features.UI.Menu.Messages;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.UI.Menu.Models
{
    public class MenuBehaviourCatchService : IInitializable
    {
        private SignalBus _signalBus;
        
        private MenuBehaviourCatchService(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            Debug.Log($"[MenuBehaviourCatchService] Initialize");
            
            _signalBus
                .GetStream<MenuBehaviourSignals.ButtonPressed>()
                .Subscribe(signal => ResolveAction(signal.Property));
        }

        private void ResolveAction(string property)
        {
            var action = property switch
            {
                "Menu" => MenuAction.MainMenu,
                "Play" => MenuAction.Play,
                "Settings" => MenuAction.Settings,
                "Credits" => MenuAction.Credits,
                "Info" => MenuAction.Info,
                "Exit" => MenuAction.Exit,
                _ => MenuAction.NaN
            };
            _signalBus.TryFire(new MenuBehaviourSignals.ActionAdded(action));
        }
    }
}