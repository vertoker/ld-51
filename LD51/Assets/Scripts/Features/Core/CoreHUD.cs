using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
using System.Globalization;
using Features.Core.Config;

namespace Features.Core.UI 
{
    public class CoreHUD : MonoBehaviour
    {
        [SerializeField] private Image timer;
        [SerializeField] private TMP_Text text;

        [Inject]
        private CoreConfig config;
        [Inject]
        private CoreData data;

        private void Update() 
        {
            timer.fillAmount = data.timer / config.startTime;
            text.text = data.timer.ToString("00.00");
        }
    }
}

