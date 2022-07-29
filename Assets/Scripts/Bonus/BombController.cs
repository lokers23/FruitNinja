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
        public static event Action<float, float, float, Vector3> EventExplosion;
        
        [SerializeField] private TextMeshPro BonusText;

        [SerializeField] private float radiusSlice;
        [SerializeField] private GameObject imageAfterSlice;

        [SerializeField] private int damage = 1;
        
        [SerializeField] private float powerExplosion;
        [SerializeField] private float minRadiusExplosion;
        [SerializeField] private float maxRadiusExplosion;
        
        [SerializeField] private ParticleSystem particles;
        
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
            if (distance <= radiusSlice)
            {
                var explosion = Instantiate(particles, basePosition, Quaternion.identity);
                Destroy(explosion, 1f);
                CreateImage(basePosition);
                CreateText(basePosition);
                
                EventSliceBomb?.Invoke(damage);
                EventExplosion?.Invoke(powerExplosion, minRadiusExplosion, maxRadiusExplosion, transform.position);
                
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
