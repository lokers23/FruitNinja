using System;
using System.Collections;
using System.Collections.Generic;
using Fruit;
using UnityEngine;
using UnityEngine.UI;

public class IceController : MonoBehaviour
{
    private const float PositionBonusImageAxisZ = 19f;

    [SerializeField] private float timeSlowAction = 5f;
    [SerializeField] private float slowPower = 0.07f;
    
    [SerializeField] private float radius;
    [SerializeField] private GameObject wholeObject;
    [SerializeField] private GameObject sliceObject;
    [SerializeField] private GameObject imageAfterSlice;

    [SerializeField] private float slicedDirectionX;
    [SerializeField] private float slicedDirectionY;
    
    private GameObject _blade;
    private PhysicObject[] _pieces;
    private PhysicObject _component;
    private DestroyInvisible _destroyInvisible;
    
    private void Awake()
    {
        _blade = GameObject.FindWithTag("Player");
        _pieces = sliceObject.GetComponentsInChildren<PhysicObject>();
        _destroyInvisible = GetComponent<DestroyInvisible>();
        _component = GetComponent<PhysicObject>();
        DetectSwipe.EventSwipe += OnSlice;
    }

    private IEnumerator TimeScaling()
    {
        while (enabled)
        {
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0.0f, 1.0f);
            Time.fixedDeltaTime = Mathf.Clamp(Time.fixedDeltaTime, 0.0f, 0.02f);
            Time.timeScale += (1f / timeSlowAction) * Time.unscaledDeltaTime;
            Time.fixedDeltaTime += (0.02f / timeSlowAction) * Time.unscaledDeltaTime;
            yield return null;
        }
        
    }
    
    private void OnSlice()
    {
        var bladePosition = _blade.transform.position;
        var basePosition = transform.position;
            
        var distance = Vector3.Distance(basePosition, bladePosition);
        if (distance <= radius)
        {
            var rotation = wholeObject.transform.rotation;
             SettingParametersOfHalves(_pieces[0], _pieces[1], rotation);
             CreatingSliceObject(basePosition);
             CreateImage(basePosition);

             StartCoroutine(TimeScaling());
             SlowMotion();
             wholeObject.SetActive(false);
             _destroyInvisible.enabled = false;
             Destroy(gameObject, timeSlowAction);
             DetectSwipe.EventSwipe -= OnSlice;
             
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
        Instantiate(imageAfterSlice,new Vector3(position.x, position.y, PositionBonusImageAxisZ) , Quaternion.identity);
    }
    
    private void OnDisable()
    {
        DetectSwipe.EventSwipe -= OnSlice;
    }
    
    private void SlowMotion()
    {
        Time.timeScale = slowPower;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
}
