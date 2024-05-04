using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour, IDamagable
{
    public CharacterBase Owner;

    [SerializeField] private float _damageMultiplyer = 1f;

    public ArmorType ArmorType { get { return StupidArmorType; } set { StupidArmorType = value; } }

    public ArmorType StupidArmorType = ArmorType.none;

    public void Die()
    {
       //unnessasary
    }

    //DAMAGE CALCULATIONS!
    public void TakeDamage(float damageToTake, Vector3 damagePosition, ArmorType levelOfPierce)
    {
        if (Owner == null)
            return;
        //print($"LoP {(int)levelOfPierce} > A {(int)StupidArmorType}");

        //if the limb does not have sufficiant armor
        if ((int)levelOfPierce > (int)StupidArmorType)
        {
            //just deal the damage times the multiplier
            Owner.TakeDamage((damageToTake * _damageMultiplyer), damagePosition, ArmorType.heavy);

            //print($"DV {damageToTake} * DM{_damageMultiplyer}");
        }
        else
        {
            //deal the damage times the multiplier divided by the armor difference
            Owner.TakeDamage((damageToTake * _damageMultiplyer) * (float)levelOfPierce / (float)StupidArmorType, damagePosition, ArmorType.heavy);

            //print($"DV {damageToTake} * DM {_damageMultiplyer} * LoP{(float)levelOfPierce} / A {(float)StupidArmorType} = " +
           //$"{(damageToTake * _damageMultiplyer) * (float)levelOfPierce / (float)StupidArmorType}");
        }
       
        //print((float)levelOfPierce / (float)_armorType);
    }
}
