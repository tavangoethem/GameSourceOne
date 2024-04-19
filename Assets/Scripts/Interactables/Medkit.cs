using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour, IInteractable
{
    [SerializeField] private int _healAmount = 2;

    [SerializeField] private AudioClip _pickupClip;


    public void Interact(GameObject objAttemptingInteraction)
    {
        objAttemptingInteraction.GetComponent<IHealable>().HealDamage(_healAmount);
        AudioManager.instance?.PlaySFX(_pickupClip);
        Destroy(gameObject);
    }
    
}
