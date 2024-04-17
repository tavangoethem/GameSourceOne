using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour, IDamagable
{
    public CharacterBase Owner;

    [SerializeField] private float _damageMultiplyer = 1f;

    public ArmorType ArmorType { get { return _armorType; } }

    [SerializeField] private ArmorType _armorType = ArmorType.none;

    public void Die()
    {
       //unnessasary
    }

    public void TakeDamage(float damageToTake, Vector3 damagePosition, ArmorType levelOfPierce)
    {
        if (Owner == null)
            return;
        if ((int)levelOfPierce > (int)_armorType)
            Owner.TakeDamage((damageToTake * _damageMultiplyer), damagePosition, ArmorType.heavy);
        else
            Owner.TakeDamage((damageToTake * _damageMultiplyer) / ((int)levelOfPierce / (int)_armorType), damagePosition, ArmorType.heavy);
    }
}
