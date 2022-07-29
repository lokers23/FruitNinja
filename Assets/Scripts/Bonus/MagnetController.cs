using System;
using System.Collections;
using System.Collections.Generic;
using Fruit;
using UnityEngine;

public class MagnetController : MonoBehaviour
{
    private const float PositionBonusImageAxisZ = 19f;
    public static event Action<float, float, Vector3> MagnetEvent;
    [SerializeField] private float powerMagnet;
    [SerializeField] private float radiusMagnet;
    [SerializeField] private float timeAction;
    [SerializeField] private float radiusSlice;
    [SerializeField] private GameObject _particleObject;
    [SerializeField] private PhysicObject _physicObject;
    
    
    [SerializeField] private GameObject wholeObject;
    [SerializeField] private GameObject sliceObject;
    [SerializeField] private GameObject imageAfterSlice;
    
    [SerializeField] private float slicedDirectionX;
    [SerializeField] private float slicedDirectionY;
    
    private GameObject _blade;
    private PhysicObject[] _pieces;
    private PhysicObject _component;
    private bool _isMagneting;
    private Vector3 _speedScale;
    private void Awake()
    {
        _blade = GameObject.FindWithTag("Player");
        _pieces = sliceObject.GetComponentsInChildren<PhysicObject>();
        _component = GetComponent<PhysicObject>();
        DetectSwipe.EventSwipe += OnSlice;
    }

    private void OnDisable()
    {
        DetectSwipe.EventSwipe -= OnSlice;
    }

    private void OnSlice()
    {
        var bladePosition = _blade.transform.position;
        var basePosition = transform.position;
            
        var distance = Vector3.Distance(basePosition, bladePosition);
        if (distance <= radiusSlice)
        {
            if (!_isMagneting)
            {
                _speedScale = _physicObject.speedScale;
                CreateImage(basePosition);
                var particles = Instantiate(_particleObject,basePosition, Quaternion.identity);
                Destroy(particles,timeAction);
                StartCoroutine(Magneting());
            }
        }
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
    
    private  void CreateImage(Vector3 position)
    {
        var image = Instantiate(imageAfterSlice,new Vector3(position.x, position.y, PositionBonusImageAxisZ) , Quaternion.identity);
    }
    
    IEnumerator Magneting()
    {
        _isMagneting = true;
        _physicObject.speedScale = Vector3.zero;
        var currentTime = Time.time;
        var positionMagnet = transform.position;
        while (Time.time - currentTime <= timeAction)
        {
            _physicObject.direction = Vector2.zero;
            MagnetEvent?.Invoke(powerMagnet * Time.deltaTime, radiusMagnet, positionMagnet);
            yield return null;
        }

        _physicObject.speedScale = _speedScale;
        SettingParametersOfHalves(_pieces[0], _pieces[1], wholeObject.transform.rotation);
        CreatingSliceObject(transform.position);
        Destroy(gameObject);
    }
}
