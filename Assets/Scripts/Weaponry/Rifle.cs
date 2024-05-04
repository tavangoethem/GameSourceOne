using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Weaponry;

namespace AiStates
{
    [Serializable]
    public class ShootingEvent : UnityEvent { }
    public class Rifle : WeaponBase, IShoot, IReload
    {
        [SerializeField] private int _damage = 1;

        [SerializeField] private int _maxAmmo;

        [SerializeField] private int _curAmmo;

        [SerializeField] private float _fireRate = .1f;

        [SerializeField] private bool _isShoot = false;

        public float recoil = 0;

        private Transform _recoilHelper;

        private bool _running = false;

        public int CurAmmo { get { return _curAmmo; } }

        public int MaxAmmo { get { return _maxAmmo; } }


        [SerializeField, Range(1, 15)] private int SoundOfGunForEnemys;

        [SerializeField] AudioClip shootingSound;
        [SerializeField] private AudioClip _reloadSound;
        [SerializeField] private bool _canReload = true;

        public ShootingEvent shootingEvent;
        public InputController Ic;
        public void Shoot(InputAction.CallbackContext obj)
        {
            Ic = GameObject.Find("Player").GetComponent<InputController>();
            if (Ic.IsPaused == false)
            {
                if (_canReload == false)
                    return;
                if (obj.started)
                {
                    StartCoroutine(Shooting());
                    Collider[] colls = Physics.OverlapSphere(this.transform.position, SoundOfGunForEnemys);
                    foreach (Collider coll in colls)
                    {
                        if (coll.gameObject.GetComponent<AIStates>() && coll.gameObject.GetComponent<AIStates>().CanSeePlayer == false)
                        {
                            coll.gameObject.GetComponent<AIStates>().CanSeePlayer = true;
                        }
                    }
                }
                else if (obj.canceled)
                {
                    _isShoot = false;
                }
            }
        }

        public IEnumerator Shooting()
        {

            if (_recoilHelper == null)
            {
                _recoilHelper = new GameObject("RecoilHelper").transform;
                _recoilHelper.rotation = Camera.main.transform.rotation;
                _recoilHelper.parent = Camera.main.transform;

            }
            _isShoot = true;
            while (_isShoot && _curAmmo > 0 && _recoilHelper != null && Ic.IsPaused == false)
            {
                if (shootingSound != null)
                    AudioManager.instance.PlaySFX(shootingSound, transform, 1);
                Transform mainCam = Camera.main.transform;
                shootingEvent.Invoke();
                _recoilHelper.eulerAngles = new Vector3(_recoilHelper.eulerAngles.x, mainCam.eulerAngles.y, mainCam.eulerAngles.z);
                _recoilHelper.position = mainCam.position;
                RaycastHit hit;
                Ray cameraRay = new Ray(mainCam.position, _recoilHelper.forward);
                if (Physics.Raycast(cameraRay, out hit))
                {
                    if (hit.transform.gameObject != null && hit.transform.gameObject.GetComponent<PlayerCharacter>() != true)
                    {
                        hit.transform.gameObject.GetComponent<IDamagable>()?.TakeDamage(_damage, hit.point, ArmorType.light);
                        Collider[] colls1 = Physics.OverlapSphere(hit.transform.position, 5);
                        foreach (Collider coll in colls1)
                        {
                            if (coll.gameObject.GetComponent<AIStates>() && coll.gameObject.GetComponent<AIStates>().CanSeePlayer == false)
                            {
                                coll.gameObject.GetComponent<AIStates>().CanSeePlayer = true;
                            }
                        }
                    }
                    LineRendManager.Instance.CreateRenederer(_firePoint.position, hit.point, .05f);
                }
                else
                    LineRendManager.Instance.CreateRenederer(_firePoint.position, _recoilHelper.forward * 10, .05f);
                _curAmmo--;
                recoil = .25f;
                Recoil();
                yield return new WaitForSeconds(_fireRate);
            }
            yield return null;
        }

        public void Reload(InputAction.CallbackContext obj)
        {
            if(_canReload == true && Ic.IsPaused == false)
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
            _curAmmo = _maxAmmo;
            _mainInput.Enable();
            _mainInput.Player.Enable();
            _mainInput.Player.Shoot.started += Shoot;
            _mainInput.Player.Shoot.canceled += Shoot;
            _mainInput.Player.Reload.performed += Reload;
        }

        public override void OnWeaponDrop()
        {
            base.OnWeaponDrop();
            _mainInput.Disable();
            _mainInput.Player.Disable();
            _mainInput.Player.Shoot.started -= Shoot;
            _mainInput.Player.Shoot.canceled -= Shoot;
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

        private void OnDrawGizmos()
        {
            if (_recoilHelper != null)
            {
                Gizmos.DrawRay(_recoilHelper.position, _recoilHelper.forward);
            }
        }

    }
}