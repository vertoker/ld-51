using DG.Tweening;
using UnityEngine;

namespace Features.Character.Views
{
    public class CharacterCameraPresenter : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        public void ChangeFOV(float requiredFOV)
        {
            _camera.DOFieldOfView(requiredFOV, 0.25f);
        }
    }
}