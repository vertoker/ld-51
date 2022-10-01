using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TextDataLib;

namespace TextDataLib.Editor 
{
    public class TextDataEditor : EditorWindow
    {
        public TextDataEditorStorage storage = null;
        public TextDataEditorFunctionality functionality = null;

        private Vector2 m_scrollPosition;
        private Vector2 m_scrollPositionIn;

        private int m_prevPopupIndex;
        

        private int m_gotoIndex = 0;

       
        private string m_newKeyVal = string.Empty;

        private string m_newContentVal;

        [MenuItem("Window/TextDataEditor")]
        static void Init()
        {
            GetNewWindow(null);
            
        }

        public static TextDataEditor GetNewWindow(TextData textData) 
        {
            TextDataEditor editor = (TextDataEditor)
                GetWindow(typeof(TextDataEditor), false, "TextDataEditor");
            editor.storage = new TextDataEditorStorage();
            editor.storage.textData = textData;
            editor.functionality = 
                new TextDataEditorFunctionality(editor.storage, editor);

            return editor;
        }

        //public void SetTextData(TextData textData)
        //{
        //    this.textData = textData;
        //}

        private void OnInspectorUpdate()
        {
            this.Repaint();
        }

        private void OnGUI()
        {
            if (storage == null || functionality == null)
                return;

            //m_borders = GUIHelper.GetBordersFromRect(this.position);

            if (storage.textData == null)
                GUIAssignData();
            else
            {
                //m_scrollPosition =
                //    GUILayout.BeginScrollView(m_scrollPosition);

                //GUILayout.Label("ASSIGNED!");
                GUIShowTextData();


                //GUILayout.EndScrollView();
            }
            

        }

        private void GUIAssignData()
        {
            GUILayout.BeginVertical();
            GUILayout.Label("Welcome!");

            if (GUILayout.Button("Create TextData"))
                functionality.CreateNewTextData();
            if (GUILayout.Button("Open TextData"))
                functionality.OpenTextData();

            GUILayout.Label("Drag n Drop:");
            storage.textData = EditorGUILayout.ObjectField(storage.textData, 
                typeof(TextData), false) as TextData;

            GUILayout.EndVertical();
        }

        private void GUIShowTextData()
        {
            

            //Keys
            storage.UpdateKeyList();

            m_scrollPosition =
                    GUILayout.BeginScrollView(m_scrollPosition);

            if (GUILayout.Button("ReturnToMainMenu", 
                GUILayout.ExpandWidth(false)))
            {
                storage.textData = null;
                GUILayout.EndScrollView();

                return;
            }

            //GUIRedoButtonsView();

            GUIDataKeyView();

            //Content
            storage.UpdateContentList();

            GUIShowContentView();

            GUILayout.EndScrollView();

            

        }

        private void GUIRedoButtonsView() 
        {
            GUILayout.BeginHorizontal();
            GUIHelper.ShowDisableableGUI(() => 
            {
                if (GUILayout.Button("Undo", GUILayout.ExpandWidth(false))) 
                {
                    functionality.PerformLocalUndo();
                    GUILayout.BeginScrollView(Vector2.zero);
                }
            }, functionality.UndoCount > 0);
            GUIHelper.ShowDisableableGUI(() =>
            {
                if (GUILayout.Button("Redo", GUILayout.ExpandWidth(false)))
                {

                    functionality.PerformLocalRedo();

                    GUILayout.BeginScrollView(Vector2.zero);
                    
                }
            }, functionality.RedoCount > 0);
            GUILayout.EndHorizontal();
        }
            
        private void GUIDataKeyView()
        {
            GUILayout.Label("Keys", GUIHelper.HeaderLabel);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("CreateNewKey", GUILayout.ExpandWidth(false)))
            {
                
                CreateNewKeyWindow.NewInstance(this);
                GUILayout.BeginScrollView(Vector2.zero);
            }
            GUIHelper.ShowDisableableGUI(()=> 
            {
                if (GUILayout.Button("DeleteKey", GUILayout.ExpandWidth(false)))
                {
                    functionality.DeleteKey();
                }
            }, !(storage.keysList.Count == 1 
                && storage.curKeyVal == TextDataDictionary.DefaultKey));

