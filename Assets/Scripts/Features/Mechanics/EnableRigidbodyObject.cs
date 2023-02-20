using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public class EnableRigidbodyObject : Activatable
    {
        [SerializeField] private Rigidbody rb;

        public override void Activate()
        {
            rb.isKinematic = false;
        }
    }
}