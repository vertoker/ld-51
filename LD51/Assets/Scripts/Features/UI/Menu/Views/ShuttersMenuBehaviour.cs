using System;
using System.Collections.Generic;
using Features.UI.Menu.Messages;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.UI.Menu.Views
{
    public class ShuttersMenuBehaviour : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [Header("Translate finish positions")]
        [SerializeField] private List<Transform> lockedPositions;
        [SerializeField] private List<Transform> unlockedPositions;
        [Header("Shutters")]
        [SerializeField] private Transform leftShutter;
        [SerializeField] private Transform rightShutter;
        private SignalBus _signalBus;

        private IDisposable _subscription;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;

            
        }

        private void OnEnable()
        {
           _subscription =
                _signalBus.GetStream<MenuBehaviourSignals.ActionAdded>()
                .Subscribe(signal =>
                {
                    if (signal.Action == MenuAction.Info) DriveIn();
                    else if (signal.Action == MenuAction.MainMenu) DriveOut();
                });
        }

        private void OnDisable()
        {
            _subscription.Dispose();
        }

        /*private void Update()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Locked"))
            {
                leftShutter = lockedPositions[0];
                rightShutter = lockedPositions[1];
            }
            
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Unlocked"))
            {
                leftShutter = unlockedPositions[0];
                rightShutter = unlockedPositions[1];
            }
            Debug.Log($"locked? {animator.GetCurrentAnimatorStateInfo(0).IsName("Locked")}; unlocked? {animator.GetCurrentAnimatorStateInfo(0).IsName("Unlocked")}");
        }*/

        private void DriveOut()
        {
            //animator.SetTrigger("Unlock");
            animator.SetBool("bLock", false);
        }
        
        private void DriveIn()
        {
            //animator.SetTrigger("Lock");
            animator.SetBool("bLock", true);
        }
    }
}
