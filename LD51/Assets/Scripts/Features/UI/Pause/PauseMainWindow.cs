using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Features.UI.Pause 
{
    public class PauseMainWindow : MonoBehaviour
    {
        private PauseSystemGeneral general;

        [SerializeField]
        private Button resumeButton;
        [SerializeField]
        private Button resetButton;
        [SerializeField]
        private Button settingsButton;
        [SerializeField]
        private Button mainMenuButton;

        // Start is called before the first frame update
        void Awake()
        {
            general = transform.GetComponentInParent<PauseSystemGeneral>();
        }

        private void Start()
        {
            resumeButton.onClick.AddListener(OnResumeButtonPress);
            resetButton.onClick.AddListener(OnResetButtonPress);
            settingsButton.onClick.AddListener(OnSettingsButtonPress);
            mainMenuButton.onClick.AddListener(OnMainMenuButtonPress);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnResumeButtonPress() 
        {
            general.events.InvokePause();
        }
        private void OnResetButtonPress()
        {
            general.events.InvokePause();
            general.events.OnGameOver?.Invoke();
        }

        private void OnSettingsButtonPress()
        {
            general.SwitchToSettings();
        }

        private void OnMainMenuButtonPress()
        {
            general.events.InvokePause();
            general.events.OnReturnToMainMenu?.Invoke();
        }
    }

}
