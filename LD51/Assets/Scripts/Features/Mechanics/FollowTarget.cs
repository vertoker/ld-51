using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void FixedUpdate()
        {
            transform.position = target.position;
        }
    }
}