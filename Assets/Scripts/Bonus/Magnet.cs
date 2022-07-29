using System;
using System.Collections;
using System.Collections.Generic;
using Fruit;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private PhysicObject _component;
    private void Awake()
    {
        MagnetController.MagnetEvent += CheckMagnet;
        _component = GetComponent<PhysicObject>();
        
    }

    private void OnDisable()
    {
        MagnetController.MagnetEvent -= CheckMagnet;    
    }

    private void CheckMagnet(float power, float radiusMagnet, Vector3 positionMagnet)
    {
        _component.speedScale = Vector3.zero;
        var heading = positionMagnet - transform.position;
        var magnitude = heading.sqrMagnitude;
        if (magnitude >= radiusMagnet * radiusMagnet)
        {
            return;
        }
        
        transform.position = Vector3.MoveTowards(transform.position, positionMagnet, power * Time.deltaTime);
        _component.direction = Vector2.zero;
    }
}
