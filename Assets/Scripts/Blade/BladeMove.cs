using System;
using UnityEngine;

namespace Blade
{
    public class BladeMove : MonoBehaviour
    {
        [SerializeField] private TrailRenderer trail;
        [SerializeField] private Camera currentCamera;
        private bool _isMoving;
        
        void Update()
        {
            if (Input.GetMouseButtonDown(0)) {
                StartSlice();
            } else if (Input.GetMouseButtonUp(0)) {
                StopSlice();
            } else if (_isMoving) {
                ContinueSlice();
            }
        }
    
        private void StartSlice()
        {
            var position = currentCamera.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0f;
            transform.position = position;
            
            _isMoving = true;
            trail.enabled = true;
            trail.Clear();
        }

        private void StopSlice()
        {
            _isMoving = false;
            trail.enabled = false;
        }

        private void ContinueSlice()
        {
            var newPosition = currentCamera.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 0f;
            transform.position = newPosition;
        }
    }
}
