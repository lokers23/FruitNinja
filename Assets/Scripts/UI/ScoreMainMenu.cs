using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class ScoreMainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    private void Awake()
    {
        if (PlayerPrefs.HasKey(ScoreController.KeyPlayerPrefs))
        {
            var score = PlayerPrefs.GetInt(ScoreController.KeyPlayerPrefs);
            textMeshPro.text = score.ToString();
        }
    }
}
