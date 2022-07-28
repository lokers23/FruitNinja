using System;
using UnityEngine;

namespace Fruit.Bonus
{
    public class HeartController : MonoBehaviour
    {
        private const float PositionBonusImageAxisZ = 19f;
        public static event Action<int> EventSliceHeart;
        
        [SerializeField] private float radius;
        [SerializeField] private GameObject imageAfterSlice;

        [SerializeField] private int recoverHeart = 1;
        private GameObject _blade;

        private void Awake()
        {
            _blade = GameObject.FindWithTag("Player");
            DetectSwipe.EventSwipe += OnSlice;
        }
        
        private void OnSlice()
        {
            var bladePosition = _blade.transform.position;
            var basePosition = transform.position;
            
            var distance = Vector3.Distance(basePosition, bladePosition);
            if (distance <= radius)
            {
                CreateImage(basePosition);

                EventSliceHeart?.Invoke(recoverHeart);

                Destroy(gameObject);
            }
        }

        private void OnDisable()
        {
            DetectSwipe.EventSwipe -= OnSlice;
        }

        private  void CreateImage(Vector3 position)
        {
            var image = Instantiate(imageAfterSlice,new Vector3(position.x, position.y, PositionBonusImageAxisZ) , Quaternion.identity);
            Destroy(image, 0.5f);
        }
    }
}