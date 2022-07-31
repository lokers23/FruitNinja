using System;
using System.Collections;
using Fruit;
using Fruit.Bonus;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public static event Action EventEndGame;
    [SerializeField] private int heartsCount;
    [SerializeField] private GameObject heartPrefab;
    
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
                _hearts[_hearts.Length - 1 - IndexHead].SetActive(false);
                IndexHead++;
            }
        }
        
        if (IndexHead >= _hearts.Length)
        {
            EventEndGame?.Invoke();
            DestroyInvisible.OnDestroy -= SetDamage;
        }
    }

    private void RecoverHeart(int countRecoverHearts, Vector3 position)
    {
        if (IndexHead > 0)
        {
            for (int i = 0; i < countRecoverHearts; i++)
            {
                var heart = Instantiate(heartPrefab, position, Quaternion.identity);
                IndexHead--;
                StartCoroutine( AnimationHeart(heart));
            }
        }
    }

    private IEnumerator AnimationHeart(GameObject heart)
    {
        var positionHeartInBar = _hearts[_hearts.Length - 1 - IndexHead].transform.position;
        var index = IndexHead;
        while (heart.transform.position != positionHeartInBar)
        {
            heart.transform.position = Vector3.MoveTowards(heart.transform.position, positionHeartInBar, 25 * Time.deltaTime);

            yield return null;
        }

        if (index == IndexHead)
        {
            _hearts[_hearts.Length - 1 - index].SetActive(true);
        }
       
        Destroy(heart);
    }
}
