using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Features.Character.Service;
using Features.Core.Config;
using Features.Character.Views;


namespace Features.Core.Mono
{
    public class CoreManager : MonoBehaviour
    {
        private CoreData data;
        private CoreEvents events;
        //private CharacterSpawnService playerSpawner;
        private LevelListConfig levelList;

        private Coroutine timerCoroutine;

        //private bool m_isGameStarted = false;
        private int m_levelListCount;
        private int m_sceneToLoad;

        private bool m_isLoading = false;
        private bool m_isRestarting = false;

        private Coroutine useStaminaCoroutine;
        private Coroutine restoreStaminaCoroutine;

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
            /*CharacterSpawnService characterSpawnService,*/ 
            LevelListConfig levelListConfig)
        {
            data = coreData;
            events = coreEvents;
            //playerSpawner = characterSpawnService;
            levelList = levelListConfig;

            m_levelListCount = levelListConfig.LevelCount;

           

           

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
            events.OnReturnToMainMenu += ReturnToMainMenu;
            events.OnSlowdownTimePressed += ActivateSlowdown;
        }

        private void OnDisable()
        {
            events.OnGameOver -= GameOver;
            events.OnLevelComplete -= LoadNextLevel;
            events.OnReturnToMainMenu -= ReturnToMainMenu;
            events.OnSlowdownTimePressed -= ActivateSlowdown;
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log($"<color=\"blue\">{data.stamina}</color>");
            //if (!m_isGameStarted && Input.GetKeyDown(KeyCode.M))
            //{
            //    m_isGameStarted = true;
            //    GameInit();
            //}

        }

        private void OnDestroy()
        {
            StopTimer();
        }

        private void GameInit() 
        {
            UpdateSceneToLoad();
            SceneManager.LoadScene(m_sceneToLoad, 
                LoadSceneMode.Additive);
            //SceneManager.MoveGameObjectToScene(
            //    playerSpawner.CharacterView.gameObject, 
            //    SceneManager.GetSceneByBuildIndex( m_sceneToLoad));
            //playerSpawner.SpawnCharacter(m_curLevelConfig.playerSpawnPosition);
         

            StartTimer();
        }

        private void LoadNextLevel() 
        {
            if (m_isLoading)
                return;
            m_isLoading = true;

            //playerSpawner.DeactivateCharacter();
            StopTimer();

            ++data.currentLevel;
            data.Init();

            bool isLevel = UpdateSceneToLoad();
            if (isLevel)
            {
                //playerSpawner.
                //    TeleportCurrentTo(m_curLevelConfig.playerSpawnPosition);
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

            //playerSpawner.DeactivateCharacter();

            StopTimer();

            data.Init();
            events.OnSlowdownTimePressed?.Invoke(false);

            //playerSpawner.TeleportCurrentTo(m_curLevelConfig.playerSpawnPosition);
            SceneManager.UnloadSceneAsync(m_sceneToLoad);

            SceneManager.sceneUnloaded -= AfterSceneUnload;
            SceneManager.sceneUnloaded += AfterSceneUnload;
        }

        private void AfterSceneUnload(Scene scene) 
        {
            SceneManager.LoadScene(m_sceneToLoad, LoadSceneMode.Additive);
            //SceneManager.MoveGameObjectToScene(
            //    playerSpawner.CharacterView.gameObject,
            //    SceneManager.GetSceneByBuildIndex(m_sceneToLoad));
            //playerSpawner.ReactivateCharacter();

            StartTimer();

            m_isRestarting = false;
            m_isLoading = false;

            SceneManager.sceneUnloaded -= AfterSceneUnload;

        }

        private void ReturnToMainMenu() 
        {
            SceneManager.LoadScene(data.MainMenuScene, LoadSceneMode.Single);
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
                float multiplier = Time.timeScale == 1 ? 1 : data.TimerMultyplier;
                data.timer -= Time.deltaTime * Time.timeScale * multiplier;

                if (data.timer < 0f)
                    data.timer = 0f;

                yield return null;
            }

            events.OnGameOver?.Invoke();
            
        }


        private void GameOver() 
        {
            RestartLevel();
           
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

        private void ActivateSlowdown(bool isSlowedDown) 
        {
            if (isSlowedDown)
            {
                StopRestoringStamina();
                StartUsingStamina();
            }
            else 
            {
                StopUsingStamina();
                StartRestoringStamina();
            }
        }

        private IEnumerator DoUseStamina()
        {
            while (data.stamina > 0f)
            {
                float dec = (Time.timeScale > 0) ? Time.unscaledDeltaTime :0;

                data.stamina -= dec;
                yield return null;
            }
            
            data.stamina = 0f;

            events.InvokeSlowdown();

        }

        private void StartUsingStamina()
        {
            if (useStaminaCoroutine != null)
                return;
            //StopUsingStamina();
            useStaminaCoroutine = StartCoroutine(DoUseStamina());
        }

        private void StopUsingStamina()
        {
            if (useStaminaCoroutine == null)
                return;

            StopCoroutine(useStaminaCoroutine);
            useStaminaCoroutine = null;
        }

        private IEnumerator DoRestoreStamina()
        {
            while (data.stamina < data.MaxStamina)
            {
                float inc = (Time.timeScale > 0) ? Time.unscaledDeltaTime : 0;

                data.stamina += inc * data.StaminaRestoringMultiplier;
                yield return null;
            }

            data.stamina = data.MaxStamina;

            // events.InvokeSlowdown();
            StopRestoringStamina();
        }

        private void StartRestoringStamina()
        {
            if (restoreStaminaCoroutine != null)
                return;
           // StopRestoringStamina();
            restoreStaminaCoroutine = StartCoroutine(DoRestoreStamina());
        }

        private void StopRestoringStamina()
        {
            if (restoreStaminaCoroutine == null)
                return;

            StopCoroutine(restoreStaminaCoroutine);
            restoreStaminaCoroutine = null;
        }

    }

}
