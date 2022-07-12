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
        //[SerializeField]private GameObject fruit;
        //[SerializeField] private float power;
        //[SerializeField] private float angle;
        //private Vector2 vectorAngle;

        //[SerializeField]private Vector2 startSpawn;
        // [SerializeField]private Vector2 endSpawn;

       [SerializeField]private float minForce;
       [SerializeField]private float maxForce;
        
       [SerializeField]private float minAngle;
       [SerializeField]private float maxAngle;
       
       [SerializeField]private float minSpawnDelay;
       [SerializeField]private float maxSpawnDelay;
        
        // Start is called before the first frame update
        private void Awake()
        {
        }

        private void OnEnable()
        {
            StartCoroutine(Spawning());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        
        private void Update()
        {
        }
        
        private IEnumerator Spawning()
        {
            yield return new WaitForSeconds(2f);
        
            while (enabled)
            {
                var position = transform.position;
                Vector2 positionFruit = new Vector2(position.x, position.y);
                Quaternion rotation = Quaternion.Euler(0f, 0f, 0);
                GameObject prefab = fruits[Random.Range(0, fruits.Length)];

                float force = Random.Range(minForce, maxForce);
                float angle = Random.Range(minAngle, maxAngle) * Mathf.Deg2Rad;

                Instantiate(prefab, positionFruit, rotation).GetComponent<Gravity>().direction = VectorFromAngle(angle) * force;
        
                yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
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
