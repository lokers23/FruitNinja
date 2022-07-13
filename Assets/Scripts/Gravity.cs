using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField] private float gravity = -9.82f;
    public Vector2 direction;
    public float rotateZ;
    private Vector2 gravityVector;
    private Vector3 scaleChange;
    
    private void Awake()
    {
        scaleChange = new Vector3(-0.12f, -0.12f, 0) ;
        gravityVector = new Vector2(0, gravity);
    }

    private void Update()
    {
        transform.localScale += scaleChange * Time.deltaTime;
        transform.GetChild(0).Rotate(0, 0, rotateZ);
        transform.Translate(direction * Time.deltaTime);
        direction += gravityVector * Time.deltaTime;
        if(Vector3.Distance(transform.position, Camera.main.transform.position) > 500)
            Destroy(gameObject);
    }
}
