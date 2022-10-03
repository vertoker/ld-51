using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Configs;
using Data;
using UnityEngine;
using Zenject;

namespace Mechanics
{
    public class Rocket : MonoBehaviour
    {
        [SerializeField] private float launchPower = 100f;
        private CallExplosion effect;
        private Transform target;

        private bool active = false;
        private bool immortality = true;
        [SerializeField] private Transform tr;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private AudioSource _rocketSource;

        private AudioStateData _rocketClipData;
        
        [Inject]
        public void Construct(SoundConfig soundConfig)
        {
            _rocketClipData = new AudioStateData(soundConfig.GetSoundsByType(SoundType.Explosion));
        }
        
        public void SetTarget(Transform target, CallExplosion effect)
        {
            if (target == null)
                return;
            rb.velocity = Vector3.zero;
            this.target = target;
            this.effect = effect;
            active = true;
        }
        public IEnumerator DelayActivate()
        {
            yield return new WaitForSeconds(0.1f);
            immortality = false;
        }
        private void FixedUpdate()
        {
            if (!active)
                return;
            tr.LookAt(target);
            Vector3 direction = target.position - tr.position;
            float multiply = launchPower / Mathf.Abs(direction.magnitude);
            rb.AddForce(direction * multiply, ForceMode.Force);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (immortality)
                return;
            active = false;
            immortality = true;

            _rocketSource.volume = PlayerPrefs.GetFloat(GlobalConst.AudioVolumePref);
            _rocketSource.clip = _rocketClipData.GetNext();
            _rocketSource.Play();
            
            effect.Invoke(tr.position, gameObject);
        }
    }
}