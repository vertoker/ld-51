using System;
using Cysharp.Threading.Tasks;
using Features.Character.Configs;
using Features.Character.Controllers;
using Features.Character.Data;
using Features.Character.Models;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Features.Character.Views
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private Camera _povCamera;
        [SerializeField] private Rigidbody _rigidbody;
        
        [SerializeField] private BoxCollider groundCheckTrigger;

        [Header("Controllers")]
        [SerializeField] private CharacterSoundController _characterSoundController;
        [SerializeField] private CharacterCameraPresenter _characterCameraPresenter;
        [SerializeField] private CharacterEffectsPresenter _characterEffectsPresenter;
        [SerializeField] private Animator _animator;
        
        private CharacterModel _model;
        private CharacterConfig _characterConfig;
        private CharacterMovementController _characterMovementController;

        [Inject]
        public void Construct(CharacterModel characterModel,
            CharacterConfig characterConfig,
            CharacterMovementController characterMovementController)
        {
            _model = characterModel;
            _characterConfig = characterConfig;
            _characterMovementController = characterMovementController;

            _characterSoundController.SetCharacter(_model);
            _characterEffectsPresenter.SetCharacter(_model);
        }

        private void Start()
        {
            Observable
                .EveryFixedUpdate()
                .Subscribe(_ =>
                {
                    var velocity = _rigidbody.velocity;
                    _rigidbody.AddForce(Physics.gravity * _characterConfig.GravityScale, 
                        ForceMode.Acceleration);

                    if (_rigidbody.velocity.y > _characterConfig.MaximumVerticalVelocity)
                    {
                        _rigidbody.velocity = new Vector3(velocity.x, _characterConfig.MaximumVerticalVelocity, velocity.z);
                    }
                    
                    var move = velocity;
                    move.y = 0f;
                    _animator.SetFloat(CharacterAnimationConst.SpeedParam, move.magnitude / 5f);
                    
                    var distance = move.magnitude * Time.fixedDeltaTime;

                    _model.IsMoving.Value = move != Vector3.zero;
                    _model.Grounded.Value = velocity.y == 0f;
                    
                    move.Normalize();
                    
                    var movement = _rigidbody.SweepTest(move, out var _, distance) 
                    ? Vector3.zero : transform.TransformDirection(
                            _model.MovementDirection.Value);
                    
                    _rigidbody.velocity = new Vector3(
                        movement.x * _model.Speed,
                        _rigidbody.velocity.y,
                        movement.z * _model.Speed);
                })
                .AddTo(this);

            MessageBroker
                .Default
                .Receive<CharacterModel.Jump>()
                .Where(_ => _model.Jumpable)
                .Subscribe(_ =>
                {
                    _animator.Play("Jump");
                    //_animator.SetBool(CharacterAnimationConst.JumpTrigger, true);
                    //_animator.SetBool(CharacterAnimationConst.GroundedTrigger, false);

                    _characterEffectsPresenter.PlayJumpEffect();
                    _characterSoundController.PlayJump();
                    _rigidbody.AddForce(transform.up * _characterConfig.JumpForce, 
                        ForceMode.Impulse);
                    _model.Jumpable = false;
                })
                .AddTo(this);

            MessageBroker
                .Default
                .Receive<CharacterModel.Dash>()
                .Where(_ => _model.Dashable)
                .Subscribe(async _ =>
                {
                    _characterSoundController.PlayDash();
                    _characterCameraPresenter.ChangeFOV(_characterConfig.DashFOV);
                    _rigidbody.useGravity = false;
                    _model.Dashable = false;
                    
                    Vector3 forceToApply;
                    if (Mathf.Abs(_model.MovementDirection.Value.magnitude) < 1f)
                        forceToApply = _model.LookDirection.Value.normalized * _characterConfig.DashForce;
                    else
                        forceToApply = _model.MovementDirection.Value.normalized * _characterConfig.DashForce;
                    forceToApply.Set(forceToApply.x, 0f, forceToApply.z);

                    _characterMovementController.LockMovement = true;

                    await SmoothLerpSpeed(_characterConfig.DashSpeed);
                    _rigidbody.AddForce(forceToApply, ForceMode.Impulse);
                    
                    await SmoothLerpSpeed(_characterConfig.Speed);
                    
                    await UniTask.Delay(TimeSpan.FromSeconds(_characterConfig.DashTime));

                    _characterMovementController.LockMovement = false;
                    _rigidbody.velocity = Vector3.zero;
                    _rigidbody.useGravity = true;
                    _characterCameraPresenter.ChangeFOV(_characterConfig.StandardFOV);
                    _model.Dashable = true;
                })
                .AddTo(this);

            _model
                .LookDirection
                .AsObservable()
                .Subscribe(rotation =>
                {
                    _povCamera.transform.localRotation = Quaternion.Euler
                    (rotation.x, 0f, 0f);
                    transform.Rotate(Vector3.up * rotation.y);
                })
                .AddTo(this);
            
            groundCheckTrigger
                .OnTriggerStayAsObservable()
                .Subscribe(_ =>
                {
                    _model.Jumpable = true;
                    _animator.Play("Running");
                })
                .AddTo(this);
        }
        
        private async UniTask SmoothLerpSpeed(float desired)
        {
            var time = 0f;
            var difference = Mathf.Abs(_model.Speed - desired);
            var initialValue = _model.Speed;
            var boost = _characterConfig.DashForce;

            while (time < difference)
            {
                _model.Speed = Mathf.Lerp(initialValue, desired, time / difference);
                time += Time.unscaledDeltaTime * boost;
                await UniTask.Yield();
            }
        }

        public void ResetVelocity() 
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }
}