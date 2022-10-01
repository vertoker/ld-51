using System;
using System.Collections;
using Features.Character.Data;
using UniRx;
using UnityEngine;

namespace Features.Character.Models
{
    public class CharacterModel : IDisposable
    {
        public bool Jumpable;
        public bool Dashable;
        public float Speed { get; private set; }
        public float JumpForce { get; private set; }
        public float DashForce { get; private set; }

        public class Jump { }
        public class Dash { }
        
        public readonly ReactiveProperty<Vector3> LookDirection = new ReactiveProperty<Vector3>();
        public readonly ReactiveProperty<Vector3> MovementDirection = new ReactiveProperty<Vector3>();

        private CharacterModel(CharacterData data)
        {
            Speed = data.Speed;
            JumpForce = data.JumpForce;
            DashForce = data.DashForce;
        }
        
        public void Dispose()
        {
            
        }
    }
}