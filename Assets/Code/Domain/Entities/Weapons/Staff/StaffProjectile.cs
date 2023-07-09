using System.Collections;
using UnityEngine;


public class StaffProjectile : Projectile
{
    [Header("CONFIGURATIONS")]
    [SerializeField]
    private float lifeTime = 2f;

    [Header("COMPONENTS")]
    [SerializeField]
    private ParticleSystem vfxParticle;

    private Rigidbody2D _rigidbody2D;
    private GameManager _gameManager;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(WaitThenDie());
    }

    public void Initialize(GameManager gameManager, float speed)
    {
        _gameManager = gameManager;

        InstantiateVFX();
        SetProjectileDirection(speed);
    }

    public override void KillIt()
    {
        DestroyProjectile();
    }

    private void SetProjectileDirection(float speed)
    {
        _rigidbody2D.velocity = WeaponUtils.GetMouseNormalizedDirection(this.transform) * speed;
    }

    private void InstantiateVFX()
    {
        ParticleSystem vfx = Instantiate(vfxParticle, this.transform);
        vfx.transform.parent = null;
    }

    private IEnumerator WaitThenDie()
    {
        yield return new WaitForSeconds(lifeTime);
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        InstantiateVFX();
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == (int) CustomLayers.Hitbox)
        {
            SwapPlayer(collision.gameObject.GetComponentInParent<Player>());
        }

        DestroyProjectile();
    }

    private void SwapPlayer(Player player)
    {
        _gameManager.SelectPlayer(player);
    }
}
