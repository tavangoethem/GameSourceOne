using System.Collections;
using UnityEngine;
using Weaponry;

public class AIPistol : MonoBehaviour, IAIWeapons
{
    [SerializeField] private int _damage = 1;

    [SerializeField] private float _fireRate = 1f;

    [SerializeField] private bool _isShoot = false;

    bool running = false;

    public Vector3 toOther;

    public bool IsShoot { get { return _isShoot; } }

    public void AIShoot(GameObject target)
    {
        if (_isShoot == true && running == false)
        {
            StartCoroutine(Shooting(target));
        }
    }
    public void shootBool(bool isShoot)
    {
        _isShoot = isShoot;
    }

    public IEnumerator Shooting(GameObject target)
    {
        while (_isShoot == true)
        {
            running = true;
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            toOther = target.transform.position - transform.position;
            toOther = toOther.normalized;
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position, toOther, out hit))
            {
                if (hit.transform.gameObject.GetComponent<PlayerCharacter>() == true)
                {
                    hit.transform.gameObject.GetComponent<IDamagable>()?.TakeDamage(_damage, hit.point);
                }
            }
            yield return new WaitForSeconds(_fireRate);
        }
        running = false;
        yield return null;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, toOther);
    }
}
