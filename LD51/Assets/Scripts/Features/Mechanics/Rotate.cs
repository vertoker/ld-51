using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] private Vector3 speed;
        private Transform self;

        private void Awake()
        {
            self = transform;
        }
        private void FixedUpdate()
        {
            self.Rotate(speed);
        }
    }
}