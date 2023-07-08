using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    [Header("SPECIFIC COMPONENTS")]
    [SerializeField]
    private SwordSlash prefab;

    private SwordSlash _swordSlash = null;

    public override void Attack()
    {
        if (_swordSlash != null) return;

        _swordSlash = Instantiate(prefab, spawnPosition);
        _swordSlash.Initialize(_gameManager.GetSelectedPlayer());
        _swordSlash.transform.parent = null;
        StartCoroutine(AttackAnimation());
    }

    private IEnumerator AttackAnimation()
    {
        FlipSword();

        yield return new WaitForSeconds(0.5f);
    }

    private void FlipSword() => this.transform.localScale = new Vector3(1, -Mathf.Sign(this.transform.localScale.y), 1);

}
