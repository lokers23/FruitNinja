using System;
using UnityEngine;

namespace Fruit
{
    public class DetectSwipe : MonoBehaviour
    {
        public static event Action EventSwipe; 
        
        private bool _isSwiping;
        private Vector2 _startPosition;
        private Vector2 _swipeDelta;
        private readonly float _minimumDistanceSwipe = 80f;
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isSwiping = true;
                _startPosition = Input.mousePosition;
            }
            else if(Input.GetMouseButtonUp(0))
            {
                ResetSwipe();
            }

            CheckSwipe();
        }

        private void CheckSwipe()
        {
            _swipeDelta = Vector2.zero;
            if (_isSwiping)
            {
                if (Input.GetMouseButton(0))
                {
                    _swipeDelta = (Vector2)Input.mousePosition - _startPosition;
                }
            }

            if (_swipeDelta.magnitude > _minimumDistanceSwipe)
            {
                EventSwipe?.Invoke();
            }
        }

        private void ResetSwipe()
        {
            _isSwiping = false;
            _startPosition = Vector2.zero;
            _swipeDelta = Vector2.zero;
        }
    }
}
