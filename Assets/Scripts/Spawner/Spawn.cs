using System;
using System.Collections;
using Fruit;
using Fruit.Bonus;
using UnityEngine;
using static System.Random;
using Random = UnityEngine.Random;

namespace Spawner
{
    public class Spawn : MonoBehaviour
    {
        [SerializeField] private Transform poolObjects;
        [SerializeField] private GameObject[] fruits;
        
        [SerializeField] private GameObject bomb;
        [SerializeField] private float chanceSpawnBomb = 0.1f;

        [SerializeField] private GameObject heart;
        [SerializeField] private float chanceSpawnHeart = 0.1f;
        
        [SerializeField] private GameObject box;
        [SerializeField] private float chanceSpawnBox = 0.1f;
        
        [SerializeField] private GameObject magnet;
        [SerializeField] private float chanceSpawnMagnet = 0.1f;
        
        [SerializeField] private GameObject iceBlock;
        [SerializeField] private float chanceSpawnIceBlock = 0.1f;
        
        [SerializeField] private float chanceSpawn;
        [SerializeField] private SpawnerSetting spawnerSetting;
        [SerializeField] private DifficultSetting difficultSetting;

        [SerializeField] private HealthController healthController;
        [SerializeField] private float startingDelay = 2f;
        private IEnumerator _coroutineDifficult;

        private void Update()
        {
            if (spawnerSetting.fruitInRow >= difficultSetting.limitNumberFruit)
            {
                StopCoroutine(_coroutineDifficult);
            }
        }

        private void OnEnable()
        {
            HealthController.EventEndGame += OnDisable;
            StartCoroutine(Spawning());
            _coroutineDifficult = IncreasingDifficulty();
            StartCoroutine(_coroutineDifficult);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            HealthController.EventEndGame -= OnDisable;
        }

        private IEnumerator Spawning()
        {
            yield return new WaitForSeconds(startingDelay);
            
            while (enabled)
            {
                int halfCount = spawnerSetting.fruitInRow / 2;
                if (Random.Range(0.0f, 1.0f) <= chanceSpawn)
                {
                    for (int i = 0; i < spawnerSetting.fruitInRow; i++)
                    {
                        var position = transform.position;
                        Vector2 positionFruit = new Vector2(position.x, position.y);
                        Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
                        
                        float force = Random.Range(spawnerSetting.minForce, spawnerSetting.maxForce);
                        float angle = Random.Range(spawnerSetting.minAngle, spawnerSetting.maxAngle) * Mathf.Deg2Rad;
                        float rotate = Random.Range(spawnerSetting.minAngleRotate, spawnerSetting.maxAngleRotate) * Mathf.Deg2Rad;
                        GameObject prefab;
                        if (halfCount > 0 && Random.Range(0.0f, 1.0f) <= chanceSpawnBomb)
                        {
                            prefab = bomb;
                            halfCount--;
                        }
                        else if (healthController.IndexHead > 0 && halfCount > 0 && Random.Range(0.0f, 1.0f) <= chanceSpawnHeart)
                        {
                            prefab = heart;
                            halfCount--;
                        }
                        else if (halfCount > 0 && Random.Range(0.0f, 1.0f) <= chanceSpawnBox)
                        {
                            prefab = box;
                            halfCount--;
                        }
                        else if (halfCount > 0 && Random.Range(0.0f, 1.0f) <= chanceSpawnMagnet)
                        {
                            prefab = magnet;
                            halfCount--;
                        }
                        else if (halfCount > 0 && Random.Range(0.0f, 1.0f) <= chanceSpawnIceBlock)
                        {
                            prefab = iceBlock;
                            halfCount--;
                        }
                        else
                        {
                            prefab = fruits[Random.Range(0, fruits.Length)];
                        }
                        
                        var gameObject = Instantiate(prefab, positionFruit, rotation, poolObjects).GetComponent<PhysicObject>();
                        gameObject.direction = VectorFromAngle(angle) * force;
                        gameObject.rotate = rotate;
                    
                        yield return new WaitForSeconds(spawnerSetting.periodInRow);
                    }
                }

                yield return new WaitForSeconds(Random.Range(spawnerSetting.minSpawnDelay, spawnerSetting.maxSpawnDelay));
            }
        }
        
        private IEnumerator IncreasingDifficulty() 
        {
            yield return new WaitForSeconds(difficultSetting.periodUpDifficult);
            while(enabled)
            {
                spawnerSetting.fruitInRow += difficultSetting.increaseNumberFruit;
                spawnerSetting.minSpawnDelay -= difficultSetting.reduceSpawnDelay;
                spawnerSetting.maxSpawnDelay -= difficultSetting.reduceSpawnDelay;
                
                yield return new WaitForSeconds(difficultSetting.periodUpDifficult);
            }
        }

        public static Vector2 VectorFromAngle(float angle)
        {
            var sin = Mathf.Sin(angle);
            var cos = Mathf.Cos(angle);
            return new Vector2(cos, sin);
        }
    }
}
