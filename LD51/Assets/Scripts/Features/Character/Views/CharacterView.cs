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
        
        [SerializeField] private BoxCollider _groundCheckTrigger;
        
        private CharacterModel _model;
        
        [Inject]
        public void Construct(CharacterModel characterModel)
        {
            _model = characterModel;
        }

        private void Start()
        {
            Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    var move = _rigidbody.velocity;
                    move.y = 0f;
                    var distance = move.magnitude * Time.fixedDeltaTime;
                    move.Normalize();
                    
                    var movement = _rigidbody.SweepTest(move, out var hit, distance) 
                        ? Vector3.zero 
                        : transform.TransformDirection(_model.MovementDirection.Value);
                    
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
                    _rigidbody.AddForce(transform.up * _model.JumpForce, ForceMode.Impulse);
                    _model.Jumpable = false;
                })
                .AddTo(this);

            MessageBroker
                .Default
                .Receive<CharacterModel.Dash>()
                .Where(_ => _model.Dashable)
                .Subscribe(_ =>
                {
                    _rigidbody.AddForce(transform.forward * _model.DashForce, ForceMode.Impulse);
                    _model.Dashable = false;
                })
                .AddTo(this);

            _model
                .LookDirection
                .AsObservable()
                .Subscribe(rotation =>
                {
                    _povCamera.transform.localRotation = Quaternion.Euler(rotation.x, 0f, 0f);
                    transform.Rotate(Vector3.up * rotation.y);
                })
                .AddTo(this);
            
            _groundCheckTrigger
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