using Features.Character.Models;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.Character.Views
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private Camera _povCamera;
        [SerializeField] private Rigidbody _rigidbody;
        
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private LayerMask _floorMask;
        
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
                    var movement = transform.TransformDirection(_model.MovementDirection.Value);
                    _rigidbody.velocity = new Vector3(
                        movement.x * _model.Speed,
                        _rigidbody.velocity.y,
                        movement.z * _model.Speed);
                })
                .AddTo(this);

            _model
                .Jump
                .AsObservable()
                .Where(status => status)
                .Subscribe(_ =>
                {
                    if (Physics.CheckSphere(_groundCheck.position, 0.1f, _floorMask))
                    {
                        _rigidbody.AddForce(Vector3.up * _model.JumpForce, ForceMode.Impulse);
                        _model.Jump.Value = false;
                    }
                })
                .AddTo(this);
            
            _model
                .LookDirection
                .AsObservable()
                .Subscribe(rotation =>
                {
                    _povCamera.transform.localRotation = Quaternion.Euler(rotation.x, 0f, 0f);
                    transform.Rotate(0f, rotation.y, 0f);
                })
                .AddTo(this);
        }
    }
}