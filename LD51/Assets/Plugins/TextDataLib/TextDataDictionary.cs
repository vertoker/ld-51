using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomSerialized.Collections;

namespace TextDataLib 
{
    [System.Serializable]
    public class TextDataDictionary :
            UnitySerializedDictionary<string, TextList>
    {
        public const string DefaultKey = "_default";
        public bool IsEmpty => this.Count == 1 &&
            this.ContainsKey(DefaultKey) &&
            this[DefaultKey].State == TextList.ListState.Empty;

        public TextDataDictionary() 
        {
            this.Add(DefaultKey, new TextList());
        }

        public bool TryGetTextSingle(string key, out string output)
        {
            if (!this.TryGetValue(key, out TextList tl)
                || tl == null || tl?.Count == 0)
            {
                output = string.Empty;
                return false;
            }

            
            output = tl[0];

            return true;
        }



        public TextList GetTextListFromDefaultKey() 
        {
            if (TryGetValue(DefaultKey, out TextList tl))
                return tl;

            List<string> keys = new List<string>(this.Keys);
            return this[keys[0]];
        }

        public bool SafeRemove(string key) 
        {
            bool success = Remove(key);
            if (!success)
                return success;

            if (this.Count == 0)
                this.Add(DefaultKey, new TextList());
            return success;
        }

        public bool SafeRemove(string key, out TextList value)
        {
            bool success = Remove(key, out value);
            if (!success)
                return success;

            if (this.Count == 0)
                this.Add(DefaultKey, new TextList());
            return success;
        }
    }
}

