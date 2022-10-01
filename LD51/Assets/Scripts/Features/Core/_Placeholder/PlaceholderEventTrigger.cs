using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Features.Core;

namespace Placeholders.Core 
{
    public class PlaceholderEventTrigger : MonoBehaviour
    {
        
        [Inject]
        private CoreEvents coreEvents;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P)) 
            {
                coreEvents.OnSlowdownTimePress?.Invoke();
            }
        }
    }

}
