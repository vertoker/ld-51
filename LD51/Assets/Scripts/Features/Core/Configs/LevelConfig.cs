using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Core.Config 
{
    [CreateAssetMenu(fileName = "LevelConfig",
    menuName = "Config/LevelConfig", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        public Vector3 playerSpawnPosition;
    }
}

