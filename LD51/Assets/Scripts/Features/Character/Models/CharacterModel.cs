﻿using System;
using Features.Character.Data;
using UnityEngine.EventSystems;
using UniRx;
using UnityEngine;
using Zenject;
using UnityEngine.Events;

namespace Features.Character.Models
{
    public class CharacterModel : IInitializable, IDisposable
    {
        public bool Jumpable;
        public bool Dashable = true;
        
        public float Speed;

        public class Jump { }
        public class Dash { }
        public class TimeManageSwitch { }

        public readonly ReactiveProperty<bool> Grounded = new ReactiveProperty<bool>();
        public readonly ReactiveProperty<bool> IsMoving = new ReactiveProperty<bool>();
        public readonly ReactiveProperty<Vector3> LookDirection = new ReactiveProperty<Vector3>();
        public readonly ReactiveProperty<Vector3> MovementDirection = new ReactiveProperty<Vector3>();

        private IDisposable _rechargeStream;

        private UnityEvent<bool, bool> _eventState = new UnityEvent<bool, bool>();
        public event UnityAction<bool, bool> EventState
        {
            add => _eventState.AddListener(value);
            remove => _eventState.RemoveListener(value);
        }
        public void UpdateState()
        {
            _eventState.Invoke(Grounded.Value, IsMoving.Value);
        }

        private CharacterModel(CharacterData data)
        {
            Speed = data.Speed;
        }
        
        public void Initialize()
        {
            
        }
        
        public void Dispose()
        {
            
        }
    }
}