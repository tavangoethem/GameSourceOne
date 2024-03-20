using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Animator doorAnimator;
    private bool _isOpen = false;

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
}
