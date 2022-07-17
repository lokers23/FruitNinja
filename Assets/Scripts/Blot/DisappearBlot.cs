using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearBlot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
       var q =  _spriteRenderer.color;
       q.a -= 0.2f * Time.deltaTime;
       _spriteRenderer.color = q;

    }
}
