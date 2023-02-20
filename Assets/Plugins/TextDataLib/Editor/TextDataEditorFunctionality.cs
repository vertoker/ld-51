using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TextDataLib.Editor 
{
    public class TextDataEditorFunctionality
    {
        private TextDataEditorStorage storage;
        private TextDataEditor editor;

        private int m_undoCount = 0;
        private int m_redoCount = 0;

        private Object[] m_objsToRecord;

        private UndoStack<int> m_undoContentIndex = new UndoStack<int>();
        //private Stack<int> m_redoContentIndex = new Stack<int>();

        //private int m_prevPopupIndex = 0;
        //private int m_redoPopupIndex = 0;

        private UndoStack<int> m_undoPopupIndex = new UndoStack<int>();

        private Stack<System.Action> LocalUndo = new Stack<System.Action>();
        private Stack<System.Action> LocalRedo = new Stack<System.Action>();

        private System.Action ClearUndoStack;

        public int UndoCount{get{ return m_undoCount; } }
        public int RedoCount { get { return m_redoCount; } }

        public TextDataEditorFunctionality(TextDataEditorStorage storage, 
            TextDataEditor editor) 
        {
            this.storage = storage;
            this.editor = editor;

            m_objsToRecord = new Object[] { storage.textData, editor };
        }

        #region TextDataInit
        public void CreateNewTextData()
        {
            storage.textData = TextDataCreate.Create();
        }

        public void OpenTextData()
        {

            string absPath = EditorUtility.
                OpenFilePanel("Select TextData object",
                Application.dataPath, "asset");
            if (!absPath.StartsWith(Application.dataPath))
            {

                Debug.LogError(
                    "[TextDataEditor]: TextData file should be located inside " +
                    "the project folder!");
                return;
                //if (inventoryItemList)
                //    EditorPrefs.SetString("ObjectPath", relPath);

            }
            string relPath = absPath.
                    Substring(Application.dataPath.Length - "Assets".Length);
            storage.textData = AssetDatabase.
                LoadAssetAtPath(relPath, typeof(TextData))
                as TextData;

            if (storage.textData == null)
            {
                Debug.LogError(
                     "[TextDataEditor]: TextData file load failure");
                return;
            }

            Debug.Log("[TextDataEditor]: The file was loaded");

        }
        #endregion

        #region Keys

        public void CreateKey(string keyVal) 
        {
            RecordTextData($"Added new key [{keyVal}]");

            TextList tl = new TextList();
            //tl.Add("");
            storage.textData.Data.Add(keyVal, tl);

            //m_prevPopupIndex = storage.popupIndex;

            //RecordPopupIndex(() => 
            //{
                storage.popupIndex = storage.textData.Data.Keys.Count - 1;
            //});
            
           // m_redoPopupIndex = storage.popupIndex;

            //LocalUndo += UndoPopupIndex;
        }

        public void DeleteKey()
        {

            RecordTextData($"Deleted key [{storage.curKeyVal}]");

            string key = storage.curKeyVal;
            //RecordPopupIndex(() =>
            //{
            if (storage.popupIndex == storage.keysList.Count - 1 &&
            storage.popupIndex > 0)
                --storage.popupIndex;
            //});


            //storage.contentList.RemoveAt(index);
            storage.keysList.Remove(key);
            storage.textData.Data.SafeRemove(key);


        }

        public void EditKey(string newVal) 
        {
            RecordTextData($"Edited key [{storage.curKeyVal}]->[{newVal}]");

            storage.textData.Data.ReplaceKey(storage.curKeyVal, newVal);

            storage.UpdateKeyList();
            storage.popupIndex = storage.keysList.Count - 1;
            storage.UpdateCurKeyVal();
        }

        #endregion

        #region Pages
        public void CreateNewPage()
        {
            RecordTextData($"Added new page in [{ storage.curKeyVal}]");

            storage.contentList.Add(string.Empty);


            //RecordContentIndex();
            //RecordContentIndex(()=> 
            //{
            storage.contentIndex = storage.contentList.Count - 1;
            //});



        }

        public void DeletePage()
        {
            
            RecordTextData($"Deleted page [{storage.curKeyVal}]" +
                $"[{storage.contentIndex}]");

            int index = storage.contentIndex;


            //RecordContentIndex(()=> 
            //{
            if (storage.contentIndex == storage.contentList.Count - 1 &&
            storage.contentIndex > 0)
                --storage.contentIndex;
            //});

            storage.contentList.RemoveAt(index);
        }

        public void EditPage(string newVal) 
        {
            RecordTextData("Changed content in " +
                    $"[{storage.curKeyVal}][{storage.contentIndex}]");

            //RecordPopupIndex(null);
            //RecordContentIndex();

            storage.contentList[storage.contentIndex] = newVal;
        }
        #endregion

        #region Undo
        public void RecordTextData(string message)
        {
            if (m_redoCount > 0)
            {
                m_redoCount = 0;
                LocalRedo.Clear();
                LocalUndo.Clear();
            }

            ClearUndoStack?.Invoke();
            ClearUndoStack = null;

            Undo.RecordObject(storage.textData,
                $"[TextData]({storage.textData.name})" + message);

            //RecordContentIndex();
            
            ++m_undoCount;
        }

        public void RecordContentIndex() 
        {
            //if (m_undoContentIndex.Count > 0 && 
            //    m_undoContentIndex.Peek() == storage.contentIndex)
            //    return;
           
            if (m_undoContentIndex.Count == 0)
            {
                //if (!m_undoContentIndex.IsNull)
                //    m_undoContentIndex.Push(m_undoContentIndex.Peek());
                m_undoContentIndex.Push(storage.contentIndex);
            }

            m_undoContentIndex.Push(storage.contentIndex);

           
            
            //if (m_undoContentIndex.Count == 1)
            //    return;
            //Debug.Log(m_undoContentIndex.ToString());
            LocalUndo.Push(UndoContentIndex);

            ClearUndoStack -= ClearContentIndex;
            ClearUndoStack += ClearContentIndex;
        }
        public void RecordContentIndex(System.Action action)
        {
            //if (m_undoContentIndex.Count > 0 &&
            //    m_undoContentIndex.Peek() == storage.contentIndex)
            //    return;

            if (m_undoContentIndex.Count == 0)
                m_undoContentIndex.Push(storage.contentIndex);
            //storage.contentIndex = storage.contentList.Count - 1;
            action?.Invoke();
            m_undoContentIndex.Push(storage.contentIndex);

            LocalUndo.Push(UndoContentIndex);

            ClearUndoStack -= ClearContentIndex;
            ClearUndoStack += ClearContentIndex;
        }

        public void RecordPopupIndex(bool autoPush = true) 
        {
            if (m_undoPopupIndex.Count == 0)
            { 
                m_undoPopupIndex.Push(storage.popupIndex); 
            }
            //if (m_undoPopupIndex.Count > 0 &&
            //    m_undoPopupIndex.Peek() != storage.popupIndex)
            //{
            //    m_undoPopupIndex.Push(storage.popupIndex);
            //}
            m_undoPopupIndex.Push(storage.popupIndex);
            
            //if (autoPush)
                LocalUndo.Push(UndoPopupIndex);
           

            ClearUndoStack -= ClearPopupIndex;
            ClearUndoStack += ClearPopupIndex;
        }

        public void RecordPopupIndex(System.Action action)
        {
            
            if (m_undoPopupIndex.Count == 0)
            {
                m_undoPopupIndex.Push(storage.popupIndex);
            }
            //if (m_undoPopupIndex.Count > 0 &&
            //    m_undoPopupIndex.Peek() != storage.popupIndex)
            //{
            //    m_undoPopupIndex.Push(storage.popupIndex);
            //}
            action?.Invoke();
            m_undoPopupIndex.Push(storage.popupIndex);

            LocalUndo.Push(UndoPopupIndex);

            ClearUndoStack -= ClearPopupIndex;
            ClearUndoStack += ClearPopupIndex;
        }


        private void ClearContentIndex() 
        {
            m_undoContentIndex.ClearInactive();
        }

        private void ClearPopupIndex() 
        {
            m_undoPopupIndex.ClearInactive();
        }

        public void PerformLocalUndo() 
        {
            Undo.PerformUndo();
            //Debug.Log("LocalUndo: " + m_undoPopupIndex.ToString());
            if (LocalUndo.Count > 0)
                LocalUndo.Pop()?.Invoke();
            
            //LocalUndo = null;

            --m_undoCount;
            ++m_redoCount;

        }

        public void PerformLocalRedo() 
        {
            Undo.PerformRedo();

            if (LocalRedo.Count > 0)
                LocalRedo.Pop()?.Invoke();
            //LocalRedo = null;

            ++m_undoCount;
            --m_redoCount;
        }

        public void UndoPopupIndex()
        {
           
            if (!m_undoPopupIndex.Undo(out storage.popupIndex))
                Debug.LogError("[TextDataEditor] Undo popupIndex error");


            LocalRedo.Push(RedoPopupIndex);
        }

        public void RedoPopupIndex()
        {

            if (!m_undoPopupIndex.Redo(out storage.popupIndex))
                Debug.LogError("[TextDataEditor] Redo popupIndex error");


            LocalUndo.Push(UndoPopupIndex);
        }

        public void UndoContentIndex()
        {

            if (!m_undoContentIndex.Undo(out storage.contentIndex))
                Debug.LogError("[TextDataEditor] Undo contentIndex error");

            
            LocalRedo.Push(RedoContentIndex);
        }

        public void RedoContentIndex()
        {
            //if (m_undoCount == 0)
            //    m_undoContentIndex.PositionDec();

            if (!m_undoContentIndex.Redo(out storage.contentIndex))
                Debug.LogError("[TextDataEditor] Redo contentIndex error");


            LocalUndo.Push(UndoContentIndex);
        }
        #endregion
    }
}

