using AiStates;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Weaponry;

public class Pistol : WeaponBase, IShoot, IReload
{
    [SerializeField] AudioClip clip;

    [SerializeField] private int _damage = 1;

    [SerializeField] private int _maxAmmo = 6;

    [SerializeField] private int _curAmmo = 6;

    [SerializeField] private float _speed = .1f;

    public float recoil = 0;

    [SerializeField] private Transform _recoilHelper;

    private bool _running = false;


    public int CurAmmo { get { return _curAmmo; } }

    public int MaxAmmo { get { return _maxAmmo; } }

    [SerializeField, Range(1, 15)] private int SoundOfGun;

    [SerializeField]private bool _canReload = true;
    [SerializeField] private AudioClip _reloadSound;

    public void Shoot(InputAction.CallbackContext obj)
    {
        if (_canReload == false)
            return;
        Collider[] colls = Physics.OverlapSphere(this.transform.position, SoundOfGun);
        foreach (Collider coll in colls)
        {
            if (coll.gameObject.GetComponent<AIStates>() && coll.gameObject.GetComponent<AIStates>().CanSeePlayer == false)
            {
                coll.gameObject.GetComponent<AIStates>().CanSeePlayer = true;
            }
        }
        if (_recoilHelper == null)
        {
            _recoilHelper = new GameObject("RecoilHelper").transform;
            _recoilHelper.parent = Camera.main.transform;
            _recoilHelper.rotation = Camera.main.transform.rotation;

        }

        if (_curAmmo <= 0) return;
        //Transform mainCam = Camera.main.transform;

        //RaycastHit hit;
        //Ray cameraRay = new Ray(mainCam.position, mainCam.transform.forward);
        //if (Physics.Raycast(cameraRay, out hit))
        //{
        //    if (hit.transform.gameObject != null && hit.transform.gameObject.GetComponent<PlayerCharacter>() != true)
        //        hit.transform.gameObject.GetComponent<IDamagable>()?.TakeDamage(_damage, hit.point);
        //}

        Transform mainCam = Camera.main.transform;
        AudioManager.instance.PlaySFX(clip, transform, 1);
        _recoilHelper.eulerAngles = new Vector3(_recoilHelper.eulerAngles.x, mainCam.eulerAngles.y, mainCam.eulerAngles.z);
        _recoilHelper.position = mainCam.position;
        RaycastHit hit;
        Ray cameraRay = new Ray(mainCam.position, _recoilHelper.forward);
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
                hit.transform.gameObject.GetComponent<IDamagable>()?.TakeDamage(_damage, hit.point, ArmorType.light);
            LineRendManager.Instance.CreateRenederer(_firePoint.position, hit.point, .05f);
        }
        else
            LineRendManager.Instance.CreateRenederer(_firePoint.position, _recoilHelper.forward * 10, .05f);

        _curAmmo--;
        recoil = .25f;
        Recoil();
    }

    public void Reload(InputAction.CallbackContext obj)
    {
        if (_canReload == true)
            StartCoroutine(ReloadTime());

    }

    private IEnumerator ReloadTime()
    {
        _canReload = false;
        recoil = 0f;
        yield return new WaitForSeconds(1f);
        _curAmmo = MaxAmmo;
        AudioManager.instance.PlaySFX(_reloadSound, transform, .7f);
        _canReload = true;
    }

    public override void OnWeaponPickup()
    {
        base.OnWeaponPickup();
        _mainInput.Enable();
        _mainInput.Player.Enable();
        _mainInput.Player.Shoot.performed += Shoot;
        _mainInput.Player.Reload.performed += Reload;
        _curAmmo = _maxAmmo;
    }

    public override void OnWeaponDrop()
    {
        base.OnWeaponDrop();
        _mainInput.Disable();
        _mainInput.Player.Disable();
        _mainInput.Player.Shoot.performed -= Shoot;
        _mainInput.Player.Reload.performed -= Reload;
    }

    private void Recoil()
    {
        if (recoil > 0)
        {
            var maxRecoil = Quaternion.Euler(-20, 0, 0);
            // Dampen towards the target rotation
            _recoilHelper.localRotation = Quaternion.Slerp(_recoilHelper.localRotation, maxRecoil, Time.deltaTime * 10);
            transform.localEulerAngles = new Vector3(_recoilHelper.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
            if (_running == false) StartCoroutine(ResetRecoil());
        }
    }
    private IEnumerator ResetRecoil()
    {
        _running = true;
        while (recoil > 0)
        {
            recoil -= Time.deltaTime;
            yield return name;
        }
        transform.localRotation = Quaternion.identity;
        Destroy(_recoilHelper.gameObject);
        _recoilHelper = null;
        recoil = 0;
        _running = false;
    }
}