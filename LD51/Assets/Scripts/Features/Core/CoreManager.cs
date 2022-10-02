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

        private bool m_isGameStarted = false;

        
        private Rigidbody playerPhysics;

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

            playerSpawner.SpawnCharacter(m_curLevelConfig.playerSpawnPosition);

            playerPhysics = GameObject.FindWithTag("Player").
                GetComponent<Rigidbody>();

            playerPhysics.gameObject.SetActive(false);

           // GameInit();
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
            events.OnTimersUp += GameOver;
        }

        private void OnDisable()
        {
            events.OnTimersUp -= GameOver;
        }

        // Update is called once per frame
        void Update()
        {

            if (!m_isGameStarted && Input.GetKeyDown(KeyCode.M))
            {
                m_isGameStarted = true;
                GameInit();
            }

        }

        private void GameInit() 
        {
            SceneManager.LoadScene(data.currentSceneIndex + data.currentLevel, 
                LoadSceneMode.Additive);

            //playerSpawner.SpawnCharacter(m_curLevelConfig.playerSpawnPosition);
            playerPhysics.gameObject.SetActive(true);

            StartTimer();
        }

        private void LoadNextLevel() 
        {

        }

        private void RestartLevel() 
        {

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

            events.OnTimersUp?.Invoke();
            
        }


        private void GameOver() 
        {
            StopTimer();
        }

        void SpawnPlayer() 
        {

        }

        //void CoolEffect() 
        //{
        //    dof.active = data.isSlowedDown;
        //}
    }

}
