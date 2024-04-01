using UnityEngine;

public class DOTVolume : MonoBehaviour
{
    private void Start()
    {
        InvokeRepeating("DealDamage", 0, 1f);
    }

    public void DealDamage()
    {
        Collider[] coll = Physics.OverlapSphere(transform.position, 2);
        if (coll != null)
        {
            foreach (Collider collider in coll)
            {
                if (collider != null)
                {
                    collider.GetComponent<IDamagable>()?.TakeDamage(2, collider.ClosestPoint(transform.position));
                }
            }
        }
    }
}
