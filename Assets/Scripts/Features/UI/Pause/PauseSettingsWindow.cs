using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Features.UI.Pause 
{
    public class PauseSettingsWindow : MonoBehaviour
    {
        private PauseSystemGeneral general;

        [SerializeField]
        private Button returnButton;
        

        // Start is called before the first frame update
        void Awake()
        {
            general = transform.GetComponentInParent<PauseSystemGeneral>();
        }

        private void Start()
        {
            returnButton.onClick.AddListener(ReturnToMainWindow);
            
        }


        void ReturnToMainWindow() 
        {
            general.SwitchToMain();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
