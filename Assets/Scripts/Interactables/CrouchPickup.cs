using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchPickup : MonoBehaviour, IInteractable
{
    public void Interact(GameObject objAttemptingInteraction)
    {
        if (objAttemptingInteraction.GetComponent<MovementController>() != null)
            objAttemptingInteraction.GetComponent<MovementController>().canCrouch = true;
        Destroy(gameObject);
    }
}
