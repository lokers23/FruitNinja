using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Fruit;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int numberOfHearts;

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    
    [SerializeField] private Text textOverThreeHeart;
    
    [SerializeField] private int damage;

    private StringBuilder _text;
    private const string Plus = "+";
    private void Awake()
    {
        DestroyInvisible.OnDestroy += SetDamage;
        _text = new StringBuilder(50);
    }

    private void FixedUpdate()
    {
        _text.Length = 0;
        if (health > numberOfHearts)
        {
            health = numberOfHearts;
        }
        
        ChangeHealth();
        
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = i < health ? fullHeart : emptyHeart;

            hearts[i].enabled = i < numberOfHearts;
        }
    }

    private void ChangeHealth()
    {
        if (health > hearts.Length)
        {
            _text.Append(Plus);
            _text.Append(health - hearts.Length);
            textOverThreeHeart.text = _text.ToString();
        }
        else
        {
            textOverThreeHeart.text = "";
        }
    }

    private void SetDamage()
    {
        health -= damage;
    }
    
}
