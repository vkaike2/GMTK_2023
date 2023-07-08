using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("CONFIGURATIONS")]
    [SerializeField]
    private string canvasSceneName = "External Canvas";

    public List<Player> Players { get; private set; } = new List<Player>();



    private StageManager _stageManager;

    private void Awake()
    {
        LoadUIScene();
    }

    private void Start()
    {
        _stageManager = GameObject.FindObjectOfType<StageManager>();

        _stageManager.OnFinishState.AddListener(OnFinishStage);
    }

    public void AddPlayer(Player player)
    {
        Players.Add(player);
    }
    
    public Player GetSelectedPlayer()
    {
        return Players.FirstOrDefault(e => e.IsSelected);
    }

    public void OnSwapInput(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;

        Player unselectedPlayer = Players.FirstOrDefault(e => !e.IsSelected);
        Player selectedPlayer = GetSelectedPlayer();

        selectedPlayer.Deselect();
        unselectedPlayer.Select();
    }

    internal void SelectPlayer(Player player)
    {
        foreach (var myPlayer in Players)
        {
            myPlayer.Deselect();
        }

        Player nextPlayer = Players.FirstOrDefault(e => e.GetInstanceID() == player.GetInstanceID());
        nextPlayer.Select();
    }

    private void LoadUIScene()
    {
        if (!IsSceneLoaded(canvasSceneName))
        {
            SceneManager.LoadScene(canvasSceneName, LoadSceneMode.Additive);
        }
    }

    private bool IsSceneLoaded(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name == sceneName)
            {
                return true;
            }
        }
        return false;
    }

    private void OnFinishStage()
    {
        foreach(var player in Players)
        {
            player.Deselect();
        }
    }

}
