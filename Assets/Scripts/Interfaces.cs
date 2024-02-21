using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickupable
{
    abstract void OnPickup();
    abstract void OnDrop();
}

public interface IDamagable { };

namespace Weaponry
{
    public interface IShoot 
    {
        abstract void Shoot();
    };

    public interface IReload 
    {
        abstract void Reload();
    };

    public interface IAltFire 
    {
        abstract void AltFire();
    };
}
