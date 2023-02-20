using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;


namespace TextDataLib.Consumers 
{
    public class TDConsumerUIGeneral : MonoBehaviour
    {
        [System.Serializable]
        public struct ConsumerObject
        {
            public TMP_Text textMeshProObject;
            public string key;
        }

        [SerializeField]
        private string relativePath;

        [SerializeField]
        private ConsumerObject[] consumerObjects;

        private TextData m_textData;
        private TextDataLoader m_loader;

        [Inject]
        public void Init(TextDataLoader textDataLoader)
        {
            m_loader = textDataLoader;
        }

        private void Start()
        {
            LoadText();
        }

        private void OnEnable()
        {
            m_loader.OnLanguageChange += LoadText;
        }

        private void OnDisable()
        {
            m_loader.OnLanguageChange -= LoadText;
        }

        private void LoadText() 
        {
            m_textData = m_loader.LoadTextData(relativePath);

            foreach (var consumerObj in consumerObjects) 
            {
                if (!m_textData.Data.
                    TryGetTextSingle(consumerObj.key, out string text))
                {
                    Debug.LogError($"The key [{consumerObj.key}] does not " +
                        $"exist in {relativePath}");
                    continue;
                }

                consumerObj.textMeshProObject.text = text;
            }
        }

    }
}

