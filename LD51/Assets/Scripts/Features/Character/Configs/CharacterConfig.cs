using Features.Character.Views;
using UnityEngine;

namespace Features.Character.Configs
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Config/Character config")]
    public class CharacterConfig : ScriptableObject
    {
        [SerializeField] private CharacterView _characterPrefab;

        [Header("Movement settings")]
        [SerializeField] private float _standardFOV;
        [SerializeField] private float _gravityScale;
        [Space]
        [SerializeField] private float _speed;
        
        [Space]
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _maximumVerticalVelocity;
        
        [Space]
        [Header("Dash settings")]
        [SerializeField] private float _dashForce;
        [SerializeField] private float _dashTime;
        [SerializeField] private float _dashSpeed;
        [SerializeField] private float _dashFOV;

        public CharacterView CharacterPrefab => _characterPrefab;

        public float GravityScale => _gravityScale;
        public float StandardFOV => _standardFOV;
        public float Speed => _speed;
        
        public float JumpForce => _jumpForce;
        public float MaximumVerticalVelocity => _maximumVerticalVelocity;
        
        public float DashForce => _dashForce;
        public float DashTime => _dashTime;
        public float DashSpeed => _dashSpeed;
        public float DashFOV => _dashFOV;
    }
}