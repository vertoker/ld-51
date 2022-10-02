using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TextDataLib.Editor
{
    public class TextDataCreate
    {
        [MenuItem("Assets/Create/TextData")]
        public static TextData Create()
        {
            TextData asset =
                ScriptableObject.CreateInstance<TextData>();

            asset.Data = new TextDataDictionary();
           

            string absPath = EditorUtility.
                SaveFilePanel("Save TextData object",
                Application.dataPath, "NewTextData.asset", "asset");
            if (!absPath.StartsWith(Application.dataPath))
            {

                throw new System.Exception(
                    "TextData file should be located inside " +
                    "the project folder!");

            }

            string relPath = absPath.
                    Substring(Application.dataPath.Length - "Assets".Length);

            AssetDatabase.
                CreateAsset(asset, relPath);
            AssetDatabase.SaveAssets();

            return asset;
        }
    }

}
