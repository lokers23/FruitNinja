using System;
using UnityEngine;

namespace Fruit
{
    public class FruitController : MonoBehaviour
    {
        private const float PositionBlotAxisZ = 19f;
        public static event Action EventSlice; 
        
        [SerializeField] private float radius;
        [SerializeField] private GameObject wholeObject;
        [SerializeField] private GameObject sliceObject;
        [SerializeField] private GameObject blot;

        [SerializeField] private float slicedDirectionX;
        [SerializeField] private float slicedDirectionY;
        
        private GameObject _blade;
        private PhysicObject[] _pieces;
        private PhysicObject _component;
        
        private void Awake()
        {
            _blade = GameObject.Find("Blade");
            _pieces = sliceObject.GetComponentsInChildren<PhysicObject>();
            _component = GetComponent<PhysicObject>();
            DetectSwipe.EventSwipe += OnSlice;
        }

        private void OnSlice()
        {
            if (this != null)
            {
                var bladePosition = _blade.transform.position;
                var fruitPosition = transform.position;
                
                var distance = Vector3.Distance(fruitPosition, bladePosition);
                if (distance <= radius)
                {
                    var rotation = wholeObject.transform.rotation;
                    SettingParametersOfHalves(_pieces[0], _pieces[1], rotation);
                    CreatingSliceObject(fruitPosition);
                    CreatingBlot(blot, fruitPosition);
                
                    EventSlice?.Invoke();
                    
                    Destroy(gameObject);
                    DetectSwipe.EventSwipe -= OnSlice;
                }
            }
        }
        
        private void SettingParametersOfHalves(PhysicObject firstObject, PhysicObject secondObject, Quaternion rotation)
        {
            firstObject.spriteFruit.rotation = rotation;
            firstObject.direction = new Vector2(_component.direction.x - slicedDirectionX, _component.direction.y - slicedDirectionY);
            secondObject.spriteFruit.rotation = rotation;
            secondObject.direction = new Vector2(_component.direction.x + slicedDirectionX, _component.direction.y + slicedDirectionY);
        }
        
        private void CreatingSliceObject(Vector3 position)
        {
            var newObject = Instantiate(sliceObject, position, Quaternion.identity);
            Destroy(newObject, 2f);
        }
        
        private static void CreatingBlot(GameObject blot, Vector3 position)
        {
            if (blot == null)
            {
                return;
            }
            
            Instantiate(blot,new Vector3(position.x, position.y, PositionBlotAxisZ) , Quaternion.identity);
        }
    }
}
