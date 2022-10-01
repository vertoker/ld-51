using System.Collections;
using System.Collections.Generic;

namespace TextDataLib 
{
    [System.Serializable]
    public class TextList : IList<string>
    {
        [UnityEngine.SerializeField]
        private List<string> text ;

        
        public string this[int i]
        {
            get { return text[i]; }
            set { text[i] = value; }
        }

        public int Count { get { return text.Count; } }
        public enum ListState
        {
            Null = 0,
            Empty = 1,
            NotEmpty = 2
        }
        public ListState State
        { get 
            {
                if (text == null)
                    return ListState.Null;
                else if (text.Count == 1 && text[0] == string.Empty)
                    return ListState.Empty;
                else
                    return ListState.NotEmpty;
            } 
        }
            

        public bool IsReadOnly => false;

        public TextList() 
        {
            text = new List<string>();
            text.Add(string.Empty);
        }

        public TextList(IEnumerable<string> collection) 
        {
            text = new List<string>(collection);
        }

        public void Add(string val)
        {
            text.Add(val);
        }

        public void Clear()
        {
            text.Clear();
        }

        public bool Contains(string item)
        {
            return text.Contains(item);

        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            text.CopyTo(array, arrayIndex);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return text.GetEnumerator();
        }

        public int IndexOf(string item)
        {
            return text.IndexOf(item);
        }

        public void Insert(int index, string item)
        {
            text.Insert(index, item);
        }

        public bool Remove(string item)
        {
            bool success = text.Remove(item);
            if (!success)
                return success;

            if (text.Count == 0)
                text.Add("");

            return success;
        }

        public void RemoveAt(int index)
        {
            text.RemoveAt(index);

            if (text.Count == 0)
                text.Add(string.Empty);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}
