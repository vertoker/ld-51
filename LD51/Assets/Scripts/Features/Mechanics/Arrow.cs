using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private Vector3 rotateSpeed = new Vector3();
        [SerializeField] private float swayingTime = 1f;
        [SerializeField] private Vector3 swayingCoord = new Vector3(0f, 1f, 0f);

        private bool moveUp = true;
        private Vector3 originPosition;
        private Transform self;

        private void Awake()
        {
            self = transform;
            originPosition = self.position;
        }

        private void FixedUpdate()
        {
            self.Rotate(rotateSpeed * Time.timeScale, Space.Self);

            if (moveUp)
            {
                self.position = Vector3.Lerp(self.position, originPosition + swayingCoord, swayingTime * Time.timeScale);
                if (Vector3.Distance(self.position, originPosition + swayingCoord) < 0.1f)
                    moveUp = false;
            }
            else
            {
                self.position = Vector3.Lerp(self.position, originPosition - swayingCoord, swayingTime * Time.timeScale);
                if (Vector3.Distance(self.position, originPosition - swayingCoord) < 0.1f)
                    moveUp = true;
            }
        }
    }
}