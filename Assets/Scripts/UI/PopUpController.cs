using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopUpController : MonoBehaviour
{
    private const string StringTriggerAppear = "GameEnd";
    private const string StringTriggerClickButton = "ButtonClick";
    
    [SerializeField] private TextMeshProUGUI popUpCurrentScore;
    [SerializeField] private TextMeshProUGUI popUpBestScore;
    
    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI bestScore;
    [SerializeField] private GameObject popUp;

    private Animator _animator;
    private string _nameScene = "";
    private static readonly int EndGame = Animator.StringToHash(StringTriggerAppear);
    private static readonly int ClickButton = Animator.StringToHash(StringTriggerClickButton);

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        HealthController.EventEndGame += AppearPopupRestart;
    }

    private void OnDisable()
    {
        HealthController.EventEndGame -= AppearPopupRestart;
    }
    private void AppearPopupRestart()
    {
        popUpCurrentScore.text = currentScore.text;
        popUpBestScore.text = "Лучший: " + bestScore.text;
        _animator.SetTrigger(EndGame);
    }
    
    public void PlayAnimationFade(string newNameScene)
    {
        _nameScene = newNameScene;
        _animator.SetTrigger(ClickButton);
    }

    private void NextScene()
    {
        SceneManager.LoadScene(_nameScene);
    }
    
}
