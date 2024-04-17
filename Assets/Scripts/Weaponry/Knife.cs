using AiStates;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Weaponry;

public class Knife : WeaponBase, IShoot
{
    [SerializeField] private int _damage = 1;

    [SerializeField] private float _speed = .1f;


    [SerializeField] private float AttackDistance = 2;

    [SerializeField] AudioClip shootingSound;

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
            Collider[] colls1 = Physics.OverlapSphere(hit.transform.position, 7);
            foreach (Collider coll in colls1)
            {
                if (coll.gameObject.GetComponent<AIStates>() && coll.gameObject.GetComponent<AIStates>().CanSeePlayer == false)
                {
                    coll.gameObject.GetComponent<AIStates>().CanSeePlayer = true;
                }
            }
        }
    }

    private IEnumerator Effects()
    {
        if (shootingSound != null)
            AudioManager.instance.PlaySFX(shootingSound, transform, 1);
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