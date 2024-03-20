using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour, IDamagable
{
    [SerializeField] private float _radius = 2f;

    [SerializeField] private int _damage = 3;

    private bool _hasExploded = false;

    public void Die()
    {
    }

    public void Explode()
    {
        if (_hasExploded) return;

        _hasExploded = true;

        Collider[] colls = Physics.OverlapSphere(transform.position, _radius);

        foreach (Collider coll in colls)
        {
            if(coll != gameObject.GetComponent<Collider>())
                coll.GetComponent<IDamagable>()?.TakeDamage(_damage);
        }
        Destroy(gameObject);
    }
    public void TakeDamage(int damageToTake)
    {
        Explode();
    }
}
