using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Weaponry;

public class Knife : WeaponBase, IShoot
{
    [SerializeField] private int _damage = 1;

    [SerializeField] private float _speed = .1f;


    [SerializeField] private float AttackDistance = 2;

    AudioSource shootingSound;

    private void Start()
    {
        shootingSound = GetComponent<AudioSource>();
    }

    public void Shoot(InputAction.CallbackContext obj)
    {
        StartCoroutine(Effects());
        Transform mainCam = Camera.main.transform;

        RaycastHit hit;
        Ray cameraRay = new Ray(mainCam.position, mainCam.transform.forward);
        if (Physics.Raycast(cameraRay, out hit, AttackDistance))
        {
            if (hit.transform.gameObject != null && hit.transform.gameObject.GetComponent<PlayerCharacter>() != true)
                hit.transform.gameObject.GetComponent<IDamagable>()?.TakeDamage(_damage, hit.point);
        }
    }

    private IEnumerator Effects()
    {
        shootingSound.Play();
        new WaitForSeconds(1);
        yield return null;
    }

    public override void OnWeaponPickup()
    {
        base.OnWeaponPickup();
        _mainInput.Enable();
        _mainInput.Player.Enable();
        _mainInput.Player.Shoot.performed += Shoot;
    }

    public override void OnWeaponDrop()
    {
        base.OnWeaponDrop();
        _mainInput.Disable();
        _mainInput.Player.Disable();
        _mainInput.Player.Shoot.performed -= Shoot;
    }
}