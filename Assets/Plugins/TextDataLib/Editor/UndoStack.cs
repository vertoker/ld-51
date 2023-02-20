using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace TextDataLib.Editor
{
    public class UndoStack<T>
    {
        private LinkedList<T> m_list;
        private LinkedListNode<T> m_activeNode;

        public int Count => m_list.Count;
        public bool IsNull => (m_list == null || m_activeNode == null);

        public UndoStack()
        {
            m_list = new LinkedList<T>();
            m_activeNode = null;
        }

        public void Push(T value) 
        {
            m_list.AddLast(value);
            m_activeNode = m_list.Last;
        }

        public void Clear() 
        {
            m_activeNode = null;
            m_list.Clear();
        }

        public void ClearInactive() 
        {
            var lastNode = m_list.Last;
            while (lastNode != m_activeNode) 
            {
                var prevNode = lastNode.Previous;
                m_list.RemoveLast();
                lastNode = prevNode;

            }
        }

        public T Peek() 
        {
            return m_activeNode.Value;
        }

        public bool Undo(out T value) 
        {
            
            m_activeNode = m_activeNode?.Previous;
            if (m_activeNode == null)
            {
                value = default;
                return false;
            }
            value = m_activeNode.Value;

            return true;
        }



        public bool Redo(out T value) 
        {
            
            m_activeNode = m_activeNode?.Next;
            if (m_activeNode == null) 
            {
                value = default(T);
                return false;
            }

            value = m_activeNode.Value;
            return true;
        }
        public bool PositionDec() 
        {
            if (m_activeNode.Previous == null)
                return false;

            m_activeNode = m_activeNode.Previous;
            return true;
        }
        public override string ToString()
        {
            //return base.ToString();
            StringBuilder stringBuilder = new StringBuilder("UndoStack: ");
            foreach (var node in m_list) 
            {
                stringBuilder.Append($"{node}; ");
            }

            return stringBuilder.ToString();
        }
    }
}

