using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    [Header("EVENTS")]
    [SerializeField]
    private UnityEvent OnTrigger;

    [Header("AUDIO")]
    [SerializeField]
    private AudioMaster audioMaster;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile == null) return;

        audioMaster.Play(AudioMaster.AudioType.Trigger);

        OnTrigger.Invoke();
        projectile.KillIt();
    }

}
