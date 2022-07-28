using System;
using System.Collections;
using System.Collections.Generic;
using Fruit;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class BombController : MonoBehaviour
{
        private const float PositionBonusImageAxisZ = 19f;
        public static event Action<int> EventSliceBomb;
        
        [SerializeField] private TextMeshPro BonusText;

        [SerializeField] private float radius;
        [SerializeField] private GameObject imageAfterSlice;

        [SerializeField] private int damage = 1;
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
                CreateText(basePosition);
                
                EventSliceBomb?.Invoke(damage);

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

        private  void CreateText(Vector3 position)
        {
            TextMeshPro newObject = Instantiate(BonusText, position, quaternion.identity);
            Destroy(newObject.gameObject, 1f);
        }
}
