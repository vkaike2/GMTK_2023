using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public List<Player> Players { get; private set; } = new List<Player>();

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
}
