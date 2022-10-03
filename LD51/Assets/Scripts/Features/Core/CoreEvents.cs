
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Features.Core 
{
    public class CoreEvents : System.IDisposable
    {
        public static CoreEvents Instance;

        private CoreData data;

       
        
        public System.Action<bool> OnSlowdownTimePressed;
        public System.Action<bool> OnPauseButtonPressed;

        public System.Action OnLevelComplete;
        public System.Action OnGameOver;
        public System.Action OnReturnToMainMenu;

        

        [Inject]
        public CoreEvents(CoreData coreData) 
        {
            Instance = this;

            data = coreData;

            OnSlowdownTimePressed += Slowdown;
            OnPauseButtonPressed += Pause;
            
        }
        public void Dispose()
        {
            if (Instance == this)
                Instance = null;

            OnSlowdownTimePressed -= Slowdown;
            OnPauseButtonPressed -= Pause;
        }

        public void Slowdown(bool isSlowedDown) 
        {
            data.isSlowedDown = isSlowedDown;

            data.timeScale = data.isSlowedDown ? data.SlowedTimeScale : 1f;

            Time.timeScale = data.timeScale;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }

       
        public void InvokeSlowdown()
        {
            OnSlowdownTimePressed?.Invoke(!data.isSlowedDown);
        }

        public void InvokePause() 
        {
            OnPauseButtonPressed?.Invoke(!data.isPaused);
        }

        void Pause(bool isPaused) 
        {
            data.isPaused = isPaused;

            Time.timeScale = data.isPaused ? 0f : data.timeScale;
        }



        
    }
}

