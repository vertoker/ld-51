using System;
using Features.Character.Data;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.Character.Models
{
    public class CharacterModel : IInitializable, IDisposable
    {
        public bool Jumpable;
        public bool Dashable;
        public float Speed { get; }
        public float JumpForce { get; }
        public float DashForce { get; }

        public class Jump { }
        public class Dash { }

        public readonly ReactiveProperty<bool> Grounded = new ReactiveProperty<bool>();
        public readonly ReactiveProperty<bool> IsMoving = new ReactiveProperty<bool>();
        public readonly ReactiveProperty<Vector3> LookDirection = new ReactiveProperty<Vector3>();
        public readonly ReactiveProperty<Vector3> MovementDirection = new ReactiveProperty<Vector3>();

        private IDisposable _rechargeStream;

        private CharacterModel(CharacterData data)
        {
            Speed = data.Speed;
            JumpForce = data.JumpForce;
            DashForce = data.DashForce;
        }
        
        public void Initialize()
        {
            
        }
        
        public void Dispose()
        {
            
        }
    }
}