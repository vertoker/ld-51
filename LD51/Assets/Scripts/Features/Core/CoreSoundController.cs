using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Configs;

namespace Features.Core.Mono 
{
    public class CoreSoundController : MonoBehaviour
    {
        [SerializeField]
        private AudioSource timeSlowSource;

        private CoreEvents events;
        private SoundConfig sounds;

        private List<AudioClip> m_timeSlowClips;

        [Inject]
        private void Init(CoreEvents coreEvents, 
            SoundConfig soundConfig)
        {
            events = coreEvents;
            sounds = soundConfig;
        }

        private void OnEnable()
        {
            events.OnSlowdownTimePressed += PlayTimeSlowSFX;
            events.OnTimersUp += StopAll;
        }

        private void OnDisable()
        {
            events.OnSlowdownTimePressed -= PlayTimeSlowSFX;
            events.OnTimersUp -= StopAll;
        }

        // Start is called before the first frame update
        void Start()
        {
            TimeSlowInit();
        }

        private void TimeSlowInit() 
        {
            m_timeSlowClips = new List<AudioClip>();
            m_timeSlowClips.Add(
                sounds.GetSoundsByType(Data.SoundType.TimeContinue)[0]);
            m_timeSlowClips.Add(
                sounds.GetSoundsByType(Data.SoundType.TimeStop)[0]);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void PlayTimeSlowSFX(bool isSlowedDown) 
        {
            int index = isSlowedDown ? 1 : 0;

            timeSlowSource.clip = m_timeSlowClips[index];
            timeSlowSource.Play();
        }

        private void StopAll() 
        {
            timeSlowSource.Stop();
        }
    }
}

