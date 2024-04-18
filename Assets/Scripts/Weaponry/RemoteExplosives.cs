using AiStates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Weaponry;

public class RemoteExplosives : WeaponBase, IShoot, IAltFire
{
    private List<Explosive> Explosives = new List<Explosive>();
    [SerializeField] private GameObject _explosive;

    [SerializeField] AudioClip shootingSound;
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
            if (exp != null)
            {
                if (shootingSound != null)
                    AudioManager.instance.PlaySFX(shootingSound, transform, 1);
                exp.Explode();
            }
        }
        Explosives = new List<Explosive>();
    }

    public override void OnWeaponPickup()
    {
        base.OnWeaponPickup();

        _mainInput.Player.Shoot.started += Shoot;
        _mainInput.Player.AltFire.started += AltFire;
    }

    public override void OnWeaponDrop()
    {
        base.OnWeaponDrop();

        _mainInput.Player.Shoot.started -= Shoot;
        _mainInput.Player.AltFire.started -= AltFire;
    }
}
