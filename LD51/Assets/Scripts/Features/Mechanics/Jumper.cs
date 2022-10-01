using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public class Jumper : MonoBehaviour
    {
        [SerializeField] private float powerBoost = 20f;

        private void Awake()
        {
            Debug.Log(transform.up);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Rigidbody rb))
            {
                rb.velocity = transform.up * powerBoost;
            }
        }
    }
}