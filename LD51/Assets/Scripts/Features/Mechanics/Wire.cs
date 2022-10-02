using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Mechanics
{
    public class Wire : Activatable
    {
#if UNITY_EDITOR
        [SerializeField] private List<Vector3> positions = new List<Vector3>();
#endif
        private List<Transform> objects = new List<Transform>();
        [SerializeField] private float wireThickness = 0.1f;
        [SerializeField] private GameObject instance;

        public override void Activate()
        {

        }
        public override void Deactivate()
        {

        }

#if UNITY_EDITOR
        [CustomEditor(typeof(Wire))]
        public class WireEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                Wire wire = (Wire)target;
                var obj = Selection.activeGameObject;

                if (GUILayout.Button("Добавить эту позицию в список"))
                {
                    if (obj != null)
                        wire.positions.Add(obj.transform.position);
                }

                base.OnInspectorGUI();

                int existCount = obj.transform.childCount;
                int mustNeedCount = wire.positions.Count - 1;

                if (wire.positions.Count < 2 || wire.instance == null)
                    return;

                if (existCount < mustNeedCount)
                {
                    for (int i = 0; i < mustNeedCount - existCount; i++)
                    {
                        var next = Instantiate(wire.instance, wire.transform);
                        wire.objects.Add(next.transform);
                        next.name = "WirePart";
                    }
                }
                else if (existCount > mustNeedCount)
                {
                    for (int i = 0; i < existCount - mustNeedCount; i++)
                    {
                        Destroy(wire.objects[0].gameObject);
                        wire.objects.RemoveAt(0);
                    }
                }

                for (int i = 0; i <= existCount; i++)
                {
                    Vector3 p1 = wire.positions[i];
                    Vector3 p2 = wire.positions[i + 1];
                    wire.objects[i].position = new Vector3((p1.x + p2.x) / 2, (p1.y + p2.y) / 2, (p1.z + p2.z) / 2);
                    wire.objects[i].localScale = new Vector3(wire.wireThickness, wire.wireThickness, Mathf.Abs(p1.z - p2.z));
                    wire.objects[i].LookAt(p2);
                }
            }
        }
#endif
    }
}