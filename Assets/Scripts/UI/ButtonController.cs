using UnityEngine;

namespace UI
{
    public class ButtonController : MonoBehaviour
    {
        private Animator _animator;
        void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void PressDown()
        {
            _animator.Play("PressedDown");
        }

        public void PressUp()
        {
            _animator.Play("PressedUp");
        }
    
    }
}
