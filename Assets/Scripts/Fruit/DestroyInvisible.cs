using System;
using UnityEngine;

namespace Fruit
{
    public class DestroyInvisible : MonoBehaviour
    {
        public static event Action OnDestroy;
        [SerializeField] private Camera mainCamera;

        private const string TagBonus = "Bonus";
        private void Update()
        {
            DetectObjectOutBorder();
        }

        private void DetectObjectOutBorder()
        {
            var bottomCorner = mainCamera.ViewportToWorldPoint (new Vector2 (0,0));
            if (transform.position.y <= bottomCorner.x)
            {
                if (!CompareTag(TagBonus))
                {
                    OnDestroy?.Invoke();
                }
                
                Destroy(gameObject);
            }
        }
    }
}
