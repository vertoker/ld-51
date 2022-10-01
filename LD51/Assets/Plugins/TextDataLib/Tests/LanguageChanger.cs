using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TextDataLib.Tests 
{
    public class LanguageChanger : MonoBehaviour
    {
        [Inject]
        private TextDataLoader m_loader;

        [SerializeField]
        private string language;

        public void ChangeLanguage() 
        {
            m_loader.ChangeLanguage(language);   
        }
    }
}

