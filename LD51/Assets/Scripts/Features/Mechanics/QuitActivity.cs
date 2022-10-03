using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Mechanics
{
    public class QuitActivity : Activatable
    {
        public override void Activate()
        {
#if UNITY_EDITOR
            Debug.Log("Выход из игры");
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}