using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public class Rocket : MonoBehaviour
    {
        [SerializeField] private float launchPower = 100f;
        private CallExplosion effect;
        private Transform target;

        private bool active = false;
        private Transform tr;
        private Rigidbody rb;

        private void Awake()
        {
            tr = GetComponent<Transform>();
            rb = GetComponent<Rigidbody>();
        }
        public void SetTarget(Transform target, CallExplosion effect)
        {
            active = true;
            this.target = target;
            this.effect = effect;
        }
        private void FixedUpdate()
        {
            if (!active)
                return;
            tr.LookAt(target);
            Vector3 direction = target.position - tr.position;
            float multiply = launchPower / Mathf.Abs(direction.magnitude);
            rb.AddForce(direction * multiply, ForceMode.Force);
        }

        private void OnCollisionEnter(Collision collision)
        {
            active = false;
            effect.Invoke(tr.position, gameObject);
        }
    }
}