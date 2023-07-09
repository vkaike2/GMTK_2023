using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    [Header("CONFIGURATIONS")]
    [SerializeField]
    private BowProjectile projectilePrefab;

    [Header("SPECIFIC CONFIGURATIONS")]
    [SerializeField]
    private float projectileSpeed = 10;


    private BowProjectile _projectile = null;

    private void Start()
    {
        base.Start();
        CanFlip = false;
    }

    public override void Attack()
    {
        if (_projectile != null) return;

        audioMaster.Play(AudioMaster.AudioType.WeaponAttack);

        _projectile = Instantiate(projectilePrefab, spawnPosition);
        _projectile.Initialize(_gameManager.GetSelectedPlayer(), projectileSpeed);
        _projectile.transform.parent = null;
    }
}
