using UnityEngine;

namespace Fruit
{
    public class FruitController : MonoBehaviour
    {
        [SerializeField] private float radius;
        [SerializeField] private GameObject whole;
        [SerializeField] private GameObject sliced;
        [SerializeField] private GameObject blot;
        [SerializeField] private float slicedDirectionX;
        [SerializeField] private float slicedDirectionY;
        
        private GameObject _blade;
        private PhysicObject[] _pieces;
        
        private const float PositionBlotAxisZ = 19f;
        
        private void Awake()
        {
            _blade = GameObject.Find("Blade");
            _pieces = sliced.GetComponentsInChildren<PhysicObject>();
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
                    whole.SetActive(false);
                    sliced.SetActive(true);

                    var rotation = whole.transform.rotation;

                    _pieces[0].spriteFruit.rotation = rotation;
                    _pieces[1].spriteFruit.rotation = rotation;

                    _pieces[0].direction = new Vector2(-slicedDirectionX, -slicedDirectionY);
                    _pieces[1].direction = new Vector2(slicedDirectionX, slicedDirectionY);

                    var position = whole.transform.position;
                    if (blot != null)
                    {
                        Instantiate(blot,new Vector3(position.x, position.y, PositionBlotAxisZ) , Quaternion.identity);
                    }
                    
                    DetectSwipe.EventSwipe -= OnSlice;
                }
            }
            
        }
    }
}
