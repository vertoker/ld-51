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

        private float horizontalLook; 
        private float verticalLook; 
        
        private readonly CompositeDisposable _compositeDisposable;
        
        private CharacterMovementController()
        {
            _compositeDisposable = new CompositeDisposable();
        }
        
        public void Initialize()
        {
            Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    var horizontal = Input.GetAxis(HorizontalMovement);
                    var depth = Input.GetAxis(DepthMovement);
                    var vertical = Input.GetKeyDown(Jump) ? 1f : 0f;
                    horizontalLook = Input.GetAxis(HorizontalLook); 
                    verticalLook += Mathf.Clamp(Input.GetAxis(VerticalLook), -45f, 45f);

                    _characterModel.Jump.Value = Input.GetKeyDown(Jump);

                    _characterModel.MovementDirection.Value = new Vector3(horizontal, vertical, depth);
                    _characterModel.LookDirection.Value = new Vector3(-verticalLook, horizontalLook, 0);
                })
                .AddTo(_compositeDisposable);
        }
        
        public void SetCharacter(CharacterModel model)
        {
            _characterModel = model;
        }
    }
}