using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentBreak : MonoBehaviour, IDamagable
{
    public ArmorType ArmorType { get { return _armorType; } }
    private ArmorType _armorType = ArmorType.none;

    public void TakeDamage(int amount, Vector3 damagePosition, ArmorType levelofPierce)
    {
        Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damageToTake, Vector3 damagePosition, ArmorType levelOfPierce)
    {
        throw new System.NotImplementedException();
    }
}
