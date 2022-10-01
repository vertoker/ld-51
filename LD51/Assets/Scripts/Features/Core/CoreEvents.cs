using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Features.Core 
{
    public class CoreEvents
    {
        
        private CoreData data;

       

        public System.Action OnTimersUp;
        public System.Action OnSlowdownTimePress;

        public System.Action OnLevelComplete;

        [Inject]
        public CoreEvents(CoreData coreData) 
        {
            data = coreData;

            OnSlowdownTimePress += Slowdown;
            
        }

        void Slowdown() 
        {
            data.isSlowedDown = !data.isSlowedDown;

            Time.timeScale = data.isSlowedDown ? CoreData.c_slowedTimeScale : 1f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }

    }
}

