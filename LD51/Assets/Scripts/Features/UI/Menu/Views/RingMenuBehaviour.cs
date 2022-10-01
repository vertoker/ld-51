using UnityEngine;

namespace Features.UI.Menu.Views
{
    public class RingMenuBehaviour : MonoBehaviour
    {
        private const float RingRotationForce = 500f;

        [SerializeField] private Rigidbody rb;
        [SerializeField] private AudioSource audioSource;

        private Vector3 _forceVector;
        private float _prevInput;
        private void FixedUpdate()
        {
            var scrollInput = Input.GetAxis("Mouse ScrollWheel");
            scrollInput = scrollInput == 0 ? 0 : Mathf.Sign(scrollInput);

            _forceVector = scrollInput switch
            {
                > 0 => new Vector3(0, 0, scrollInput * 100),
                < 0 => new Vector3(0, 0, scrollInput * 100),
                _ => _forceVector
            };

            rb.AddTorque(-rb.angularVelocity * (0.1f * Time.fixedTime), ForceMode.Force);
            
            if (_forceVector == Vector3.zero) return;

            if (_prevInput != 0 && scrollInput != 0 && 
                _prevInput != scrollInput)
                rb.angularVelocity = Vector3.zero;

            //rb.AddTorque(_forceVector * RingRotationForce * Time.fixedTime, 
            //    ForceMode.Impulse);

            rb.angularVelocity += _forceVector * RingRotationForce;

            _forceVector = Vector3.zero;
            audioSource.Play();

            _prevInput = scrollInput;
        }
    }
}
