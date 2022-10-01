using Features.Character.Models;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.Character.Controllers
{
    public class CharacterMovementController : IInitializable
    {
        private const string HorizontalMovement = "Horizontal";
        private const string DepthMovement = "Vertical";

        private const string HorizontalLook = "Mouse X";
        private const string VerticalLook = "Mouse Y";
        
        private const KeyCode Jump = KeyCode.Space;
        private const KeyCode Dash = KeyCode.LeftShift;
        
        private CharacterModel _characterModel;

        private Vector2 _lookView;
        
        private readonly CompositeDisposable _compositeDisposable;
        
        private CharacterMovementController()
        {
            _compositeDisposable = new CompositeDisposable();
        }
        
        public void Initialize()
        {
            Cursor.lockState = CursorLockMode.Locked;
            
            Observable
                .EveryUpdate()
               
                .Subscribe(_ =>
                {
                    var horizontal = Input.GetAxis(HorizontalMovement);
                    var depth = Input.GetAxis(DepthMovement);
                    
                    _lookView.x = Input.GetAxisRaw(HorizontalLook) * 100f * Time.deltaTime; 
                    _lookView.y += Input.GetAxisRaw(VerticalLook) * 100f * Time.deltaTime;
                    

                    if (Input.GetKeyDown(Jump))
                        MessageBroker.Default.Publish(new CharacterModel.Jump());
                    
                    if (Input.GetKeyDown(Dash))
                        MessageBroker.Default.Publish(new CharacterModel.Dash());

                    _characterModel.MovementDirection.Value = 
                        new Vector3(horizontal, 0, depth);
                    _characterModel.LookDirection.Value =
                        new Vector3(Mathf.Clamp(-_lookView.y, -90f, 90f),
                        _lookView.x, 0);
                })
                .AddTo(_compositeDisposable);
        }
        
        public void SetCharacter(CharacterModel model)
        {
            _characterModel = model;
        }
    }
}