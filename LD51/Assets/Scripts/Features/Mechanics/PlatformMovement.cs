using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Mechanics
{
    public class PlatformMovement : MonoBehaviour
    {
        [SerializeField] private List<Vector3> positions;
        [SerializeField] private int currentTargetPosition = 0;
        [SerializeField] private float speed = 0.1f;
        private Transform _self;

        private void Awake()
        {
            _self = GetComponent<Transform>();
        }

        private void FixedUpdate()
        {
            var nextPosition = Vector3.MoveTowards(_self.position, positions[currentTargetPosition], speed);
            if (_self.position == nextPosition)
            {
                currentTargetPosition++;
                if (currentTargetPosition == positions.Count)
                    currentTargetPosition = 0;
            }
            _self.position = nextPosition;
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(PlatformMovement))]
        public class PlatformMovementEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                PlatformMovement platform = (PlatformMovement)target;

                if (GUILayout.Button("Добавить эту позицию в список"))
                {
                    var obj = Selection.activeGameObject;
                    if (obj != null)
                        platform.positions.Add(obj.transform.position);
                }

                base.OnInspectorGUI();
            }
        }
#endif
    }
}