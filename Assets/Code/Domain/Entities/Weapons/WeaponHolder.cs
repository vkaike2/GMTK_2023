using System;
using System.Collections;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [Header("CONFIGURATIONS")]
    [SerializeField]
    private Weapon currentWeapon;

    [Header("COMPONENTS")]
    [SerializeField]
    private Transform rotationalObject;
    [SerializeField]
    private Transform weaponTransform;

    private PlayerStatus _status;
    private StageManager _stageManager;

    public Weapon CurrentWeapon { get => currentWeapon; private set => currentWeapon = value; }
    private void Awake()
    {
        _status = GetComponent<PlayerStatus>();

    }

    private void Start()
    {
        _stageManager = GameObject.FindObjectOfType<StageManager>();
        if (currentWeapon != null) SpawnWeapon();
    }



    private void FixedUpdate()
    {
        RotateWeaponAroudPlayer();
    }

    internal void EquipWeapon(Weapon weapon, bool playAudio = true)
    {
        weapon.transform.position = weaponTransform.position;
        weapon.transform.SetParent(weaponTransform);
        weapon.transform.rotation = new Quaternion();

        weapon.Equip(playAudio);

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

    private void SpawnWeapon()
    {
        Weapon initialWeapon = Instantiate(currentWeapon, weaponTransform);
        CurrentWeapon = initialWeapon;

        _stageManager.OnStartGame.AddListener(() =>
        {
            EquipWeapon(CurrentWeapon, false);
        });
    }


}
