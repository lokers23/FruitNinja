using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private string _nameScene = "";
    private Animator _animator;
    private static readonly int ClickButtonPlay = Animator.StringToHash("ClickButtonPlay");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAnimationFade(string nameScene)
    {
        _nameScene = nameScene;
        _animator.SetTrigger(ClickButtonPlay);
    }
    public void NextScene()
    {
        SceneManager.LoadScene(_nameScene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
