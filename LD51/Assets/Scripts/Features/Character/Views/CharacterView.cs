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

        [SerializeField] private CharacterSoundController _characterSoundController;
        
        private CharacterModel _model;
        
        [Inject]
        public void Construct(CharacterModel characterModel)
        {
            _model = characterModel;
            
            _characterSoundController.SetCharacter(_model);
        }

        private void Start()
        {
            Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    var velocity = _rigidbody.velocity;
                    var move = velocity;
                    move.y = 0f;
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
                    _rigidbody.AddForce(transform.up * _model.JumpForce, 
                        ForceMode.Impulse);
                    _model.Jumpable = false;
                })
                .AddTo(this);

            MessageBroker
                .Default
                .Receive<CharacterModel.Dash>()
                .Where(_ => _model.Dashable)
                .Subscribe(_ =>
                {
                    _rigidbody.AddForce(transform.forward * _model.DashForce, 
                        ForceMode.Acceleration);
                    _model.Dashable = false;
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
                    _model.Dashable = true;
                })
                .AddTo(this);
        }
    }
}