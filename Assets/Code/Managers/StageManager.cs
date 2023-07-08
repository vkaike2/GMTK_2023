using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [Header("INFORMATION")]
    [SerializeField]
    private int stageNumber;
    [SerializeField]
    private Sprite winConditionSprite;

    [Header("CONFIGURATION")]
    [SerializeField]
    private string nextStageName;

    public UnityEvent OnStartGame { get; set; } = new UnityEvent();
    public UnityEvent OnFinishState { get; set; } = new UnityEvent();

    public int StageNumber => stageNumber;
    public Sprite WinConditionSprite => winConditionSprite;

    private GameManager _gameManager;
    private SceneTransition _sceneTransiton;
    

    private void Awake()
    {
        if (string.IsNullOrEmpty(nextStageName)) throw new Exception("nextStageName can't be null");

        OnStartGame.AddListener(StartGame);
    }

    private void Start()
    {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        _sceneTransiton = GameObject.FindObjectOfType<SceneTransition>();

        _sceneTransiton.OnEndTransitionEnd.AddListener(LoadNextStage);
    }

    private void LoadNextStage()
    {
        if(nextStageName == null)
        {
            Debug.Log("win game - lol");
            return;
        }

        SceneManager.LoadScene(nextStageName);
    }

    private void StartGame()
    {
        _gameManager.GetSelectedPlayer().Select();
    }
}
