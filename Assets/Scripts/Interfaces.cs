using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public interface IDamagable 
{
    abstract void TakeDamage(int damageToTake);

    abstract void Die();
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
