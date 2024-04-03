using UnityEngine;
using UnityEngine.AI;

public class AIUpdateAnimations : MonoBehaviour
{
    public Animator Anim;

    public void Update()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;

        bool isShooting = GetComponent<AiStates.AIStates>().isShoot;

        //print(isShooting);

        print(velocity.magnitude);

        //Anim.SetFloat("Horizontal", velocity.magnitude);
        Anim.SetFloat("Vertical", velocity.magnitude);
        Anim.SetBool("IsShooting", isShooting);
    
    }
}
