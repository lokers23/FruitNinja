using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace Fruit
{
    public class FruitController : MonoBehaviour
    {
        private const float PositionBlotAxisZ = 19f;
        public static event Action<int> EventSlice;

        [SerializeField] private int damage = 1;
        public int Damage { get => damage; private set => damage = value; }
        [SerializeField] private int score = 30; 
        
        [SerializeField] private TextMeshPro scoreText;

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
            _blade = GameObject.FindWithTag("Player");
            _pieces = sliceObject.GetComponentsInChildren<PhysicObject>();
            _component = GetComponent<PhysicObject>();
            DetectSwipe.EventSwipe += OnSlice;
            BombController.EventExplosion += CheckExplosion;
        }
        
        private void OnSlice()
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
                CreateTextScore(fruitPosition);
                
                EventSlice?.Invoke(score);
                
                Destroy(gameObject);
            }
        }

        private void OnDisable()
        {
            DetectSwipe.EventSwipe -= OnSlice;
            BombController.EventExplosion -= CheckExplosion;
        }

        private void SettingParametersOfHalves(PhysicObject firstObject, PhysicObject secondObject, Quaternion rotation)
        {
            sliceObject.transform.localScale = wholeObject.transform.lossyScale;
            firstObject.spriteFruit.rotation = rotation;
            firstObject.direction = new Vector2(_component.direction.x - slicedDirectionX, _component.direction.y - slicedDirectionY);
            secondObject.spriteFruit.rotation = rotation;
            secondObject.direction = new Vector2(_component.direction.x + slicedDirectionX, _component.direction.y + slicedDirectionY);
        }
         
        private void CreatingSliceObject(Vector3 position)
        {
            var newObject = Instantiate(sliceObject, position, Quaternion.identity);
            Destroy(newObject, 3f);
        }
        
        private  void CreatingBlot(GameObject blot, Vector3 position)
        {
            if (blot == null)
            {
                return;
            }
            
            Instantiate(blot,new Vector3(position.x, position.y, PositionBlotAxisZ) , Quaternion.identity);
        }

        private  void CreateTextScore(Vector3 position)
        {
            TextMeshPro newObject = Instantiate(scoreText, position, quaternion.identity);
            newObject.text = score.ToString();
            Destroy(newObject.gameObject, 1f);
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
                _component.direction = new Vector2(_component.direction.x + heading.x*powerExplosion, _component.direction.y + heading.y* powerExplosion);
            }
            else
            {
                _component.direction = new Vector2(_component.direction.x + heading.x * powerExplosion / 2, _component.direction.y + heading.y * powerExplosion /2 );
            }
        }
    }
}
