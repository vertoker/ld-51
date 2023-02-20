using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Mechanics
{
    public class Borders : MonoBehaviour
    {
        [SerializeField] private Vector3 sizeBorders = new Vector3(100f, 100f, 100f);
        [SerializeField] private float borderThickness = 5f;

#if UNITY_EDITOR
        [CustomEditor(typeof(Borders))]
        public class BordersEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                Borders b = (Borders)target;
                base.OnInspectorGUI();

                if (b.transform.childCount < 6)
                    return;

                var b1 = b.transform.GetChild(0);
                var b2 = b.transform.GetChild(1);
                var b3 = b.transform.GetChild(2);
                var b4 = b.transform.GetChild(3);
                var b5 = b.transform.GetChild(4);
                var b6 = b.transform.GetChild(5);

                b1.localPosition = new Vector3((b.sizeBorders.x + b.borderThickness) / 2f, 0f, 0f);
                b1.localScale = new Vector3(b.borderThickness, b.sizeBorders.y + b.borderThickness * 2f, b.sizeBorders.z + b.borderThickness * 2f);
                b2.localPosition = new Vector3((-b.sizeBorders.x - b.borderThickness) / 2f, 0f, 0f);
                b2.localScale = new Vector3(b.borderThickness, b.sizeBorders.y + b.borderThickness * 2f, b.sizeBorders.z + b.borderThickness * 2f);

                b3.localPosition = new Vector3(0f, 0f, (b.sizeBorders.z + b.borderThickness) / 2f);
                b3.localScale = new Vector3(b.sizeBorders.x + b.borderThickness * 2f, b.sizeBorders.y + b.borderThickness * 2f, b.borderThickness);
                b4.localPosition = new Vector3(0f, 0f, (-b.sizeBorders.z - b.borderThickness) / 2f);
                b4.localScale = new Vector3(b.sizeBorders.x + b.borderThickness * 2f, b.sizeBorders.y + b.borderThickness * 2f, b.borderThickness);

                b5.localPosition = new Vector3(0f, (b.sizeBorders.y + b.borderThickness) / 2f, 0f);
                b5.localScale = new Vector3(b.sizeBorders.x + b.borderThickness * 2f, b.borderThickness, b.sizeBorders.z + b.borderThickness * 2f);
                b6.localPosition = new Vector3(0f, (-b.sizeBorders.y - b.borderThickness) / 2f, 0f);
                b6.localScale = new Vector3(b.sizeBorders.x + b.borderThickness * 2f, b.borderThickness, b.sizeBorders.z + b.borderThickness * 2f);
            }
        }
#endif
    }
}