using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Explode : UnityEvent { }
public class ExplosiveObject : MonoBehaviour, IDamagable
{
    [SerializeField, Min(0f)] private float _explodeRange;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private Color _gizmoColor;
    [SerializeField] private int _explosionDamage = 50;
    private bool _hasExploded = false;

    public Explode explode;
    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmoColor;
        Gizmos.DrawWireSphere(transform.position, _explodeRange);
    }

    public void TakeDamage(int amount, Vector3 damagePosition)
    {
        if (_hasExploded)
        {
            return;
        }

        _hasExploded = true;

        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

        Collider[] collidersFound = Physics.OverlapSphere(transform.position, _explodeRange);

        for (int i = 0; i < collidersFound.Length; i++)
        {
            collidersFound[i].GetComponent<IDamagable>()?.TakeDamage(_explosionDamage, damagePosition);
        }

        explode?.Invoke();
        Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
