using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using Weaponry;

public class Rifle : MonoBehaviour, IPickupableWeapon, IShoot, IReload
{
    [SerializeField] private int _maxAmmo;

    private int _curAmmo;

    [SerializeField] private Transform _firePoint;

    private MainInput _mainInput;

    private void Awake()
    {
        _mainInput = new MainInput();

    }

    public void Shoot(InputAction.CallbackContext obj)
    {
        print("bang");
    }

    public void Reload(InputAction.CallbackContext obj)
    {
        _curAmmo = _maxAmmo;
    }

    public void OnWeaponPickup()
    {
        if (GetComponent<Rigidbody>() == null) return;

        GetComponent<Rigidbody>().isKinematic = true;

        _mainInput.Enable();
        _mainInput.Player.Enable();
        _mainInput.Player.Shoot.started += Shoot;
    }

    public void OnWeaponDrop()
    {
        if (GetComponent<Rigidbody>() == null) return;

        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(transform.forward * 3f, ForceMode.Impulse);

        _mainInput.Disable();
        _mainInput.Player.Disable();
        _mainInput.Player.Shoot.started -= Shoot;
    }
}
