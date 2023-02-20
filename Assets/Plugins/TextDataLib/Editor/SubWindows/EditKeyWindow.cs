using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TextDataLib.Editor 
{
    public class EditKeyWindow : EditorWindow
    {
        private TextDataEditor m_editor;
        private string m_keyVal = string.Empty;
        private string m_errKey;
        private bool m_errorFlag = false;
        public static EditKeyWindow NewInstance(TextDataEditor editor)
        {
            EditKeyWindow instance = (EditKeyWindow)GetWindow(
                typeof(EditKeyWindow), false, "EditKey");

            instance.Init(editor);
            instance.ShowModalUtility();

            return instance;
        }
        private void Init(TextDataEditor editor)
        {
            m_editor = editor;
        }

        private void OnGUI()
        {
            if (m_editor == null)
                return;

            GUILayout.Label($"OldKeyValue: {m_editor.storage.curKeyVal}");
            GUILayout.BeginHorizontal();
            GUILayout.Label("NewKeyValue:", GUILayout.ExpandWidth(false));
            m_keyVal = GUILayout.TextField(m_keyVal);
            GUILayout.EndHorizontal();

            GUILayout.BeginVertical();
            if (GUILayout.Button("Apply", GUILayout.ExpandWidth(false)))
            {
                EditKey();
            }
            if (m_errorFlag)
                GUILayout.Label($"key {m_errKey} already exists",
                    GUIHelper.ErrorLabel);
            GUILayout.EndVertical();
        }

        private void EditKey()
        {

            if (m_editor.storage.textData.Data.ContainsKey(m_keyVal))
            {
                
                m_errKey = m_keyVal;
                m_errorFlag = true;

                return;
            }
           
            m_editor.functionality.EditKey(m_keyVal);

            this.Close();

        }
    }
}
