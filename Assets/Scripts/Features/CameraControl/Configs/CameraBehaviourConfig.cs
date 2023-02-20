using UnityEngine;

namespace Features.CameraControl.Configs
{
    [CreateAssetMenu(menuName = "Config/Camera behaviour", fileName = "CameraBehaviourConfig")]
    public class CameraBehaviourConfig : ScriptableObject
    {
        [SerializeField] private float lookSensitivityX;
        [SerializeField] private float lookSensitivityY;
        [SerializeField] private float cameraTranslateSpeed;
        [SerializeField] private float cameraRotateSpeed;
        [SerializeField] private Vector2 cameraLockX;
        [SerializeField] private Vector2 cameraLockY;
        [SerializeField] private Vector3 actorOffset;

        public float LookSensitivityX => lookSensitivityX;
        public float LookSensitivityY => lookSensitivityY;
        public float CameraTranslateSpeed => cameraTranslateSpeed;
        public float CameraRotateSpeed => cameraRotateSpeed;
        public Vector2 CameraLockX => cameraLockX;
        public Vector2 CameraLockY => cameraLockY;
        public Vector3 ActorOffset => actorOffset;
    }
}