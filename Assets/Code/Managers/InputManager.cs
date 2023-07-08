using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private bool isTesting = false;

    [Header("MANAGERS")]
    [SerializeField]
    private GameManager gameManager;

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        Player player =  gameManager.GetSelectedPlayer();
        if (player == null) return;

        player.OnMoveInput(context);
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        Player player = gameManager.GetSelectedPlayer();
        if (player == null) return;

        player.OnInteraction(context);
    }

    public void OnRightMouseButton(InputAction.CallbackContext context)
    {
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
