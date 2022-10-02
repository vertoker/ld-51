using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public class Rocket : MonoBehaviour
    {
        [SerializeField] private float durationRotate = 0.1f;
        [SerializeField] private float launchPower = 100f;
        private Transform target;
        [SerializeField] private Transform tr;
        [SerializeField] private Rigidbody rb;

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
        private void FixedUpdate()
        {
            tr.LookAt(target);
            Vector3 direction = target.position - tr.position;
            float multiply = launchPower / Mathf.Abs(direction.magnitude);
            rb.AddForce(direction * multiply, ForceMode.Force);
        }
    }
}