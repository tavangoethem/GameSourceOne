using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Weaponry;

public class Rifle : WeaponBase, IShoot, IReload
{
    [SerializeField] private int _damage = 1;

    [SerializeField] private int _maxAmmo;

    [SerializeField] private int _curAmmo;

    [SerializeField] private float _speed = .1f;

    private bool _isShoot = false;

    public void Shoot(InputAction.CallbackContext obj)
    {
        if (obj.started)
        {
            StartCoroutine(Shooting());
        }
        else if(obj.canceled)
        {
            _isShoot = false;
        }
    }

    public IEnumerator Shooting()
    {
        _isShoot = true;
        while (_isShoot)
        {

            Transform mainCam = Camera.main.transform;

            RaycastHit hit;
            Ray cameraRay = new Ray(mainCam.position, mainCam.transform.forward);
            if (Physics.Raycast(cameraRay, out hit))
            {
                if (hit.transform.gameObject != null && hit.transform.gameObject.GetComponent<PlayerCharacter>() != true)
                    hit.transform.gameObject.GetComponent<IDamagable>()?.TakeDamage(_damage);
            }
            yield return new WaitForSeconds(_speed);
        }
        yield return null;
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
        _mainInput.Player.Shoot.canceled += Shoot;
    }

    public override void OnWeaponDrop()
    {
        base.OnWeaponDrop();
        _mainInput.Disable();
        _mainInput.Player.Disable();
        _mainInput.Player.Shoot.started -= Shoot;
        _mainInput.Player.Shoot.canceled -= Shoot;
    }
}