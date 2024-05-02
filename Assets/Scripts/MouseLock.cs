using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLock : MonoBehaviour
{
    public bool LockOff;
    void Start()
    {
        if (LockOff == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    private void Update()
    {
        if (LockOff == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void MouseLockFlip()
    {
        if (LockOff == true)
        {
            LockOff = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (LockOff == false)
        {
            LockOff = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
