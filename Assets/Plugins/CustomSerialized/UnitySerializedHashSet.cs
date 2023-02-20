using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomSerialized.Collections 
{
    [System.Serializable]
    public abstract class UnitySerializedHashSet<TKey> : 
        ISet<TKey>, ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<TKey> keys = new List<TKey>();
        
        private HashSet<TKey> hashSet;

        public int Count => hashSet.Count;

        public bool IsReadOnly => false;

        public void OnBeforeSerialize()
        {
         
            var cur = new HashSet<TKey>(keys);
            if (hashSet != null)
            {
                foreach (var key in hashSet)
                {
                    if (!cur.Contains(key))
                    {
                        keys.Add(key);
                    }
                }
            }
        }

        public void OnAfterDeserialize()
        {
            if (hashSet == null)
                hashSet = new HashSet<TKey>();
            else
                hashSet.Clear();

            foreach (var key in keys)
            {
                hashSet.Add(key);
            }
        }

        public bool Add(TKey item)
        {
            return hashSet.Add(item);
        }

        public void ExceptWith(IEnumerable<TKey> other)
        {
            hashSet.ExceptWith(other);
        }

        public void IntersectWith(IEnumerable<TKey> other)
        {
            hashSet.IntersectWith(other);
        }

        public bool IsProperSubsetOf(IEnumerable<TKey> other)
        {
            return hashSet.IsProperSubsetOf(other);
        }

        public bool IsProperSupersetOf(IEnumerable<TKey> other)
        {
            return hashSet.IsProperSupersetOf(other);
        }

        public bool IsSubsetOf(IEnumerable<TKey> other)
        {
            return hashSet.IsSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<TKey> other)
        {
            return hashSet.IsSupersetOf(other);
        }

        public bool Overlaps(IEnumerable<TKey> other)
        {
            return hashSet.Overlaps(other);
        }

        public bool SetEquals(IEnumerable<TKey> other)
        {
            return hashSet.SetEquals(other);
        }

        public void SymmetricExceptWith(IEnumerable<TKey> other)
        {
            hashSet.SymmetricExceptWith(other);
        }

        public void UnionWith(IEnumerable<TKey> other)
        {
            hashSet.UnionWith(other);
        }

        void ICollection<TKey>.Add(TKey item)
        {
            this.Add(item);
        }

        public void Clear()
        {
            hashSet.Clear();
        }

        public bool Contains(TKey item)
        {
            return hashSet.Contains(item);
        }

        public void CopyTo(TKey[] array, int arrayIndex)
        {
            hashSet.CopyTo(array, arrayIndex);
        }

        public bool Remove(TKey item)
        {
            return hashSet.Remove(item);
        }

        public IEnumerator<TKey> GetEnumerator()
        {
            return hashSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
           return this.GetEnumerator();
        }
    }
}

