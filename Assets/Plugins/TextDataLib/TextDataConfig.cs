using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomSerialized.Collections;

namespace TextDataLib 
{
    [CreateAssetMenu(fileName = "TextDataConfig",
    menuName = "Config/TextDataConfig", order = 1)]
    public class TextDataConfig : ScriptableObject
    {
        [Tooltip("Full path will look like this: " +
            "Assets/Resources/*pathPrefix*/*languages[i]*/")]
        public string pathPrefix;
        [SerializeField]
        public HashSetString languages;
        public string defaultLanguage;
        public string playerPrefsKey;

        [System.Serializable]
        public class HashSetString : UnitySerializedHashSet<string> { }
    }
}


