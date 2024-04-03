using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentBreak : MonoBehaviour, IDamagable
{
    public void TakeDamage(int amount, Vector3 damagePosition)
    {
        Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
