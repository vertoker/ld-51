using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Features.Character.Service;
using Features.Core.Config;
using Features.Character.Views;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Features.Core.Mono
{
    public class CoreManager : MonoBehaviour
    {
        private CoreData data;
        private CoreEvents events;
        private CharacterSpawnService playerSpawner;
        private LevelListConfig levelList;

        private LevelConfig m_curLevelConfig;

        private Coroutine timerCoroutine;

        //private bool m_isGameStarted = false;
        private int m_levelListCount;
        private int m_sceneToLoad;

        private bool m_isLoading = false;
        private bool m_isRestarting = false;

        //private int SceneToLoad 
        //{ 
        //    get 
        //    {
               
        //    }
        //}

        
        

        //[SerializeField]
        //private Volume vol;
        //private DepthOfField dof;

        [Inject]
        void Initialize(CoreData coreData, CoreEvents coreEvents,
            CharacterSpawnService characterSpawnService, 
            LevelListConfig levelListConfig)
        {
            data = coreData;
            events = coreEvents;
            playerSpawner = characterSpawnService;
            levelList = levelListConfig;

            m_curLevelConfig = levelListConfig.levelList[0];
            m_levelListCount = levelListConfig.levelList.Count;

           

           

           GameInit();
        }



        // Start is called before the first frame update
        void Start()
        {
           

            //if (vol.profile.TryGet<DepthOfField>(out dof)) 
            //{
            //    dof.active = false;
            //}
        }

        private void OnEnable()
        {
            events.OnGameOver += GameOver;
            events.OnLevelComplete += LoadNextLevel;
        }

        private void OnDisable()
        {
            events.OnGameOver -= GameOver;
            events.OnLevelComplete -= LoadNextLevel;
        }

        // Update is called once per frame
        void Update()
        {

            //if (!m_isGameStarted && Input.GetKeyDown(KeyCode.M))
            //{
            //    m_isGameStarted = true;
            //    GameInit();
            //}

        }

        private void GameInit() 
        {
            UpdateSceneToLoad();
            SceneManager.LoadScene(data.currentSceneIndex + data.currentLevel, 
                LoadSceneMode.Additive);

            playerSpawner.SpawnCharacter(m_curLevelConfig.playerSpawnPosition);
         

            StartTimer();
        }

        private void LoadNextLevel() 
        {
            if (m_isLoading)
                return;
            m_isLoading = true;

            playerSpawner.DeactivateCharacter();
            StopTimer();
            ++data.currentLevel;
            if (data.currentLevel <= m_levelListCount)
                m_curLevelConfig = levelList.levelList[data.currentLevel-1];
            data.Init();

            bool isLevel = UpdateSceneToLoad();
            if (isLevel)
            {
                playerSpawner.
                    TeleportCurrentTo(m_curLevelConfig.playerSpawnPosition);
                SceneManager.UnloadSceneAsync(m_sceneToLoad-1);

                SceneManager.sceneUnloaded -= AfterSceneUnload;
                SceneManager.sceneUnloaded += AfterSceneUnload;
            }
            else 
            {
                SceneManager.LoadScene(m_sceneToLoad, LoadSceneMode.Single);
            }
        }

        private void RestartLevel() 
        {
            if (m_isRestarting)
                return;

            m_isRestarting = true;

            playerSpawner.DeactivateCharacter();

            StopTimer();

            data.Init();

            playerSpawner.TeleportCurrentTo(m_curLevelConfig.playerSpawnPosition);
            SceneManager.UnloadSceneAsync(m_sceneToLoad);

            SceneManager.sceneUnloaded -= AfterSceneUnload;
            SceneManager.sceneUnloaded += AfterSceneUnload;
        }

        private void AfterSceneUnload(Scene scene) 
        {
            SceneManager.LoadScene(m_sceneToLoad, LoadSceneMode.Additive);

            playerSpawner.ReactivateCharacter();

            StartTimer();

            m_isRestarting = false;
            m_isLoading = false;

        }

        private void StartTimer() 
        {
            if (timerCoroutine != null)
                return;

            timerCoroutine = StartCoroutine(DoTimer());
        }

        private void StopTimer() 
        {
            if (timerCoroutine == null)
                return;

            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }

        private IEnumerator DoTimer() 
        {
            while (data.timer > 0f) 
            {
                data.timer -= Time.deltaTime * Time.timeScale;

                if (data.timer < 0f)
                    data.timer = 0f;

                yield return null;
            }

            events.OnGameOver?.Invoke();
            
        }


        private void GameOver() 
        {
            RestartLevel();
            Debug.Log($"Ìîðøåíøòåðí — ÏÎÑÎÑÈ {m_isRestarting}");
        }

        

        private bool UpdateSceneToLoad() 
        {
            m_sceneToLoad = data.currentSceneIndex + data.currentLevel;
            bool isLevel = true;
            if (data.currentLevel <= m_levelListCount)
            {
                return isLevel;
            }

            isLevel = false;
            if (!(m_sceneToLoad <= SceneManager.sceneCount - 1))
            {
                m_sceneToLoad = data.currentSceneIndex;
            }
            return isLevel;
        }

        
    }

}
