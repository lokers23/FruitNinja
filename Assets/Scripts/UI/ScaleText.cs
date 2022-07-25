using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleText : MonoBehaviour
{
    [SerializeField] private Transform scale;
    [SerializeField] private float speedReduceScale;

    private void Update()
    {
        scale.localScale -= new Vector3(speedReduceScale, speedReduceScale) * Time.deltaTime;
    }
}
