using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicObject : MonoBehaviour
{
    [SerializeField] private Vector2 gravityVector = new Vector2(0, -9.82f);
    [SerializeField] private Vector3 speedScale = new Vector3(-0.12f, -0.12f, 0) ;
    
    [HideInInspector] public Vector2 direction;
    [HideInInspector] public float rotate;
    
    private void Update()
    {
        transform.localScale += speedScale * Time.deltaTime;
        transform.GetChild(0).Rotate(new Vector3(0, 0, rotate));
        transform.Translate(direction * Time.deltaTime);
        direction += gravityVector * Time.deltaTime;
    }
}
