using System;
using Fruit;
using Fruit.Bonus;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public static event Action EventEndGame;
    [SerializeField] private int heartsCount;

    //[SerializeField] private int damage;

    private GameObject[] _hearts;

    public int IndexHead { get; private set; } = 0;

    private void OnEnable()
    {
        HeartController.EventSliceHeart += RecoverHeart;
        DestroyInvisible.OnDestroy += SetDamage;
        BombController.EventSliceBomb += SetDamage;
    }

    private void OnDisable()
    {
        HeartController.EventSliceHeart -= RecoverHeart;
        DestroyInvisible.OnDestroy -= SetDamage;
        BombController.EventSliceBomb -= SetDamage;    
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

    private void SetDamage(int damage)
    {
        if (IndexHead < _hearts.Length)
        {
            for (int i = 0; i < damage; i++)
            {
                _hearts[IndexHead].SetActive(false);
                IndexHead++;
            }
        }
        
        if (IndexHead >= _hearts.Length)
        {
            EventEndGame?.Invoke();
            DestroyInvisible.OnDestroy -= SetDamage;
        }
    }

    private void RecoverHeart(int countRecoverHearts)
    {
        if (IndexHead > 0)
        {
            for (int i = 0; i < countRecoverHearts; i++)
            {
                IndexHead--;
                _hearts[IndexHead].SetActive(true);
            }
        }
    }
}
