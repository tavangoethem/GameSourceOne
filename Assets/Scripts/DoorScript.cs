using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorScript : MonoBehaviour, IInteractable
{
    [SerializeField] private Keys _keysRequired;
    [SerializeField] private Material[] _materials;
    public Animator doorAnimator;
    private bool _isOpen = false;

    [SerializeField] private AudioClip _openClip;

    private void OnEnable()
    {
        Renderer temp = GetComponent<Renderer>();
        switch (_keysRequired)
        {
            case Keys.Key1:
                if (_materials[0])
                    temp.material = _materials[1];
                break;
            case Keys.Key2:
                if (_materials[1])
                    temp.material = _materials[2];
                break;
            case Keys.Key3:
                if (_materials[2])
                    temp.material = _materials[3];
                break;
            case Keys.Key4:
                if (_materials[3])
                    temp.material = _materials[4];
                break;
        }
    }

    public void DoorInteraction()
    {
        if (!_isOpen)
        {
            doorAnimator.SetTrigger("Open");
            doorAnimator.ResetTrigger("Close");
            _isOpen = true;
        }
        else
        {
            doorAnimator.SetTrigger("Close");
            doorAnimator.ResetTrigger("Open");
            _isOpen = false;
        }
    }

    public void Interact(GameObject objAttemptingInteraction)
    {
        if (objAttemptingInteraction.GetComponent<CharacterInventory>() != null)
        {
            if (objAttemptingInteraction.GetComponent<CharacterInventory>().CurKeys.HasFlag(_keysRequired) == true)
            {
                DoorInteraction();
                AudioManager.instance?.PlaySFX(_openClip);
            }
            else
                //locked noise or stuff 
                ;
        }        
    }
}