            if (GUILayout.Button("EditKey", GUILayout.ExpandWidth(false)))
            {

                EditKeyWindow.NewInstance(this);
                GUILayout.BeginScrollView(Vector2.zero);
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            EditorGUI.BeginChangeCheck();
            storage.popupIndex = EditorGUILayout.Popup(storage.popupIndex, 
                storage.keysList?.ToArray());

           
            storage.UpdateCurKeyVal();

            if (EditorGUI.EndChangeCheck()) 
            {
                m_newKeyVal = string.Empty;
                storage.contentIndex = 0;
            }
            //if (m_newKeyVal == string.Empty)
            //    m_newKeyVal = storage.curKeyVal;

            //GUILayout.BeginHorizontal();
            //GUILayout.Label("EditKey:", GUILayout.ExpandWidth(false));

            //m_newKeyVal = GUILayout.TextField(m_newKeyVal);
            //GUILayout.EndHorizontal();
            //if (GUILayout.Button("Apply", GUILayout.ExpandWidth(false)))
            //{
            //}
        }

        private void GUIShowContentView() 
        {

            
            storage.CheckContentIndex();

            GUILayout.Label("Content", GUIHelper.HeaderLabel);
            GUILayout.Label($"Index [{storage.contentIndex}] of " +
                $"[{storage.contentList.Count - 1}]");

            GUILayout.BeginHorizontal();
            if (storage.contentIndex > 0)
            {

                if (GUILayout.Button("<<", GUILayout.ExpandWidth(false)))
                {
                    storage.contentIndex = 0;
                }

                if (GUILayout.Button("<", GUILayout.ExpandWidth(false)))
                {
                    --storage.contentIndex;
                }

            }
            else
                GUILayout.Space(57);

            if (storage.contentIndex < storage.contentList.Count - 1)
            {
                if (GUILayout.Button(">", GUILayout.ExpandWidth(false)))
                {
                    ++storage.contentIndex;
                }
                if (GUILayout.Button(">>", GUILayout.ExpandWidth(false)))
                {
                    storage.contentIndex = storage.contentList.Count - 1;
                }

            }
            else
                GUILayout.Space(54);

            if (GUILayout.Button("NewPage", GUILayout.ExpandWidth(false)))
            {
                functionality.CreateNewPage();
            }
            GUIHelper.ShowDisableableGUI(()=> 
            {
                if (GUILayout.Button("DeletePage", GUILayout.ExpandWidth(false)))
                {
                    functionality.DeletePage();
                }
            }, storage.contentList.State != TextList.ListState.Empty);
            
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            m_gotoIndex = EditorGUILayout.IntField(m_gotoIndex, 
                EditorStyles.numberField, GUILayout.ExpandWidth(false), 
                GUILayout.Width(50));
            if (GUILayout.Button("Go to", GUILayout.ExpandWidth(false)) &&
               m_gotoIndex >= 0 && m_gotoIndex < storage.contentList.Count)
            {
                storage.contentIndex = m_gotoIndex;
            }
            GUILayout.EndHorizontal();

            m_scrollPositionIn = GUILayout.BeginScrollView(m_scrollPositionIn);

            EditorGUI.BeginChangeCheck();
            m_newContentVal =
                GUILayout.TextArea(storage.contentList[storage.contentIndex],
                EditorStyles.textArea);
            if (EditorGUI.EndChangeCheck()) 
            {
                //functionality.RecordTextData("Changed content in " +
                //    $"[{storage.curKeyVal}][{storage.contentIndex}]");

                //storage.contentList[storage.contentIndex] = m_newContentVal;
                functionality.EditPage(m_newContentVal);
            }

            GUILayout.EndScrollView();
        }

        
        
    }
}


