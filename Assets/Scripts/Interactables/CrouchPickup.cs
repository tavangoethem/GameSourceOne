using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip _pickupClip;
    public void Interact(GameObject objAttemptingInteraction)
    {
        if (objAttemptingInteraction.GetComponent<MovementController>() != null)
            objAttemptingInteraction.GetComponent<MovementController>().canCrouch = true;
        AudioManager.instance.PlaySFX(_pickupClip);
        Destroy(gameObject);
    }
}
