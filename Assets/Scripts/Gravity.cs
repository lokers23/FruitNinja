using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField] private float gravity = -9.82f;
    public Vector2 direction;
    private Vector2 gravityVector;
    
    private void Awake()
    {
        gravityVector = new Vector2(0, gravity) * Time.deltaTime;
    }

    private void Update()
    {
        transform.Translate(direction * Time.deltaTime);
        direction += gravityVector;
    }
}
