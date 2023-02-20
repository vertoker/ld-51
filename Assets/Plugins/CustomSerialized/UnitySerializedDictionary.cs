using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CustomSerialized.Collections 
{
	public abstract class UnitySerializedDictionary<TKey, TValue> :
	Dictionary<TKey, TValue>, ISerializationCallbackReceiver
	{
		[SerializeField]
		private List<TKey> keys = new List<TKey>();

		[SerializeField]
		private List<TValue> values = new List<TValue>();


		#region ISerializationCallbackReceiver implementation
		// save the dictionary to lists
		public void OnBeforeSerialize()
		{
			keys.Clear();
			values.Clear();
			foreach (KeyValuePair<TKey, TValue> pair in this)
			{
				keys.Add(pair.Key);
				values.Add(pair.Value);
			}
		}

		// load dictionary from lists
		public void OnAfterDeserialize()
		{
			this.Clear();

            if (keys.Count != values.Count)
                throw new System.Exception(
                    $"there are {keys.Count} keys " +
                    $"and {values.Count} values after deserialization.");

            for (int i = 0; i < keys.Count; i++)
				this.Add(keys[i], values[i]);
		}
        #endregion

        public bool TryReplaceKey(TKey oldKey, TKey newKey)
		{
			TValue val;
			if (!this.TryGetValue(oldKey, out val))
				return false;

			if (!this.TryAdd(newKey, val))
				return false;

			this.Remove(oldKey);
			return true;
		}

		public void ReplaceKey(TKey oldKey, TKey newKey)
		{
			TValue val = this[oldKey];


			Add(newKey, val);

			this.Remove(oldKey);
		}


	}
}

