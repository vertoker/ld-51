using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Features.Core;
using Configs;
using Features.Character.Controllers;

namespace Features.Core.Mono
{
    public class CoreInputController : MonoBehaviour
    {
        
       
        private CoreEvents coreEvents;
        private CoreData coreData;
        private InputConfig inputConfig;
        //private CharacterMovementController characterController;

        [Inject]
        private void Init(CoreData coreData, CoreEvents coreEvents, 
            InputConfig inputConfig/*, 
            CharacterMovementController characterMovementController*/)
        {
            this.coreData = coreData;
            this.coreEvents = coreEvents;
            this.inputConfig = inputConfig;
            //characterController = characterMovementController;
        }

        private void OnEnable()
        {
            coreEvents.OnPauseButtonPressed += LockMouseLook;
        }

        private void OnDisable()
        {
            coreEvents.OnPauseButtonPressed -= LockMouseLook;
        }

        // Update is called once per frame
        void Update()
        {

            InputTrigger();
            PauseTrigger();
            
        }

        private void InputTrigger()
        {
            if (coreData.isPaused)
                return;

            if (Input.GetKeyDown(inputConfig.TimeStopButton))
            {
                coreEvents.OnSlowdownTimePressed?.Invoke(!coreData.isSlowedDown);
            }

        }

        private void PauseTrigger() 
        {
            if (Input.GetKeyDown(inputConfig.MenuButton))
            {
                coreEvents.OnPauseButtonPressed?.Invoke(!coreData.isPaused);
            }
        }

        private void LockMouseLook(bool isPaused) 
        {
            if (CharacterMovementController.Instance != null)
                CharacterMovementController.Instance.LockMouse = isPaused;
        }

    }

}
