using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IPickupableWeapon
{
    abstract void OnWeaponPickup();
    abstract void OnWeaponDrop();
}

public interface IDamagable 
{
    abstract void TakeDamage();
};

namespace Weaponry
{
    public interface IShoot 
    {
        abstract void Shoot(InputAction.CallbackContext obj);
    };

    public interface IReload 
    {
        abstract void Reload(InputAction.CallbackContext obj);
    };

    public interface IAltFire 
    {
        abstract void AltFire(InputAction.CallbackContext obj);
    };
}
