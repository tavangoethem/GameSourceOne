using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public interface IDamagable 
{
    abstract void TakeDamage(int damageToTake, Vector3 damagePosition);

    abstract void Die();
};

public interface IHealable
{
    abstract void HealDamage(int valueToHeal);
}

public interface IInteractable
{
    //public string OnInteractText { get; }
    abstract void Interact(GameObject objAttemptingInteraction);
}

namespace Weaponry
{
    public interface IShoot 
    {
        abstract void Shoot(InputAction.CallbackContext obj);
    };

    public interface IReload 
    {
        int CurAmmo { get;}
        int MaxAmmo { get;}

        abstract void Reload(InputAction.CallbackContext obj);
    }
    public interface IAltFire 
    {
        abstract void AltFire(InputAction.CallbackContext obj);
    };

    public interface IAIWeapons
    {
        public abstract bool IsShoot { get; set; }

        abstract void AIShoot(GameObject target);

        abstract void shootBool(bool shoot);
    }
}
