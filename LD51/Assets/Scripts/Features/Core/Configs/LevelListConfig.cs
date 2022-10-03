using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Features.Core.Config 
{
    [CreateAssetMenu(fileName = "LevelListConfig",
    menuName = "Config/LevelListConfig", order = 0)]
    public class LevelListConfig : ScriptableObject
    {
        [SerializeField]
        public List<LevelConfig> levelList;
    }
}

