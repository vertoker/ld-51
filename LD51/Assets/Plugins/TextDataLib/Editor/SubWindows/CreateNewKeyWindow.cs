using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TextDataLib.Editor 
{
    public class CreateNewKeyWindow : EditorWindow
    {
        private TextDataEditor m_editor;
        private string m_keyVal = string.Empty;
        private string m_errKey;
        private bool m_errorFlag = false;
        public static CreateNewKeyWindow NewInstance(TextDataEditor editor)
        {
            CreateNewKeyWindow instance = (CreateNewKeyWindow)GetWindow(
                typeof(CreateNewKeyWindow), false, "CreateKey");

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
            GUILayout.BeginHorizontal();
            GUILayout.Label("NewKeyValue:", GUILayout.ExpandWidth(false));
            m_keyVal = GUILayout.TextField(m_keyVal);
            GUILayout.EndHorizontal();

            GUILayout.BeginVertical();
            if (GUILayout.Button("Create", GUILayout.ExpandWidth(false)))
            {
                CreateKey();
            }
            if (m_errorFlag)
                GUILayout.Label($"key {m_errKey} already exists",
                    GUIHelper.ErrorLabel);
            GUILayout.EndVertical();
        }

        private void CreateKey()
        {

            if (m_editor.storage.textData.Data.ContainsKey(m_keyVal))
            {
                
                m_errKey = m_keyVal;
                m_errorFlag = true;

                return;
            }
           
            m_editor.functionality.CreateKey(m_keyVal);

            this.Close();

        }
    }
}
