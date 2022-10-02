using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public class ButtonActivate : MonoBehaviour
    {
        private Button parent;
        private int countOfTouching = 0;

        private void Awake()
        {
            parent = transform.parent.GetComponent<Button>();
        }
        private void OnCollisionEnter(Collision collision)
        {
            countOfTouching++;
            parent.Press();
        }
        private void OnCollisionExit(Collision collision)
        {
            countOfTouching--;
            if (countOfTouching == 0)
                parent.Unpress();
        }
    }
}