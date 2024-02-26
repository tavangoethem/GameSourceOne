using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour, IDamagable
{
    public void Explode()
    {
        print("Kablamo!");
        Destroy(gameObject);
    }

    public void TakeDamage()
    {
        Explode();
    }
}
