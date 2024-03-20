using System;
using UnityEngine;

public class PlayerInteractionAndWeaponPickup : MonoBehaviour
{
    [SerializeField] private Transform _weaponHoldPoint;

    [SerializeField] private GameObject _curWeapon;

    [SerializeField] private TMPro.TMP_Text highlightText;

    [SerializeField] private GameObject _highlightedInteractable;

    private void Update()
    {
        LookForInteracable();

        if(Input.GetKeyDown(KeyCode.E))
        {
            AttemptInteraction();
        }
        if (Input.GetKeyDown(KeyCode.Q))
           AttemptDrop();
    }

    private void LookForInteracable()
    {
        Camera camera = Camera.main;
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
        {
            GameObject hitObj = hit.transform.gameObject;
            if(_highlightedInteractable != null)
                UnHighlight(_highlightedInteractable);

            if (hitObj.GetComponent<IInteractable>() == null)
            {
                _highlightedInteractable = null;
            }
            else
            {
                _highlightedInteractable = hitObj;

                HighlightObject(_highlightedInteractable);
            }
        }
    }

    private void UnHighlight(GameObject objectToHighlight)
    {
        if (objectToHighlight.GetComponentInChildren<Renderer>() != null)
        {
            objectToHighlight.GetComponentInChildren<Renderer>().material.DisableKeyword("_EMISSION");
        }
    }

    private void HighlightObject(GameObject objectToHighlight)
    {
        if (objectToHighlight.GetComponentInChildren<Renderer>() != null)
        {
            objectToHighlight.GetComponentInChildren<Renderer>().material.EnableKeyword("_EMISSION");
        }
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
        if (_curWeapon != null)
            return;

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
