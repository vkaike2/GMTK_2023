using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowProjectile : Projectile
{
    [Header("SPECIFIC CONFIGURATIONS")]
    [SerializeField]
    private float lifeTime = 2;

    [Header("SPECIFIC COMPONENTS")]
    [SerializeField]
    private Animator animator;
    private const string ANIMATION_IDLE = "Arrow_Idle";
    private const string ANIMATION_DIE = "Arrow_Die";

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

    public void Initialize(Player player, float speed)
    {
        _player = player;

        Vector2 velocity = WeaponUtils.GetMouseNormalizedDirection(this.transform) * speed;

        _rigidbody2D.velocity = velocity;

        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        angle += -90f;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        this.transform.rotation = rotation;
    }

    public override void KillIt()
    {
        StartCoroutine(TriggerDieAnimation());
    }

    private IEnumerator WaitThenDie()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)CustomLayers.Weapon) return;

        if (collision.gameObject.layer == (int)CustomLayers.Hitbox)
        {
            Player player = collision.gameObject.GetComponentInParent<Player>();

            if (player.GetInstanceID() == _player.GetInstanceID()) return;

            player.ReceiveDamage(this.transform.position, StageManager.WinCondition.Bow);
        }

        StartCoroutine(TriggerDieAnimation());
    }

    private IEnumerator TriggerDieAnimation()
    {
        Vector2 direction = _rigidbody2D.velocity.normalized;
        direction *= 0.4f;
        _rigidbody2D.velocity = -direction;

        audioMaster.PlayInstance(AudioMaster.AudioType.ProjectileDestoyed);

        animator.Play(ANIMATION_DIE);
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }


}
