using System;
using UnityEngine;

namespace Features.UI.Menu.Views
{
    public class CubeMenuBehaviour : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 25;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private AudioSource applySource;
        [SerializeField] private AudioSource throwSource;
        
        private Vector3 _velocityImpulse;
        private bool _torqueMoment;

        private void Update ()
        {
            if (!_torqueMoment) return;
            rb.AddTorque(_velocityImpulse, ForceMode.Impulse);
            _torqueMoment = false;
        }

        private void OnMouseDown()
        {
            applySource.Play();
        }

        private void OnMouseDrag()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero; 
            var yRot = Input.GetAxis("Mouse X");
            var xRot = Input.GetAxis("Mouse Y");
            _velocityImpulse = new Vector3(0, -yRot * rotationSpeed * Time.deltaTime,
                -xRot * rotationSpeed * Time.deltaTime);
            transform.Rotate(_velocityImpulse.x, _velocityImpulse.y, _velocityImpulse.z, Space.Self);
        }

        private void OnMouseUp()
        {
            _torqueMoment = true;
            throwSource.Play();
        }
    }
}
