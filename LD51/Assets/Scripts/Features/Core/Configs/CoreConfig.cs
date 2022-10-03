using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Core.Config 
{
    [CreateAssetMenu(fileName = "CoreConfig",
    menuName = "Config/CoreConfig", order = 0)]
    public class CoreConfig : ScriptableObject
    {
        public float startTime;
        public float slowedTimeScale;
        public float timerMultiplier;
    }

}
