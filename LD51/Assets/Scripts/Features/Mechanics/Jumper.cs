using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using Configs;
using Data;
using UnityEngine;
using Zenject;

namespace Mechanics
{
    public class Jumper : MonoBehaviour
    {
        [SerializeField] private float powerBoost = 20f;
        [SerializeField] private AudioSource _jumperSource;

        private AudioStateData _jumperClipData;
        
        [Inject]
        public void Construct(SoundConfig soundConfig)
        {
            _jumperClipData = new AudioStateData(soundConfig.GetSoundsByType(SoundType.JumpBoost));
        }
        
        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Rigidbody rb))
            {
                rb.velocity = transform.up * powerBoost;
                _jumperSource.volume = PlayerPrefs.GetFloat(GlobalConst.AudioVolumePref);
                _jumperSource.clip = _jumperClipData.GetRandom();
                _jumperSource.Play();
            }
        }
    }
}