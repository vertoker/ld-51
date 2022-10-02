using Configs;
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
        
        private CharacterModel _characterModel;
        private Vector2 _look;

        private readonly InputConfig _inputConfig;
        private readonly CompositeDisposable _compositeDisposable;
        
        private CharacterMovementController(InputConfig inputConfig)
        {
            _inputConfig = inputConfig;
            
            _compositeDisposable = new CompositeDisposable();
        }
        
        public void Initialize()
        {
            Cursor.lockState = CursorLockMode.Locked;
            
            Observable
                .EveryUpdate()
                .Where(_ => _characterModel != null)
                .Subscribe(_ =>
                {
                    var horizontal = Input.GetAxis(HorizontalMovement);
                    var depth = Input.GetAxis(DepthMovement);

                    _look.x = Input.GetAxis(HorizontalLook) * _inputConfig.MouseSensitivityX * 
                        Time.unscaledDeltaTime;
                    _look.y = Mathf.Clamp(_look.y - Input.GetAxis(VerticalLook) * _inputConfig.MouseSensitivityY * 
                        Time.unscaledDeltaTime,
                        _inputConfig.MouseLock.x, _inputConfig.MouseLock.y);
                    
                    if (Input.GetKeyDown(_inputConfig.JumpButton))
                        MessageBroker.Default.Publish(new CharacterModel.Jump());
                    
                    if (Input.GetKeyDown(_inputConfig.DashButton))
                        MessageBroker.Default.Publish(new CharacterModel.Dash());

                    if (Input.GetKeyDown(_inputConfig.MenuButton))
                        Cursor.lockState = CursorLockMode.None;

                    _characterModel.MovementDirection.Value = 
                        new Vector3(horizontal, 0, depth);
                    _characterModel.LookDirection.Value = 
                        new Vector3(_look.y, _look.x, 0);
                })
                .AddTo(_compositeDisposable);
        }
        
        public void SetCharacter(CharacterModel model)
        {
            _characterModel = model;
        }
    }
}