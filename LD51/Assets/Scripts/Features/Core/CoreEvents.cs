
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Features.Core 
{
    public class CoreEvents : System.IDisposable
    {
        
        private CoreData data;

       
        
        public System.Action<bool> OnSlowdownTimePressed;
        public System.Action<bool> OnPauseButtonPressed;

        public System.Action OnLevelComplete;
        public System.Action OnTimersUp;


        [Inject]
        public CoreEvents(CoreData coreData) 
        {
            data = coreData;

            OnSlowdownTimePressed += Slowdown;
            OnPauseButtonPressed += Pause;
            
        }
        public void Dispose()
        {
            OnSlowdownTimePressed -= Slowdown;
            OnPauseButtonPressed -= Pause;
        }

        void Slowdown(bool isSlowedDown) 
        {
            data.isSlowedDown = isSlowedDown;

            data.timeScale = data.isSlowedDown ? CoreData.c_slowedTimeScale : 1f;

            Time.timeScale = data.timeScale;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }

        void Pause(bool isPaused) 
        {
            data.isPaused = isPaused;

            Time.timeScale = data.isPaused ? 0f : data.timeScale;
        }

        
    }
}

