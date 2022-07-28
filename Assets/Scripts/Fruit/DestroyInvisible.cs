using System;
using UnityEngine;

namespace Fruit
{
    public class DestroyInvisible : MonoBehaviour
    {
        public static event Action<int> OnDestroy;
        [SerializeField] private Camera mainCamera;

        private const string TagBonus = "Bonus";

        private FruitController _fruitController;
        private void Awake()
        {
            _fruitController = GetComponent<FruitController>();
        }

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
                    OnDestroy?.Invoke(_fruitController.Damage);
                }
                
                Destroy(gameObject);
            }
        }
    }
}
