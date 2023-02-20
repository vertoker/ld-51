using Features.SceneSwitcher.Messages;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Features.SceneSwitcher.Models
{
    public class SceneSwitcherService : IInitializable
    {
        private SignalBus _signalBus;

        private SceneSwitcherService(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            Debug.Log($"[SceneSwitcherService] Initialize");
            
            _signalBus
                .GetStream<SceneSwitcherSignals.SwitchToLevel>()
                .Subscribe(signal => SceneManager.LoadScene(signal.LevelIndex));
        }
    }
}
