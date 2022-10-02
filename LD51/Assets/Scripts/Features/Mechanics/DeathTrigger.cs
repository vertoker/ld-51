using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public class DeathTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("Death");
            }
        }
    }
}