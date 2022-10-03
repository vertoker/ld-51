using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public class DeathCollider : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                Debug.Log("Death");
            }
        }
    }
}