using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("COMPONENTS")]
    [SerializeField]
    private BoxCollider2D boxCollider2D;
    [SerializeField]
    private Animator animator;
    private const string ANIMATION_OPEN = "Open";
    private const string ANIMATION_CLOSE = "Close";


    [Header("CONFIGURATIONS")]
    [SerializeField]
    private bool isClosed = true;

    private void Awake()
    {
        TriggerAnimation();
    }

    public void OnTrigger(bool value)
    {
        isClosed = !value;
        TriggerAnimation();
    }

    public void OnToggleDoor()
    {
        isClosed = !isClosed;
        TriggerAnimation();
    }

    private void TriggerAnimation()
    {
        if (isClosed)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        animator.Play(ANIMATION_OPEN);
        boxCollider2D.enabled = false;
    }

    private void CloseDoor()
    {
        animator.Play(ANIMATION_CLOSE);
        boxCollider2D.enabled = true;
    }
}
