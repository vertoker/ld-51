using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TextDataLib.Editor
{
    [CustomEditor(typeof(TextData))]
    public class TextDataInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            TextData textData = (TextData)target;

            if (GUILayout.Button("Open in TextDataEditor")) 
            {
                TextDataEditor.GetNewWindow(textData);
            }
        }
    }
}
