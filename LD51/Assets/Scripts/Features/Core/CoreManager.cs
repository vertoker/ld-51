using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Features.Character.Service;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Features.Core 
{
    public class CoreManager : MonoBehaviour
    {
        private CoreData data;
        private CoreEvents events;
        private CharacterSpawnService playerSpawner;

        [SerializeField]
        private Volume vol;
        private DepthOfField dof;

        [Inject]
        void Initialize(CoreData coreData, CoreEvents coreEvents,
            CharacterSpawnService characterSpawnService)
        {
            data = coreData;
            events = coreEvents;
            playerSpawner = characterSpawnService;
        }



        // Start is called before the first frame update
        void Start()
        {
            playerSpawner.SpawnCharacter(Vector3.zero);

            if (vol.profile.TryGet<DepthOfField>(out dof)) 
            {
                dof.active = false;
            }
        }

        private void OnEnable()
        {
            events.OnSlowdownTimePress += CoolEffect;
        }

        private void OnDisable()
        {
            events.OnSlowdownTimePress -= CoolEffect;
        }

        // Update is called once per frame
        void Update()
        {
            if (data.timer > 0f)
                data.timer -= Time.deltaTime * Time.timeScale;


        }

        void SpawnPlayer() 
        {

        }

        void CoolEffect() 
        {
            dof.active = data.isSlowedDown;
        }
    }

}
