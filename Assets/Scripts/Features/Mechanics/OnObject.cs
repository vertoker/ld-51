using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Mechanics
{
    public class OnObject : MonoBehaviour
    {
        private Transform parent, self;
        private Vector3 scaleSelf;

        private void Awake()
        {
            parent = GetComponent<Transform>();
            self = parent.GetChild(0);
            scaleSelf = parent.lossyScale;
            self.localScale = new Vector3(1 / scaleSelf.x, 1 / scaleSelf.y, 1 / scaleSelf.z);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("NotInteractable"))
                other.transform.SetParent(self, true);
        }
        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("NotInteractable"))
                other.transform.SetParent(null, true);
        }
    }
}