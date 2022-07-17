using System;
using System.Collections;
using Fruit;
using UnityEngine;
using static System.Random;
using Random = UnityEngine.Random;

namespace Spawner
{
    public class Spawn : MonoBehaviour
    { 
        [SerializeField] private GameObject[] fruits;
        [SerializeField] private SpawnerSetting spawnerSetting;
        [SerializeField] private DifficultSetting difficultSetting;

        private const float StartingDelay = 2f;
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
            StartCoroutine(Spawning());
            _coroutineDifficult = IncreasingDifficulty();
            StartCoroutine(_coroutineDifficult);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator Spawning()
        {
            yield return new WaitForSeconds(StartingDelay);
            while (enabled)
            {
                for (int i = 0; i < spawnerSetting.fruitInRow; i++)
                {
                    var position = transform.position;
                    Vector2 positionFruit = new Vector2(position.x, position.y);
                    Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
                    GameObject prefab = fruits[Random.Range(0, fruits.Length)];
                    
                    float force = Random.Range(spawnerSetting.minForce, spawnerSetting.maxForce);
                    float angle = Random.Range(spawnerSetting.minAngle, spawnerSetting.maxAngle) * Mathf.Deg2Rad;
                    float rotate = Random.Range(spawnerSetting.minAngleRotate, spawnerSetting.maxAngleRotate) * Mathf.Deg2Rad;
                    
                    var fruit = Instantiate(prefab, positionFruit, rotation).GetComponent<PhysicObject>();
                    fruit.direction = VectorFromAngle(angle) * force;
                    fruit.rotate = rotate;
                    
                    yield return new WaitForSeconds(spawnerSetting.periodInRow);
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

        private static Vector2 VectorFromAngle(float angle)
        {
            var sin = Mathf.Sin(angle);
            var cos = Mathf.Cos(angle);
            return new Vector2(cos, sin);
        }
    }
}
