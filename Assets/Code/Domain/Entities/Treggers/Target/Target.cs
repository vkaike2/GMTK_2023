using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    [Header("EVENTS")]
    [SerializeField]
    private UnityEvent OnTrigger;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();

        if (projectile == null) return;
        OnTrigger.Invoke();
        projectile.KillIt();
    }
}
