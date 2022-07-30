using System;
using UnityEngine;

namespace Fruit
{
    public class PhysicObject : MonoBehaviour
    {
        [SerializeField] private Vector2 gravityVector = new Vector2(0, -9.82f);
        [SerializeField] public Vector3 speedScale = new Vector3(-0.12f, -0.12f, 0) ;
        public Transform spriteFruit;
        
        [HideInInspector] public Vector2 direction = Vector2.zero;
        [HideInInspector] public float rotate;

        private void Awake()
        {
            BombController.EventExplosion += CheckExplosion;
        }

        private void OnDisable()
        {
            BombController.EventExplosion -= CheckExplosion;        }

        private void Update()
        {
            transform.localScale += speedScale * Time.deltaTime;
            spriteFruit.Rotate(new Vector3(0, 0, rotate));
            transform.Translate(direction * Time.deltaTime);
            direction += gravityVector * Time.deltaTime;
        }
        
        private void CheckExplosion(float powerExplosion, float minRadiusExplosion, float maxRadiusExplosion, Vector3 positionBomb)
        {
            var heading = transform.position - positionBomb;
            var magnitude = heading.sqrMagnitude;
            if (magnitude >= maxRadiusExplosion * maxRadiusExplosion)
            {
                return;
            }
            
            if (magnitude < minRadiusExplosion * minRadiusExplosion)
            {
                direction = new Vector2(direction.x + heading.x*powerExplosion, direction.y + heading.y* powerExplosion);
            }
            else
            {
                direction = new Vector2(direction.x + heading.x * powerExplosion / 2, direction.y + heading.y * powerExplosion /2 );
            }
        }
    }
}
