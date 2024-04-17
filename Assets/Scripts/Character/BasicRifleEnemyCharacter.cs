using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicRifleEnemyCharacter : CharacterBase
{
    public override void TakeDamage(int damageToTake, Vector3 damagePosition)
    {
        base.TakeDamage(damageToTake, damagePosition);
        if(health.curHealth <= 0 && damageToTake > 5)
        {
            GetComponentInChildren<Rigidbody>().AddForce(new Vector3(Random.Range(-1,1),
                Random.Range(0, 1),
                Random.Range(-1, 1)) * 100, ForceMode.Impulse);
        }
    }

    public override void Die()
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<AiStates.AIStates>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }
}
