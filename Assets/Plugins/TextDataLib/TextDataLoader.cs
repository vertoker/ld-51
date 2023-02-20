using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;

namespace TextDataLib 
{
    public class TextDataLoader
    {
        public string Language => m_language;
        public event System.Action OnLanguageChange;

        private string m_language;
        private TextDataConfig m_config;



        [Inject]

        public TextDataLoader(TextDataConfig textDataConfig) 
        {
            m_config = textDataConfig;
            m_language = m_config.defaultLanguage;

            if (PlayerPrefs.HasKey(m_config.playerPrefsKey)) 
            {
                string prefsVal = PlayerPrefs.GetString(m_config.playerPrefsKey);
                if (m_config.languages.Contains(prefsVal))
                    m_language = prefsVal;
            }
            
        }

        public void ChangeLanguage(string val) 
        {
            if (m_language == val)
                return;

            if (!m_config.languages.Contains(val))
            {
                Debug.LogError("This language is not supported!");
                return;
            }

            m_language = val;
            PlayerPrefs.SetString(m_config.playerPrefsKey, m_language);

            OnLanguageChange?.Invoke();
        }

        public TextData LoadTextData(string relativePath)
        {
            string fullPath = 
                $"{m_config.pathPrefix}/{m_language}/{relativePath}";
            var result = Resources.Load<TextData>(fullPath);

            if (result == null)
                throw new System.Exception("InvalidPath: /Resources/" +
                    fullPath);

            return result;
        }
    }
}

