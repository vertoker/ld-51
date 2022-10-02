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
        [SerializeField] private Transform axis, box, spawn;
        [SerializeField] private PoolSpawner rocketSpawner;
        [SerializeField] private Transform target;

        public void SetTarget(Transform target)
        {

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
            rocket.GetComponent<Rocket>().SetTarget(target);
            rocket.gameObject.SetActive(true);
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
}