using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Mechanics
{
    public class Button : MonoBehaviour
    {
        [SerializeField] private bool isOn = false;
        [SerializeField] private bool isInstantly = true;
        private Transform _bottom, _active;
#if UNITY_EDITOR
        public float OFFSET_SIZE = 0.5f;
#endif

        //[SerializeField] private List<> isInstantly = true;

        private void Awake()
        {
            _bottom = transform.GetChild(0);
            _active = transform.GetChild(1);
        }

        public void Press()
        {
            if (isOn)
                return;
            isOn = true;
            _active.DOLocalMoveY(0.11f, 0.3f);
        }
        public void Unpress()
        {
            if (!isOn || isInstantly)
                return;
            isOn = false;
            _active.DOLocalMoveY(0.3f, 0.3f);
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Button))]
    public class ButtonEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Button button = (Button)target;

            var parent = Selection.activeGameObject.transform;
            var bottom = Selection.activeGameObject.transform.GetChild(0);
            var active = Selection.activeGameObject.transform.GetChild(1);

            base.OnInspectorGUI();
            float allButX = parent.lossyScale.x;
            float allButZ = parent.lossyScale.z;
            float localSizeButX = (allButX - button.OFFSET_SIZE) / allButX;
            float localSizeButZ = (allButZ - button.OFFSET_SIZE) / allButZ;

            bottom.localScale = new Vector3(1f, bottom.localScale.y, 1f);
            active.localScale = new Vector3(localSizeButX, active.localScale.y, localSizeButZ);
        }
    }
#endif
}