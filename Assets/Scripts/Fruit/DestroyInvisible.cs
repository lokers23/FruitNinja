using System;
using UnityEngine;

namespace Fruit
{
    public class DestroyInvisible : MonoBehaviour
    {
        public static event Action OnDestroy;
        [SerializeField] private Camera mainCamera;

        private void Update()
        {
            DetectObjectOutBorder();
        }

        private void DetectObjectOutBorder()
        {
            var bottomCorner = mainCamera.ViewportToWorldPoint (new Vector2 (0,0));
            if (transform.position.y <= bottomCorner.x)
            {
                OnDestroy?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
