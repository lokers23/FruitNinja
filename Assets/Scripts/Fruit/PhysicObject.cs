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

        private void Update()
        {
            transform.localScale += speedScale * Time.deltaTime;
            spriteFruit.Rotate(new Vector3(0, 0, rotate));
            transform.Translate(direction * Time.deltaTime);
            direction += gravityVector * Time.deltaTime;
        }
    }
}
