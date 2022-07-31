using System;
using System.Collections;
using System.Collections.Generic;
using Fruit;
using Spawner;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
public class BoxFruitController : MonoBehaviour
{
    [SerializeField] private GameObject[] _objects;
    [SerializeField] private SpawnerSetting spawnerSetting;
    [SerializeField] private int countObjectsInBox;
    [SerializeField] private float radius;
    [SerializeField] private GameObject wholeObject;
    [SerializeField] private GameObject sliceObject;

    [SerializeField] private float slicedDirectionX;
    [SerializeField] private float slicedDirectionY;
    
    private GameObject _blade;
    private PhysicObject[] _pieces;
    private PhysicObject _component;
        
    private void Awake()
    {
        _blade = GameObject.FindWithTag("Player");
        _pieces = sliceObject.GetComponentsInChildren<PhysicObject>();
        _component = GetComponent<PhysicObject>();
        DetectSwipe.EventSwipe += OnSlice;
    }
        
        private void OnSlice()
        {
            var bladePosition = _blade.transform.position;
            var fruitPosition = transform.position;
            
            var distance = Vector3.Distance(fruitPosition, bladePosition);
            if (distance <= radius)
            {
                var rotation = wholeObject.transform.rotation;
                SettingParametersOfHalves(_pieces[0], _pieces[1], rotation);
                CreatingSliceObject(fruitPosition);

                CreateObject();
                Destroy(gameObject);
            }
        }

        private void CreateObject()
        {
            for (int i = 0; i < countObjectsInBox; i++)
            {
                var position = transform.position;
                Vector2 positionFruit = new Vector2(position.x, position.y);
                Quaternion rotationPrefab = Quaternion.Euler(0f, 0f, 0f);
                        
                float force = Random.Range(spawnerSetting.minForce, spawnerSetting.maxForce);
                float angle = Random.Range(spawnerSetting.minAngle, spawnerSetting.maxAngle) * Mathf.Deg2Rad;
                float rotate = Random.Range(spawnerSetting.minAngleRotate, spawnerSetting.maxAngleRotate) * Mathf.Deg2Rad;
                var prefab = _objects[Random.Range(0, _objects.Length)];

                var gameObject = Instantiate(prefab, positionFruit, rotationPrefab).GetComponent<PhysicObject>();
                gameObject.direction = Spawn.VectorFromAngle(angle) * force;
                gameObject.rotate = rotate;
            }
        }
        
        private void OnDisable()
        {
            DetectSwipe.EventSwipe -= OnSlice;
            //BombController.EventExplosion -= CheckExplosion;
        }

        private void SettingParametersOfHalves(PhysicObject firstObject, PhysicObject secondObject, Quaternion rotation)
        {
            sliceObject.transform.localScale = wholeObject.transform.lossyScale;
            firstObject.spriteFruit.rotation = rotation;
            firstObject.direction = new Vector2(_component.direction.x - slicedDirectionX, _component.direction.y - slicedDirectionY);
            secondObject.spriteFruit.rotation = rotation;
            secondObject.direction = new Vector2(_component.direction.x + slicedDirectionX, _component.direction.y + slicedDirectionY);
        }
         
        private void CreatingSliceObject(Vector3 position)
        {
            var newObject = Instantiate(sliceObject, position, Quaternion.identity);
            Destroy(newObject, 3f);
        }
}
