using UnityEngine;

namespace Features.UI.Menu.Views
{
    public class RingMenuBehaviour : MonoBehaviour
    {
        private const float RingRotationForce = 10f;

        [SerializeField] private Rigidbody _ring;
        [SerializeField] private AudioSource audioSource;

        private Vector3 _forceVector;
        private float _prevInput;
        private void FixedUpdate()
        {
            var scrollInput = Input.GetAxis("Mouse ScrollWheel");
            _ring.AddTorque(new Vector3(scrollInput, 0, 0) * RingRotationForce, ForceMode.Impulse);

            if (scrollInput != 0f)
            {
                audioSource.Play();
            }
        }
    }
}
