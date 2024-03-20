using System;
using UnityEngine;

public class PlayerInteractionAndWeaponPickup : MonoBehaviour
{
    [SerializeField] private Transform _weaponHoldPoint;

    [SerializeField] private GameObject _curWeapon;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            AttemptInteraction();
        }
        if (Input.GetKeyDown(KeyCode.Q))
           AttemptDrop();
    }

    private void AttemptInteraction()
    {
        Camera camera = Camera.main;
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
        {
            GameObject hitObj = hit.transform.gameObject;
            if (hitObj.GetComponent<IInteractable>() == null)
                return;

            hitObj.GetComponent<IInteractable>().Interact(gameObject);
        }
    }

    public void WeaponPickup(GameObject weaponToPickup)
    {
        _curWeapon = weaponToPickup;

        _curWeapon.transform.parent = _weaponHoldPoint;
        _curWeapon.transform.rotation = _weaponHoldPoint.rotation;
        _curWeapon.transform.localPosition = Vector3.zero;

    }

    private void AttemptDrop()
    {
        if (_curWeapon == null) return;

        _curWeapon.transform.parent = null;

        _curWeapon.GetComponent<WeaponBase>().OnWeaponDrop();

        _curWeapon = null;
    }
}
