using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour, IDamagable
{
    public CharacterBase Owner;

    [SerializeField] private float _damageMultiplyer = 1f;

    public void Die()
    {
       //unnessasary
    }

    public void TakeDamage(int damageToTake, Vector3 damagePosition)
    {
        if(Owner != null)
            Owner.TakeDamage(Mathf.CeilToInt(damageToTake * _damageMultiplyer), damagePosition);
    }
}
