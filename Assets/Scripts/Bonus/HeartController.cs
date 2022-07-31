using System;
using System.Collections;
using UnityEngine;

namespace Fruit.Bonus
{
    public class HeartController : MonoBehaviour
    {
        private const float PositionBonusImageAxisZ = 19f;
        public static event Action<int, Vector3> EventSliceHeart;
        
        [SerializeField] private float radius;
        [SerializeField] private GameObject imageAfterSlice;
        [SerializeField] private GameObject particalObject;
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

                var particalGameObject = Instantiate(particalObject, basePosition, Quaternion.identity);
                Destroy(particalGameObject, 2f);
                EventSliceHeart?.Invoke(recoverHeart, transform.position);
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
        }
    }
}