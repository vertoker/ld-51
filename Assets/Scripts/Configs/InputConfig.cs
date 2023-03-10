using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "InputConfig", menuName = "Config/Input config")]
    public class InputConfig : ScriptableObject
    {
        [Header("Bind settings")]
        [SerializeField] private KeyCode jumpButton;
        [SerializeField] private KeyCode dashButton;
        [SerializeField] private KeyCode actionButton;
        [SerializeField] private KeyCode timeStopButton;
        [SerializeField] private KeyCode menuButton;
        [SerializeField] private KeyCode resetButton;
        
        [Header("Mouse input settings")]
        [Range(0f, 250f)]
        [SerializeField] private float mouseSensitivityX;
        [Range(0f, 250f)]
        [SerializeField] private float mouseSensitivityY;
        [SerializeField] private Vector2 mouseLock;

        public KeyCode JumpButton => jumpButton;
        public KeyCode DashButton => dashButton;
        public KeyCode ActionButton => actionButton;
        public KeyCode TimeStopButton => timeStopButton;
        public KeyCode MenuButton => menuButton;
        public KeyCode ResetButton => resetButton;

        public float MouseSensitivityX => mouseSensitivityX;
        public float MouseSensitivityY => mouseSensitivityY;
        public Vector2 MouseLock => mouseLock;
    }
}