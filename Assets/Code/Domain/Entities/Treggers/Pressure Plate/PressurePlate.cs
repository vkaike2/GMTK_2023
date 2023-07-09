using System;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [Header("EVENTS")]
    [SerializeField]
    private OnPressurPlate OnPressurePlate;
    
    [Header("COMPONENTS")]
    [SerializeField]
    private Animator animator;
    private const string ANIMATION_UP = "Pressure Plate_Up";
    private const string ANIMATION_DOWN = "Pressure Plate_Down";

    [Header("AUDIO")]
    [SerializeField]
    private AudioMaster audioMaster;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player == null) return;

        animator.Play(ANIMATION_DOWN);
        audioMaster.Play(AudioMaster.AudioType.Footstep);
        OnPressurePlate.Invoke(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player == null) return;

        animator.Play(ANIMATION_UP);
        OnPressurePlate.Invoke(false);
    }
}
[Serializable]
public class OnPressurPlate : UnityEvent<bool> { };
