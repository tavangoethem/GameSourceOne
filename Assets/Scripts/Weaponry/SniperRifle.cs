using AiStates;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Weaponry;

public class SniperRifle : WeaponBase, IShoot, IReload, IAltFire
{
    [SerializeField] private int _damage = 1;

    [SerializeField] private int _maxAmmo = 6;

    [SerializeField] private int _curAmmo = 6;

    [SerializeField] private float _cycleSpeed = 1f;

    [SerializeField] private float _zoomSens = 5;

    private bool _canShoot = true;

    public bool _isZoomed = false;

    public int CurAmmo { get { return _curAmmo; } }

    public int MaxAmmo { get { return _maxAmmo; } }

    [SerializeField, Range(1, 15)] private int SoundOfGun;

    public void Shoot(InputAction.CallbackContext obj)
    {
        if (_canShoot == false)
            return;

        Collider[] colls = Physics.OverlapSphere(transform.position, SoundOfGun);
        foreach (Collider coll in colls)
        {
            if (coll.gameObject.GetComponent<AIStates>() && coll.gameObject.GetComponent<AIStates>().CanSeePlayer == false)
            {
                coll.gameObject.GetComponent<AIStates>().CanSeePlayer = true;
            }
        }

        if (_curAmmo <= 0) return;

        Transform mainCam = Camera.main.transform;

        RaycastHit hit;
        Ray cameraRay = new Ray(mainCam.position, mainCam.forward);
        if (Physics.Raycast(cameraRay, out hit))
        {
            Collider[] colls1 = Physics.OverlapSphere(hit.transform.position, 5);
            foreach (Collider coll in colls1)
            {
                if (coll.gameObject.GetComponent<AIStates>() && coll.gameObject.GetComponent<AIStates>().CanSeePlayer == false)
                {
                    coll.gameObject.GetComponent<AIStates>().CanSeePlayer = true;
                }
            }
            if (hit.transform.gameObject != null && hit.transform.gameObject.GetComponent<PlayerCharacter>() != true)
                hit.transform.gameObject.GetComponent<IDamagable>()?.TakeDamage(_damage, hit.point, ArmorType.heavy);
        }

        _curAmmo--;
        _canShoot = false;
        StartCoroutine(CycleGun());
        if (_isZoomed)
            StupidAltFire();
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
        _mainInput.Player.Shoot.performed += Shoot;
        _mainInput.Player.Reload.performed += Reload;
        _mainInput.Player.AltFire.performed += AltFire;
        _curAmmo = _maxAmmo;
    }

    public override void OnWeaponDrop()
    {
        base.OnWeaponDrop();

        if (_isZoomed)
            StupidAltFire();

        _mainInput.Disable();
        _mainInput.Player.Disable();
        _mainInput.Player.Shoot.performed -= Shoot;
        _mainInput.Player.Reload.performed -= Reload;
        _mainInput.Player.AltFire.performed -= AltFire;
    }

    public IEnumerator CycleGun()
    {
        yield return new WaitForSeconds(_cycleSpeed);
        _canShoot = true;
    }

    public void AltFire(InputAction.CallbackContext obj)
    {
        //print("attempted alt fire");
        if (_isZoomed == false)
        {
            //print("zoomed");
            Camera.main.fieldOfView = 15f;
            Camera.main.GetComponent<CameraLook>().Sens = _zoomSens;
            _isZoomed = true;
        }
        else if (_isZoomed == true)
        {
            //print("Unzoomed");
            Camera.main.fieldOfView = 60f;
            Camera.main.GetComponent<CameraLook>().Sens = 10;
            _isZoomed = false;
        }
    }
    public void StupidAltFire()
    {
        if (_isZoomed == false)
        {
            Camera.main.fieldOfView = 15f;
            Camera.main.GetComponent<CameraLook>().Sens = _zoomSens;
            _isZoomed = true;
        }
        else if (_isZoomed == true)
        {
            Camera.main.fieldOfView = 60f;
            Camera.main.GetComponent<CameraLook>().Sens = 10;
            _isZoomed = false;
        }
    }
}