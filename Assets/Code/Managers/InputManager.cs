using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("MANAGERS")]
    [SerializeField]
    private GameManager gameManager;

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        gameManager.GetSelectedPlayer().OnMoveInput(context);
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        gameManager.GetSelectedPlayer().OnInteraction(context);
    }

    public void OnRightMouseButton(InputAction.CallbackContext context)
    {
        gameManager.GetSelectedPlayer().OnDropWeapon(context);
    }

    public void OnSwapinput(InputAction.CallbackContext context)
    {
        gameManager.OnSwapInput(context);
    }
}
