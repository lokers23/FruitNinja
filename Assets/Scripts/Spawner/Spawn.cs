using System;
using System.Collections;
using UnityEngine;
using static System.Random;
using Random = UnityEngine.Random;

namespace Spawner
{
    public class Spawn : MonoBehaviour
    { 
        [SerializeField]private GameObject[] fruits;
        
        [SerializeField] private int fruitInRow;
        [SerializeField] private float periodInRow;
        
        [SerializeField] private int periodUpDifficult;
        [SerializeField] private int increaseNumberFruit;
        [SerializeField] private float limitNumberFruit;
        [SerializeField] private float reduceSpawnDelay;
        
        [SerializeField]private float maxAngleRotate;
        [SerializeField]private float minAngleRotate;
        
        [SerializeField]private float minForce;
        [SerializeField]private float maxForce;
         
        [SerializeField]private float minAngle;
        [SerializeField]private float maxAngle;
        
        [SerializeField]private float minSpawnDelay;
        [SerializeField]private float maxSpawnDelay;

        private IEnumerator coroutineDifficult;

        private void Update()
        {
            if (fruitInRow >= limitNumberFruit)
            {
                StopCoroutine(coroutineDifficult);
            }
        }

        private void OnEnable()
        {
            StartCoroutine(Spawning());
            coroutineDifficult = IncreasingDifficulty();
            StartCoroutine(coroutineDifficult);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator Spawning()
        {
            yield return new WaitForSeconds(2f);
            while (enabled)
            {
                for (int i = 0; i < fruitInRow; i++)
                {
                    var position = transform.position;
                    Vector2 positionFruit = new Vector2(position.x, position.y);
                    Quaternion rotation = Quaternion.Euler(0f, 0f, 0);
                    GameObject prefab = fruits[Random.Range(0, fruits.Length)];
                    
                    float force = Random.Range(minForce, maxForce);
                    float angle = Random.Range(minAngle, maxAngle) * Mathf.Deg2Rad;
                    float rotate = Random.Range(minAngleRotate, maxAngleRotate);
                    
                    var fruit = Instantiate(prefab, positionFruit, rotation).GetComponent<Gravity>();
                    fruit.direction = VectorFromAngle(angle) * force;
                    fruit.rotateZ = rotate * Mathf.Deg2Rad;
                    
                    yield return new WaitForSeconds(periodInRow);
                }
                
                yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            }
        }
        
        private IEnumerator IncreasingDifficulty() {
            yield return new WaitForSeconds(periodUpDifficult);
            for(;;)
            {
                fruitInRow += increaseNumberFruit;
                minSpawnDelay -= reduceSpawnDelay;
                maxSpawnDelay -= reduceSpawnDelay;
                
                yield return new WaitForSeconds(periodUpDifficult);
            }
        }
        
        Vector2 VectorFromAngle(float angle)
        {
            var sin = Mathf.Sin(angle);
            var cos = Mathf.Cos(angle);
            return new Vector2(cos, sin);
        }
    }
}
