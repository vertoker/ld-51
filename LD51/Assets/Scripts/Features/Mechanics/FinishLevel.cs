using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Features.Core;


namespace Mechanics
{
    public class FinishLevel : Activatable
    {
        [Inject]
        private CoreEvents events;

        public override void Activate()
        {
            events.OnLevelComplete?.Invoke();
            Debug.Log("<color=\"green\">лнкндеж!</color>");
        }
    }
}