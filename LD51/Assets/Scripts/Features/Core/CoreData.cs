using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Core 
{
    public class CoreData
    {
        private const float c_StartTime = 10f;
        public const float c_slowedTimeScale = 0.15f;

        public int currentSceneIndex;
        public int currentLevel;

        public float timer;
        public float timeScale;

        public bool isPaused;
        public bool isSlowedDown;

        public CoreData() 
        {
            Init();

            currentLevel = 1;
            currentSceneIndex =
               UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        }

        public void Init() 
        {
            timer = c_StartTime;
            timeScale = 1f;
            isPaused = false;
            isSlowedDown = false;
        }

    }
}

