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

    [Header("AUDIO")]
    [SerializeField]
    private AudioMaster audioMaster;

    private void Awake()
    {
        TriggerAnimation();
    }

    public void OnTrigger(bool value)
    {
        isClosed = !value;
        audioMaster.Play(AudioMaster.AudioType.Trigger);
        TriggerAnimation();
    }

    public void OnToggleDoor()
    {
        isClosed = !isClosed;
        audioMaster.Play(AudioMaster.AudioType.Trigger);
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
