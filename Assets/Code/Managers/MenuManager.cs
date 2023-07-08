using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("CONFIGURATIONS")]
    [SerializeField]
    private string firstStageName = "Stage 1";
    [SerializeField]
    private SceneTransition sceneTransition;

    private void Start()
    {
        sceneTransition.OnEndTransitionEnd.AddListener(OnEndTransition);
    }

    private void OnEndTransition()
    {
        SceneManager.LoadScene(firstStageName);
    }

    public void StartGame()
    {
        sceneTransition.OnFinishStage();
    }
}
