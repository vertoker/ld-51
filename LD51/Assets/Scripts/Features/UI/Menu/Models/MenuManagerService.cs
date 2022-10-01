using System;
using System.Threading.Tasks;
using Features.CameraControl.Messages;
using Features.SceneSwitcher.Data;
using Features.SceneSwitcher.Messages;
using Features.UI.Menu.Messages;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.UI.Menu.Models
{
    public class MenuManagerService : IInitializable
    {
        private SignalBus _signalBus;
        [Inject(Id = "FadeCanvasGroup")] private CanvasGroup _canvasGroup;
        
        private MenuManagerService(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus
                .GetStream<MenuBehaviourSignals.ActionAdded>()
                .Subscribe(signal => GetAction(signal.Action));
        }

        private async void GetAction(MenuAction action)
        {
            switch (action)
            {
                case MenuAction.MainMenu:
                {
                    _signalBus.TryFire(new CameraControlSignals.CameraToMenu(MenuAction.MainMenu));
                    break;
                }
                case MenuAction.Play:
                {
                    await PlayButtonPressed();
                    break;
                }
                case MenuAction.Settings:
                {
                    _signalBus.TryFire(new CameraControlSignals.CameraToMenu(MenuAction.Settings));
                    break;
                }
                case MenuAction.Credits:
                {
                    _signalBus.TryFire(new CameraControlSignals.CameraToMenu(MenuAction.Credits));
                    break;
                }
                case MenuAction.Info:
                {
                    _signalBus.TryFire(new CameraControlSignals.CameraToMenu(MenuAction.Info));
                    break;
                }
                case MenuAction.Exit:
                {
                    break;
                }
            }
        }

        private async Task PlayButtonPressed()
        {
            while (_canvasGroup.alpha < 1)
            {
                _canvasGroup.alpha = Mathf.MoveTowards(_canvasGroup.alpha, 1, 2f * Time.deltaTime);
                await Task.Yield();
            }
            await Task.Delay(TimeSpan.FromSeconds(2f));
            _signalBus.TryFire(new SceneSwitcherSignals.SwitchToLevel(SceneSwitcherRegistry.FirstLevel));
        }
    }
}