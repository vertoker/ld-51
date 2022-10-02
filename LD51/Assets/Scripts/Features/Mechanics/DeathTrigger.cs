using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Features.Core;

namespace Mechanics
{
    public class DeathTrigger : MonoBehaviour
    {
        [Inject]
        private CoreEvents events; 

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                events.OnGameOver?.Invoke();
            }
        }
    }
}