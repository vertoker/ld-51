using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Features.Core;

namespace Mechanics
{
    public class DeathCollider : MonoBehaviour
    {
        //[Inject]
        //private CoreEvents events;
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                CoreEvents.Instance.OnGameOver?.Invoke();
            }
        }
    }
}