﻿using System;
using Configs;
using Data;
using Features.Character.Models;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.Character.Controllers
{
    public class CharacterMovementController : IInitializable
    {
        public bool LockMouse;
        public bool LockMovement;

        private const string HorizontalMovement = "Horizontal";
        private const string DepthMovement = "Vertical";

        private const string HorizontalLook = "Mouse X";
        private const string VerticalLook = "Mouse Y";
        
        private CharacterModel _characterModel;
        private Vector2 _look;

        private IDisposable _lookStream;

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
            var mouseSense = PlayerPrefs.HasKey(GlobalConst.MouseSensitivityPref) 
                ? PlayerPrefs.GetFloat(GlobalConst.MouseSensitivityPref)
                : 10f;
                
#if UNITY_EDITOR
            mouseSense = _inputConfig.MouseSensitivityX;
#endif
            
            Observable
                .EveryUpdate()
                .Where(_ => _characterModel != null)
                .Subscribe(_ =>
                {
                    if (!LockMovement)
                    {
                        var horizontal = Input.GetAxis(HorizontalMovement);
                        var depth = Input.GetAxis(DepthMovement);
                    
                        if (Input.GetKeyDown(_inputConfig.JumpButton))
                            MessageBroker.Default.Publish(new CharacterModel.Jump());
                    
                        if (Input.GetKeyDown(_inputConfig.DashButton))
                            MessageBroker.Default.Publish(new CharacterModel.Dash());

                        if (Input.GetKeyDown(_inputConfig.MenuButton))
                            Cursor.lockState = CursorLockMode.None;
                    
                        if (Input.GetKeyDown(_inputConfig.TimeStopButton))
                            MessageBroker.Default.Publish(new CharacterModel.TimeManageSwitch());

                        _characterModel.MovementDirection.Value =
                            new Vector3(horizontal, 0, depth);
                    }
                    
                    if (LockMouse) return;
                    _look.x = Input.GetAxis(HorizontalLook) * mouseSense * 
                              Time.unscaledDeltaTime;
                    _look.y = Mathf.Clamp(_look.y - Input.GetAxis(VerticalLook) * mouseSense * 
                        Time.unscaledDeltaTime,
                        _inputConfig.MouseLock.x, _inputConfig.MouseLock.y);
                    
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