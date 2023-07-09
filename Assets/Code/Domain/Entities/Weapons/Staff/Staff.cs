using UnityEngine;

public class Staff : Weapon
{
    [Header("SPECIFIC COMPONENTS")]
    [SerializeField]
    private StaffProjectile projectilePrefab;

    [Header("SPECIFIC CONFIGURATIONS")]
    [SerializeField]
    private float projectileSpeed = 5;

    private StaffProjectile _projectile = null;

    public override void Attack()
    {
        if (_projectile != null) return;

        audioMaster.Play(AudioMaster.AudioType.WeaponAttack);

        _projectile = Instantiate(projectilePrefab, spawnPosition);
        _projectile.Initialize(_gameManager, projectileSpeed);
        _projectile.transform.parent = null;
    }
}
