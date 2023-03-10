using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Features.Core.Config;
using UnityEngine.SceneManagement;

namespace Features.Core 
{
    public class CoreData
    {
        public float TimerMultyplier => m_timerMultiplier;
        public float SlowedTimeScale => m_slowedTimeScale;

        public string MainMenuScene => m_mainMenuScene;
        public float MaxStamina => m_maxStamina;
        public float StaminaRestoringMultiplier => m_staminaMultiplyer;
        
        private CoreConfig config;

        private float m_startTime;
        private float m_slowedTimeScale;
        private float m_timerMultiplier;
        private string m_mainMenuScene;
        private float m_maxStamina;
        private float m_staminaMultiplyer;


        public int currentSceneIndex;
        public int currentLevel;

        public float stamina;

        public float timer;
        public float timeScale;

        public bool isPaused;
        public bool isSlowedDown;

        [Inject]
        public CoreData(CoreConfig coreConfig) 
        {
            config = coreConfig;

            m_startTime = config.startTime;
            m_slowedTimeScale = config.slowedTimeScale;
            m_timerMultiplier = config.timerMultiplier;
            m_mainMenuScene = config.mainMenuSceneName;
            m_maxStamina = config.maxStamina;
            m_staminaMultiplyer = config.staminaRestoringMultiplyer;


            Init();

            currentLevel = 1;
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        }

        public void Init() 
        {
            stamina = m_maxStamina;
            timer = m_startTime;
            timeScale = 1f;
            isPaused = false;
            isSlowedDown = false;
        }

    }
}

