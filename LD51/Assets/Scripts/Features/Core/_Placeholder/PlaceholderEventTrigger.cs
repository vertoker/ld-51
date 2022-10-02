using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Features.Core;

namespace Placeholders.Core 
{
    public class PlaceholderEventTrigger : MonoBehaviour
    {
        
       
        private CoreEvents coreEvents;
        private CoreData coreData;

        [Inject]
        private void Init(CoreData coreData, CoreEvents coreEvents)
        {
            this.coreData = coreData;
            this.coreEvents = coreEvents;
        }

        // Update is called once per frame
        void Update()
        {

            InputTrigger();
            PauseTrigger();
            
        }

        void InputTrigger()
        {
            if (coreData.isPaused)
                return;

            if (Input.GetMouseButtonDown(1))
            {
                coreEvents.OnSlowdownTimePressed?.Invoke(!coreData.isSlowedDown);
            }

        }

        void PauseTrigger() 
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                coreEvents.OnPauseButtonPressed?.Invoke(!coreData.isPaused);
            }
        }
    }

}
