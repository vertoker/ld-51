using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Pool;
using UnityEngine.UIElements;
using DG.Tweening;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Mechanics
{
    public class RocketLauncherTurrel : MonoBehaviour
    {
        [SerializeField] private float angleY = 0f;
        [SerializeField] private float angleZ = 0f;
        [SerializeField] private float durationRotation = 0.3f;
        [SerializeField] private float delayShot = 0.4f;
        [SerializeField] private Transform axis, box, spawn;
        [SerializeField] private PoolSpawner rocketSpawner;
        [SerializeField] private PoolSpawner explosionSpawner;
        [SerializeField] private Transform target;

        public void SetTarget(Transform target)
        {

        }

        private void Start()
        {
            StartCoroutine(AutoShot());
        }
        private IEnumerator AutoShot()
        {
            yield return new WaitForSeconds(delayShot);
            Shoot();
            StartCoroutine(AutoShot());
        }

        private void FixedUpdate()
        {
            UpdateAngle();
        }
        private void UpdateAngle()
        {
            if (target == null)
                return;
            box.DOLookAt(target.position, durationRotation);
            axis.eulerAngles = new Vector3(axis.eulerAngles.x, box.eulerAngles.y, axis.eulerAngles.z);
            angleY = box.eulerAngles.y;
            angleZ = box.eulerAngles.z;
        }
        private void UpdateAngleDebug()
        {
            axis.eulerAngles = new Vector3(axis.eulerAngles.x, angleY, axis.eulerAngles.z);
            box.eulerAngles = new Vector3(box.eulerAngles.x, angleY, angleZ);
        }

        private void Shoot()
        {
            var rocket = rocketSpawner.Dequeue(false).transform;
            rocket.position = spawn.position;
            rocket.eulerAngles = new Vector3(0f, angleY, angleZ);
            rocket.GetComponent<Rocket>().SetTarget(target, ExplosionRocket);
            rocket.gameObject.SetActive(true);
            StartCoroutine(rocket.GetComponent<Rocket>().DelayActivate());
        }

        private void ExplosionRocket(Vector3 position, GameObject rocket)
        {
            rocketSpawner.Enqueue(rocket);
            var effect = explosionSpawner.Dequeue();
            effect.transform.position = position;
            effect.GetComponent<ParticleSystem>().Play();
            StartCoroutine(DelayOffExplosion(effect));
        }
        private IEnumerator DelayOffExplosion(GameObject explosion)
        {
            yield return new WaitForSeconds(0.3f);
            explosionSpawner.Enqueue(explosion);
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(RocketLauncherTurrel))]
        public class RocketLauncherTurrelEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                RocketLauncherTurrel turrel = (RocketLauncherTurrel)target;

                if (GUILayout.Button("Выстрелить (только в игре)"))
                {
                    turrel.Shoot();
                }

                base.OnInspectorGUI();

                if (turrel.axis == null || turrel.box == null)
                    return;

                turrel.UpdateAngleDebug();
            }
        }
#endif
    }

    public delegate void CallExplosion(Vector3 position, GameObject rocket);
}