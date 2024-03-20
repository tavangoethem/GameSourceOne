using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class EnableCrouch : MonoBehaviour
{
    public MovementController controller;

    public void Start()
    {
        controller = GetComponent<MovementController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ActivateAbility")
        {
            controller.canCrouch = true;
        }
        else if (other.tag == "DeactivateAbility")
        {
            controller.canCrouch = false;
        }
    }
}