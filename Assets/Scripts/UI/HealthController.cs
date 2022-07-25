using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Fruit;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public static event Action EventEndGame;
    [SerializeField] private int heartsCount;

    [SerializeField] private int damage;

    private GameObject[] _hearts;
    private int _indexHead = 0;
    private void Awake()
    {
        DestroyInvisible.OnDestroy += SetDamage;
    }

    private void Start()
    {
        _hearts = new GameObject[heartsCount];
        var heartTemplate = transform.GetChild(0).gameObject;
        for (var i = 0; i < heartsCount; i++)
        {
            _hearts[i] = Instantiate(heartTemplate, transform);
        }
        
        Destroy(heartTemplate);
    }

    private void SetDamage()
    {
        if (_indexHead < _hearts.Length)
        {
            for (int i = 0; i < damage; i++)
            {
                _hearts[_indexHead].SetActive(false);
                _indexHead++;
            }
        }
        
        if (_indexHead >= _hearts.Length)
        {
            EventEndGame?.Invoke();
            DestroyInvisible.OnDestroy -= SetDamage;
        }
    }
}
