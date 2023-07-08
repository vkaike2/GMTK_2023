using System;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [Header("CONFIGURATIONS")]
    [SerializeField]
    private bool hasWeapon;

    [Header("COMPONENTS")]
    [SerializeField]
    private Transform rotationalObject;
    [SerializeField]
    private Transform weaponTransform;

    private PlayerStatus _status;

    public Weapon CurrentWeapon { get; private set; } = null;

    private void Awake()
    {
        _status = GetComponent<PlayerStatus>();
    }

    private void FixedUpdate()
    {
        RotateWeaponAroudPlayer();
    }

    internal void EquipWeapon(Weapon weapon)
    {
        weapon.transform.position = weaponTransform.position;
        weapon.transform.SetParent(weaponTransform);
        weapon.transform.rotation = new Quaternion();

        weapon.Equip();

        CurrentWeapon = weapon;
    }

    public void OnAttack()
    {
        CurrentWeapon.Attack();
    }

    internal void DropWeapon()
    {
        CurrentWeapon.Drop();
        CurrentWeapon = null;
    }

    private void RotateWeaponAroudPlayer()
    {
        if (!_status.IsSelected) return;

        Vector3 mousePosition = Input.mousePosition;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePosition.x -= objectPos.x;
        mousePosition.y -= objectPos.y;


        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

        Quaternion mouseRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        rotationalObject.rotation = mouseRotation;

        float currentAngle = rotationalObject.rotation.eulerAngles.z;

        if (CurrentWeapon == null || !CurrentWeapon.CanFlip) return;

        if (currentAngle >= 120 && currentAngle <= 230)
        {
            weaponTransform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            weaponTransform.localScale = new Vector3(1, 1, 1);
        }
    }

}
