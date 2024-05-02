using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Weaponry;

public class BasicRifleEnemyCharacter : CharacterBase
{
    public override void TakeDamage(float damageToTake, Vector3 damagePosition, ArmorType levelofPierce)
    {
        base.TakeDamage(damageToTake, damagePosition, levelofPierce);
        if(health.curHealth <= 0 && damageToTake > 5)
        {
            GetComponentInChildren<Rigidbody>().AddForce(new Vector3(Random.Range(-1,1),
                Random.Range(0, 1),
                Random.Range(-1, 1)) * 100, ForceMode.Impulse);
        }
    }

    public override void Die()
    {
        StartCoroutine(OnDeath());
        GetComponent<Animator>().enabled = false;
        GetComponent<AiStates.AIStates>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<AiStates.AIStates>().weapon.GetComponent<IAIWeapons>().IsShoot = false;
    }

    public IEnumerator OnDeath()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
