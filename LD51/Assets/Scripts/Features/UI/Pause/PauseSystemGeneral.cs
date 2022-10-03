using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Features.Core;

namespace Features.UI.Pause 
{
    public class PauseSystemGeneral : MonoBehaviour
    {
        [HideInInspector]
        public CoreEvents events;
        [SerializeField]
        private CanvasGroup mainWindow;
        [SerializeField]
        private CanvasGroup settingsWindow;

        private GameObject m_windows;

        [Inject]
        void Init(CoreEvents coreEvents) 
        {
            events = coreEvents; 
        }

        private void OnEnable()
        {
            events.OnPauseButtonPressed += ShowPauseMenu;
        }

        private void OnDisable()
        {
            events.OnPauseButtonPressed -= ShowPauseMenu;
        }

        // Start is called before the first frame update
        void Awake()
        {
            m_windows = transform.GetChild(0).gameObject;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SwitchToMain() 
        {
            settingsWindow.gameObject.SetActive(false);

            mainWindow.interactable = true;
        }

        public void SwitchToSettings() 
        {
            mainWindow.interactable = false;
            settingsWindow.gameObject.SetActive(true);
        }

        private void ShowPauseMenu(bool isPaused) 
        {
            m_windows.SetActive(isPaused);
        }
    }
}

