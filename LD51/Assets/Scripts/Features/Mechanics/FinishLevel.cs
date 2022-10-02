using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public class FinishLevel : Activatable
    {
        public override void Activate()
        {
            Debug.Log("Finish Level");
        }
    }
}