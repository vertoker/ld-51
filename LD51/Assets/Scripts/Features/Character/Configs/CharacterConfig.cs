using Features.Character.Views;
using UnityEngine;

namespace Features.Character.Configs
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Config/Character")]
    public class CharacterConfig : ScriptableObject
    {
        [SerializeField] private CharacterView _characterPrefab;

        [Header("Movement settings")] 
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _dashForce;

        public CharacterView CharacterPrefab => _characterPrefab;

        public float Speed => _speed;
        public float JumpForce => _jumpForce;
        public float DashForce => _dashForce;
    }
}