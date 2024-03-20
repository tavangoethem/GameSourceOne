using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class WeaponBase : MonoBehaviour, IInteractable
{
    [SerializeField] protected Transform _firePoint;

    protected MainInput _mainInput;

    protected virtual void Awake()
    {
        _mainInput = new MainInput();
    }

    public virtual void OnWeaponPickup()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        _mainInput.Enable();
        _mainInput.Player.Enable();
    }
    public virtual void OnWeaponDrop()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(transform.forward * 3f, ForceMode.Impulse);
        _mainInput.Disable();
        _mainInput.Player.Disable();
    }

    public void Interact(GameObject objAttemptingInteraction)
    {
        OnWeaponPickup();
        if(objAttemptingInteraction.GetComponent<PlayerInteractionAndWeaponPickup>() != null)
        {
            objAttemptingInteraction.GetComponent<PlayerInteractionAndWeaponPickup>().WeaponPickup(gameObject);
        }
    }

    public void OnHighlight(GameObject objAttemptingInteraction)
    {        
    }
}
