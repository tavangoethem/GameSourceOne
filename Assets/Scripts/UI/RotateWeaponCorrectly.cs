using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWeaponCorrectly : MonoBehaviour
{
    [SerializeField] private Transform _point1, _point2;

    private void Update()
    {
        transform.LookAt(_point2.position + _point2.forward * .1f);
    }
}
