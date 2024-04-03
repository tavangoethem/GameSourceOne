using System;
using UnityEngine;

public class PlayerInteractionAndWeaponPickup : MonoBehaviour
{
    [Header("Interactable Stuff")]
    [SerializeField] private Transform _weaponHoldPoint;
    [SerializeField] private GameObject _curWeapon;
    public GameObject CurWeapon { get { return _curWeapon; } }
    [SerializeField] private GameObject _highlightedInteractable;
    [SerializeField] private float _maxInteractableDistance = 3;
    private void Update()
    {
        LookForInteracable();

        if(Input.GetKeyDown(KeyCode.E))
        {
            AttemptInteraction();
        }
        if (Input.GetKeyDown(KeyCode.Q))
           AttemptWeaponDrop();
    }

    /// <summary>
    /// checks for interactables and highlights the ones its looking at
    /// </summary>
    private void LookForInteracable()
    {
        Camera camera = Camera.main;
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, _maxInteractableDistance))
        {
            GameObject hitObj = hit.transform.gameObject;
            if(_highlightedInteractable != null || hitObj == null)
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

    /// <summary>
    /// Attempts interactions with the interactabel currently looked at
    /// </summary>
    private void AttemptInteraction()
    {
        Camera camera = Camera.main;
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit,_maxInteractableDistance))
        {
            GameObject hitObj = hit.transform.gameObject;
            if (hitObj.GetComponent<IInteractable>() == null)
                return;

            hitObj.GetComponent<IInteractable>().Interact(gameObject);
        }
    }

    /// <summary>
    /// handles weapons position and rotation
    /// </summary>
    public void WeaponPickup(GameObject weaponToPickup)
    {
        if (_curWeapon != null)
            return;

        _curWeapon = weaponToPickup;

        _curWeapon.transform.parent = _weaponHoldPoint;
        _curWeapon.transform.rotation = _weaponHoldPoint.rotation;
        _curWeapon.transform.localPosition = Vector3.zero;

    }

    /// <summary>
    /// handles weapon drops
    /// </summary>
    private void AttemptWeaponDrop()
    {
        if (_curWeapon == null) return;

        _curWeapon.transform.parent = null;

        _curWeapon.GetComponent<WeaponBase>().OnWeaponDrop();

        _curWeapon = null;
    }
}
