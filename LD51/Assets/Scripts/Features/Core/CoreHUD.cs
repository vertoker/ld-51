using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;
using System.Globalization;

namespace Features.Core.UI 
{
    public class CoreHUD : MonoBehaviour
    {
        [SerializeField]
        private string timerPrefix;
        [SerializeField]
        private TMP_Text timerRenderer;
        
        

        [Inject]
        private CoreData data;

        private void Update() 
        {
            timerRenderer.text = timerPrefix + data.timer.
                ToString("F2", CultureInfo.CreateSpecificCulture("en-US"));
        }
    }
}

