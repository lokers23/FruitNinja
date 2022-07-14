using UnityEngine;

namespace Fruit
{
    public class PhysicObject : MonoBehaviour
    {
        [SerializeField] private Vector2 gravityVector = new Vector2(0, -9.82f);
        [SerializeField] private Vector3 speedScale = new Vector3(-0.12f, -0.12f, 0) ;
        [SerializeField] private Transform spriteFruit;
        
        [HideInInspector] public Vector2 direction;
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
