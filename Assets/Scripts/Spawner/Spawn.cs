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
        [SerializeField]private float angleRotate;
        [SerializeField]private float minForce;
        [SerializeField]private float maxForce;
         
        [SerializeField]private float minAngle;
        [SerializeField]private float maxAngle;
        
        [SerializeField]private float minSpawnDelay;
        [SerializeField]private float maxSpawnDelay;
        private float random;

        private void Awake()
        {
            // Vector2 leftbottom = Camera.main.ViewportToWorldPoint (new Vector2 (0,0)); // bottom-left corner
            // Vector2 righttop = Camera.main.ViewportToWorldPoint (new Vector2 (1,1)); // right-top
            // Vector2 rightbot = Camera.main.ViewportToWorldPoint(new Vector2(1, 0)); // right-bot
            // Vector2 lefttop = Camera.main.ViewportToWorldPoint(new Vector2(0, 1)); // right-bot
            // //random = Random.Range(righttop.x/2, leftbottom.x/2);
            // float x = transform.position.x;
            // float y = transform.position.y;
            //
            // if (leftbottom.y < y)
            // {
            //     transform.position = new Vector2(0, leftbottom.y - 1f);
            // }
            // else if (leftbottom.x < x)
            // {
            //     transform.position = new Vector2(righttop.x - 0.5f, righttop.y/2);
            // }
            // else if (leftbottom.x > x)
            // {
            //     transform.position = new Vector2(lefttop.x - 0.5f, lefttop.y/2);
            // }
        }

        private void OnEnable()
        {
            
            
            StartCoroutine(Spawning());
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
                var position = transform.position;
                Vector2 positionFruit = new Vector2(position.x, position.y);
                Quaternion rotation = Quaternion.Euler(0f, 0f, 0);
                GameObject prefab = fruits[Random.Range(0, fruits.Length)];
                float force = Random.Range(minForce, maxForce);
                float angle = Random.Range(minAngle, maxAngle) * Mathf.Deg2Rad;
                
                var fruit = Instantiate(prefab, positionFruit, rotation).GetComponent<Gravity>();
                fruit.direction = VectorFromAngle(angle) * force;
                fruit.rotateZ = angleRotate * Mathf.Deg2Rad;
                
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
