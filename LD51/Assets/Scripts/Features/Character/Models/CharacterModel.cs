using System;
using Features.Character.Data;
using UniRx;
using UnityEngine;

namespace Features.Character.Models
{
    public class CharacterModel : IDisposable
    {
        public float Speed { get; }
        public float JumpForce { get; }

        public readonly ReactiveProperty<bool> Jump;
        public readonly ReactiveProperty<Vector3> LookDirection;
        public readonly ReactiveProperty<Vector3> MovementDirection;

        private CharacterModel(CharacterData data)
        {
            Speed = data.Speed;
            JumpForce = data.JumpForce;

            Jump = new ReactiveProperty<bool>();
            LookDirection = new ReactiveProperty<Vector3>();
            MovementDirection = new ReactiveProperty<Vector3>();
        }
        
        public void Dispose()
        {
            
        }
    }
}