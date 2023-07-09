using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private bool isTesting = false;

    [Header("MANAGERS")]
    [SerializeField]
    private GameManager gameManager;

    private bool _isGameStarted = false;
    private StageManager _stageManager;

    private void Start()
    {
        _stageManager = GameObject.FindObjectOfType<StageManager>();

        _stageManager.OnStartGame.AddListener(StartGame);
    }

    private void StartGame()
    {
        _isGameStarted = true;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (!_isGameStarted) return;

        Player player =  gameManager.GetSelectedPlayer();
        if (player == null) return;
        player.OnMoveInput(context);
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (!_isGameStarted) return;

        Player player = gameManager.GetSelectedPlayer();
        if (player == null) return;

        player.OnInteraction(context);
    }

    public void OnRightMouseButton(InputAction.CallbackContext context)
    {
        if (!_isGameStarted) return;

        Player player = gameManager.GetSelectedPlayer();
        if (player == null) return;

        player.OnDropWeapon(context);
    }

    public void OnSwapinput(InputAction.CallbackContext context)
    {
        if (!isTesting) return;

        gameManager.OnSwapInput(context);
    }
}
