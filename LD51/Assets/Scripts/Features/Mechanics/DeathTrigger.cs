using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public class DeathTrigger : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Death");
            }
        }
    }
}