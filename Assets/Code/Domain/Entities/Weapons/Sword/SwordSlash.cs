using System.Collections;
using UnityEngine;

public class SwordSlash : Projectile
{
    [Header("CONFIGURATIONS")]
    [SerializeField]
    private float lifeTime = 1;
    [SerializeField]
    private float speed = 1;

    private Rigidbody2D _rigidbody2D;
    private Player _player;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(WaitThenDie());
    }

    public void Initialize(Player player)
    {
        _player = player;
        _rigidbody2D.velocity = WeaponUtils.GetMouseNormalizedDirection(this.transform) * speed;
    }

    public override void KillIt()
    {
        Destroy(gameObject);
    }

    private IEnumerator WaitThenDie()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)CustomLayers.Hitbox)
        {
            Player player = collision.gameObject.GetComponentInParent<Player>();

            if (player.GetInstanceID() == _player.GetInstanceID()) return;

            player.ReceiveDamage(this.transform.position, StageManager.WinCondition.Sword);
        }
        if (collision.gameObject.layer == (int)CustomLayers.Door)
        {
            Destroy(this.gameObject);
        }
    }
}
