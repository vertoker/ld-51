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
        
        private const float MouseSensitivityX = 100f;
       

        private static readonly Vector2 MouseLock = new (-90f, 90f);

        private const KeyCode Jump = KeyCode.Space;
        private const KeyCode Dash = KeyCode.LeftShift;
        
        private CharacterModel _characterModel;
        private Vector2 _look;
        
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

                    _look.x = Input.GetAxis(HorizontalLook) * MouseSensitivityX * 
                        Time.unscaledDeltaTime;
                    _look.y = Mathf.Clamp(_look.y - Input.GetAxis(VerticalLook),
                        MouseLock.x, MouseLock.y);

                    

                    if (Input.GetKeyDown(Jump))
                        MessageBroker.Default.Publish(new CharacterModel.Jump());
                    
                    if (Input.GetKeyDown(Dash))
                        MessageBroker.Default.Publish(new CharacterModel.Dash());

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