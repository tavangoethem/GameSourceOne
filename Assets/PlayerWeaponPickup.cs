using UnityEngine;

public class PlayerWeaponPickup : MonoBehaviour
{
    [SerializeField] private Transform _weaponHoldPoint;

    [SerializeField] private GameObject _curPickup;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
            AttemptPickup();
        if (Input.GetKeyDown(KeyCode.Q))
            AttemptDrop();
    }

    private void AttemptPickup()
    {
        Camera camera = Camera.main;
        RaycastHit hit;
        if(Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
        {
            print(hit.transform.name);

            GameObject objHit = hit.transform.gameObject;

            if (objHit.GetComponent<IPickupable>() == null || _curPickup != null) return;

            _curPickup = objHit;

            _curPickup.GetComponent<IPickupable>().OnPickup();

            _curPickup.transform.parent = _weaponHoldPoint;
            _curPickup.transform.rotation = _weaponHoldPoint.rotation;
            _curPickup.transform.localPosition = Vector3.zero;
        }
    }

    private void AttemptDrop()
    {
        if (_curPickup == null) return;

        _curPickup.transform.parent = null;

        _curPickup.GetComponent<IPickupable>().OnDrop();

        _curPickup = null;
    }
}
