using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField]
    private bool isSelected;
    [SerializeField]
    private bool imMonster;

    [Header("CONFIGURATION")]
    [SerializeField]
    private float movementSpeed = 5f;

    public float MovementSpeed => movementSpeed;
    public bool IsSelected { get => isSelected; set => isSelected = value; }
    public bool ImMonster { get => imMonster; set => imMonster = value; }

    public Weapon InteractableWeapon { get; private set; }

    internal void CanInteractWith(Weapon weapon)
    {
        InteractableWeapon = weapon;
    }

    internal void CantInteract()
    {
        InteractableWeapon = null;
    }
}
