using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Features.Core.Mono
{
    public class CoreVFX : MonoBehaviour
    {

        private CoreData data;
        private CoreEvents events;

        [SerializeField]
        private Volume volume;

        [SerializeField]
        private float pauseBlur;
        [SerializeField]
        private float slowMoBlur;
        private bool m_isUnpausedBlurActive = false;

       private float m_curUnpausedBlurValue;

        private DepthOfField m_dof;

        [Inject]
        void Init(CoreData coreData, CoreEvents coreEvents) 
        {
            data = coreData;
            events = coreEvents;
        }

        private void OnEnable()
        {
            events.OnPauseButtonPressed += ApplyPauseEffect;
            events.OnSlowdownTimePressed += ApplySlowDownTimeEffect;
        }

        private void OnDisable()
        {
            events.OnPauseButtonPressed -= ApplyPauseEffect;
            events.OnSlowdownTimePressed -= ApplySlowDownTimeEffect;
        }




        // Start is called before the first frame update
        void Start()
        {
            if (volume.profile.TryGet<DepthOfField>(out m_dof)) 
            {
                m_dof.active = false;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        void ApplySlowDownTimeEffect(bool isSlowedDown) 
        {
            m_isUnpausedBlurActive = isSlowedDown;
            m_curUnpausedBlurValue = slowMoBlur;
            m_dof.focalLength.value = slowMoBlur;
            m_dof.active = isSlowedDown;

        }

        void ApplyPauseEffect(bool isPaused) 
        {
           
            m_dof.focalLength.value = (isPaused) ? pauseBlur :
                m_curUnpausedBlurValue;
            m_dof.active = m_isUnpausedBlurActive || isPaused;

            
        }
    }
}

