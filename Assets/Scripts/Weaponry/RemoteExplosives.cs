using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Weaponry;

public class RemoteExplosives : MonoBehaviour, IPickupableWeapon, IShoot, IAltFire
{
    private MainInput _mainInput;

    private List<Explosive> Explosives = new List<Explosive>();
    [SerializeField] private GameObject _explosive;

    [SerializeField] private Transform _firePoint;

    private void Awake()
    {
        _mainInput = new MainInput();

    }

    public void Shoot(InputAction.CallbackContext obj)
    {
        GameObject temp = Instantiate(_explosive, _firePoint.position, Quaternion.identity);
        temp.GetComponent<Rigidbody>().AddForce(_firePoint.forward * 5f, ForceMode.Impulse);
        Explosives.Add(temp.GetComponent<Explosive>());
    }

    public void AltFire(InputAction.CallbackContext obj)
    {
        foreach (Explosive exp in Explosives)
        {
            if(exp != null)
                exp.Explode();
        }
        Explosives = new List<Explosive>();
    }

    public void OnWeaponPickup()
    {
        if (GetComponent<Rigidbody>() == null) return;

        GetComponent<Rigidbody>().isKinematic = true;

        _mainInput.Enable();
        _mainInput.Player.Enable();
        _mainInput.Player.Shoot.started += Shoot;
        _mainInput.Player.AltFire.started += AltFire;
    }

    public void OnWeaponDrop()
    {
        if (GetComponent<Rigidbody>() == null) return;

        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(transform.forward * 3f, ForceMode.Impulse);

        _mainInput.Disable();
        _mainInput.Player.Disable();
        _mainInput.Player.Shoot.started -= Shoot;
        _mainInput.Player.AltFire.started -= AltFire;
    }
}
