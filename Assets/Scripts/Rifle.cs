using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weaponry;

public class Rifle : MonoBehaviour, IPickupable, IShoot, IReload
{
    [SerializeField] private int _maxAmmo;

    private int _curAmmo;

    [SerializeField] private Transform _firePoint;

    public void Shoot()
    {

    }

    public void Reload()
    {
        _curAmmo = _maxAmmo;
    }

    public void OnPickup()
    {
        if (GetComponent<Rigidbody>() == null) return;

        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void OnDrop()
    {
        if (GetComponent<Rigidbody>() == null) return;

        GetComponent<Rigidbody>().isKinematic = false;
    }
}
