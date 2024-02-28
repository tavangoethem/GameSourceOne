using UnityEngine;
using UnityEngine.InputSystem;
using Weaponry;

public class Rifle : WeaponBase, IShoot, IReload
{
    [SerializeField] private int _maxAmmo;

    private int _curAmmo;

    public void Shoot(InputAction.CallbackContext obj)
    {
        Transform mainCam = Camera.main.transform;

        RaycastHit hit;
        Ray cameraRay = new Ray(mainCam.position, mainCam.transform.forward);
        if(Physics.Raycast(cameraRay, out hit))
        {
            if (hit.transform.gameObject != null)
                hit.transform.gameObject.GetComponent<IDamagable>()?.TakeDamage(1);

        }
    }

    public void Reload(InputAction.CallbackContext obj)
    {
        _curAmmo = _maxAmmo;
    }

    public override void OnWeaponPickup()
    {
        base.OnWeaponPickup();
        _mainInput.Enable();
        _mainInput.Player.Enable();
        _mainInput.Player.Shoot.started += Shoot;
    }

    public override void OnWeaponDrop()
    {
        base.OnWeaponDrop();
        _mainInput.Disable();
        _mainInput.Player.Disable();
        _mainInput.Player.Shoot.started -= Shoot;
    }
}
