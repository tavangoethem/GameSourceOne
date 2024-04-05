using AiStates;
using UnityEngine;
using UnityEngine.AI;
using Weaponry;

public class AIUpdateAnimations : MonoBehaviour
{
    public Animator Anim;
    public bool isShooting;
    public void Update()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;

        isShooting = Anim.GetBool("IsShooting");
        //print(isShooting);

        //print(velocity.magnitude);

        //Anim.SetFloat("Horizontal", velocity.magnitude);
        if(Anim.GetBool("IsShooting"))
            Anim.SetFloat("Vertical", 0);
        else
            Anim.SetFloat("Vertical", velocity.magnitude);
        Anim.SetBool("IsShooting", (GetComponent<AIStates>().isStopped));
    
    }
}
